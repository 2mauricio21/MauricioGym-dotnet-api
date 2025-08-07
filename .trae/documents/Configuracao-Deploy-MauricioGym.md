# Guia de Configuração e Deploy - Sistema MauricioGym

**STATUS: ✅ SISTEMA PRONTO PARA PRODUÇÃO**

## 1. Requisitos do Sistema

### 1.1 Requisitos de Hardware

#### Ambiente de Desenvolvimento
- **CPU**: Intel i5 ou AMD Ryzen 5 (mínimo)
- **RAM**: 8GB (mínimo), 16GB (recomendado)
- **Armazenamento**: 50GB livres em SSD
- **Rede**: Conexão estável à internet

#### Ambiente de Produção
- **CPU**: 4 cores (mínimo), 8 cores (recomendado)
- **RAM**: 16GB (mínimo), 32GB (recomendado)
- **Armazenamento**: 200GB SSD para aplicação + 500GB para banco de dados
- **Rede**: Banda larga dedicada com backup

### 1.2 Requisitos de Software

#### Desenvolvimento
- **Sistema Operacional**: Windows 10/11, macOS 12+, ou Ubuntu 20.04+
- **.NET**: .NET 8.0 SDK
- **IDE**: Visual Studio 2022 ou Visual Studio Code
- **Banco de Dados**: SQL Server 2019+ ou SQL Server Express
- **Git**: Para controle de versão

#### Produção
- **Sistema Operacional**: Windows Server 2019+ ou Ubuntu Server 20.04+
- **.NET Runtime**: .NET 8.0 Runtime
- **Web Server**: IIS (Windows) ou Nginx (Linux)
- **Banco de Dados**: SQL Server 2019+ (recomendado Standard Edition)

## 2. Configuração do Ambiente de Desenvolvimento

### 2.1 Instalação do .NET 8.0

```bash
# Windows (via winget)
winget install Microsoft.DotNet.SDK.8

# macOS (via Homebrew)
brew install --cask dotnet

# Ubuntu
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

### 2.2 Configuração do SQL Server

#### Windows (SQL Server Express)
```powershell
# Download e instalação via PowerShell
Invoke-WebRequest -Uri "https://go.microsoft.com/fwlink/?linkid=866658" -OutFile "SQLServer2022-SSEI-Expr.exe"
.\SQLServer2022-SSEI-Expr.exe
```

#### Linux (SQL Server em Docker)
```bash
# Executar SQL Server em container Docker
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuaSenhaForte123!" \
   -p 1433:1433 --name sqlserver --hostname sqlserver \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

### 2.3 Configuração do Projeto

#### 2.3.1 Clone do Repositório
```bash
git clone https://github.com/seu-usuario/MauricioGym.git
cd MauricioGym
```

#### 2.3.2 Restauração de Pacotes
```bash
# Restaurar pacotes NuGet para toda a solução
dotnet restore MauricioGym.sln
```

#### 2.3.3 Configuração de Connection Strings

