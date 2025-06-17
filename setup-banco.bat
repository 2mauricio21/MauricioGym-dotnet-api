@echo off
chcp 65001 > nul
setlocal enabledelayedexpansion

echo ================================================================================
echo                  MauricioGym - Setup Automatico do Banco de Dados
echo ================================================================================
echo.
echo Este script vai:
echo  1. Verificar se o SQL Server LocalDB esta disponivel
echo  2. Criar o banco de dados MauricioGymDB (ou recriar se ja existir)
echo  3. Criar todas as tabelas necessarias para os dois dominios
echo  4. Inserir dados de exemplo para testes
echo.
echo ATENCAO: Se o banco MauricioGymDB ja existir, ele sera EXCLUIDO e recriado!
echo.
echo Pressione ENTER para continuar ou CTRL+C para cancelar...
pause > nul

echo.
echo Verificando SQL Server LocalDB...
echo.

sqlcmd -S "(localdb)\mssqllocaldb" -Q "SELECT @@VERSION" > nul 2>&1

if errorlevel 1 (
    echo [ERRO] Nao foi possivel conectar ao SQL Server LocalDB.
    echo.
    echo Tente o seguinte:
    echo  1. Verifique se o SQL Server LocalDB esta instalado: sqllocaldb info
    echo  2. Inicie o LocalDB manualmente: sqllocaldb start mssqllocaldb
    echo  3. Verifique se o servico esta rodando: sqllocaldb info mssqllocaldb
    echo.
    echo Se o problema persistir, instale o SQL Server LocalDB pelo instalador do Visual Studio
    echo ou baixe o SQL Server Express com suporte a LocalDB.
    echo.
    pause
    exit /b 1
)

echo [OK] SQL Server LocalDB esta disponivel!
echo.
echo Executando setup do banco de dados...
echo.

REM Executa o script SQL
sqlcmd -S "(localdb)\mssqllocaldb" -i "sql\setup_completo.sql" > sqloutput.txt 2>&1
set SQL_RESULT=%ERRORLEVEL%

if %SQL_RESULT% EQU 0 (
    echo.
    echo ================================================================================
    echo                               SUCESSO!
    echo ================================================================================
    echo O banco MauricioGymDB foi criado com sucesso com todos os dados de exemplo!
    echo.
    echo Proximos passos:
    echo  1. Execute: executar-apis.bat             - Para iniciar as APIs e verificar se tudo esta funcionando
    echo  2. Acesse: http://localhost:5001/swagger - Para a API do Administrador
    echo  3. Acesse: http://localhost:5002/swagger - Para a API do Usuario
    echo.
    echo Dados de exemplo disponiveis:
    echo  - 2 Administradores
    echo  - 4 Planos (Mensal, Trimestral, Semestral, Anual)
    echo  - 5 Usuarios
    echo  - 5 Vinculos UsuarioPlano
    echo  - 3 CheckIns
    echo  - 5 Mensalidades
    echo ================================================================================
    
    del sqloutput.txt > nul 2>&1
    echo.
    echo Pressione qualquer tecla para sair...
    pause > nul
    exit /b 0
) else (
    echo.
    echo ================================================================================
    echo                               ERRO!
    echo ================================================================================
    echo Houve um erro na criacao do banco de dados.
    echo.
    echo Causas comuns:
    echo  1. Erro de sintaxe no script SQL
    echo  2. Banco em uso por outro processo
    echo  3. Permissoes insuficientes
    echo.
    echo Tente o seguinte:
    echo  1. Verifique se o SQL Server LocalDB esta funcionando: sqllocaldb info
    echo  2. Reinicie o LocalDB: sqllocaldb stop mssqllocaldb ^& sqllocaldb start mssqllocaldb
    echo  3. Execute este script novamente
    echo ================================================================================
    
    del sqloutput.txt > nul 2>&1
    pause
    exit /b 1
)
