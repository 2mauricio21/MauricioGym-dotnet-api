# MauricioGym - Setup Rápido

## 🚀 Início Rápido (3 passos)

### 1. Setup do Banco (Automático)
```bash
# Windows
setup-banco.bat

# Linux/Mac
sqlcmd -S "(localdb)\mssqllocaldb" -i "sql/setup_completo.sql"
```

### 2. Executar API
```bash
dotnet run --project MauricioGym.Api
```

### 3. Testar
- **Swagger**: http://localhost:5000/swagger
- **API**: http://localhost:5000

## 📊 Dados de Exemplo Inclusos

- **2 Administradores**: admin@mauriciogym.com, mauricio@mauriciogym.com
- **4 Planos**: Mensal (R$ 99,90), Trimestral (R$ 269,90), Semestral (R$ 499,90), Anual (R$ 999,90)
- **5 Pessoas**: João, Maria, Pedro, Ana, Carlos
- **5 Vínculos**: Cada pessoa com um plano diferente
- **3 Check-ins**: Registros de entrada recentes
- **5 Mensalidades**: Mix de pagas e pendentes

## 🧪 Endpoints para Testar

- `GET /api/Administrador` - Lista administradores
- `GET /api/Pessoa` - Lista pessoas/clientes
- `GET /api/Plano` - Lista planos disponíveis
- `GET /api/Mensalidade` - Lista mensalidades
- `GET /api/CheckIn` - Lista check-ins
- `GET /api/Caixa` - Resumo financeiro

## ⚡ Solução de Problemas

**Erro de conexão com banco?**
```bash
# Verificar LocalDB
sqllocaldb info

# Iniciar LocalDB se necessário
sqllocaldb start mssqllocaldb

# Recriar banco
sqlcmd -S "(localdb)\mssqllocaldb" -i "sql/setup_completo.sql"
```

**Testes falhando?**
```bash
dotnet test
```

**Swagger não carrega?**
- Verificar se está rodando em Development
- Acessar: http://localhost:5000/swagger (não https)
