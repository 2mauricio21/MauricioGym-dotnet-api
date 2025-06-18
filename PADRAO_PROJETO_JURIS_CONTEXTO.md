# PADRÃO DE PROJETO JURIS - INSTRUÇÕES DE CONTEXTO

## ESTRUTURA DE PROJETO E ARQUITETURA DEFINIDA

### 1. ESTRUTURA DE CAMADAS (Clean Architecture)

**Juris.Infra** - Camada de Infraestrutura Compartilhada:
- `Entities/`: Entidades base e interfaces (IEntity, MongoDbEntity)
- `Repositories/`: Abstrações e implementações (SqlServer, MongoDB)
  - `SQLServer/Abstracts/SqlServerRepository.cs` - Classe base para repositórios SQL
  - `SQLServer/Interfaces/` - Interfaces dos repositórios
- `Services/`: Serviços base e compartilhados
- `Shared/`: Utilitários (ResultadoValidacao, Common)
- `Database/`: Contextos de banco (SQLServerDbContext, MongoDbContext)
- `Controller/ApiController.cs` - Controller base com autenticação

**Juris.Cadastro** - Domínio de Cadastro:
- `Entities/`: Entidades específicas do domínio (PessoaEntity, etc.)
- `Repositories/SqlServer/`: Repositórios SQL do domínio
  - `Interfaces/` - Interfaces específicas do domínio
  - `Queries/` - Queries SQL organizadas por entidade
- `Services/`: Serviços de negócio do domínio
  - `Interfaces/` - Interfaces dos serviços
  - `Validators/` - Validadores específicos
- `IServiceCollectionExtension.cs` - Registro de DI do domínio

**Juris.Cadastro.Api** - Camada de Apresentação:
- `Controllers/`: Controllers específicos do domínio
- `Startup.cs` - Configuração da API usando base KosmosStartupBase

### 2. PADRÕES DE IMPLEMENTAÇÃO OBRIGATÓRIOS

#### A. Entidades
```csharp
// Entidades implementam IEntity (da Juris.Infra)
public class PessoaEntity : IEntity
{
    public int IdPessoa { get; set; }
    public string NomePessoa { get; set; }
    // Propriedades de navegação como IEnumerable
    public IEnumerable<PessoaTelefoneEntity> Telefones { get; set; }
}
```

#### B. Repositórios
```csharp
// Herdam de SqlServerRepository e implementam interface específica
public class PessoaSqlServerRepository : SqlServerRepository, IPessoaSqlServerRepository
{
    public PessoaSqlServerRepository(SQLServerDbContext sqlServerDbContext)
        : base(sqlServerDbContext) { }
    
    // Métodos usam Dapper com queries da pasta Queries/
    public async Task<PessoaEntity> AlterarPessoaAsync(PessoaEntity pessoa)
    {
        await ExecuteNonQueryAsync(PessoaSqlServerQuery.AlterarPessoa, pessoa);
        return pessoa;
    }
}
```

#### C. Interfaces de Repositório
```csharp
// Herdam de ISqlServerRepository
public interface IPessoaSqlServerRepository : ISqlServerRepository
{
    Task<PessoaEntity> IncluirPessoaAsync(PessoaEntity pessoa);
    Task<PessoaEntity> AlterarPessoaAsync(PessoaEntity pessoa);
    // Sempre async, sempre retornam Task
}
```

#### D. Services
```csharp
// Herdam de ServiceBase<TValidator> e implementam interface
public class PessoaService : ServiceBase<PessoaValidator>, IPessoaService
{
    private readonly IPessoaSqlServerRepository pessoaSqlServerRepository;
    private readonly ITransactionSqlServerRepository transaction;
    
    public PessoaService(
        IPessoaSqlServerRepository pessoaSqlServerRepository,
        ITransactionSqlServerRepository transaction)
    {
        this.pessoaSqlServerRepository = pessoaSqlServerRepository;
        this.transaction = transaction;
    }
    
    // Métodos retornam IResultadoValidacao ou IResultadoValidacao<T>
    public async Task<IResultadoValidacao<PessoaEntity>> IncluirPessoaAsync(PessoaEntity pessoaEntity, int idUsuario)
    {
        // Implementação com transação e validação
    }
}
```

