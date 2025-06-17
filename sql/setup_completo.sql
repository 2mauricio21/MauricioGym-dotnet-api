-- ====================================================================
-- Script Completo de Criação do Banco MauricioGym
-- ====================================================================
-- Este script cria automaticamente:
-- 1. Banco de dados MauricioGymDB
-- 2. Todas as tabelas necessárias para os domínios Administrador e Usuario
-- 3. Dados de exemplo para testes
-- 
-- Para executar: sqlcmd -S "(localdb)\mssqllocaldb" -i "sql\setup_completo.sql"
-- ====================================================================

PRINT '=== Iniciando criação do banco MauricioGym ==='

-- ====================================================================
-- 1. CRIAÇÃO DO BANCO DE DADOS
-- ====================================================================
PRINT '1. Criando banco de dados MauricioGymDB...'

-- Remover banco se existir (para garantir ambiente limpo)
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'MauricioGymDB')
BEGIN
    PRINT '   - Removendo banco existente...'
    ALTER DATABASE MauricioGymDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE MauricioGymDB
END

-- Criar banco
CREATE DATABASE MauricioGymDB
PRINT '   ✓ Banco MauricioGymDB criado com sucesso!'
GO

-- Usar o banco criado
USE MauricioGymDB
PRINT '   ✓ Conectado ao banco MauricioGymDB'
GO

-- ====================================================================
-- 2. CRIAÇÃO DAS TABELAS PRINCIPAIS
-- ====================================================================
PRINT '2. Criando tabelas principais...'

-- Tabela Usuario
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Telefone NVARCHAR(20),
    DataNascimento DATE NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);
PRINT '   ✓ Tabela Usuario criada'

-- Tabela Administrador
CREATE TABLE Administrador (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    SenhaHash NVARCHAR(255) NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);
PRINT '   ✓ Tabela Administrador criada'

-- Tabela Plano
CREATE TABLE Plano (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DuracaoMeses INT NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL
);
PRINT '   ✓ Tabela Plano criada'

-- Tabela Caixa
CREATE TABLE Caixa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    QuantidadeAlunos INT NOT NULL,
    ValorTotal DECIMAL(18,2) NOT NULL,
    DataAtualizacao DATETIME NOT NULL DEFAULT GETDATE()
);
PRINT '   ✓ Tabela Caixa criada'

-- ====================================================================
-- 3. CRIAÇÃO DAS TABELAS ASSOCIATIVAS
-- ====================================================================
PRINT '3. Criando tabelas associativas...'

-- Tabela UsuarioPlano (relacionamento entre Usuario e Plano)
CREATE TABLE UsuarioPlano (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    PlanoId INT NOT NULL,
    DataInicio DATE NOT NULL,
    DataFim DATE NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (PlanoId) REFERENCES Plano(Id)
);
PRINT '   ✓ Tabela UsuarioPlano criada'

-- Tabela PermissaoManipulacaoUsuario
CREATE TABLE PermissaoManipulacaoUsuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AdministradorId INT NOT NULL,
    UsuarioId INT NOT NULL,
    TipoPermissao NVARCHAR(50) NOT NULL, -- Cadastrar, Editar, Excluir
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL,
    FOREIGN KEY (AdministradorId) REFERENCES Administrador(Id),
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);
PRINT '   ✓ Tabela PermissaoManipulacaoUsuario criada'

-- Tabela CheckIn
CREATE TABLE CheckIn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Observacoes NVARCHAR(255) NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id)
);
PRINT '   ✓ Tabela CheckIn criada'

-- Tabela Mensalidade
CREATE TABLE Mensalidade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioPlanoId INT NOT NULL,
    MesReferencia INT NOT NULL,
    AnoReferencia INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataVencimento DATE NOT NULL,
    DataPagamento DATE NULL,
    Status NVARCHAR(50) NOT NULL, -- Paga, Pendente, Atrasada
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL,
    FOREIGN KEY (UsuarioPlanoId) REFERENCES UsuarioPlano(Id)
);
PRINT '   ✓ Tabela Mensalidade criada'

-- ====================================================================
-- 4. INSERÇÃO DE DADOS DE EXEMPLO
-- ====================================================================
PRINT '4. Inserindo dados de exemplo...'

-- Inserir Administradores
INSERT INTO Administrador (Nome, Email, SenhaHash, Ativo, DataCriacao) VALUES 
('Admin', 'admin@mauriciogym.com', 'hash123', 1, GETDATE()),
('Mauricio', 'mauricio@mauriciogym.com', 'hash456', 1, GETDATE());
PRINT '   ✓ Administradores inseridos (2)'

-- Inserir Planos
INSERT INTO Plano (Nome, Valor, DuracaoMeses, Ativo, DataCriacao) VALUES 
('Mensal', 99.90, 1, 1, GETDATE()),
('Trimestral', 269.90, 3, 1, GETDATE()),
('Semestral', 499.90, 6, 1, GETDATE()),
('Anual', 999.90, 12, 1, GETDATE());
PRINT '   ✓ Planos inseridos (4)'

