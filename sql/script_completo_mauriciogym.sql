-- Script completo e profissional para MauricioGym

-- Criar o banco de dados se não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'MauricioGymDB')
BEGIN
    CREATE DATABASE MauricioGymDB;
END
GO

-- Usar o banco de dados MauricioGym
USE MauricioGymDB;
GO

-- Tabelas principais
CREATE TABLE Pessoa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20),
    DataNascimento DATE NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);

CREATE TABLE Administrador (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    SenhaHash NVARCHAR(255) NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);

CREATE TABLE Plano (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DuracaoMeses INT NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);

CREATE TABLE Caixa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    QuantidadeAlunos INT NOT NULL,
    ValorTotal DECIMAL(18,2) NOT NULL,
    DataAtualizacao DATETIME NOT NULL DEFAULT GETDATE()
);

-- Tabelas associativas e de relacionamento
CREATE TABLE PessoaPlano (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    PlanoId INT NOT NULL,
    DataInicio DATE NOT NULL,
    DataFim DATE NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL,
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id),
    FOREIGN KEY (PlanoId) REFERENCES Plano(Id)
);

CREATE TABLE PermissaoManipulacaoUsuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AdministradorId INT NOT NULL,
    PessoaId INT NOT NULL,
    PodeEditar BIT NOT NULL DEFAULT 0,
    PodeExcluir BIT NOT NULL DEFAULT 0,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AdministradorId) REFERENCES Administrador(Id),
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id)
);

CREATE TABLE CheckIn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    DataHora DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id)
);

CREATE TABLE Mensalidade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    PlanoId INT NOT NULL,
    ValorPago DECIMAL(10,2) NOT NULL,
    DataPagamento DATETIME NOT NULL DEFAULT GETDATE(),
    PeriodoInicio DATE NOT NULL,
    PeriodoFim DATE NOT NULL,
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id),
    FOREIGN KEY (PlanoId) REFERENCES Plano(Id)
);

-- Dados iniciais para testes
INSERT INTO Administrador (Nome, Email, SenhaHash, Ativo, DataCriacao) VALUES
('Administrador Principal', 'admin@mauriciogym.com', 'HASH_SENHA_ADMIN', 1, GETDATE());

INSERT INTO Plano (Nome, Valor, DuracaoMeses, Ativo, DataCriacao) VALUES
('Mensal', 100.00, 1, 1, GETDATE()),
('Trimestral', 270.00, 3, 1, GETDATE()),
('Anual', 960.00, 12, 1, GETDATE());

INSERT INTO Pessoa (Nome, Email, Telefone, DataNascimento, Ativo, DataCriacao) VALUES
('João Silva', 'joao@exemplo.com', '11999990001', '1990-01-01', 1, GETDATE()),
('Maria Souza', 'maria@exemplo.com', '11999990002', '1992-02-02', 1, GETDATE()),
('Carlos Lima', 'carlos@exemplo.com', '11999990003', '1988-03-03', 1, GETDATE()),
('Ana Paula', 'ana@exemplo.com', '11999990004', '1995-04-04', 1, GETDATE()),
('Fernanda Costa', 'fernanda@exemplo.com', '11999990005', '1993-05-05', 1, GETDATE());

-- Vinculando alunos a planos
INSERT INTO PessoaPlano (PessoaId, PlanoId, DataInicio, Ativo, DataCriacao) VALUES
(1, 1, '2025-06-01', 1, GETDATE()),
(2, 2, '2025-06-01', 1, GETDATE()),
(3, 3, '2025-06-01', 1, GETDATE()),
(4, 1, '2025-06-01', 1, GETDATE()),
(5, 2, '2025-06-01', 1, GETDATE());

-- Permissões de manipulação
INSERT INTO PermissaoManipulacaoUsuario (AdministradorId, PessoaId, PodeEditar, PodeExcluir, DataCriacao) VALUES
(1, 1, 1, 1, GETDATE()),
(1, 2, 1, 1, GETDATE()),
(1, 3, 1, 1, GETDATE()),
(1, 4, 1, 1, GETDATE()),
(1, 5, 1, 1, GETDATE());

-- Mensalidades pagas
INSERT INTO Mensalidade (PessoaId, PlanoId, ValorPago, DataPagamento, PeriodoInicio, PeriodoFim) VALUES
(1, 1, 100.00, GETDATE(), '2025-06-01', '2025-07-01'),
(2, 2, 270.00, GETDATE(), '2025-06-01', '2025-09-01'),
(3, 3, 960.00, GETDATE(), '2025-06-01', '2026-06-01'),
(4, 1, 100.00, GETDATE(), '2025-06-01', '2025-07-01'),
(5, 2, 270.00, GETDATE(), '2025-06-01', '2025-09-01');

-- Check-ins de alunos
INSERT INTO CheckIn (PessoaId, DataHora) VALUES
(1, DATEADD(day, -1, GETDATE())),
(1, GETDATE()),
(2, GETDATE()),
(3, GETDATE()),
(4, GETDATE()),
(5, GETDATE());

-- Caixa inicial
INSERT INTO Caixa (QuantidadeAlunos, ValorTotal, DataAtualizacao) VALUES
(5, 1700.00, GETDATE());
