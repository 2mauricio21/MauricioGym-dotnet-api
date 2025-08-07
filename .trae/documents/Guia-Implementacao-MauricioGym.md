# Guia de Implementação - Sistema MauricioGym

**STATUS: ✅ SISTEMA COMPLETAMENTE IMPLEMENTADO E FUNCIONAL**

## 1. Estrutura do Projeto

### 1.1 Organização de Pastas

```
MauricioGym/
├── MauricioGym.Infra/                 # Infraestrutura base
│   ├── Entities/                     # Entidades base
│   ├── Repositories/                 # Repositórios base
│   ├── Services/                     # Serviços base
│   ├── Controllers/                  # Controllers base
│   └── Shared/                       # Utilitários compartilhados
├── MauricioGym.Usuario/              # Domínio de usuários
│   ├── Entities/                     # UsuarioEntity, RecursoEntity
│   ├── Repositories/SqlServer/       # Repositórios SQL Server
│   ├── Services/                     # Serviços de negócio
│   └── IServiceCollectionExtension.cs
├── MauricioGym.Usuario.Api/          # API de usuários
│   ├── Controllers/                  # UsuarioController
│   └── Program.cs
├── MauricioGym.Academia/             # Domínio de academias
├── MauricioGym.Academia.Api/         # API de academias
├── MauricioGym.Plano/               # Domínio de planos
├── MauricioGym.Plano.Api/           # API de planos
├── MauricioGym.Pagamento/           # Domínio de pagamentos
├── MauricioGym.Pagamento.Api/       # API de pagamentos
├── MauricioGym.Acesso/              # Domínio de controle de acesso
└── MauricioGym.Acesso.Api/          # API de controle de acesso
```

### 1.2 Padrões de Nomenclatura

- **Entidades**: `{Nome}Entity` (ex: UsuarioEntity, AcademiaEntity)
- **Repositórios**: `{Nome}SqlServerRepository` (ex: UsuarioSqlServerRepository)
- **Interfaces de Repositório**: `I{Nome}SqlServerRepository`
- **Serviços**: `{Nome}Service` (ex: UsuarioService, AcademiaService)
- **Interfaces de Serviço**: `I{Nome}Service`
- **Validadores**: `{Nome}Validator` (ex: UsuarioValidator)
- **Controllers**: `{Nome}Controller` (ex: UsuarioController)
- **Queries**: `{Nome}SqlServerQuery` (ex: UsuarioSqlServerQuery)

## 2. Implementação de Entidades

### 2.1 Padrão Base para Entidades

```csharp
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.{Dominio}.Entities
{
    public class {Nome}Entity : IEntity
    {
        public int Id{Nome} { get; set; }
        // Propriedades específicas da entidade
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
```

### 2.2 Exemplo: UsuarioEntity Completa

```csharp
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class UsuarioEntity : IEntity
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimoLogin { get; set; }
    }
}
```

## 3. Implementação de Repositórios

### 3.1 Interface do Repositório

```csharp
using MauricioGym.Infra.Repositories.Interfaces;
using MauricioGym.{Dominio}.Entities;

namespace MauricioGym.{Dominio}.Repositories.SqlServer.Interfaces
{
    public interface I{Nome}SqlServerRepository : ISqlServerRepository
    {
        Task<{Nome}Entity> Incluir{Nome}Async({Nome}Entity entity);
        Task<{Nome}Entity> Consultar{Nome}Async(int id);
        Task Alterar{Nome}Async({Nome}Entity entity);
        Task Excluir{Nome}Async(int id);
        Task<IEnumerable<{Nome}Entity>> Listar{Nome}sAsync();
        Task<IEnumerable<{Nome}Entity>> Listar{Nome}sAtivosAsync();
    }
}
```

### 3.2 Implementação do Repositório

```csharp
using Dapper;
using MauricioGym.Infra.Repositories.SQLServer.Abstracts;
using MauricioGym.Infra.SQLServer;
using MauricioGym.{Dominio}.Entities;
using MauricioGym.{Dominio}.Repositories.SqlServer.Interfaces;
using MauricioGym.{Dominio}.Repositories.SqlServer.Queries;

namespace MauricioGym.{Dominio}.Repositories.SqlServer
{
    public class {Nome}SqlServerRepository : SqlServerRepository, I{Nome}SqlServerRepository
    {
        public {Nome}SqlServerRepository(SQLServerDbContext sQLServerDbContext) : base(sQLServerDbContext)
        {
        }

        public async Task<{Nome}Entity> Incluir{Nome}Async({Nome}Entity entity)
        {
            entity.Id{Nome} = (await QueryAsync<int>({Nome}SqlServerQuery.Incluir{Nome}, entity)).Single();
            return entity;
        }

        public async Task<{Nome}Entity> Consultar{Nome}Async(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id{Nome}", id);
            
            var result = await QueryAsync<{Nome}Entity>({Nome}SqlServerQuery.Consultar{Nome}, p);
            return result.FirstOrDefault();
        }

        public async Task Alterar{Nome}Async({Nome}Entity entity)
        {
            await ExecuteNonQueryAsync({Nome}SqlServerQuery.Alterar{Nome}, entity);
        }

        public async Task Excluir{Nome}Async(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id{Nome}", id);
            
            await ExecuteNonQueryAsync({Nome}SqlServerQuery.Excluir{Nome}, p);
        }

        public async Task<IEnumerable<{Nome}Entity>> Listar{Nome}sAsync()
        {
            return await QueryAsync<{Nome}Entity>({Nome}SqlServerQuery.Listar{Nome}s);
        }

        public async Task<IEnumerable<{Nome}Entity>> Listar{Nome}sAtivosAsync()
        {
            return await QueryAsync<{Nome}Entity>({Nome}SqlServerQuery.Listar{Nome}sAtivos);
        }
    }
}
```