-- Inserir Usuarios
INSERT INTO Usuario (Nome, Email, Telefone, DataNascimento, Ativo, DataCriacao) VALUES 
('João Silva', 'joao@email.com', '(11) 98765-4321', '1990-05-15', 1, GETDATE()),
('Maria Oliveira', 'maria@email.com', '(11) 97654-3210', '1985-10-20', 1, GETDATE()),
('Pedro Santos', 'pedro@email.com', '(11) 96543-2109', '1995-03-25', 1, GETDATE()),
('Ana Costa', 'ana@email.com', '(11) 95432-1098', '1992-07-12', 1, GETDATE()),
('Carlos Souza', 'carlos@email.com', '(11) 94321-0987', '1988-12-30', 1, GETDATE());
PRINT '   ✓ Usuários inseridos (5)'

-- Definir datas para os exemplos
DECLARE @DataHoje DATE = GETDATE();
DECLARE @DataInicio DATE = DATEADD(MONTH, -1, @DataHoje);
DECLARE @DataFim1Mes DATE = DATEADD(MONTH, 1, @DataInicio);
DECLARE @DataFim3Meses DATE = DATEADD(MONTH, 3, @DataInicio);
DECLARE @DataFim6Meses DATE = DATEADD(MONTH, 6, @DataInicio);
DECLARE @DataFim12Meses DATE = DATEADD(MONTH, 12, @DataInicio);

-- Inserir UsuarioPlano
INSERT INTO UsuarioPlano (UsuarioId, PlanoId, DataInicio, DataFim, Ativo, DataCriacao) VALUES 
(1, 1, @DataInicio, @DataFim1Mes, 1, GETDATE()),    -- João - Mensal
(2, 2, @DataInicio, @DataFim3Meses, 1, GETDATE()),  -- Maria - Trimestral
(3, 3, @DataInicio, @DataFim6Meses, 1, GETDATE()),  -- Pedro - Semestral
(4, 4, @DataInicio, @DataFim12Meses, 1, GETDATE()), -- Ana - Anual
(5, 1, @DataInicio, @DataFim1Mes, 1, GETDATE());    -- Carlos - Mensal
PRINT '   ✓ Vínculos UsuarioPlano inseridos (5)'

-- Inserir CheckIn
INSERT INTO CheckIn (UsuarioId, DataHora, Observacoes, Ativo, DataCriacao) VALUES 
(1, DATEADD(DAY, -1, GETDATE()), 'Check-in realizado com sucesso', 1, GETDATE()),
(2, DATEADD(DAY, -2, GETDATE()), 'Check-in realizado com sucesso', 1, GETDATE()),
(3, DATEADD(DAY, -3, GETDATE()), 'Check-in realizado com sucesso', 1, GETDATE());
PRINT '   ✓ Check-ins inseridos (3)'

-- Definir mês/ano atual para as mensalidades
DECLARE @MesAtual INT = MONTH(GETDATE());
DECLARE @AnoAtual INT = YEAR(GETDATE());

-- Inserir Mensalidades
INSERT INTO Mensalidade (UsuarioPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status, Ativo, DataCriacao) VALUES 
(1, @MesAtual, @AnoAtual, 99.90, DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -12, GETDATE()), 'Paga', 1, GETDATE()),
(2, @MesAtual, @AnoAtual, 89.97, DATEADD(DAY, 5, GETDATE()), NULL, 'Pendente', 1, GETDATE()),
(3, @MesAtual, @AnoAtual, 83.32, DATEADD(DAY, 15, GETDATE()), NULL, 'Pendente', 1, GETDATE()),
(4, @MesAtual, @AnoAtual, 83.33, DATEADD(DAY, 20, GETDATE()), NULL, 'Pendente', 1, GETDATE()),
(5, @MesAtual, @AnoAtual, 99.90, DATEADD(DAY, 8, GETDATE()), NULL, 'Pendente', 1, GETDATE());
PRINT '   ✓ Mensalidades inseridas (5)'

-- Atualizar Caixa
INSERT INTO Caixa (QuantidadeAlunos, ValorTotal, DataAtualizacao) VALUES 
(5, 456.42, GETDATE());
PRINT '   ✓ Caixa atualizado'

-- ====================================================================
-- 6. VERIFICAÇÃO FINAL
-- ====================================================================
PRINT '6. Verificando dados inseridos...'

SELECT 
    'Administradores' as Tabela, 
    COUNT(*) as Quantidade 
FROM Administrador
UNION ALL
SELECT 'Usuarios', COUNT(*) FROM Usuario
UNION ALL
SELECT 'Planos', COUNT(*) FROM Plano
UNION ALL
SELECT 'UsuarioPlano', COUNT(*) FROM UsuarioPlano
UNION ALL
SELECT 'CheckIns', COUNT(*) FROM CheckIn
UNION ALL
SELECT 'Mensalidades', COUNT(*) FROM Mensalidade
UNION ALL
SELECT 'Caixa', COUNT(*) FROM Caixa

PRINT ''
PRINT '=== ✓ Setup do banco MauricioGymDB concluído com sucesso! ==='
PRINT '=== Banco: MauricioGymDB | Tabelas: 7 | Dados de exemplo inseridos ==='
PRINT '=== APIs disponíveis em:'
PRINT '===  - Administrador: http://localhost:5001/swagger'
PRINT '===  - Usuario: http://localhost:5002/swagger'
PRINT ''
