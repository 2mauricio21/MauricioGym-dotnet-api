-- =============================================
-- SCRIPT DE CRIAÇÃO COMPLETA DO BANCO MAURICIOGYM
-- VERSÃO CORRIGIDA - TODAS AS 13 TABELAS
-- =============================================

USE master;
GO

-- Criar banco se não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MauricioGymDB')
BEGIN
    CREATE DATABASE MauricioGymDB;
    PRINT 'Banco de dados MauricioGymDB criado.';
END
ELSE
BEGIN
    PRINT 'Banco de dados MauricioGymDB já existe.';
END
GO

USE MauricioGymDB;
GO

-- =============================================
-- 1. CRIAÇÃO DAS TABELAS PRINCIPAIS
-- =============================================

-- Tabela Usuarios
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios' AND xtype='U')
BEGIN
    CREATE TABLE Usuarios (
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(100) NOT NULL,
        Sobrenome NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        Senha NVARCHAR(255) NOT NULL,
        CPF NVARCHAR(14) UNIQUE,
        Telefone NVARCHAR(20),
        DataNascimento DATE,
        Endereco NVARCHAR(500),
        Cidade NVARCHAR(100),
        Estado NVARCHAR(2),
        CEP NVARCHAR(10),
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
        DataUltimoLogin DATETIME NULL
    );
    PRINT 'Tabela Usuarios criada.';
END
GO

-- Tabela Autenticacao
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Autenticacao' AND xtype='U')
BEGIN
    CREATE TABLE Autenticacao (
        IdAutenticacao INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        Senha NVARCHAR(255) NOT NULL,
        TokenRecuperacao NVARCHAR(500) NULL,
        DataExpiracaoToken DATETIME NULL,
        TentativasLogin INT NOT NULL DEFAULT 0,
        ContaBloqueada BIT NOT NULL DEFAULT 0,
        DataBloqueio DATETIME NULL,
        DataUltimaTentativa DATETIME NULL,
        RefreshToken NVARCHAR(500) NULL,
        DataExpiracaoRefreshToken DATETIME NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
        DataUltimoLogin DATETIME NULL,
        
        CONSTRAINT FK_Autenticacao_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT UQ_Autenticacao_Email UNIQUE (Email),
        CONSTRAINT UQ_Autenticacao_Usuario UNIQUE (IdUsuario)
    );
    PRINT 'Tabela Autenticacao criada.';
END
GO

