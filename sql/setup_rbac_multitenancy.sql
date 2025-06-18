-- Script de criação das tabelas para o sistema MauricioGym refatorado
-- Implementação de RBAC e Multi-tenancy

USE MauricioGymDB;
GO

-- ===================================================
-- DOMÍNIO ADMINISTRADOR - RBAC
-- ===================================================

-- Tabela de Papéis
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Papel' AND xtype='U')
CREATE TABLE Papel (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL UNIQUE,
    Descricao NVARCHAR(500) NULL,
    EhSistema BIT NOT NULL DEFAULT 0,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

-- Tabela de Permissões
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Permissao' AND xtype='U')
CREATE TABLE Permissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL UNIQUE,
    Descricao NVARCHAR(500) NULL,
    Recurso NVARCHAR(100) NOT NULL,
    Acao NVARCHAR(50) NOT NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

-- Tabela de relacionamento Papel-Permissão
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PapelPermissao' AND xtype='U')
CREATE TABLE PapelPermissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPapel INT NOT NULL,
    IdPermissao INT NOT NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdPapel) REFERENCES Papel(Id),
    FOREIGN KEY (IdPermissao) REFERENCES Permissao(Id),
    UNIQUE(IdPapel, IdPermissao)
);

-- Tabela de Academias (Multi-tenant)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Academia' AND xtype='U')
CREATE TABLE Academia (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(200) NOT NULL,
    Cnpj NVARCHAR(18) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL,
    Telefone NVARCHAR(20) NULL,
    Endereco NVARCHAR(500) NULL,
    Cidade NVARCHAR(100) NULL,
    Estado NVARCHAR(2) NULL,
    Cep NVARCHAR(10) NULL,
    DataVencimentoLicenca DATETIME2 NOT NULL,
    MaximoClientes INT NOT NULL DEFAULT 0,
    LicencaAtiva BIT NOT NULL DEFAULT 1,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

-- Atualizar tabela de Administradores
IF EXISTS (SELECT * FROM sysobjects WHERE name='Administrador' AND xtype='U')
BEGIN
    -- Adicionar novas colunas se não existirem
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'Cpf')
        ALTER TABLE Administrador ADD Cpf NVARCHAR(14) NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'IdAcademia')
        ALTER TABLE Administrador ADD IdAcademia INT NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'EhSuperAdmin')
        ALTER TABLE Administrador ADD EhSuperAdmin BIT NOT NULL DEFAULT 0;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'DataInclusao')
        ALTER TABLE Administrador ADD DataInclusao DATETIME2 NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'UsuarioInclusao')
        ALTER TABLE Administrador ADD UsuarioInclusao INT NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'UsuarioAlteracao')
        ALTER TABLE Administrador ADD UsuarioAlteracao INT NULL;
    
    -- Atualizar dados existentes onde as colunas existem
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'DataInclusao')
       AND EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'DataCriacao')
    BEGIN
        UPDATE Administrador 
        SET DataInclusao = ISNULL(DataInclusao, DataCriacao)
        WHERE DataInclusao IS NULL;
    END
    
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'UsuarioInclusao')
    BEGIN
        UPDATE Administrador 
        SET UsuarioInclusao = ISNULL(UsuarioInclusao, 1)
        WHERE UsuarioInclusao IS NULL;
    END
END

-- Tabela de relacionamento Administrador-Papel
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AdministradorPapel' AND xtype='U')
CREATE TABLE AdministradorPapel (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdAdministrador INT NOT NULL,
    IdPapel INT NOT NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdAdministrador) REFERENCES Administrador(Id),
    FOREIGN KEY (IdPapel) REFERENCES Papel(Id),
    UNIQUE(IdAdministrador, IdPapel)
);

-- ===================================================
-- DOMÍNIO CLIENTE - Multi-tenancy com CPF global
-- ===================================================

