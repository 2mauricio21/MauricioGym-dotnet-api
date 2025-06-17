-- Usar o banco de dados MauricioGym
USE MauricioGymDB;
GO

-- Tabela associativa para vincular Pessoa a Plano
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

-- Tabela para definir permissões de manipulação de usuário
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
