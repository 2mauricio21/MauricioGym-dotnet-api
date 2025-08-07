-- =============================================
-- Script de Criação Completa do Banco MauricioGym
-- Versão: 1.0
-- Data: Dezembro 2024
-- Descrição: Script completo para criação do banco de dados,
--            tabelas, índices, constraints e dados iniciais
-- =============================================

-- =============================================
-- 1. CRIAÇÃO DO BANCO DE DADOS
-- =============================================

-- Verificar se o banco existe e criar se necessário
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MauricioGymDB')
BEGIN
    CREATE DATABASE MauricioGymDB;
    PRINT 'Banco de dados MauricioGymDB criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'Banco de dados MauricioGymDB já existe.';
END
GO

-- Usar o banco criado
USE MauricioGymDB;
GO

-- =============================================
-- 2. CRIAÇÃO DAS TABELAS
-- =============================================

PRINT 'Iniciando criação das tabelas...';
GO

-- =============================================
-- 2.1 DOMÍNIO USUARIO
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

-- Tabela Recursos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Recursos' AND xtype='U')
BEGIN
    CREATE TABLE Recursos (
        IdRecurso INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(100) NOT NULL,
        Descricao NVARCHAR(500),
        Codigo NVARCHAR(50) NOT NULL UNIQUE,
        Ativo BIT NOT NULL DEFAULT 1
    );
    PRINT 'Tabela Recursos criada.';
END
GO

-- Tabela UsuarioRecursos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UsuarioRecursos' AND xtype='U')
BEGIN
    CREATE TABLE UsuarioRecursos (
        IdUsuarioRecurso INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdRecurso INT NOT NULL,
        IdAcademia INT NULL, -- NULL para recursos globais
        DataAtribuicao DATETIME NOT NULL DEFAULT GETDATE(),
        Ativo BIT NOT NULL DEFAULT 1,
        
        CONSTRAINT FK_UsuarioRecursos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_UsuarioRecursos_Recurso FOREIGN KEY (IdRecurso) REFERENCES Recursos(IdRecurso),
        CONSTRAINT UQ_UsuarioRecursos_Usuario_Recurso_Academia UNIQUE (IdUsuario, IdRecurso, IdAcademia)
    );
    PRINT 'Tabela UsuarioRecursos criada.';
END
GO

-- =============================================
-- 2.2 DOMÍNIO ACADEMIA
-- =============================================

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

-- Tabela UsuarioAcademias
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UsuarioAcademias' AND xtype='U')
BEGIN
    CREATE TABLE UsuarioAcademias (
        IdUsuarioAcademia INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdAcademia INT NOT NULL,
        DataVinculo DATETIME NOT NULL DEFAULT GETDATE(),
        Ativo BIT NOT NULL DEFAULT 1,
        
        CONSTRAINT FK_UsuarioAcademias_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_UsuarioAcademias_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
        CONSTRAINT UQ_UsuarioAcademias_Usuario_Academia UNIQUE (IdUsuario, IdAcademia)
    );
    PRINT 'Tabela UsuarioAcademias criada.';
END
GO

-- Adicionar FK para UsuarioRecursos -> Academias (após criação da tabela Academias)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_UsuarioRecursos_Academia')
BEGIN
    ALTER TABLE UsuarioRecursos
    ADD CONSTRAINT FK_UsuarioRecursos_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia);
    PRINT 'FK UsuarioRecursos -> Academias criada.';
END
GO

-- =============================================
-- 2.3 DOMÍNIO PLANO
-- =============================================

-- Tabela Planos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Planos' AND xtype='U')
BEGIN
    CREATE TABLE Planos (
        IdPlano INT IDENTITY(1,1) PRIMARY KEY,
        IdAcademia INT NOT NULL,
        Nome NVARCHAR(100) NOT NULL,
        Descricao NVARCHAR(1000),
        Valor DECIMAL(10,2) NOT NULL,
        DuracaoEmDias INT NOT NULL,
        PermiteAcessoTotal BIT NOT NULL DEFAULT 1,
        HorarioInicioPermitido TIME NULL,
        HorarioFimPermitido TIME NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_Planos_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia)
    );
    PRINT 'Tabela Planos criada.';
END
GO