-- Tabela de Clientes (CPF como chave global)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cliente' AND xtype='U')
CREATE TABLE Cliente (
    Cpf NVARCHAR(14) PRIMARY KEY,
    Nome NVARCHAR(200) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Telefone NVARCHAR(20) NULL,
    DataNascimento DATE NULL,
    Endereco NVARCHAR(500) NULL,
    Cidade NVARCHAR(100) NULL,
    Estado NVARCHAR(2) NULL,
    Cep NVARCHAR(10) NULL,
    Observacoes NVARCHAR(1000) NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

-- Tabela de Vínculo Cliente-Academia
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='VinculoClienteAcademia' AND xtype='U')
CREATE TABLE VinculoClienteAcademia (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CpfCliente NVARCHAR(14) NOT NULL,
    IdAcademia INT NOT NULL,
    DataVinculo DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataDesvinculo DATETIME2 NULL,
    VinculoAtivo BIT NOT NULL DEFAULT 1,
    Observacoes NVARCHAR(1000) NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CpfCliente) REFERENCES Cliente(Cpf),
    FOREIGN KEY (IdAcademia) REFERENCES Academia(Id),
    UNIQUE(CpfCliente, IdAcademia)
);

-- Tabela de Modalidades
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Modalidade' AND xtype='U')
CREATE TABLE Modalidade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(500) NULL,
    IdAcademia INT NOT NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdAcademia) REFERENCES Academia(Id)
);

-- Atualizar tabela de Planos
IF EXISTS (SELECT * FROM sysobjects WHERE name='Plano' AND xtype='U')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'IdAcademia')
        ALTER TABLE Plano ADD IdAcademia INT NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'Descricao')
        ALTER TABLE Plano ADD Descricao NVARCHAR(500) NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'LimiteDiasVencimento')
        ALTER TABLE Plano ADD LimiteDiasVencimento INT NULL DEFAULT 5;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'PermiteCheckInVencido')
        ALTER TABLE Plano ADD PermiteCheckInVencido BIT NOT NULL DEFAULT 0;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'DataInclusao')
        ALTER TABLE Plano ADD DataInclusao DATETIME2 NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'UsuarioInclusao')
        ALTER TABLE Plano ADD UsuarioInclusao INT NULL;
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'UsuarioAlteracao')
        ALTER TABLE Plano ADD UsuarioAlteracao INT NULL;
    
    -- Atualizar dados existentes onde as colunas existem
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'DataInclusao')
       AND EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'DataCriacao')
    BEGIN
        UPDATE Plano 
        SET DataInclusao = ISNULL(DataInclusao, DataCriacao)
        WHERE DataInclusao IS NULL;
    END
    
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'UsuarioInclusao')
    BEGIN
        UPDATE Plano 
        SET UsuarioInclusao = ISNULL(UsuarioInclusao, 1),
            IdAcademia = ISNULL(IdAcademia, 1)
        WHERE UsuarioInclusao IS NULL OR IdAcademia IS NULL;
    END
END

-- Tabela de relacionamento Plano-Modalidade
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PlanoModalidade' AND xtype='U')
CREATE TABLE PlanoModalidade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPlano INT NOT NULL,
    IdModalidade INT NOT NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (IdPlano) REFERENCES Plano(Id),
    FOREIGN KEY (IdModalidade) REFERENCES Modalidade(Id),
    UNIQUE(IdPlano, IdModalidade)
);

-- Tabela de Plano-Cliente (substitui UsuarioPlano)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PlanoCliente' AND xtype='U')
CREATE TABLE PlanoCliente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CpfCliente NVARCHAR(14) NOT NULL,
    IdPlano INT NOT NULL,
    IdAcademia INT NOT NULL,
    DataContratacao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataVencimento DATETIME2 NOT NULL,
    ValorPago DECIMAL(10,2) NOT NULL,
    MesesContratados INT NOT NULL,
    PlanoAtivo BIT NOT NULL DEFAULT 1,
    DataCancelamento DATETIME2 NULL,
    MotivoCancelamento NVARCHAR(500) NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CpfCliente) REFERENCES Cliente(Cpf),
    FOREIGN KEY (IdPlano) REFERENCES Plano(Id),
    FOREIGN KEY (IdAcademia) REFERENCES Academia(Id)
);

