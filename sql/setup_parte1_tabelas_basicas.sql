-- Script simplificado - Parte 1: Criar novas tabelas
USE MauricioGymDB;
GO

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

-- Inserir Academia padrão
IF NOT EXISTS (SELECT 1 FROM Academia WHERE Id = 1)
INSERT INTO Academia (Id, Nome, Cnpj, Email, Telefone, DataVencimentoLicenca, MaximoClientes, LicencaAtiva)
VALUES (1, 'MauricioGym Principal', '12.345.678/0001-90', 'contato@mauriciogym.com', '(11) 99999-9999', 
        DATEADD(year, 1, GETDATE()), 1000, 1);

PRINT 'Tabelas básicas criadas com sucesso!';
