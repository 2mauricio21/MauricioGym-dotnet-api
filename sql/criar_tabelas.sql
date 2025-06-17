-- Script de criação do banco de dados e tabelas para MauricioGym

-- Criar o banco de dados se não existir
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MauricioGymDB')
BEGIN
    CREATE DATABASE MauricioGymDB;
END
GO

-- Usar o banco de dados criado
USE MauricioGymDB;
GO

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