-- Tabela UsuarioPlanos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UsuarioPlanos' AND xtype='U')
BEGIN
    CREATE TABLE UsuarioPlanos (
        IdUsuarioPlano INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdPlano INT NOT NULL,
        DataInicio DATE NOT NULL,
        DataFim DATE NOT NULL,
        ValorPago DECIMAL(10,2) NOT NULL,
        StatusPlano NVARCHAR(20) NOT NULL DEFAULT 'Ativo', -- Ativo, Suspenso, Cancelado, Expirado
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_UsuarioPlanos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_UsuarioPlanos_Plano FOREIGN KEY (IdPlano) REFERENCES Planos(IdPlano),
        CONSTRAINT CK_UsuarioPlanos_StatusPlano CHECK (StatusPlano IN ('Ativo', 'Suspenso', 'Cancelado', 'Expirado')),
        CONSTRAINT CK_UsuarioPlanos_DataFim CHECK (DataFim >= DataInicio)
    );
    PRINT 'Tabela UsuarioPlanos criada.';
END
GO

-- =============================================
-- 2.4 DOMÍNIO PAGAMENTO
-- =============================================

-- Tabela FormasPagamento
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FormasPagamento' AND xtype='U')
BEGIN
    CREATE TABLE FormasPagamento (
        IdFormaPagamento INT IDENTITY(1,1) PRIMARY KEY,
        IdAcademia INT NOT NULL,
        Nome NVARCHAR(50) NOT NULL,
        Descricao NVARCHAR(200),
        AceitaParcelamento BIT NOT NULL DEFAULT 0,
        MaximoParcelas INT NOT NULL DEFAULT 1,
        TaxaProcessamento DECIMAL(5,2) NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        
        CONSTRAINT FK_FormasPagamento_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia)
    );
    PRINT 'Tabela FormasPagamento criada.';
END
GO

-- Tabela Pagamentos
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pagamentos' AND xtype='U')
BEGIN
    CREATE TABLE Pagamentos (
        IdPagamento INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdUsuarioPlano INT NOT NULL,
        Valor DECIMAL(10,2) NOT NULL,
        DataPagamento DATETIME NOT NULL DEFAULT GETDATE(),
        DataVencimento DATE NULL,
        FormaPagamento NVARCHAR(50) NOT NULL,
        StatusPagamento NVARCHAR(20) NOT NULL DEFAULT 'Pendente', -- Pendente, Aprovado, Rejeitado, Cancelado
        TransacaoId NVARCHAR(100),
        ObservacaoPagamento NVARCHAR(1000),
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_Pagamentos_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_Pagamentos_UsuarioPlano FOREIGN KEY (IdUsuarioPlano) REFERENCES UsuarioPlanos(IdUsuarioPlano),
        CONSTRAINT CK_Pagamentos_StatusPagamento CHECK (StatusPagamento IN ('Pendente', 'Aprovado', 'Rejeitado', 'Cancelado')),
        CONSTRAINT CK_Pagamentos_Valor CHECK (Valor > 0)
    );
    PRINT 'Tabela Pagamentos criada.';
END
GO

-- =============================================
-- 2.5 DOMÍNIO ACESSO
-- =============================================

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

-- Tabela BloqueiosAcesso
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BloqueiosAcesso' AND xtype='U')
BEGIN
    CREATE TABLE BloqueiosAcesso (
        IdBloqueioAcesso INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        IdAcademia INT NOT NULL,
        DataInicioBloqueio DATETIME NOT NULL DEFAULT GETDATE(),
        DataFimBloqueio DATETIME NULL,
        MotivoBloqueio NVARCHAR(200) NOT NULL,
        ObservacaoBloqueio NVARCHAR(1000),
        Ativo BIT NOT NULL DEFAULT 1,
        IdUsuarioResponsavel INT NOT NULL,
        
        CONSTRAINT FK_BloqueiosAcesso_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT FK_BloqueiosAcesso_Academia FOREIGN KEY (IdAcademia) REFERENCES Academias(IdAcademia),
        CONSTRAINT FK_BloqueiosAcesso_UsuarioResponsavel FOREIGN KEY (IdUsuarioResponsavel) REFERENCES Usuarios(IdUsuario),
        CONSTRAINT CK_BloqueiosAcesso_DataFimBloqueio CHECK (DataFimBloqueio IS NULL OR DataFimBloqueio >= DataInicioBloqueio)
    );
    PRINT 'Tabela BloqueiosAcesso criada.';
END
GO

-- =============================================
-- 2.6 DOMÍNIO AUDITORIA (INFRA)
-- =============================================