-- Tabela de Pagamentos (substitui Mensalidade)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pagamento' AND xtype='U')
CREATE TABLE Pagamento (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CpfCliente NVARCHAR(14) NOT NULL,
    IdAcademia INT NOT NULL,
    IdPlanoCliente INT NULL,
    TipoPagamento NVARCHAR(20) NOT NULL DEFAULT 'MENSALIDADE', -- MENSALIDADE, AVULSA, ANTECIPACAO
    Valor DECIMAL(10,2) NOT NULL,
    DataPagamento DATETIME2 NOT NULL DEFAULT GETDATE(),
    MesReferencia INT NULL,
    AnoReferencia INT NULL,
    FormaPagamento NVARCHAR(20) NOT NULL DEFAULT 'DINHEIRO', -- DINHEIRO, CARTAO, PIX, etc
    Observacoes NVARCHAR(1000) NULL,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CpfCliente) REFERENCES Cliente(Cpf),
    FOREIGN KEY (IdAcademia) REFERENCES Academia(Id),
    FOREIGN KEY (IdPlanoCliente) REFERENCES PlanoCliente(Id)
);

-- Atualizar tabela de CheckIn
IF EXISTS (SELECT * FROM sysobjects WHERE name='CheckIn' AND xtype='U')
BEGIN
    -- Verificar se existe a coluna UsuarioId para fazer a migração
    IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'UsuarioId')
    BEGIN
        -- Adicionar novas colunas
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'CpfCliente')
            ALTER TABLE CheckIn ADD CpfCliente NVARCHAR(14) NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'IdAcademia')
            ALTER TABLE CheckIn ADD IdAcademia INT NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'DataHoraEntrada')
            ALTER TABLE CheckIn ADD DataHoraEntrada DATETIME2 NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'DataHoraSaida')
            ALTER TABLE CheckIn ADD DataHoraSaida DATETIME2 NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'TipoCheckIn')
            ALTER TABLE CheckIn ADD TipoCheckIn NVARCHAR(10) NULL DEFAULT 'PLANO';
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'ValorPago')
            ALTER TABLE CheckIn ADD ValorPago DECIMAL(10,2) NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'EhAvulsa')
            ALTER TABLE CheckIn ADD EhAvulsa BIT NULL DEFAULT 0;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'DataInclusao')
            ALTER TABLE CheckIn ADD DataInclusao DATETIME2 NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'DataAlteracao')
            ALTER TABLE CheckIn ADD DataAlteracao DATETIME2 NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'UsuarioInclusao')
            ALTER TABLE CheckIn ADD UsuarioInclusao INT NULL;
        
        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('CheckIn') AND name = 'UsuarioAlteracao')
            ALTER TABLE CheckIn ADD UsuarioAlteracao INT NULL;
    END
    ELSE
    BEGIN
        -- Se não existe UsuarioId, é uma tabela nova, criar corretamente
        DROP TABLE CheckIn;
    END
END

-- Criar/Recriar tabela CheckIn
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CheckIn' AND xtype='U')
CREATE TABLE CheckIn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CpfCliente NVARCHAR(14) NOT NULL,
    IdAcademia INT NOT NULL,
    DataHoraEntrada DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataHoraSaida DATETIME2 NULL,
    TipoCheckIn NVARCHAR(10) NOT NULL DEFAULT 'PLANO', -- PLANO, AVULSA
    ValorPago DECIMAL(10,2) NULL,
    Observacoes NVARCHAR(1000) NULL,
    EhAvulsa BIT NOT NULL DEFAULT 0,
    DataInclusao DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataAlteracao DATETIME2 NULL,
    UsuarioInclusao INT NOT NULL DEFAULT 1,
    UsuarioAlteracao INT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (CpfCliente) REFERENCES Cliente(Cpf),
    FOREIGN KEY (IdAcademia) REFERENCES Academia(Id)
);

