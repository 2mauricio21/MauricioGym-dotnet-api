-- Script de Verificação do Banco MauricioGym
-- Execute este script após rodar o create-database-complete.sql

USE master;
GO

-- 1. Verificar se o banco MauricioGymDB existe
PRINT '=== VERIFICAÇÃO DO BANCO DE DADOS ===';
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MauricioGymDB')
    PRINT '✅ Banco MauricioGymDB encontrado';
ELSE
    PRINT '❌ Banco MauricioGymDB NÃO encontrado - Execute o script create-database-complete.sql';

USE MauricioGymDB;
GO

-- 2. Verificar todas as tabelas
PRINT '';
PRINT '=== VERIFICAÇÃO DAS TABELAS ===';

DECLARE @TabelasEsperadas TABLE (NomeTabela NVARCHAR(50));
INSERT INTO @TabelasEsperadas VALUES 
    ('Usuarios'), ('Autenticacao'), ('Academias'), ('Funcionarios'), 
    ('Planos'), ('Usuarios_Planos'), ('Pagamentos'), ('Acessos'), 
    ('Bloqueios'), ('RecuperacaoSenha'), ('Logs'), ('Configuracoes'), ('Notificacoes');

DECLARE @TabelaAtual NVARCHAR(50);
DECLARE @Contador INT = 0;

DECLARE cursor_tabelas CURSOR FOR 
SELECT NomeTabela FROM @TabelasEsperadas;

OPEN cursor_tabelas;
FETCH NEXT FROM cursor_tabelas INTO @TabelaAtual;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TabelaAtual)
    BEGIN
        PRINT '✅ Tabela ' + @TabelaAtual + ' encontrada';
        SET @Contador = @Contador + 1;
    END
    ELSE
        PRINT '❌ Tabela ' + @TabelaAtual + ' NÃO encontrada';
    
    FETCH NEXT FROM cursor_tabelas INTO @TabelaAtual;
END

CLOSE cursor_tabelas;
DEALLOCATE cursor_tabelas;

PRINT '';
PRINT 'Total de tabelas encontradas: ' + CAST(@Contador AS NVARCHAR(10)) + '/13';

-- 3. Verificar estrutura específica da tabela Autenticacao
PRINT '';
PRINT '=== VERIFICAÇÃO DA TABELA AUTENTICACAO ===';

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Autenticacao')
BEGIN
    PRINT '✅ Tabela Autenticacao existe';
    
    -- Verificar colunas essenciais
    DECLARE @ColunasEsperadas TABLE (NomeColuna NVARCHAR(50));
    INSERT INTO @ColunasEsperadas VALUES 
        ('IdAutenticacao'), ('IdUsuario'), ('Email'), ('Senha'), 
        ('TokenRecuperacao'), ('TentativasLogin'), ('ContaBloqueada'), 
        ('RefreshToken'), ('Ativo'), ('DataCriacao'), ('DataUltimoLogin');
    
    DECLARE @ColunaAtual NVARCHAR(50);
    DECLARE @ContadorColunas INT = 0;
    
    DECLARE cursor_colunas CURSOR FOR 
    SELECT NomeColuna FROM @ColunasEsperadas;
    
    OPEN cursor_colunas;
    FETCH NEXT FROM cursor_colunas INTO @ColunaAtual;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Autenticacao' AND COLUMN_NAME = @ColunaAtual)
        BEGIN
            PRINT '  ✅ Coluna ' + @ColunaAtual + ' encontrada';
            SET @ContadorColunas = @ContadorColunas + 1;
        END
        ELSE
            PRINT '  ❌ Coluna ' + @ColunaAtual + ' NÃO encontrada';
        
        FETCH NEXT FROM cursor_colunas INTO @ColunaAtual;
    END
    
    CLOSE cursor_colunas;
    DEALLOCATE cursor_colunas;
    
    PRINT '  Total de colunas encontradas: ' + CAST(@ContadorColunas AS NVARCHAR(10)) + '/11';
END
ELSE
    PRINT '❌ Tabela Autenticacao NÃO existe';

-- 4. Verificar dados de teste
PRINT '';
PRINT '=== VERIFICAÇÃO DOS DADOS DE TESTE ===';

IF EXISTS (SELECT * FROM Usuarios WHERE Email = 'admin@mauriciogym.com')
    PRINT '✅ Usuário admin@mauriciogym.com encontrado na tabela Usuarios';
ELSE
    PRINT '❌ Usuário admin@mauriciogym.com NÃO encontrado na tabela Usuarios';

IF EXISTS (SELECT * FROM Autenticacao WHERE Email = 'admin@mauriciogym.com')
    PRINT '✅ Autenticação admin@mauriciogym.com encontrada na tabela Autenticacao';
ELSE
    PRINT '❌ Autenticação admin@mauriciogym.com NÃO encontrada na tabela Autenticacao';

-- 5. Resumo final
PRINT '';
PRINT '=== RESUMO FINAL ===';
IF @Contador = 13 AND @ContadorColunas = 11
BEGIN
    PRINT '🎉 SUCESSO: Banco de dados configurado corretamente!';
    PRINT '   - Todas as 13 tabelas foram criadas';
    PRINT '   - Tabela Autenticacao tem todas as colunas necessárias';
    PRINT '   - Sistema pronto para uso!';
END
ELSE
BEGIN
    PRINT '⚠️  ATENÇÃO: Configuração incompleta!';
    PRINT '   - Execute novamente o script create-database-complete.sql';
    PRINT '   - Verifique se não há erros durante a execução';
END

PRINT '';
PRINT '=== FIM DA VERIFICAÇÃO ===';