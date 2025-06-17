@echo off
echo ====================================================================
echo            MauricioGym - Setup Automatico do Banco de Dados
echo ====================================================================
echo.
echo Este script vai:
echo 1. Criar o banco de dados MauricioGymDB
echo 2. Criar todas as tabelas necessarias
echo 3. Inserir dados de exemplo para testes
echo.
echo Pressione qualquer tecla para continuar ou Ctrl+C para cancelar...
pause > nul

echo.
echo Executando setup do banco de dados...
echo.

sqlcmd -S "(localdb)\mssqllocaldb" -i "sql\setup_completo.sql"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ====================================================================
    echo                          SUCESSO!
    echo ====================================================================
    echo O banco MauricioGymDB foi criado com sucesso!
    echo.
    echo Proximos passos:
    echo 1. Execute: dotnet run --project MauricioGym.Api
    echo 2. Acesse: http://localhost:5000/swagger
    echo 3. Teste os endpoints da API
    echo.
    echo Dados de exemplo disponiveis:
    echo - 2 Administradores
    echo - 4 Planos (Mensal, Trimestral, Semestral, Anual)
    echo - 5 Pessoas
    echo - 5 Vinculos PessoaPlano
    echo - 3 CheckIns
    echo - 5 Mensalidades
    echo ====================================================================
) else (
    echo.
    echo ====================================================================
    echo                          ERRO!
    echo ====================================================================
    echo Houve um erro na criacao do banco de dados.
    echo Verifique se o SQL Server LocalDB esta instalado e funcionando.
    echo.
    echo Para verificar: sqllocaldb info
    echo Para iniciar: sqllocaldb start mssqllocaldb
    echo ====================================================================
)

echo.
echo Pressione qualquer tecla para fechar...
pause > nul
