@echo off
setlocal enabledelayedexpansion

echo ================================================================================
echo                    MAURICIO GYM - EXECUÇÃO DAS APIS
echo ================================================================================
echo.

REM Verificar se .NET está instalado
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo [ERRO] .NET SDK nao encontrado. Por favor, instale o .NET 8 SDK.
    echo Voce pode baixa-lo em: https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    pause
    exit /b 1
)

echo [1] Verificando .NET SDK...
dotnet --version | findstr /r "8\.[0-9]*\.[0-9]*" >nul
if %errorlevel% neq 0 (
    echo [ATENCAO] A versao do .NET SDK pode nao ser compativel com este projeto.
    echo Recomendamos o .NET 8.0 ou superior.
    echo.
    echo Sua versao atual:
    dotnet --version
    echo.
    echo Deseja continuar mesmo assim?
    choice /c SN /m "Digite S para continuar ou N para sair"
    if errorlevel 2 exit /b 1
) else (
    echo [OK] .NET SDK 8.x detectado.
)
echo.

echo [2] Testando Build da Solucao...
dotnet build MauricioGym.sln -c Release --nologo
if %errorlevel% neq 0 (
    echo [ERRO] Falha no build da solucao!
    echo Verifique os erros acima e corrija-os antes de continuar.
    echo.
    pause
    exit /b 1
)
echo [OK] Build da solucao concluido com sucesso!
echo.

echo [3] Executando Testes do Administrador...
dotnet test MauricioGym.Administrador.Testes\MauricioGym.Administrador.Testes.csproj --nologo --verbosity minimal
if %errorlevel% neq 0 (
    echo [ERRO] Falha nos testes do Administrador!
    echo.
    pause
    exit /b 1
)
echo [OK] Testes do Administrador passaram com sucesso!
echo.

echo [4] Executando Testes do Usuario...
dotnet test MauricioGym.Usuario.Testes\MauricioGym.Usuario.Testes.csproj --nologo --verbosity minimal
if %errorlevel% neq 0 (
    echo [ERRO] Falha nos testes do Usuario!
    echo.
    pause
    exit /b 1
)
echo [OK] Testes do Usuario passaram com sucesso!
echo.

echo ================================================================================
echo                              SISTEMA VERIFICADO!
echo               Todos os testes passaram e o sistema esta pronto.
echo ================================================================================
echo.
echo [5] APIS DISPONIVEIS:
echo.
echo  [A] API ADMINISTRADOR:
echo     URL: http://localhost:5001
echo     Swagger: http://localhost:5001/swagger
echo     Comando: dotnet run --project MauricioGym.Administrador.Api\MauricioGym.Administrador.Api.csproj
echo.
echo  [U] API USUARIO:
echo     URL: http://localhost:5002  
echo     Swagger: http://localhost:5002/swagger
echo     Comando: dotnet run --project MauricioGym.Usuario.Api\MauricioGym.Usuario.Api.csproj
echo.
echo ================================================================================
echo.
echo Escolha qual API deseja iniciar:
echo  [A] = API Administrador (porta 5001)
echo  [U] = API Usuario (porta 5002)
echo  [B] = Ambas as APIs (em janelas separadas)
echo  [N] = Nenhuma (sair)
echo.

choice /c AUBN /m "Sua escolha"

if errorlevel 4 (
    echo.
    echo Nenhuma API iniciada. Execute este script novamente quando desejar iniciar as APIs.
) else if errorlevel 3 (    echo.
    echo Iniciando ambas as APIs em janelas separadas...
    start "API Administrador" cmd /c "dotnet run --project MauricioGym.Administrador.Api\MauricioGym.Administrador.Api.csproj --environment Development & pause"
    start "API Usuario" cmd /c "dotnet run --project MauricioGym.Usuario.Api\MauricioGym.Usuario.Api.csproj --environment Development & pause"
    echo [OK] Ambas as APIs foram iniciadas!
    echo  - API Administrador: http://localhost:5001/swagger
    echo  - API Usuario: http://localhost:5002/swagger
) else if errorlevel 2 (    echo.
    echo Iniciando API Usuario...
    start "API Usuario" cmd /c "dotnet run --project MauricioGym.Usuario.Api\MauricioGym.Usuario.Api.csproj --environment Development & pause"
    echo [OK] API Usuario iniciada! Acesse: http://localhost:5002/swagger
) else (    echo.
    echo Iniciando API Administrador...
    start "API Administrador" cmd /c "dotnet run --project MauricioGym.Administrador.Api\MauricioGym.Administrador.Api.csproj --environment Development & pause"
    echo [OK] API Administrador iniciada! Acesse: http://localhost:5001/swagger
)

echo.
echo Pressione qualquer tecla para sair...
pause > nul
