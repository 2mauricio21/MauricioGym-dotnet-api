-- Usar o banco de dados MauricioGym
USE MauricioGymDB;
GO

-- Tabela para Check-in de alunos
CREATE TABLE CheckIn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    DataHora DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id)
);

-- Tabela para controle de pagamentos de mensalidades
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
