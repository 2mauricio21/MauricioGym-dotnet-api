# MauricioGym.Usuario.API

API REST para gerenciamento de usuários do sistema MauricioGym.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- Dapper
- SQL Server
- Docker

## Endpoints

- `POST /api/usuario` - Incluir novo usuário
- `GET /api/usuario/{id}` - Consultar usuário por ID
- `GET /api/usuario/email/{email}` - Consultar usuário por email
- `GET /api/usuario/cpf/{cpf}` - Consultar usuário por CPF
- `PUT /api/usuario/{id}` - Alterar usuário
- `DELETE /api/usuario/{id}` - Excluir usuário
- `GET /api/usuario` - Listar todos os usuários
- `GET /api/usuario/ativos` - Listar usuários ativos
- `POST /api/usuario/login` - Realizar login

## Como Executar

### Desenvolvimento
```bash
dotnet run --project MauricioGym.Usuario.API
```

### Docker
```bash
docker build -t mauriciogym-usuario-api .
docker run -p 8080:8080 mauriciogym-usuario-api
```

## Configuração

Ajuste a connection string no arquivo `appsettings.json` para apontar para seu banco de dados.