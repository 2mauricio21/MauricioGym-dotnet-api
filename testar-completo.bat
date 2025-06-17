@echo off
setlocal enabledelayedexpansion

echo ================================================================================
echo                    MAURICIO GYM - TESTE COMPLETO DO SISTEMA
echo ================================================================================
echo.
echo Este script executa:
echo  1. Verificacao do ambiente .NET
echo  2. Restauracao de pacotes NuGet
echo  3. Build completo da solucao (modo Release)
echo  4. Todos os testes unitarios
echo  5. Validacao final do sistema
echo.
echo Pressione ENTER para iniciar os testes completos...
pause > nul

echo.
echo [1/5] Verificando ambiente .NET...
echo ================================================================================
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo [ERRO] .NET SDK nao encontrado! Por favor, instale o .NET 8 SDK.
    echo Voce pode baixa-lo em: https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    pause
    exit /b 1
)

dotnet --version | findstr /r "8\.[0-9]*\.[0-9]*" >nul
if %errorlevel% neq 0 (
    echo [ATENCAO] A versao do .NET SDK pode nao ser compativel com este projeto.
    echo Recomendamos o .NET 8.0 ou superior.
    echo.
    echo Sua versao atual:
    dotnet --version
    echo.
    echo Deseja continuar mesmo assim? (S/N)
    set /p continuar=
    if /i "!continuar!" neq "S" exit /b 1
) else (
    echo [OK] .NET SDK 8.x detectado.
)
echo.

echo [2/5] Restaurando pacotes NuGet...
echo ================================================================================
dotnet restore MauricioGym.sln --nologo
if %errorlevel% neq 0 (
    echo [ERRO] Falha ao restaurar pacotes NuGet!
    echo Verifique sua conexao com a Internet e tente novamente.
    echo.
    pause
    exit /b 1
)
echo [OK] Pacotes restaurados com sucesso.
echo.

echo [3/5] Compilando solucao (modo Release)...
echo ================================================================================
dotnet build MauricioGym.sln -c Release --nologo
if %errorlevel% neq 0 (
    echo [ERRO] Falha na compilacao da solucao!
    echo Verifique os erros acima e corrija-os antes de continuar.
    echo.
    pause
    exit /b 1
)
echo [OK] Compilacao concluida com sucesso.
echo.

echo [4/5] Executando TODOS os testes unitarios...
echo ================================================================================
echo.
dotnet test MauricioGym.sln --nologo -c Release
if %errorlevel% neq 0 (
    echo.
    echo [ERRO] Alguns testes falharam!
    echo Verifique os erros acima e corrija-os antes de continuar.
    echo.
    pause
    exit /b 1
)
echo.
echo [OK] Todos os testes passaram com sucesso!
echo.

echo [5/5] Informacoes do Sistema
echo ================================================================================
echo.
echo API USUARIO:
echo  - Porta: 5002
echo  - Swagger: http://localhost:5002/swagger
echo  - Principal para: check-ins e mensalidades
echo.
echo API ADMINISTRADOR:
echo  - Porta: 5001
echo  - Swagger: http://localhost:5001/swagger
echo  - Principal para: gestao de usuarios, planos e caixa
echo.
echo BANCO DE DADOS:
echo  - Nome: MauricioGymDB
echo  - Instancia: (localdb)\mssqllocaldb
echo  - Setup: execute setup-banco.bat
echo.

echo ================================================================================
echo                             VALIDACAO CONCLUIDA!
echo                      O sistema esta pronto para uso.
echo ================================================================================
echo.
echo Proximos passos:
echo  1. Execute 'setup-banco.bat' para configurar o banco de dados (se ainda nao fez)
echo  2. Execute 'executar-apis.bat' para iniciar as APIs e acessar o sistema
echo.
echo Pressione qualquer tecla para sair...
pause > nul