**appsettings.Development.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MauricioGymDB;Trusted_Connection=true;TrustServerCertificate=true;",
    "SqlServerDb": {
      "MauricioGym": "Server=localhost;Database=MauricioGymDB;Trusted_Connection=true;TrustServerCertificate=true;"
    }
  },
  "JwtSettings": {
    "SecretKey": "sua-chave-secreta-desenvolvimento-muito-longa-e-segura",
    "Issuer": "MauricioGym",
    "Audience": "MauricioGym",
    "ExpirationHours": 24
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2.4 Criação do Banco de Dados

#### 2.4.1 Script de Criação
```sql
-- Criar banco de dados
CREATE DATABASE MauricioGymDB;
GO

USE MauricioGymDB;
GO

-- Executar scripts de criação de tabelas
-- (Usar os scripts do arquivo Scripts-Criacao-Tabelas-SQL.md)
```

#### 2.4.2 Execução via Command Line
```bash
# Executar scripts SQL
sqlcmd -S localhost -d MauricioGymDB -i "sql/create-tables.sql"
sqlcmd -S localhost -d MauricioGymDB -i "sql/initial-data.sql"
```

### 2.5 Execução do Projeto

```bash
# Executar API de Usuários
cd MauricioGym.Usuario.Api
dotnet run

# Executar outras APIs em terminais separados
cd ../MauricioGym.Academia.Api
dotnet run --urls="https://localhost:7002"

cd ../MauricioGym.Plano.Api
dotnet run --urls="https://localhost:7003"

# E assim por diante para cada API
```

## 3. Configuração de Produção

### 3.1 Preparação do Servidor

#### 3.1.1 Windows Server com IIS

```powershell
# Instalar IIS e ASP.NET Core Module
Enable-WindowsOptionalFeature -Online -FeatureName IIS-WebServerRole, IIS-WebServer, IIS-CommonHttpFeatures, IIS-HttpErrors, IIS-HttpLogging, IIS-RequestFiltering, IIS-StaticContent, IIS-DefaultDocument, IIS-DirectoryBrowsing, IIS-ASPNET45

# Instalar .NET 8.0 Hosting Bundle
Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/xxx/dotnet-hosting-8.0.x-win.exe" -OutFile "dotnet-hosting.exe"
.\dotnet-hosting.exe

# Reiniciar IIS
iisreset
```

#### 3.1.2 Ubuntu Server com Nginx

```bash
# Instalar .NET 8.0 Runtime
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y aspnetcore-runtime-8.0

# Instalar Nginx
sudo apt-get install -y nginx

# Configurar firewall
sudo ufw allow 'Nginx Full'
sudo ufw allow OpenSSH
sudo ufw enable
```

### 3.2 Configuração do Banco de Dados de Produção

#### 3.2.1 SQL Server em Windows

```sql
-- Configurações de produção
ALTER DATABASE MauricioGymDB SET RECOVERY FULL;
ALTER DATABASE MauricioGymDB SET AUTO_SHRINK OFF;
ALTER DATABASE MauricioGymDB SET AUTO_CREATE_STATISTICS ON;
ALTER DATABASE MauricioGymDB SET AUTO_UPDATE_STATISTICS ON;

-- Backup inicial
BACKUP DATABASE MauricioGymDB 
TO DISK = 'C:\Backups\MauricioGymDB_Initial.bak'
WITH FORMAT, INIT;
```

#### 3.2.2 Configuração de Usuários

```sql
-- Criar usuário para aplicação
CREATE LOGIN mauriciogym_app WITH PASSWORD = 'SenhaSeguraProducao123!';
CREATE USER mauriciogym_app FOR LOGIN mauriciogym_app;

-- Conceder permissões necessárias
ALTER ROLE db_datareader ADD MEMBER mauriciogym_app;
ALTER ROLE db_datawriter ADD MEMBER mauriciogym_app;
ALTER ROLE db_ddladmin ADD MEMBER mauriciogym_app;
```

### 3.3 Build e Deploy da Aplicação

#### 3.3.1 Build para Produção

```bash
# Build de todas as APIs
dotnet publish MauricioGym.Usuario.Api -c Release -o ./publish/usuario-api
dotnet publish MauricioGym.Academia.Api -c Release -o ./publish/academia-api
dotnet publish MauricioGym.Plano.Api -c Release -o ./publish/plano-api
dotnet publish MauricioGym.Pagamento.Api -c Release -o ./publish/pagamento-api
dotnet publish MauricioGym.Acesso.Api -c Release -o ./publish/acesso-api
```

#### 3.3.2 Configuração de Produção

**appsettings.Production.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=servidor-producao;Database=MauricioGymDB;User Id=mauriciogym_app;Password=SenhaSeguraProducao123!;TrustServerCertificate=true;",
    "SqlServerDb": {
      "MauricioGym": "Server=servidor-producao;Database=MauricioGymDB;User Id=mauriciogym_app;Password=SenhaSeguraProducao123!;TrustServerCertificate=true;"
    }
  },
  "JwtSettings": {
    "SecretKey": "chave-secreta-producao-extremamente-longa-e-complexa-com-256-bits",
    "Issuer": "MauricioGym",
    "Audience": "MauricioGym",
    "ExpirationHours": 8
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Error"
    }
  },
  "AllowedHosts": "mauriciogym.com,*.mauriciogym.com"
}
```

### 3.4 Configuração do IIS (Windows)

#### 3.4.1 Criação de Application Pools

```powershell
# Criar Application Pools para cada API
New-WebAppPool -Name "MauricioGym.Usuario.Api" -Force
New-WebAppPool -Name "MauricioGym.Academia.Api" -Force
New-WebAppPool -Name "MauricioGym.Plano.Api" -Force
New-WebAppPool -Name "MauricioGym.Pagamento.Api" -Force
New-WebAppPool -Name "MauricioGym.Acesso.Api" -Force

