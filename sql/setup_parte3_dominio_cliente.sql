-- Script parte 3 - Criar tabelas do domínio Cliente
USE MauricioGymDB;
GO

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
    TipoPagamento NVARCHAR(20) NOT NULL DEFAULT 'MENSALIDADE',
    Valor DECIMAL(10,2) NOT NULL,
    DataPagamento DATETIME2 NOT NULL DEFAULT GETDATE(),
    MesReferencia INT NULL,
    AnoReferencia INT NULL,
    FormaPagamento NVARCHAR(20) NOT NULL DEFAULT 'DINHEIRO',
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

-- Recriar tabela CheckIn com nova estrutura
IF EXISTS (SELECT * FROM sysobjects WHERE name='CheckIn' AND xtype='U')
    DROP TABLE CheckIn;

CREATE TABLE CheckIn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CpfCliente NVARCHAR(14) NOT NULL,
    IdAcademia INT NOT NULL,
    DataHoraEntrada DATETIME2 NOT NULL DEFAULT GETDATE(),
    DataHoraSaida DATETIME2 NULL,
    TipoCheckIn NVARCHAR(10) NOT NULL DEFAULT 'PLANO',
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

PRINT 'Tabelas do domínio Cliente criadas com sucesso!';
