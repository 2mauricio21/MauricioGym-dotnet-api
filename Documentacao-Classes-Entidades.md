# MauricioGym - Documentação das Classes e Entidades

## 1. MauricioGym.Usuario

### UsuarioEntity.cs
```csharp
using System;
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
        public DateTime DataNascimento { get; set; }
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

### RecursoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class RecursoEntity : IEntity
    {
        public int IdRecurso { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
```

### UsuarioRecursoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Usuario.Entities
{
    public class UsuarioRecursoEntity : IEntity
    {
        public int IdUsuarioRecurso { get; set; }
        public int IdUsuario { get; set; }
        public int IdRecurso { get; set; }
        public int? IdAcademia { get; set; } // Null para recursos globais
        public DateTime DataAtribuicao { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
    }
}
```

## 2. MauricioGym.Academia

### AcademiaEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Academia.Entities
{
    public class AcademiaEntity : IEntity
    {
        public int IdAcademia { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TimeSpan HorarioAbertura { get; set; }
        public TimeSpan HorarioFechamento { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
```

### UsuarioAcademiaEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Academia.Entities
{
    public class UsuarioAcademiaEntity : IEntity
    {
        public int IdUsuarioAcademia { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcademia { get; set; }
        public DateTime DataVinculo { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
    }
}
```

## 3. MauricioGym.Plano

### PlanoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Plano.Entities
{
    public class PlanoEntity : IEntity
    {
        public int IdPlano { get; set; }
        public int IdAcademia { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int DuracaoEmDias { get; set; }
        public bool PermiteAcessoTotal { get; set; } = true;
        public TimeSpan? HorarioInicioPermitido { get; set; }
        public TimeSpan? HorarioFimPermitido { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
```

### UsuarioPlanoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Plano.Entities
{
    public class UsuarioPlanoEntity : IEntity
    {
        public int IdUsuarioPlano { get; set; }
        public int IdUsuario { get; set; }
        public int IdPlano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal ValorPago { get; set; }
        public string StatusPlano { get; set; } = "Ativo"; // Ativo, Suspenso, Cancelado, Expirado
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
```

## 4. MauricioGym.Pagamento

### PagamentoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Pagamento.Entities
{
    public class PagamentoEntity : IEntity
    {
        public int IdPagamento { get; set; }
        public int IdUsuario { get; set; }
        public int IdUsuarioPlano { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string FormaPagamento { get; set; } = string.Empty; // Dinheiro, Cartão, PIX, etc.
        public string StatusPagamento { get; set; } = "Pendente"; // Pendente, Aprovado, Rejeitado, Cancelado
        public string TransacaoId { get; set; } = string.Empty; // ID do gateway de pagamento
        public string ObservacaoPagamento { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
```

### FormaPagamentoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Pagamento.Entities
{
    public class FormaPagamentoEntity : IEntity
    {
        public int IdFormaPagamento { get; set; }
        public int IdAcademia { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool AceitaParcelamento { get; set; } = false;
        public int MaximoParcelas { get; set; } = 1;
        public decimal? TaxaProcessamento { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
```

## 5. MauricioGym.Acesso

### AcessoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Acesso.Entities
{
    public class AcessoEntity : IEntity
    {
        public int IdAcesso { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcademia { get; set; }
        public DateTime DataHoraEntrada { get; set; } = DateTime.Now;
        public DateTime? DataHoraSaida { get; set; }
        public string TipoAcesso { get; set; } = string.Empty; // Normal, Visitante, Funcionario
        public string ObservacaoAcesso { get; set; } = string.Empty;
        public bool AcessoLiberado { get; set; } = true;
        public string MotivoNegacao { get; set; } = string.Empty;
    }
}
```

### BloqueioAcessoEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Acesso.Entities
{
    public class BloqueioAcessoEntity : IEntity
    {
        public int IdBloqueioAcesso { get; set; }
        public int IdUsuario { get; set; }
        public int IdAcademia { get; set; }
        public DateTime DataInicioBloqueio { get; set; } = DateTime.Now;
        public DateTime? DataFimBloqueio { get; set; }
        public string MotivoBloqueio { get; set; } = string.Empty;
        public string ObservacaoBloqueio { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public int IdUsuarioResponsavel { get; set; } // Quem aplicou o bloqueio
    }
}
```

## 6. MauricioGym.Auditoria (Infra)

### AuditoriaEntity.cs
```csharp
using System;
using MauricioGym.Infra.Entities.Interfaces;

namespace MauricioGym.Infra.Entities
{
    public class AuditoriaEntity : IEntity
    {
        public int IdAuditoria { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        
        public AuditoriaEntity()
        {
            Data = DateTime.Now;
        }
        
        public AuditoriaEntity(int idUsuario, string descricao) : this()
        {
            IdUsuario = idUsuario;
            Descricao = descricao;
        }
    }
}
```

## Recursos do Sistema

Os recursos definem as permissões no sistema. Exemplos de códigos de recursos:

### Recursos Globais
- `ADMIN_GLOBAL` - Administrador do sistema
- `CRIAR_ACADEMIA` - Criar novas academias
- `GERENCIAR_USUARIOS_GLOBAIS` - Gerenciar usuários em todas as academias

### Recursos por Academia
- `DONO_ACADEMIA` - Dono da academia
- `FUNCIONARIO` - Funcionário da academia
- `CLIENTE` - Cliente da academia
- `GERENCIAR_PLANOS` - Criar e editar planos
- `GERENCIAR_PAGAMENTOS` - Processar pagamentos
- `LIBERAR_ACESSO` - Liberar entrada de clientes
- `BLOQUEAR_ACESSO` - Bloquear acesso de clientes
- `VER_RELATORIOS` - Visualizar relatórios financeiros
- `CADASTRAR_FUNCIONARIOS` - Cadastrar novos funcionários

## Padrões de Implementação

### Interfaces de Repositório
Todos os repositórios devem implementar interfaces seguindo o padrão:
```csharp
public interface INomeRepository
{
    Task<NomeEntity> ObterPorIdAsync(int id);
    Task<IEnumerable<NomeEntity>> ListarAsync();
    Task<int> IncluirAsync(NomeEntity entity);
    Task<bool> AlterarAsync(NomeEntity entity);
    Task<bool> ExcluirAsync(int id);
}
```

### Interfaces de Serviço
Todos os serviços devem implementar interfaces seguindo o padrão:
```csharp
public interface INomeService
{
    Task<ResultadoOperacao<NomeEntity>> ObterPorIdAsync(int id);
    Task<ResultadoOperacao<IEnumerable<NomeEntity>>> ListarAsync();
    Task<ResultadoOperacao<int>> IncluirAsync(NomeEntity entity, int idUsuario);
    Task<ResultadoOperacao<bool>> AlterarAsync(NomeEntity entity, int idUsuario);
    Task<ResultadoOperacao<bool>> ExcluirAsync(int id, int idUsuario);
}
```

### Controllers
Todos os controllers devem herdar de ApiController e seguir o padrão estabelecido com métodos HTTP apropriados e tratamento de erros consistente.