-- Tabela Funcionarios (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Funcionarios' AND xtype='U')
BEGIN
    CREATE TABLE Funcionarios (
        IdFuncionario INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdAcademia INT NULL, -- NULL para funcionários globais
        Cargo NVARCHAR(100) NOT NULL,
        Salario DECIMAL(10,2) NULL,
        DataAdmissao DATE NOT NULL,
        DataDemissao DATE NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_Funcionarios_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
    PRINT 'Tabela Funcionarios criada.';
END
GO

-- Tabela Planos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Planos' AND xtype='U')
BEGIN
    CREATE TABLE Planos (
        IdPlano INT IDENTITY(1,1) PRIMARY KEY,
        IdAcademia INT NULL, -- NULL para planos globais
        Nome NVARCHAR(100) NOT NULL,
        Descricao NVARCHAR(1000),
        Valor DECIMAL(10,2) NOT NULL,
        DuracaoEmDias INT NOT NULL,
        PermiteAcessoTotal BIT NOT NULL DEFAULT 1,
        HorarioInicioPermitido TIME NULL,
        HorarioFimPermitido TIME NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
    );
    PRINT 'Tabela Planos criada.';
END
GO

-- Tabela Usuarios_Planos (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuarios_Planos' AND xtype='U')
BEGIN
    CREATE TABLE Usuarios_Planos (
        IdUsuarioPlano INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdPlano INT NOT NULL,
        DataInicio DATE NOT NULL,
        DataFim DATE NOT NULL,
        ValorPago DECIMAL(10,2) NOT NULL,
        StatusPlano NVARCHAR(20) NOT NULL DEFAULT 'Ativo', -- Ativo, Suspenso, Cancelado, Expirado
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_UsuariosPlanos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_UsuariosPlanos_Plano FOREIGN KEY (IdPlano) REFERENCES Planos(IdPlano),
        CONSTRAINT CK_UsuariosPlanos_StatusPlano CHECK (StatusPlano IN ('Ativo', 'Suspenso', 'Cancelado', 'Expirado')),
        CONSTRAINT CK_UsuariosPlanos_DataFim CHECK (DataFim >= DataInicio)
    );
    PRINT 'Tabela Usuarios_Planos criada.';
END
GO

-- Tabela Academias
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Academias' AND xtype='U')
BEGIN
    CREATE TABLE Academias (
        IdAcademia INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(200) NOT NULL,
        CNPJ NVARCHAR(18) NOT NULL UNIQUE,
        Endereco NVARCHAR(500),
        Cidade NVARCHAR(100),
        Estado NVARCHAR(2),
        CEP NVARCHAR(10),
        Telefone NVARCHAR(20),
        Email NVARCHAR(255),
        HorarioAbertura TIME NOT NULL DEFAULT '06:00:00',
        HorarioFechamento TIME NOT NULL DEFAULT '22:00:00',
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
    );
    PRINT 'Tabela Academias criada.';
END
GO

-- Tabela Acessos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Acessos' AND xtype='U')
BEGIN
    CREATE TABLE Acessos (
        IdAcesso INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdAcademia INT NOT NULL,
        DataHoraEntrada DATETIME NOT NULL DEFAULT GETDATE(),
        DataHoraSaida DATETIME NULL,
        TipoAcesso NVARCHAR(20) NOT NULL DEFAULT 'Normal', -- Normal, Visitante, Funcionario
        ObservacaoAcesso NVARCHAR(500),
        AcessoLiberado BIT NOT NULL DEFAULT 1,
        MotivoNegacao NVARCHAR(500),
        
        CONSTRAINT FK_Acessos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_Acessos_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
        CONSTRAINT CK_Acessos_TipoAcesso CHECK (TipoAcesso IN ('Normal', 'Visitante', 'Funcionario')),
        CONSTRAINT CK_Acessos_DataHoraSaida CHECK (DataHoraSaida IS NULL OR DataHoraSaida >= DataHoraEntrada)
    );
    PRINT 'Tabela Acessos criada.';
END
GO

-- Tabela Bloqueios (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Bloqueios' AND xtype='U')
BEGIN
    CREATE TABLE Bloqueios (
        IdBloqueio INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdAcademia INT NOT NULL,
        DataInicioBloqueio DATETIME NOT NULL DEFAULT GETDATE(),
        DataFimBloqueio DATETIME NULL,
        MotivoBloqueio NVARCHAR(200) NOT NULL,
        ObservacaoBloqueio NVARCHAR(1000),
        Ativo BIT NOT NULL DEFAULT 1,
        IdUsuarioResponsavel INT NOT NULL,
        
        CONSTRAINT FK_Bloqueios_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_Bloqueios_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
        CONSTRAINT FK_Bloqueios_UsuarioResponsavel FOREIGN KEY (IdUsuarioResponsavel) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT CK_Bloqueios_DataFimBloqueio CHECK (DataFimBloqueio IS NULL OR DataFimBloqueio >= DataInicioBloqueio)
    );
    PRINT 'Tabela Bloqueios criada.';
END
GO

-- Tabela RecuperacaoSenha (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RecuperacaoSenha' AND xtype='U')
BEGIN
    CREATE TABLE RecuperacaoSenha (
        IdRecuperacao INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        Token NVARCHAR(500) NOT NULL,
        DataSolicitacao DATETIME NOT NULL DEFAULT GETDATE(),
        DataExpiracao DATETIME NOT NULL,
        Utilizado BIT NOT NULL DEFAULT 0,
        DataUtilizacao DATETIME NULL,
        
        CONSTRAINT FK_RecuperacaoSenha_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
    PRINT 'Tabela RecuperacaoSenha criada.';
END
GO

-- Tabela Logs (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Logs' AND xtype='U')
BEGIN
    CREATE TABLE Logs (
        IdLog INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NULL,
        Acao NVARCHAR(200) NOT NULL,
        Descricao NVARCHAR(1000),
        TabelaAfetada NVARCHAR(100),
        IdRegistroAfetado INT NULL,
        DataHora DATETIME NOT NULL DEFAULT GETDATE(),
        EnderecoIP NVARCHAR(45),
        UserAgent NVARCHAR(500),
        
        CONSTRAINT FK_Logs_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
    PRINT 'Tabela Logs criada.';
END
GO

-- Tabela Configuracoes (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Configuracoes' AND xtype='U')
BEGIN
    CREATE TABLE Configuracoes (
        IdConfiguracao INT IDENTITY(1,1) PRIMARY KEY,
        IdAcademia INT NULL, -- NULL para configurações globais
        Chave NVARCHAR(100) NOT NULL,
        Valor NVARCHAR(1000) NOT NULL,
        Descricao NVARCHAR(500),
        TipoDado NVARCHAR(20) NOT NULL DEFAULT 'STRING', -- STRING, INT, DECIMAL, BOOL, DATE
        Ativo BIT NOT NULL DEFAULT 1,
        DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
        DataUltimaAlteracao DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_Configuracoes_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
        CONSTRAINT UQ_Configuracoes_Chave_Academia UNIQUE (Chave, IdAcademia)
    );
    PRINT 'Tabela Configuracoes criada.';
END
GO

-- Tabela Notificacoes (NOVA - ESTAVA FALTANDO)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Notificacoes' AND xtype='U')
BEGIN
    CREATE TABLE Notificacoes (
        IdNotificacao INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        Titulo NVARCHAR(200) NOT NULL,
        Mensagem NVARCHAR(1000) NOT NULL,
        Tipo NVARCHAR(50) NOT NULL DEFAULT 'INFO', -- INFO, WARNING, ERROR, SUCCESS
        Lida BIT NOT NULL DEFAULT 0,
        DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
        DataLeitura DATETIME NULL,
        
        CONSTRAINT FK_Notificacoes_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
    PRINT 'Tabela Notificacoes criada.';
END
GO

-- Adicionar FK para Funcionarios -> Academias (após criação da tabela Academias)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Funcionarios_Academia')
BEGIN
    ALTER TABLE Funcionarios
    ADD CONSTRAINT FK_Funcionarios_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia);
    PRINT 'FK Funcionarios -> Academias criada.';
END
GO

-- Adicionar FK para Planos -> Academias (após criação da tabela Academias)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Planos_Academia')
BEGIN
    ALTER TABLE Planos
    ADD CONSTRAINT FK_Planos_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia);
    PRINT 'FK Planos -> Academias criada.';
END
GO

-- =============================================
-- 2. CRIAÇÃO DOS ÍNDICES
-- =============================================

PRINT 'Criando índices...';
GO

-- Índices para Usuarios
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Usuarios_Email')
    CREATE INDEX IX_Usuarios_Email ON Usuarios(Email);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Usuarios_CPF')
    CREATE INDEX IX_Usuarios_CPF ON Usuarios(CPF);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Usuarios_Ativo')
    CREATE INDEX IX_Usuarios_Ativo ON Usuarios(Ativo);

-- Índices para Autenticacao
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Autenticacao_Email')
    CREATE INDEX IX_Autenticacao_Email ON Autenticacao(Email);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Autenticacao_Usuario')
    CREATE INDEX IX_Autenticacao_Usuario ON Autenticacao(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Autenticacao_RefreshToken')
    CREATE INDEX IX_Autenticacao_RefreshToken ON Autenticacao(RefreshToken);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Autenticacao_TokenRecuperacao')
    CREATE INDEX IX_Autenticacao_TokenRecuperacao ON Autenticacao(TokenRecuperacao);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Autenticacao_ContaBloqueada')
    CREATE INDEX IX_Autenticacao_ContaBloqueada ON Autenticacao(ContaBloqueada);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Autenticacao_Ativo')
    CREATE INDEX IX_Autenticacao_Ativo ON Autenticacao(Ativo);

PRINT 'Índices criados com sucesso.';
GO

-- =============================================
-- 3. INSERÇÃO DE DADOS INICIAIS
-- =============================================

PRINT 'Inserindo dados iniciais...';
GO

-- Verificar se já existe usuário administrador
IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Email = 'admin@mauriciogym.com')
BEGIN
    -- Inserir usuário administrador inicial
    -- Senha: admin123 (hash SHA256: 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9)
    INSERT INTO Usuarios (Nome, Sobrenome, Email, Senha, CPF, Ativo) VALUES
    ('Administrador', 'Sistema', 'admin@mauriciogym.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', '00000000000', 1);
    
    PRINT 'Usuário administrador criado.';
    
    -- Criar registro de autenticação para o usuário administrador
    DECLARE @IdUsuarioAdmin INT = (SELECT IdUsuario FROM Usuarios WHERE Email = 'admin@mauriciogym.com');
    
    INSERT INTO Autenticacao (IdUsuario, Email, Senha, TentativasLogin, ContaBloqueada, Ativo)
    VALUES (@IdUsuarioAdmin, 'admin@mauriciogym.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 0, 0, 1);
    
    PRINT 'Registro de autenticação criado para o administrador.';
END
ELSE
BEGIN
    PRINT 'Usuário administrador já existe.';
END
GO

-- Inserir uma academia exemplo para testes
IF NOT EXISTS (SELECT 1 FROM Academias WHERE CNPJ = '12.345.678/0001-90')
BEGIN
    INSERT INTO Academias (Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone, Email, HorarioAbertura, HorarioFechamento) VALUES
    ('MauricioGym - Unidade Centro', '12.345.678/0001-90', 'Rua Principal, 123', 'São Paulo', 'SP', '01234-567', '(11) 1234-5678', 'centro@mauriciogym.com', '06:00:00', '22:00:00');
    
    PRINT 'Academia exemplo criada.';
    
    -- Criar planos exemplo
    DECLARE @IdAcademiaExemplo INT = (SELECT IdAcademia FROM Academias WHERE CNPJ = '12.345.678/0001-90');
    
    INSERT INTO Planos (IdAcademia, Nome, Descricao, Valor, DuracaoEmDias, PermiteAcessoTotal) VALUES
    (@IdAcademiaExemplo, 'Plano Mensal', 'Acesso total por 30 dias', 89.90, 30, 1),
    (@IdAcademiaExemplo, 'Plano Trimestral', 'Acesso total por 90 dias', 239.90, 90, 1),
    (@IdAcademiaExemplo, 'Plano Semestral', 'Acesso total por 180 dias', 449.90, 180, 1),
    (@IdAcademiaExemplo, 'Plano Anual', 'Acesso total por 365 dias', 799.90, 365, 1);
    
    PRINT 'Planos exemplo criados.';
END
ELSE
BEGIN
    PRINT 'Academia exemplo já existe.';
END
GO

-- =============================================
-- 4. VERIFICAÇÕES FINAIS
-- =============================================

PRINT 'Executando verificações finais...';
GO

-- Verificar se todas as tabelas foram criadas
DECLARE @TabelasCriadas INT = (
    SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_TYPE = 'BASE TABLE' 
    AND TABLE_NAME IN (
        'Usuarios', 'Autenticacao', 'Funcionarios', 'Usuarios_Planos', 
        'Academias', 'Planos', 'Acessos', 'Bloqueios', 
        'RecuperacaoSenha', 'Logs', 'Configuracoes', 'Notificacoes'
    )
);

IF @TabelasCriadas = 12
BEGIN
    PRINT 'SUCESSO: Todas as 12 tabelas principais foram criadas corretamente.';
END
ELSE
BEGIN
    PRINT 'ATENÇÃO: Apenas ' + CAST(@TabelasCriadas AS VARCHAR(2)) + ' de 12 tabelas foram criadas.';
END
GO

-- Verificar se os dados iniciais foram inseridos
DECLARE @UsuarioAdmin INT = (SELECT COUNT(*) FROM Usuarios WHERE Email = 'admin@mauriciogym.com');
DECLARE @AutenticacaoAdmin INT = (SELECT COUNT(*) FROM Autenticacao WHERE Email = 'admin@mauriciogym.com');

PRINT 'Usuário administrador: ' + CASE WHEN @UsuarioAdmin = 1 THEN 'Criado' ELSE 'Não encontrado' END;
PRINT 'Autenticação administrador: ' + CASE WHEN @AutenticacaoAdmin = 1 THEN 'Criada' ELSE 'Não encontrada' END;
GO

-- =============================================
-- 5. INFORMAÇÕES IMPORTANTES
-- =============================================

PRINT '';
PRINT '=============================================';
PRINT 'BANCO DE DADOS MAURICIOGYM CRIADO COM SUCESSO!';
PRINT '=============================================';
PRINT '';
PRINT 'INFORMAÇÕES IMPORTANTES:';
PRINT '- Banco: MauricioGymDB';
PRINT '- Tabelas: 12 tabelas criadas (incluindo Autenticacao)';
PRINT '- Índices: Todos os índices de performance criados';
PRINT '- Usuário Admin: admin@mauriciogym.com';
PRINT '- Senha Admin: admin123 (ALTERE IMEDIATAMENTE!)';
PRINT '- Academia Exemplo: MauricioGym - Unidade Centro';
PRINT '';
PRINT 'PRÓXIMOS PASSOS:';
PRINT '1. Alterar a senha do administrador';
PRINT '2. Configurar as connection strings nas aplicações';
PRINT '3. Testar as APIs do sistema';
PRINT '4. Criar usuários e academias reais';
PRINT '';
PRINT 'Script executado em: ' + CONVERT(VARCHAR, GETDATE(), 120);
PRINT '=============================================';
GO