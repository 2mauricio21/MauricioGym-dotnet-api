# MauricioGym.Infra

Infraestrutura reutilizável e modular para o projeto MauricioGym, seguindo os padrões e boas práticas do Juris.Infra.

## 📋 Estrutura

```
MauricioGym.Infra/
├── Config/                    # Configurações da aplicação
├── Controller/                # Controller base para APIs
├── Database/                  # Contextos de banco de dados
├── Entities/                  # Entidades base e interfaces
├── Enums/                     # Enumerações compartilhadas
├── Interfaces/                # Interfaces principais
├── Repositories/              # Repositórios base
├── Services/                  # Services base e interfaces
├── Shared/                    # Classes compartilhadas (ResultadoValidacao)
└── AppConfiguration.cs        # Configuração principal
```

## 🏗️ Padrão de Arquitetura

### ServiceBase com Validator

O projeto implementa o padrão ServiceBase genérico com Validators, seguindo o modelo do Juris:

```csharp
public class UsuarioService : ServiceBase<UsuarioValidator>, IUsuarioService
{
    public async Task<IResultadoValidacao<int>> CriarAsync(UsuarioEntity usuario)
    {
        // Validação usando o Validator
        var validacao = Validator.CriarUsuario(usuario);
        if (validacao.OcorreuErro)
            return new ResultadoValidacao<int>(validacao);

        // Lógica de negócio
        var id = await _repository.CriarAsync(usuario);
        return new ResultadoValidacao<int>(id);
    }
}
```

### Validators

Cada domínio possui seus próprios validators que herdam de `ValidatorService`:

```csharp
public class UsuarioValidator : ValidatorService
{
    public IResultadoValidacao CriarUsuario(UsuarioEntity usuario)
    {
        if (usuario == null)
            return new ResultadoValidacao("O usuário não pode ser nulo.");

        if (string.IsNullOrWhiteSpace(usuario.Nome))
            return new ResultadoValidacao("O nome é obrigatório.");

        return new ResultadoValidacao(); // Sucesso
    }
}
```

### ResultadoValidacao

Todas as operações retornam `IResultadoValidacao` ou `IResultadoValidacao<T>`:

```csharp
// Sucesso
var resultado = new ResultadoValidacao<UsuarioEntity>(usuario);

// Erro
var resultado = new ResultadoValidacao<UsuarioEntity>("Usuário não encontrado");

// Verificação
if (resultado.OcorreuErro)
{
    // Tratar erro
    return BadRequest(resultado.MensagemErro);
}
```

## 🔧 Configuração

### 1. Configuração no Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Configurar infraestrutura
    services.ConfigurarInfraestrutura(connectionString);
    
    // Registrar services específicos
    services.AddScoped<IUsuarioService, UsuarioService>();
}
```

### 2. Controllers

Os controllers herdam de `ApiController` que possui método `ProcessarResultado()`:

```csharp
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioEntity>> ObterPorId(int id)
    {
        var resultado = await _usuarioService.ObterPorIdAsync(id);
        return ProcessarResultado(resultado);
    }
}
```

## 📦 Recursos Disponíveis

### Entidades Base
- `EntityBase`: Classe base com propriedades comuns (Id, DataCriacao, DataAlteracao, Ativo)
- `AuditoriaEntity`: Entidade para auditoria de operações
- `IEntity`: Interface base para todas as entidades

### Repositórios
- `SqlServerRepository`: Repositório base com Dapper
- `ITransactionSqlServerRepository`: Interface para transações
- Suporte a queries parametrizadas e transações

### Services
- `ServiceBase<TValidator>`: Classe base genérica para services
- `ValidatorService`: Classe base para validators
- `IAuditoriaService`: Interface para auditoria

### Utilitários
- `AppConfig`: Configurações centralizadas
- `SqlServerConnectionOptions`: Opções de conexão
- `StatusEnum` e `TipoUsuarioEnum`: Enumerações comuns

## 🎯 Vantagens

✅ **Padronização**: Mesmo padrão usado no Juris  
✅ **Reutilização**: Componentes modulares e reutilizáveis  
✅ **Validação**: Sistema de validação integrado  
✅ **Tratamento de Erros**: ResultadoValidacao unifica retornos  
✅ **Clean Code**: Seguindo princípios SOLID  
✅ **Testabilidade**: Fácil de testar e mockar  

## 📝 Exemplo Completo

### 1. Validator (MauricioGym.Usuario)
```csharp
public class UsuarioValidator : ValidatorService
{
    public IResultadoValidacao CriarUsuario(UsuarioEntity usuario)
    {
        if (usuario == null)
            return new ResultadoValidacao("O usuário não pode ser nulo.");
            
        if (string.IsNullOrWhiteSpace(usuario.Email))
            return new ResultadoValidacao("O email é obrigatório.");
            
        return new ResultadoValidacao();
    }
}
```

### 2. Service (MauricioGym.Usuario)
```csharp
public class UsuarioService : ServiceBase<UsuarioValidator>, IUsuarioService
{
    public async Task<IResultadoValidacao<int>> CriarAsync(UsuarioEntity usuario)
    {
        var validacao = Validator.CriarUsuario(usuario);
        if (validacao.OcorreuErro)
            return new ResultadoValidacao<int>(validacao);
            
        var id = await _repository.CriarAsync(usuario);
        return new ResultadoValidacao<int>(id);
    }
}
```

### 3. Controller (MauricioGym.Usuario.Api)
```csharp
public class UsuarioController : ApiController
{
    [HttpPost]
    public async Task<ActionResult<int>> Criar([FromBody] UsuarioEntity usuario)
    {
        var resultado = await _usuarioService.CriarAsync(usuario);
        if (resultado.OcorreuErro)
            return ProcessarResultado(resultado);
            
        return CreatedAtAction(nameof(ObterPorId), 
            new { id = resultado.Objeto }, resultado.Objeto);
    }
}
```

## 🚀 Integração

Esta infraestrutura está integrada nos projetos:
- **MauricioGym.Usuario** - Módulo de usuários
- **MauricioGym.Administrador** - Módulo administrativo

Seguindo o mesmo padrão de validação e tratamento de resultados em toda a aplicação.
