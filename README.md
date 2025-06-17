# MauricioGym

API de exemplo para gerenciamento de academia, seguindo padrões profissionais, nomes em português, integração com SQL Server, Dapper, Moq, Swagger e testes unitários.

## Como rodar o projeto

1. Configure o SQL Server e execute os scripts de criação de tabelas (ver pasta /sql).
2. Atualize a string de conexão em `appsettings.json`.
3. Execute o comando:
   ```bash
   dotnet build
   dotnet run --project MauricioGym.Api/src/MauricioGym.Api/MauricioGym.Api.csproj
   ```
4. Acesse a documentação Swagger em `http://localhost:5000/swagger` (ou porta configurada).

## Testes

Para rodar os testes:
```bash
dotnet test
```

## Estrutura
- MauricioGym (Class Library): entidades, serviços, repositórios, validações, etc.
- MauricioGym.Api: API AWS Lambda (.NET Core), controllers, configuração, documentação.

## GitHub
Repositório: https://github.com/2mauricio21/MauricioGym-dotnet-api.git

---

> Projeto criado para fins de estudo e demonstração profissional.
