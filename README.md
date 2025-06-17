# MauricioGym - Sistema de Academia

Sistema de gerenciamento de academia desenvolvido em .NET 8, com arquitetura limpa e testes unitários.

## 🏗️ Arquitetura

- **MauricioGym**: Biblioteca de classes principal (Services, Repositories, Entities, Validators)
- **MauricioGym.Api**: API Web local (.NET 8)
- **MauricioGym.Testes**: Testes unitários com xUnit e Moq

## 🚀 Como Executar

### Pré-requisitos
- .NET 8 SDK
- SQL Server (local ou remoto)

### 1. Clonar o Repositório
```bash
git clone <url-do-repositorio>
cd MauricioGym
```

### 2. Configurar String de Conexão
Edite o arquivo `MauricioGym.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=localhost;Database=MauricioGymDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### 3. Executar a API
```bash
dotnet run --project MauricioGym.Api
```

A API estará disponível em: `http://localhost:5000`

### 4. Executar os Testes
```bash
dotnet test
```

## 📋 Funcionalidades

- **Administradores**: Gerenciamento de usuários administrativos
- **Pessoas**: Cadastro de clientes da academia
- **Planos**: Gerenciamento de planos de assinatura
- **Mensalidades**: Controle de pagamentos
- **Check-ins**: Registro de entrada na academia
- **Caixa**: Controle financeiro

## 🛠️ Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **Dapper**: ORM para acesso aos dados
- **System.Data.SqlClient**: Driver SQL Server
- **FluentValidation**: Validação de dados
- **Swagger**: Documentação da API
- **xUnit**: Framework de testes
- **Moq**: Biblioteca de mocking

## 📁 Estrutura do Projeto

```
MauricioGym/
├── Controllers/          # Controllers Web API
├── Entities/            # Entidades do domínio
├── Repositories/        # Repositórios de dados
├── Services/           # Serviços de negócio
└── IServiceCollectionExtension.cs

MauricioGym.Api/
├── Controllers/        # Controllers da API
├── Program.cs         # Configuração da aplicação
└── appsettings.json   # Configurações

MauricioGym.Testes/
├── *Test.cs          # Testes unitários
└── MauricioGym.Testes.csproj
```

## 🔧 Comandos Úteis

### Build
```bash
dotnet build
```

### Executar Testes
```bash
dotnet test
```

### Executar API
```bash
dotnet run --project MauricioGym.Api
```

### Restore de Pacotes
```bash
dotnet restore
```

## 📊 Cobertura de Testes

O projeto possui **51 testes unitários** cobrindo:
- Controllers
- Services
- Validações
- Repositórios

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 🏃‍♂️ Execução Rápida

Para executar rapidamente:

```bash
# Restaurar, buildar e executar testes
dotnet restore && dotnet build && dotnet test

# Executar a API
dotnet run --project MauricioGym.Api
```

## 📝 Notas

- Este projeto roda 100% localmente
- Não possui dependências de AWS ou Azure
- Usar SQL Server local ou remoto conforme necessário
- Swagger disponível em `/swagger` quando executando a API

## 🏋️‍♂️ Dados de Exemplo

Execute o script `sql/script_completo_mauriciogym.sql` para criar:
- 1 administrador: admin@mauriciogym.com
- 5 alunos já cadastrados
- Planos (mensal, trimestral, anual)
- Mensalidades, check-ins e caixa populados

## GitHub
Repositório: https://github.com/2mauricio21/MauricioGym-dotnet-api.git

---

> Projeto 100% local, sem dependências de nuvem, pronto para desenvolvimento e estudo.