-- ===================================================
-- ÍNDICES PARA PERFORMANCE
-- ===================================================

-- Índices para multi-tenancy
CREATE NONCLUSTERED INDEX IX_VinculoClienteAcademia_IdAcademia ON VinculoClienteAcademia(IdAcademia);
CREATE NONCLUSTERED INDEX IX_PlanoCliente_IdAcademia ON PlanoCliente(IdAcademia);
CREATE NONCLUSTERED INDEX IX_Pagamento_IdAcademia ON Pagamento(IdAcademia);
CREATE NONCLUSTERED INDEX IX_CheckIn_IdAcademia ON CheckIn(IdAcademia);
CREATE NONCLUSTERED INDEX IX_Plano_IdAcademia ON Plano(IdAcademia);
CREATE NONCLUSTERED INDEX IX_Modalidade_IdAcademia ON Modalidade(IdAcademia);

-- Índices para CPF (chave do cliente)
CREATE NONCLUSTERED INDEX IX_VinculoClienteAcademia_CpfCliente ON VinculoClienteAcademia(CpfCliente);
CREATE NONCLUSTERED INDEX IX_PlanoCliente_CpfCliente ON PlanoCliente(CpfCliente);
CREATE NONCLUSTERED INDEX IX_Pagamento_CpfCliente ON Pagamento(CpfCliente);
CREATE NONCLUSTERED INDEX IX_CheckIn_CpfCliente ON CheckIn(CpfCliente);

-- Índices para consultas de data
CREATE NONCLUSTERED INDEX IX_Pagamento_DataPagamento ON Pagamento(DataPagamento);
CREATE NONCLUSTERED INDEX IX_CheckIn_DataHoraEntrada ON CheckIn(DataHoraEntrada);
CREATE NONCLUSTERED INDEX IX_PlanoCliente_DataVencimento ON PlanoCliente(DataVencimento);

-- ===================================================
-- DADOS INICIAIS - RBAC
-- ===================================================

-- Inserir Academia padrão
IF NOT EXISTS (SELECT 1 FROM Academia WHERE Id = 1)
INSERT INTO Academia (Id, Nome, Cnpj, Email, Telefone, DataVencimentoLicenca, MaximoClientes, LicencaAtiva)
VALUES (1, 'MauricioGym Principal', '12.345.678/0001-90', 'contato@mauriciogym.com', '(11) 99999-9999', 
        DATEADD(year, 1, GETDATE()), 1000, 1);

-- ===================================================
-- FOREIGN KEYS - Adicionar após inserir dados básicos
-- ===================================================

-- Adicionar FK do Administrador para Academia
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Administrador_Academia')
   AND EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'IdAcademia')
    ALTER TABLE Administrador ADD CONSTRAINT FK_Administrador_Academia FOREIGN KEY (IdAcademia) REFERENCES Academia(Id);

-- Adicionar FK do Plano para Academia  
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Plano_Academia')
   AND EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'IdAcademia')
    ALTER TABLE Plano ADD CONSTRAINT FK_Plano_Academia FOREIGN KEY (IdAcademia) REFERENCES Academia(Id);

-- Inserir Papéis do Sistema
INSERT INTO Papel (Nome, Descricao, EhSistema) VALUES
('SuperAdmin', 'Administrador com acesso total ao sistema', 1),
('AdminSistema', 'Administrador de sistema para gerenciar academias', 1),
('AdminAcademia', 'Administrador de academia específica', 1),
('Suporte', 'Usuário de suporte com acesso limitado', 1);