# Configurar .NET Core para os pools
Set-ItemProperty -Path "IIS:\AppPools\MauricioGym.Usuario.Api" -Name "managedRuntimeVersion" -Value ""
Set-ItemProperty -Path "IIS:\AppPools\MauricioGym.Academia.Api" -Name "managedRuntimeVersion" -Value ""
# Repetir para todos os pools
```

#### 3.4.2 Criação de Sites

```powershell
# Criar sites para cada API
New-Website -Name "MauricioGym.Usuario.Api" -Port 8001 -PhysicalPath "C:\inetpub\wwwroot\mauriciogym\usuario-api" -ApplicationPool "MauricioGym.Usuario.Api"
New-Website -Name "MauricioGym.Academia.Api" -Port 8002 -PhysicalPath "C:\inetpub\wwwroot\mauriciogym\academia-api" -ApplicationPool "MauricioGym.Academia.Api"
# Repetir para todas as APIs
```

### 3.5 Configuração do Nginx (Linux)

#### 3.5.1 Configuração do Reverse Proxy

**/etc/nginx/sites-available/mauriciogym**
```nginx
server {
    listen 80;
    server_name mauriciogym.com www.mauriciogym.com;
    return 301 https://$server_name$request_uri;
}

server {
    listen 443 ssl http2;
    server_name mauriciogym.com www.mauriciogym.com;

    ssl_certificate /etc/ssl/certs/mauriciogym.crt;
    ssl_certificate_key /etc/ssl/private/mauriciogym.key;

    # API de Usuários
    location /api/usuario {
        proxy_pass http://localhost:5001;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }

    # API de Academias
    location /api/academia {
        proxy_pass http://localhost:5002;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
        proxy_cache_bypass $http_upgrade;
    }

    # Repetir para outras APIs...
}
```

#### 3.5.2 Ativação da Configuração

```bash
# Ativar site
sudo ln -s /etc/nginx/sites-available/mauriciogym /etc/nginx/sites-enabled/

# Testar configuração
sudo nginx -t

# Reiniciar Nginx
sudo systemctl restart nginx
```

### 3.6 Configuração de Serviços Systemd (Linux)

#### 3.6.1 Serviço para API de Usuários

**/etc/systemd/system/mauriciogym-usuario.service**
```ini
[Unit]
Description=MauricioGym Usuario API
After=network.target

[Service]
Type=notify
ExecStart=/usr/bin/dotnet /var/www/mauriciogym/usuario-api/MauricioGym.Usuario.Api.dll
Restart=always
RestartSec=5
KillSignal=SIGINT
SyslogIdentifier=mauriciogym-usuario
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:5001
WorkingDirectory=/var/www/mauriciogym/usuario-api

[Install]
WantedBy=multi-user.target
```

#### 3.6.2 Ativação dos Serviços

```bash
# Recarregar systemd
sudo systemctl daemon-reload

# Ativar e iniciar serviços
sudo systemctl enable mauriciogym-usuario
sudo systemctl start mauriciogym-usuario

# Verificar status
sudo systemctl status mauriciogym-usuario
```

## 4. Monitoramento e Logs

### 4.1 Configuração de Logs

#### 4.1.1 Serilog (Recomendado)

```csharp
// Program.cs
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/mauriciogym-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341") // Se usar Seq
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
```

#### 4.1.2 Application Insights (Azure)

```csharp
// Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

### 4.2 Health Checks

```csharp
// Program.cs
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString)
    .AddCheck("self", () => HealthCheckResult.Healthy());

app.MapHealthChecks("/health");
```

### 4.3 Métricas de Performance

```bash
# Instalar ferramentas de monitoramento
# Prometheus + Grafana (Linux)
docker run -d -p 9090:9090 prom/prometheus
docker run -d -p 3000:3000 grafana/grafana

# Performance Monitor (Windows)
perfmon.exe
```

## 5. Backup e Recuperação

### 5.1 Backup Automático do Banco

```sql
-- Script de backup automático
DECLARE @BackupPath NVARCHAR(500) = 'C:\Backups\MauricioGym_' + FORMAT(GETDATE(), 'yyyyMMdd_HHmmss') + '.bak'

BACKUP DATABASE MauricioGymDB 
TO DISK = @BackupPath
WITH FORMAT, COMPRESSION, CHECKSUM;

-- Verificar backup
RESTORE VERIFYONLY FROM DISK = @BackupPath;
```

### 5.2 Agendamento de Backups

#### Windows (Task Scheduler)
```powershell
# Criar tarefa agendada
$Action = New-ScheduledTaskAction -Execute "sqlcmd" -Argument "-S localhost -d master -i C:\Scripts\backup-mauriciogym.sql"
$Trigger = New-ScheduledTaskTrigger -Daily -At "02:00AM"
$Settings = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries
Register-ScheduledTask -TaskName "MauricioGym Backup" -Action $Action -Trigger $Trigger -Settings $Settings
```

#### Linux (Cron)
```bash
# Adicionar ao crontab
echo "0 2 * * * /usr/bin/sqlcmd -S localhost -d master -i /scripts/backup-mauriciogym.sql" | crontab -
```

## 6. Segurança

### 6.1 Configurações de Segurança

```csharp
// Program.cs - Configurações de segurança
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 443;
});

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
    });
});
```

### 6.2 Certificados SSL

#### Let's Encrypt (Linux)
```bash
# Instalar Certbot
sudo apt-get install certbot python3-certbot-nginx

# Obter certificado
sudo certbot --nginx -d mauriciogym.com -d www.mauriciogym.com

# Renovação automática
echo "0 12 * * * /usr/bin/certbot renew --quiet" | sudo crontab -
```

## 7. Troubleshooting

### 7.1 Problemas Comuns

#### 7.1.1 Erro de Conexão com Banco
```bash
# Verificar conectividade
telnet servidor-banco 1433

# Testar conexão SQL
sqlcmd -S servidor-banco -U usuario -P senha -Q "SELECT 1"
```

#### 7.1.2 Erro 500 na API
```bash
# Verificar logs
tail -f /var/log/mauriciogym/usuario-api.log

# Verificar status do serviço
sudo systemctl status mauriciogym-usuario
```

#### 7.1.3 Problemas de Performance
```sql
-- Verificar queries lentas
SELECT TOP 10 
    total_elapsed_time/execution_count AS avg_elapsed_time,
    text
FROM sys.dm_exec_query_stats 
CROSS APPLY sys.dm_exec_sql_text(sql_handle)
ORDER BY avg_elapsed_time DESC;
```

### 7.2 Comandos Úteis

```bash
# Verificar portas em uso
netstat -tulpn | grep :5001

# Verificar uso de recursos
top -p $(pgrep dotnet)

# Verificar logs do sistema
journalctl -u mauriciogym-usuario -f

# Reiniciar todos os serviços
sudo systemctl restart mauriciogym-*
```

## 8. Checklist de Deploy

### 8.1 Pré-Deploy
- [ ] Backup do banco de dados atual
- [ ] Verificação de dependências
- [ ] Testes em ambiente de staging
- [ ] Validação de configurações
- [ ] Notificação da equipe

### 8.2 Deploy
- [ ] Parar serviços da aplicação
- [ ] Executar scripts de migração do banco
- [ ] Copiar novos arquivos da aplicação
- [ ] Atualizar configurações
- [ ] Iniciar serviços da aplicação
- [ ] Verificar health checks
- [ ] Testar funcionalidades críticas

### 8.3 Pós-Deploy
- [ ] Monitorar logs por 30 minutos
- [ ] Verificar métricas de performance
- [ ] Confirmar funcionamento de todas as APIs
- [ ] Notificar sucesso do deploy
- [ ] Documentar alterações realizadas

Este guia garante uma configuração robusta e segura do sistema MauricioGym em qualquer ambiente.