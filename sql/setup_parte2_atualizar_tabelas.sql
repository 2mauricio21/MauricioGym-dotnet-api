-- Script para inserir dados iniciais
USE MauricioGymDB;
GO

-- Inserir Academia padrão
IF NOT EXISTS (SELECT 1 FROM Academia WHERE Cnpj = '12.345.678/0001-90')
INSERT INTO Academia (Nome, Cnpj, Email, Telefone, DataVencimentoLicenca, MaximoClientes, LicencaAtiva)
VALUES ('MauricioGym Principal', '12.345.678/0001-90', 'contato@mauriciogym.com', '(11) 99999-9999', 
        DATEADD(year, 1, GETDATE()), 1000, 1);

-- Atualizar tabela Administrador
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'Cpf')
    ALTER TABLE Administrador ADD Cpf NVARCHAR(14) NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'IdAcademia')
    ALTER TABLE Administrador ADD IdAcademia INT NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'EhSuperAdmin')
    ALTER TABLE Administrador ADD EhSuperAdmin BIT NOT NULL DEFAULT 0;

-- Atualizar tabela Plano
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'IdAcademia')
    ALTER TABLE Plano ADD IdAcademia INT NULL;

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'Descricao')
    ALTER TABLE Plano ADD Descricao NVARCHAR(500) NULL;

-- Atualizar dados existentes
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Plano') AND name = 'IdAcademia')
    UPDATE Plano SET IdAcademia = 1 WHERE IdAcademia IS NULL;

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Administrador') AND name = 'EhSuperAdmin')
    UPDATE Administrador SET EhSuperAdmin = 1 WHERE Id = 1;

PRINT 'Dados atualizados com sucesso!';