-- Inserir Permissões
INSERT INTO Permissao (Nome, Descricao, Recurso, Acao) VALUES
-- Administrador
('Admin.GerenciarAdministradoresSistema', 'Gerenciar administradores do sistema', 'Administrador', 'Gerenciar'),
('Admin.GerenciarPapeis', 'Gerenciar papéis e permissões', 'Papel', 'Gerenciar'),

-- Academia
('Academia.Criar', 'Criar novas academias', 'Academia', 'Criar'),
('Academia.Ler', 'Visualizar dados das academias', 'Academia', 'Ler'),
('Academia.Atualizar', 'Atualizar dados das academias', 'Academia', 'Atualizar'),
('Academia.Excluir', 'Excluir academias', 'Academia', 'Excluir'),

-- Cliente
('Cliente.Gerenciar', 'Gerenciar clientes da academia', 'Cliente', 'Gerenciar'),
('Cliente.Ler', 'Visualizar dados dos clientes', 'Cliente', 'Ler'),

-- Plano
('Plano.Gerenciar', 'Gerenciar planos da academia', 'Plano', 'Gerenciar'),

-- Pagamento
('Pagamento.Processar', 'Processar pagamentos', 'Pagamento', 'Processar'),
('Pagamento.Ler', 'Visualizar pagamentos', 'Pagamento', 'Ler'),

-- CheckIn
('CheckIn.Processar', 'Processar check-ins', 'CheckIn', 'Processar'),

-- Financeiro
('Financeiro.VisualizarRelatoriosSistema', 'Visualizar relatórios financeiros do sistema', 'Financeiro', 'Ler'),
('Financeiro.VisualizarRelatoriosAcademia', 'Visualizar relatórios financeiros da academia', 'Financeiro', 'Ler');

-- Associar Permissões aos Papéis
DECLARE @SuperAdminId INT = (SELECT Id FROM Papel WHERE Nome = 'SuperAdmin');
DECLARE @AdminSistemaId INT = (SELECT Id FROM Papel WHERE Nome = 'AdminSistema');
DECLARE @AdminAcademiaId INT = (SELECT Id FROM Papel WHERE Nome = 'AdminAcademia');
DECLARE @SuporteId INT = (SELECT Id FROM Papel WHERE Nome = 'Suporte');

-- SuperAdmin - Todas as permissões
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @SuperAdminId, Id FROM Permissao;

-- AdminSistema - Gerenciar academias e ver relatórios
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @AdminSistemaId, Id FROM Permissao 
WHERE Nome IN ('Academia.Criar', 'Academia.Ler', 'Academia.Atualizar', 'Academia.Excluir', 'Financeiro.VisualizarRelatoriosSistema');

-- AdminAcademia - Gerenciar sua academia
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @AdminAcademiaId, Id FROM Permissao 
WHERE Nome IN ('Cliente.Gerenciar', 'Plano.Gerenciar', 'Pagamento.Processar', 'CheckIn.Processar', 'Financeiro.VisualizarRelatoriosAcademia');

-- Suporte - Apenas leitura
INSERT INTO PapelPermissao (IdPapel, IdPermissao)
SELECT @SuporteId, Id FROM Permissao 
WHERE Nome IN ('Academia.Ler', 'Cliente.Ler', 'Pagamento.Ler');

-- Atualizar administrador existente para SuperAdmin
UPDATE Administrador 
SET EhSuperAdmin = 1, IdAcademia = NULL 
WHERE Id = 1;

-- Associar papel SuperAdmin ao primeiro administrador
IF NOT EXISTS (SELECT 1 FROM AdministradorPapel WHERE IdAdministrador = 1 AND IdPapel = @SuperAdminId)
INSERT INTO AdministradorPapel (IdAdministrador, IdPapel) VALUES (1, @SuperAdminId);

PRINT 'Script executado com sucesso! Estrutura RBAC e Multi-tenancy implementada.';