#### E. Interfaces de Service
```csharp
// Herdam de IService<TValidator>
public interface IPessoaService : IService<PessoaValidator>
{
    Task<IResultadoValidacao<PessoaEntity>> IncluirPessoaAsync(PessoaEntity pessoaEntity, int idUsuario);
    Task<IResultadoValidacao<PessoaEntity>> ConsultarPessoaAsync(int idPessoa, int? idEscritorio);
}
```

#### F. Queries
```csharp
// Classes estáticas com queries SQL organizadas
public static class PessoaSqlServerQuery
{
    public static string AlterarPessoa => @"
        UPDATE Pessoa 
        SET NomePessoa = @NomePessoa,
            Documento = @Documento
        WHERE IdPessoa = @IdPessoa";
}
```

#### G. Registro de DI
```csharp
public static class IServiceCollectionExtension
{
    public static IServiceCollection ConfigureServicesCadastro(this IServiceCollection services)
    {
        // Repositories
        services.AddTransient<IPessoaSqlServerRepository, PessoaSqlServerRepository>();
        
        // Services
        services.AddTransient<IPessoaService, PessoaService>();
        
        return services;
    }
}
```

#### H. Controllers
```csharp
// Herdam de ApiController (da Juris.Infra)
[Route("api/[controller]")]
public class PessoaController : ApiController
{
    private readonly IPessoaService pessoaService;
    
    public PessoaController(IPessoaService pessoaService)
    {
        this.pessoaService = pessoaService;
    }
    
    [HttpPost]
    public async Task<IActionResult> IncluirPessoa([FromBody] PessoaEntity pessoa)
    {
        var resultado = await pessoaService.IncluirPessoaAsync(pessoa, IdUsuario);
        return CreatedAtAction(nameof(IncluirPessoa), resultado);
    }
}
```

### 3. CONVENÇÕES DE NOMENCLATURA

- **Entidades**: `[Nome]Entity.cs` (ex: PessoaEntity, PessoaTelefoneEntity)
- **Repositórios**: `[Nome]SqlServerRepository.cs` (ex: PessoaSqlServerRepository)
- **Interfaces Repositório**: `I[Nome]SqlServerRepository.cs` (ex: IPessoaSqlServerRepository)
- **Services**: `[Nome]Service.cs` (ex: PessoaService)
- **Interfaces Service**: `I[Nome]Service.cs` (ex: IPessoaService)
- **Queries**: `[Nome]SqlServerQuery.cs` (ex: PessoaSqlServerQuery)
- **Validators**: `[Nome]Validator.cs` (ex: PessoaValidator)

### 4. ESTRUTURA DE PASTAS OBRIGATÓRIA

```
[Dominio]/
├── Entities/
├── Repositories/
│   └── SqlServer/
│       ├── Interfaces/
│       └── Queries/
├── Services/
│   ├── Interfaces/
│   └── Validators/
└── IServiceCollectionExtension.cs

[Dominio].Api/
├── Controllers/
├── Properties/
├── appsettings.json
├── appsettings.Development.json
└── Startup.cs
```

### 5. DEPENDÊNCIAS ESSENCIAIS

- **Dapper** para acesso a dados
- **Juris.Infra** como referência base
- **System.Data.SqlClient** para SQL Server
- **IResultadoValidacao** para retornos de métodos
- **SqlServerRepository** como classe base
- **ApiController** como controller base

### 6. REGRAS DE IMPLEMENTAÇÃO

1. **NUNCA** usar Entity Framework, sempre Dapper
2. **SEMPRE** implementar interfaces para repositórios e services
3. **SEMPRE** usar padrão async/await
4. **SEMPRE** usar transações para operações de escrita
5. **SEMPRE** retornar IResultadoValidacao nos services
6. **SEMPRE** herdar de classes base (SqlServerRepository, ServiceBase, ApiController)
7. **SEMPRE** organizar queries em classes estáticas separadas
8. **SEMPRE** usar SQLServerDbContext injetado nos repositórios
9. **SEMPRE** registrar dependências em IServiceCollectionExtension
10. **NUNCA** usar DTOs, trabalhar diretamente com entities

### 7. CONTROLE DE TRANSAÇÃO

```csharp
// Sempre usar ITransactionSqlServerRepository para transações
await transaction.BeginTransactionAsync();
try
{
    // Operações
    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

---

**IMPORTANTE**: Este padrão é OBRIGATÓRIO e deve ser seguido rigorosamente em todos os projetos MauricioGym. Qualquer desvio deve ser corrigido imediatamente.