### 3.3 Queries SQL

```csharp
namespace MauricioGym.{Dominio}.Repositories.SqlServer.Queries
{
    public class {Nome}SqlServerQuery
    {
        public static string Incluir{Nome} => @"
            INSERT INTO {Tabela} (
                -- Listar colunas exceto ID e campos automáticos
            ) VALUES (
                -- Listar parâmetros correspondentes
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string Consultar{Nome} => @"
            SELECT * FROM {Tabela}
            WHERE Id{Nome} = @Id{Nome} AND Ativo = 1";

        public static string Alterar{Nome} => @"
            UPDATE {Tabela} SET
                -- Listar campos = @parametros
            WHERE Id{Nome} = @Id{Nome}";

        public static string Excluir{Nome} => @"
            UPDATE {Tabela} SET Ativo = 0 WHERE Id{Nome} = @Id{Nome}";

        public static string Listar{Nome}s => @"
            SELECT * FROM {Tabela}
            ORDER BY Nome";

        public static string Listar{Nome}sAtivos => @"
            SELECT * FROM {Tabela}
            WHERE Ativo = 1
            ORDER BY Nome";
    }
}
```

## 4. Implementação de Serviços

### 4.1 Interface do Serviço

```csharp
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.{Dominio}.Entities;
using MauricioGym.{Dominio}.Services.Validators;

namespace MauricioGym.{Dominio}.Services.Interfaces
{
    public interface I{Nome}Service : IService<{Nome}Validator>
    {
        Task<IResultadoValidacao<int>> Incluir{Nome}Async({Nome}Entity entity);
        Task<IResultadoValidacao<{Nome}Entity>> Consultar{Nome}Async(int id);
        Task<IResultadoValidacao> Alterar{Nome}Async({Nome}Entity entity);
        Task<IResultadoValidacao> Excluir{Nome}Async(int id);
        Task<IResultadoValidacao<IEnumerable<{Nome}Entity>>> Listar{Nome}sAsync();
        Task<IResultadoValidacao<IEnumerable<{Nome}Entity>>> Listar{Nome}sAtivosAsync();
    }
}
```

### 4.2 Implementação do Serviço

```csharp
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.{Dominio}.Entities;
using MauricioGym.{Dominio}.Repositories.SqlServer.Interfaces;
using MauricioGym.{Dominio}.Services.Interfaces;
using MauricioGym.{Dominio}.Services.Validators;

namespace MauricioGym.{Dominio}.Services
{
    public class {Nome}Service : ServiceBase<{Nome}Validator>, I{Nome}Service
    {
        private readonly I{Nome}SqlServerRepository {nome}Repository;
        private readonly IAuditoriaService auditoriaService;

        public {Nome}Service(
            I{Nome}SqlServerRepository {nome}Repository,
            IAuditoriaService auditoriaService)
        {
            this.{nome}Repository = {nome}Repository ?? throw new ArgumentNullException(nameof({nome}Repository));
            this.auditoriaService = auditoriaService ?? throw new ArgumentNullException(nameof(auditoriaService));
        }

        public async Task<IResultadoValidacao<int>> Incluir{Nome}Async({Nome}Entity entity)
        {
            try
            {
                var validacao = Validator.Incluir{Nome}(entity);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                entity.DataCadastro = DateTime.Now;
                entity.Ativo = true;
                
                var resultado = await {nome}Repository.Incluir{Nome}Async(entity);
                
                await auditoriaService.IncluirAuditoriaAsync(
                    resultado.Id{Nome}, 
                    $"{Nome} incluído: {resultado.Nome}");

                return new ResultadoValidacao<int>(resultado.Id{Nome});
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<int>(ex, $"[{Nome}Service] - Erro ao incluir {nome.ToLower()}");
            }
        }

        // Implementar outros métodos seguindo o mesmo padrão...
    }
}
```

## 5. Implementação de Validadores

### 5.1 Validador Base