-- Tabela Auditorias
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Auditorias' AND xtype='U')
BEGIN
    CREATE TABLE Auditorias (
        IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        Descricao NVARCHAR(1000) NOT NULL,
        Data DATETIME NOT NULL DEFAULT GETDATE(),
        
        CONSTRAINT FK_Auditorias_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
    PRINT 'Tabela Auditorias criada.';
END
GO

-- =============================================
-- 3. CRIAÇÃO DOS ÍNDICES
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

-- Índices para Recursos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_Codigo')
    CREATE INDEX IX_Recursos_Codigo ON Recursos(Codigo);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Recursos_Ativo')
    CREATE INDEX IX_Recursos_Ativo ON Recursos(Ativo);

-- Índices para UsuarioRecursos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioRecursos_Usuario')
    CREATE INDEX IX_UsuarioRecursos_Usuario ON UsuarioRecursos(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioRecursos_Recurso')
    CREATE INDEX IX_UsuarioRecursos_Recurso ON UsuarioRecursos(IdRecurso);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioRecursos_Academia')
    CREATE INDEX IX_UsuarioRecursos_Academia ON UsuarioRecursos(IdAcademia);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioRecursos_Ativo')
    CREATE INDEX IX_UsuarioRecursos_Ativo ON UsuarioRecursos(Ativo);

-- Índices para Academias
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Academias_CNPJ')
    CREATE INDEX IX_Academias_CNPJ ON Academias(CNPJ);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Academias_Ativo')
    CREATE INDEX IX_Academias_Ativo ON Academias(Ativo);

-- Índices para UsuarioAcademias
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioAcademias_Usuario')
    CREATE INDEX IX_UsuarioAcademias_Usuario ON UsuarioAcademias(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioAcademias_Academia')
    CREATE INDEX IX_UsuarioAcademias_Academia ON UsuarioAcademias(IdAcademia);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioAcademias_Ativo')
    CREATE INDEX IX_UsuarioAcademias_Ativo ON UsuarioAcademias(Ativo);

-- Índices para Planos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Planos_Academia')
    CREATE INDEX IX_Planos_Academia ON Planos(IdAcademia);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Planos_Ativo')
    CREATE INDEX IX_Planos_Ativo ON Planos(Ativo);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Planos_Valor')
    CREATE INDEX IX_Planos_Valor ON Planos(Valor);

-- Índices para UsuarioPlanos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioPlanos_Usuario')
    CREATE INDEX IX_UsuarioPlanos_Usuario ON UsuarioPlanos(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioPlanos_Plano')
    CREATE INDEX IX_UsuarioPlanos_Plano ON UsuarioPlanos(IdPlano);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioPlanos_DataInicio')
    CREATE INDEX IX_UsuarioPlanos_DataInicio ON UsuarioPlanos(DataInicio);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioPlanos_DataFim')
    CREATE INDEX IX_UsuarioPlanos_DataFim ON UsuarioPlanos(DataFim);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioPlanos_StatusPlano')
    CREATE INDEX IX_UsuarioPlanos_StatusPlano ON UsuarioPlanos(StatusPlano);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_UsuarioPlanos_Ativo')
    CREATE INDEX IX_UsuarioPlanos_Ativo ON UsuarioPlanos(Ativo);

-- Índices para FormasPagamento
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_FormasPagamento_Academia')
    CREATE INDEX IX_FormasPagamento_Academia ON FormasPagamento(IdAcademia);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_FormasPagamento_Ativo')
    CREATE INDEX IX_FormasPagamento_Ativo ON FormasPagamento(Ativo);

-- Índices para Pagamentos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagamentos_Usuario')
    CREATE INDEX IX_Pagamentos_Usuario ON Pagamentos(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagamentos_UsuarioPlano')
    CREATE INDEX IX_Pagamentos_UsuarioPlano ON Pagamentos(IdUsuarioPlano);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagamentos_DataPagamento')
    CREATE INDEX IX_Pagamentos_DataPagamento ON Pagamentos(DataPagamento);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagamentos_StatusPagamento')
    CREATE INDEX IX_Pagamentos_StatusPagamento ON Pagamentos(StatusPagamento);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagamentos_TransacaoId')
    CREATE INDEX IX_Pagamentos_TransacaoId ON Pagamentos(TransacaoId);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Pagamentos_Ativo')
    CREATE INDEX IX_Pagamentos_Ativo ON Pagamentos(Ativo);

-- Índices para Acessos
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Acessos_Usuario')
    CREATE INDEX IX_Acessos_Usuario ON Acessos(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Acessos_Academia')
    CREATE INDEX IX_Acessos_Academia ON Acessos(IdAcademia);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Acessos_DataHoraEntrada')
    CREATE INDEX IX_Acessos_DataHoraEntrada ON Acessos(DataHoraEntrada);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Acessos_DataHoraSaida')
    CREATE INDEX IX_Acessos_DataHoraSaida ON Acessos(DataHoraSaida);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Acessos_TipoAcesso')
    CREATE INDEX IX_Acessos_TipoAcesso ON Acessos(TipoAcesso);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Acessos_AcessoLiberado')
    CREATE INDEX IX_Acessos_AcessoLiberado ON Acessos(AcessoLiberado);

-- Índices para BloqueiosAcesso
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BloqueiosAcesso_Usuario')
    CREATE INDEX IX_BloqueiosAcesso_Usuario ON BloqueiosAcesso(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BloqueiosAcesso_Academia')
    CREATE INDEX IX_BloqueiosAcesso_Academia ON BloqueiosAcesso(IdAcademia);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BloqueiosAcesso_DataInicioBloqueio')
    CREATE INDEX IX_BloqueiosAcesso_DataInicioBloqueio ON BloqueiosAcesso(DataInicioBloqueio);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BloqueiosAcesso_DataFimBloqueio')
    CREATE INDEX IX_BloqueiosAcesso_DataFimBloqueio ON BloqueiosAcesso(DataFimBloqueio);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BloqueiosAcesso_Ativo')
    CREATE INDEX IX_BloqueiosAcesso_Ativo ON BloqueiosAcesso(Ativo);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BloqueiosAcesso_UsuarioResponsavel')
    CREATE INDEX IX_BloqueiosAcesso_UsuarioResponsavel ON BloqueiosAcesso(IdUsuarioResponsavel);

-- Índices para Auditorias
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Auditorias_Usuario')
    CREATE INDEX IX_Auditorias_Usuario ON Auditorias(IdUsuario);
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Auditorias_Data')
    CREATE INDEX IX_Auditorias_Data ON Auditorias(Data);

PRINT 'Índices criados com sucesso.';
GO

-- =============================================
-- 4. INSERÇÃO DE DADOS INICIAIS
-- =============================================

PRINT 'Inserindo dados iniciais...';
GO

-- =============================================
-- 4.1 RECURSOS BÁSICOS DO SISTEMA
-- =============================================

-- Verificar se já existem recursos para evitar duplicação
IF NOT EXISTS (SELECT 1 FROM Recursos WHERE Codigo = 'ADMIN_GLOBAL')
BEGIN
    INSERT INTO Recursos (Nome, Descricao, Codigo) VALUES
    ('Administrador Global', 'Acesso total ao sistema', 'ADMIN_GLOBAL'),
    ('Criar Academia', 'Permissão para criar novas academias', 'CRIAR_ACADEMIA'),
    ('Gerenciar Usuários Globais', 'Gerenciar usuários em todas as academias', 'GERENCIAR_USUARIOS_GLOBAIS'),
    ('Dono da Academia', 'Proprietário da academia com acesso total', 'DONO_ACADEMIA'),
    ('Funcionário', 'Funcionário da academia', 'FUNCIONARIO'),
    ('Cliente', 'Cliente da academia', 'CLIENTE'),
    ('Gerenciar Planos', 'Criar e editar planos da academia', 'GERENCIAR_PLANOS'),
    ('Gerenciar Pagamentos', 'Processar pagamentos', 'GERENCIAR_PAGAMENTOS'),
    ('Liberar Acesso', 'Liberar entrada de clientes', 'LIBERAR_ACESSO'),
    ('Bloquear Acesso', 'Bloquear acesso de clientes', 'BLOQUEAR_ACESSO'),
    ('Ver Relatórios', 'Visualizar relatórios financeiros', 'VER_RELATORIOS'),
    ('Cadastrar Funcionários', 'Cadastrar novos funcionários', 'CADASTRAR_FUNCIONARIOS');
    
    PRINT 'Recursos básicos inseridos.';
END
ELSE
BEGIN
    PRINT 'Recursos básicos já existem.';
END
GO

-- =============================================
-- 4.2 USUÁRIO ADMINISTRADOR INICIAL
-- =============================================

-- Verificar se já existe usuário administrador
IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE Email = 'admin@mauriciogym.com')
BEGIN
    -- Inserir usuário administrador inicial
    -- Senha: admin123 (hash SHA256: 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9)
    INSERT INTO Usuarios (Nome, Sobrenome, Email, Senha, CPF, Ativo) VALUES
    ('Administrador', 'Sistema', 'admin@mauriciogym.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', '00000000000', 1);
    
    PRINT 'Usuário administrador criado.';
    
    -- Atribuir o recurso de Administrador Global ao usuário inicial
    DECLARE @IdUsuarioAdmin INT = (SELECT IdUsuario FROM Usuarios WHERE Email = 'admin@mauriciogym.com');
    DECLARE @IdRecursoAdmin INT = (SELECT IdRecurso FROM Recursos WHERE Codigo = 'ADMIN_GLOBAL');
    
    INSERT INTO UsuarioRecursos (IdUsuario, IdRecurso, IdAcademia, Ativo)
    VALUES (@IdUsuarioAdmin, @IdRecursoAdmin, NULL, 1);
    
    PRINT 'Permissão de administrador global atribuída.';
END
ELSE
BEGIN
    PRINT 'Usuário administrador já existe.';
END
GO

-- =============================================
-- 4.3 ACADEMIA EXEMPLO (OPCIONAL)
-- =============================================

-- Inserir uma academia exemplo para testes
IF NOT EXISTS (SELECT 1 FROM Academias WHERE CNPJ = '12.345.678/0001-90')
BEGIN
    INSERT INTO Academias (Nome, CNPJ, Endereco, Cidade, Estado, CEP, Telefone, Email, HorarioAbertura, HorarioFechamento) VALUES
    ('MauricioGym - Unidade Centro', '12.345.678/0001-90', 'Rua Principal, 123', 'São Paulo', 'SP', '01234-567', '(11) 1234-5678', 'centro@mauriciogym.com', '06:00:00', '22:00:00');
    
    PRINT 'Academia exemplo criada.';
    
    -- Criar formas de pagamento padrão para a academia
    DECLARE @IdAcademiaExemplo INT = (SELECT IdAcademia FROM Academias WHERE CNPJ = '12.345.678/0001-90');
    
    INSERT INTO FormasPagamento (IdAcademia, Nome, Descricao, AceitaParcelamento, MaximoParcelas) VALUES
    (@IdAcademiaExemplo, 'Dinheiro', 'Pagamento em espécie', 0, 1),
    (@IdAcademiaExemplo, 'Cartão de Débito', 'Pagamento com cartão de débito', 0, 1),
    (@IdAcademiaExemplo, 'Cartão de Crédito', 'Pagamento com cartão de crédito', 1, 12),
    (@IdAcademiaExemplo, 'PIX', 'Pagamento via PIX', 0, 1),
    (@IdAcademiaExemplo, 'Transferência Bancária', 'Transferência entre contas', 0, 1);
    
    PRINT 'Formas de pagamento padrão criadas.';
    
    -- Criar planos exemplo
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
-- 5. VERIFICAÇÕES FINAIS
-- =============================================

PRINT 'Executando verificações finais...';
GO

-- Verificar se todas as tabelas foram criadas
DECLARE @TabelasCriadas INT = (
    SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_TYPE = 'BASE TABLE' 
    AND TABLE_NAME IN (
        'Usuarios', 'Recursos', 'UsuarioRecursos', 
        'Academias', 'UsuarioAcademias', 
        'Planos', 'UsuarioPlanos', 
        'FormasPagamento', 'Pagamentos', 
        'Acessos', 'BloqueiosAcesso', 
        'Auditorias'
    )
);

IF @TabelasCriadas = 12
BEGIN
    PRINT 'SUCESSO: Todas as 12 tabelas foram criadas corretamente.';
END
ELSE
BEGIN
    PRINT 'ATENÇÃO: Apenas ' + CAST(@TabelasCriadas AS VARCHAR(2)) + ' de 12 tabelas foram criadas.';
END
GO

-- Verificar se os dados iniciais foram inseridos
DECLARE @RecursosCriados INT = (SELECT COUNT(*) FROM Recursos);
DECLARE @UsuarioAdmin INT = (SELECT COUNT(*) FROM Usuarios WHERE Email = 'admin@mauriciogym.com');

PRINT 'Recursos criados: ' + CAST(@RecursosCriados AS VARCHAR(10));
PRINT 'Usuário administrador: ' + CASE WHEN @UsuarioAdmin = 1 THEN 'Criado' ELSE 'Não encontrado' END;
GO

-- =============================================
-- 6. INFORMAÇÕES IMPORTANTES
-- =============================================

PRINT '';
PRINT '=============================================';
PRINT 'BANCO DE DADOS MAURICIOGYM CRIADO COM SUCESSO!';
PRINT '=============================================';
PRINT '';
PRINT 'INFORMAÇÕES IMPORTANTES:';
PRINT '- Banco: MauricioGymDB';
PRINT '- Tabelas: 12 tabelas criadas';
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