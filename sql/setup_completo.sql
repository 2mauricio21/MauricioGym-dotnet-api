-- ====================================================================
-- Script Completo de Criação do Banco MauricioGym
-- ====================================================================
-- Este script cria automaticamente:
-- 1. Banco de dados MauricioGymDB
-- 2. Todas as tabelas necessárias
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

-- Tabela Pessoa
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
PRINT '   ✓ Tabela Pessoa criada'

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

-- Tabela PessoaPlano (relacionamento entre Pessoa e Plano)
CREATE TABLE PessoaPlano (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    PlanoId INT NOT NULL,
    DataInicio DATE NOT NULL,
    DataFim DATE NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1,
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NULL,
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id),
    FOREIGN KEY (PlanoId) REFERENCES Plano(Id)
);
PRINT '   ✓ Tabela PessoaPlano criada'

-- Tabela PermissaoManipulacaoUsuario
CREATE TABLE PermissaoManipulacaoUsuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AdministradorId INT NOT NULL,
    PessoaId INT NOT NULL,
    TipoOperacao NVARCHAR(50) NOT NULL,
    DataOperacao DATETIME NOT NULL DEFAULT GETDATE(),
    Observacoes NVARCHAR(500),
    FOREIGN KEY (AdministradorId) REFERENCES Administrador(Id),
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id)
);
PRINT '   ✓ Tabela PermissaoManipulacaoUsuario criada'

-- ====================================================================
-- 4. CRIAÇÃO DAS TABELAS DE CONTROLE
-- ====================================================================
PRINT '4. Criando tabelas de controle...'

-- Tabela CheckIn
CREATE TABLE CheckIn (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaId INT NOT NULL,
    DataHora DATETIME NOT NULL DEFAULT GETDATE(),
    Observacoes NVARCHAR(500),
    FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id)
);
PRINT '   ✓ Tabela CheckIn criada'

-- Tabela Mensalidade
CREATE TABLE Mensalidade (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PessoaPlanoId INT NOT NULL,
    MesReferencia INT NOT NULL,
    AnoReferencia INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataVencimento DATE NOT NULL,
    DataPagamento DATE NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pendente',
    FOREIGN KEY (PessoaPlanoId) REFERENCES PessoaPlano(Id)
);
PRINT '   ✓ Tabela Mensalidade criada'

-- ====================================================================
-- 5. INSERÇÃO DE DADOS DE EXEMPLO
-- ====================================================================
PRINT '5. Inserindo dados de exemplo...'

-- Inserir Administradores
INSERT INTO Administrador (Nome, Email, SenhaHash) VALUES 
('Admin Sistema', 'admin@mauriciogym.com', 'HASH_SENHA_ADMIN'),
('Mauricio Gym', 'mauricio@mauriciogym.com', 'HASH_SENHA_MAURICIO');
PRINT '   ✓ Administradores inseridos (2)'

-- Inserir Planos
INSERT INTO Plano (Nome, Valor, DuracaoMeses) VALUES 
('Plano Mensal', 99.90, 1),
('Plano Trimestral', 269.90, 3),
('Plano Semestral', 499.90, 6),
('Plano Anual', 999.90, 12);
PRINT '   ✓ Planos inseridos (4)'

-- Inserir Pessoas
INSERT INTO Pessoa (Nome, Email, Telefone, DataNascimento) VALUES 
('João Silva', 'joao.silva@email.com', '(11) 99999-9999', '1990-01-15'),
('Maria Santos', 'maria.santos@email.com', '(11) 88888-8888', '1985-06-20'),
('Pedro Oliveira', 'pedro.oliveira@email.com', '(11) 77777-7777', '1992-03-10'),
('Ana Costa', 'ana.costa@email.com', '(11) 66666-6666', '1988-12-05'),
('Carlos Pereira', 'carlos.pereira@email.com', '(11) 55555-5555', '1995-08-22');
PRINT '   ✓ Pessoas inseridas (5)'

-- Inserir PessoaPlano (vincular pessoas aos planos)
INSERT INTO PessoaPlano (PessoaId, PlanoId, DataInicio, DataFim) VALUES 
(1, 1, GETDATE(), DATEADD(MONTH, 1, GETDATE())),    -- João - Mensal
(2, 2, GETDATE(), DATEADD(MONTH, 3, GETDATE())),    -- Maria - Trimestral
(3, 3, GETDATE(), DATEADD(MONTH, 6, GETDATE())),    -- Pedro - Semestral
(4, 4, GETDATE(), DATEADD(MONTH, 12, GETDATE())),   -- Ana - Anual
(5, 1, GETDATE(), DATEADD(MONTH, 1, GETDATE()));    -- Carlos - Mensal
PRINT '   ✓ Vínculos PessoaPlano inseridos (5)'

-- Inserir CheckIns
INSERT INTO CheckIn (PessoaId, DataHora, Observacoes) VALUES 
(1, GETDATE(), 'Check-in de teste - João'),
(2, DATEADD(HOUR, -2, GETDATE()), 'Check-in de teste - Maria'),
(3, DATEADD(HOUR, -5, GETDATE()), 'Check-in de teste - Pedro');
PRINT '   ✓ Check-ins inseridos (3)'

-- Inserir Mensalidades
DECLARE @MesAtual INT = MONTH(GETDATE())
DECLARE @AnoAtual INT = YEAR(GETDATE())

INSERT INTO Mensalidade (PessoaPlanoId, MesReferencia, AnoReferencia, Valor, DataVencimento, DataPagamento, Status) VALUES 
(1, @MesAtual, @AnoAtual, 99.90, DATEADD(DAY, 10, GETDATE()), GETDATE(), 'Pago'),
(2, @MesAtual, @AnoAtual, 89.97, DATEADD(DAY, 5, GETDATE()), NULL, 'Pendente'),
(3, @MesAtual, @AnoAtual, 83.32, DATEADD(DAY, 15, GETDATE()), NULL, 'Pendente'),
(4, @MesAtual, @AnoAtual, 83.33, DATEADD(DAY, 20, GETDATE()), NULL, 'Pendente'),
(5, @MesAtual, @AnoAtual, 99.90, DATEADD(DAY, 8, GETDATE()), NULL, 'Pendente');
PRINT '   ✓ Mensalidades inseridas (5)'

-- Atualizar Caixa
INSERT INTO Caixa (QuantidadeAlunos, ValorTotal) VALUES 
(5, 456.42);
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
SELECT 'Pessoas', COUNT(*) FROM Pessoa
UNION ALL
SELECT 'Planos', COUNT(*) FROM Plano
UNION ALL
SELECT 'PessoaPlano', COUNT(*) FROM PessoaPlano
UNION ALL
SELECT 'CheckIns', COUNT(*) FROM CheckIn
UNION ALL
SELECT 'Mensalidades', COUNT(*) FROM Mensalidade
UNION ALL
SELECT 'Caixa', COUNT(*) FROM Caixa

PRINT ''
PRINT '=== ✓ Setup do banco MauricioGym concluído com sucesso! ==='
PRINT '=== Banco: MauricioGymDB | Tabelas: 8 | Dados de exemplo inseridos ==='
PRINT '=== A API já pode ser testada em: http://localhost:5000/swagger ==='
PRINT ''