```csharp
using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.{Dominio}.Entities;

namespace MauricioGym.{Dominio}.Services.Validators
{
    public class {Nome}Validator : ValidatorService
    {
        public IResultadoValidacao Incluir{Nome}({Nome}Entity entity)
        {
            if (entity == null)
                return new ResultadoValidacao("O {nome} não pode ser nulo.");

            // Validações específicas da entidade
            if (string.IsNullOrWhiteSpace(entity.Nome))
                return new ResultadoValidacao("O nome é obrigatório.");

            // Adicionar outras validações conforme necessário

            return new ResultadoValidacao();
        }

        public IResultadoValidacao Alterar{Nome}({Nome}Entity entity)
        {
            if (entity == null)
                return new ResultadoValidacao("O {nome} não pode ser nulo.");

            if (entity.Id{Nome} <= 0)
                return new ResultadoValidacao("ID do {nome} inválido.");

            return Incluir{Nome}(entity);
        }

        public IResultadoValidacao Consultar{Nome}(int id)
        {
            return ValidarId(id);
        }
    }
}
```

## 6. Implementação de Controllers

### 6.1 Controller Base

```csharp
using Microsoft.AspNetCore.Mvc;
using MauricioGym.Infra.Controller;
using MauricioGym.{Dominio}.Entities;
using MauricioGym.{Dominio}.Services.Interfaces;

namespace MauricioGym.{Dominio}.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class {Nome}Controller : ApiController
    {
        private readonly I{Nome}Service {nome}Service;

        public {Nome}Controller(I{Nome}Service {nome}Service)
        {
            this.{nome}Service = {nome}Service;
        }

        [HttpPost]
        public async Task<IActionResult> Incluir{Nome}([FromBody] {Nome}Entity entity)
        {
            var resultado = await {nome}Service.Incluir{Nome}Async(entity);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return CreatedAtAction(nameof(Consultar{Nome}), new { id = resultado.Retorno }, new { id = resultado.Retorno });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Consultar{Nome}(int id)
        {
            var resultado = await {nome}Service.Consultar{Nome}Async(id);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            if (resultado.Retorno == null)
                return NotFound();

            return Ok(resultado.Retorno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Alterar{Nome}(int id, [FromBody] {Nome}Entity entity)
        {
            if (id != entity.Id{Nome})
                return BadRequest("ID do {nome} não corresponde");

            var resultado = await {nome}Service.Alterar{Nome}Async(entity);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir{Nome}(int id)
        {
            var resultado = await {nome}Service.Excluir{Nome}Async(id);
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Listar{Nome}s()
        {
            var resultado = await {nome}Service.Listar{Nome}sAsync();
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return Ok(resultado.Retorno);
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> Listar{Nome}sAtivos()
        {
            var resultado = await {nome}Service.Listar{Nome}sAtivosAsync();
            
            if (resultado.OcorreuErro)
                return BadRequest(new { erro = resultado.MensagemErro });

            return Ok(resultado.Retorno);
        }
    }
}
```

## 7. Configuração de Injeção de Dependência

### 7.1 IServiceCollectionExtension

```csharp
using Microsoft.Extensions.DependencyInjection;
using MauricioGym.{Dominio}.Repositories.SqlServer;
using MauricioGym.{Dominio}.Repositories.SqlServer.Interfaces;
using MauricioGym.{Dominio}.Services;
using MauricioGym.{Dominio}.Services.Interfaces;

namespace MauricioGym.{Dominio}
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection Configure{Dominio}Services(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<I{Nome}SqlServerRepository, {Nome}SqlServerRepository>();
            
            // Services
            services.AddScoped<I{Nome}Service, {Nome}Service>();
            
            return services;
        }
    }
}
```

### 7.2 Program.cs da API

```csharp
using MauricioGym.Infra;
using MauricioGym.{Dominio};
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MauricioGym",
            ValidAudience = "MauricioGym",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua-chave-secreta-aqui"))
        };
    });

// Configure Infrastructure
builder.Services.ConfigureServicesInfra(builder.Configuration);

// Configure Domain Services
builder.Services.Configure{Dominio}Services();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

## 8. Checklist de Implementação

### 8.1 Para cada novo domínio:

- [ ] Criar projeto de domínio (`MauricioGym.{Dominio}`)
- [ ] Criar projeto de API (`MauricioGym.{Dominio}.Api`)
- [ ] Implementar entidades seguindo o padrão `{Nome}Entity`
- [ ] Implementar interfaces de repositório
- [ ] Implementar repositórios SQL Server
- [ ] Implementar queries SQL
- [ ] Implementar interfaces de serviço
- [ ] Implementar serviços de negócio
- [ ] Implementar validadores
- [ ] Implementar controllers
- [ ] Configurar injeção de dependência
- [ ] Configurar Program.cs
- [ ] Testar endpoints via Swagger
- [ ] Implementar testes unitários

### 8.2 Validações obrigatórias:

- [ ] Todas as entidades herdam de `IEntity`
- [ ] Todos os repositórios herdam de `SqlServerRepository`
- [ ] Todos os serviços herdam de `ServiceBase<TValidator>`
- [ ] Todos os controllers herdam de `ApiController`
- [ ] Auditoria implementada em operações críticas
- [ ] Validações de negócio implementadas
- [ ] Tratamento de exceções adequado
- [ ] Documentação Swagger configurada

Este guia garante a consistência e qualidade na implementação de todos os módulos do sistema MauricioGym.