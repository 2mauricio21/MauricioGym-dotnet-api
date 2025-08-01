## base: Juris.Cadastro

### Informação:
- Abaixo é um arquivo que tem toda a base necessária e que deve ser seguido para implementação do projeto inteiro.
- todas entidades segue o padrão de NomeEntity
- todas entity herda de IEntity que está na infra.
- usamos o repository pattern (implementação com base a interface)
- controller é em outro padrão de projeto NomeBase.NomeLogica.API
- os retornos devem seguir o mesmo padrão...
- não Juris é apenas o nome de exemplo, deve ser o da sua solução

#### Aplicação toda abaixo
- \Juris.Cadastro\Entities\
``` C#
using Juris.Infra.Entities.Interfaces;

namespace Juris.Cadastro.Entities
{
    public class VaraEntity:IEntity
    {
        public int IdVara { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string JuizTitular { get; set; }

        public string JuizSubstituto { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }  

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Cep { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Site { get; set; }

        public string Diretor { get; set; }

        public int? IdEscritorio { get; set; }
    }
}
```
- \Juris.Cadastro\Repositories\SqlServer\Interfaces\IVaraSqlServerRepository.cs
```  C#
using Juris.Cadastro.Entities;
using Juris.Infra.Repositories.Interfaces;

namespace Juris.Cadastro.Repositories.SqlServer.Interfaces
{
    public interface IVaraSqlServerRepository : ISqlServerRepository
    {
        Task<VaraEntity> IncluirVaraAsync(VaraEntity vara);

        Task<VaraEntity> ConsultarVaraAsync(int idVara, int idEscritorio);

        Task AlterarVaraAsync(VaraEntity vara);

        Task ExcluirVaraAsync(int idVara, int idEscritorio);

        Task<IEnumerable<VaraEntity>> ListarVaraAsync(int idEscritorio);

        Task<VaraEntity> ConsultarNomeVaraAsync(string nomeVara, int idEscritorio);

    }
}
```
- \Juris.Cadastro\Repositories\SqlServer\Queries\VaraSqlServerQuery.cs
``` C#
namespace Juris.Cadastro.Repositories.SqlServer.Queries
{
    public class VaraSqlServerQuery
    {
        public static string IncluirVara => @"INSERT INTO VARA(
                                    [Nome], 
                                    [Descricao], 
                                    [JuizTitular], 
                                    [JuizSubstituto], 
                                    [Logradouro],
                                    [Numero], 
                                    [Complemento], 
                                    [Bairro],
                                    [Cidade], 
                                    [Uf],
                                    [Cep], 
                                    [Telefone], 
                                    [Email],
                                    [Site],
                                    [IdEscritorio],
                                    [Diretor])
                                  VALUES(@Nome,
                                         @Descricao,
                                         @JuizTitular,
                                         @JuizSubstituto,
                                         @Logradouro,
                                         @Numero,
                                         @Complemento, 
                                         @Bairro,
                                         @Cidade,
                                         @Uf, 
                                         @Cep,
                                         @Telefone, 
                                         @Email, 
                                         @Site,
                                         @IdEscritorio,
                                         @Diretor);
                                   SELECT CAST(SCOPE_IDENTITY() as int)";
        public static string AlterarVara => @"UPDATE VARA SET
                                                Nome = @Nome,
                                                Descricao = @Descricao,
                                                JuizTitular = @JuizTitular,
                                                JuizSubstituto = @JuizSubstituto,
                                                Logradouro = @Logradouro,
                                                Numero = @Numero,
                                                Complemento = @Complemento,
                                                Bairro = @Bairro,
                                                Cidade = @Cidade,
                                                Uf = @Uf,
                                                Cep = @Cep,
                                                Telefone = @Telefone,
                                                Email = @Email,
                                                Site = @Site,
                                                Diretor = @Diretor
                                            WHERE IdVara = @IdVara 
                                            AND (IdEscritorio = @IdEscritorio OR IdEscritorio is null)";

        public static string ConsultarVara => @"SELECT [IdVara],
                                        [Nome],
                                        [Descricao],
                                        [JuizTitular],
                                        [JuizSubstituto],
                                        [Logradouro],
                                        [Numero],
                                        [Complemento],
                                        [Bairro],
                                        [Cidade],
                                        [Uf],
                                        [Cep],
                                        [Telefone],
                                        [Email],
                                        [Site],
                                        [IdEscritorio],
                                        [Diretor]
                                      FROM [dbo].[VARA]
                                      WHERE IDVARA = @IDVARA
                                      AND (IdEscritorio = @IdEscritorio OR IdEscritorio is null)";

        public static string ExcluirVara => @"UPDATE VARA SET ATIVO = 0 WHERE IDVARA = @IDVARA AND (IdEscritorio = @IdEscritorio OR IdEscritorio is null)";

        public static string ListarVara = @"SELECT IdVara,
                                                   Nome,
                                                   Descricao,
                                                   JuizTitular,
                                                   JuizSubstituto,
                                                   Logradouro,
                                                   Numero,
                                                   Complemento,
                                                   Bairro,
                                                   Cidade,
                                                   UF,
                                                   CEP,
                                                   Telefone,
                                                   Email,
                                                   Site,
                                                   IdEscritorio,
                                                   Diretor
                                            FROM Vara
                                            WHERE ATIVO = 1 AND (IdEscritorio = @IdEscritorio OR IdEscritorio is null)
                                            ORDER BY NOME";

        public static string ConsultarNomeVara => @"SELECT IdVara,
                                                           Nome
                                                    FROM Vara
                                                    WHERE Nome = @Nome
                                                      AND (IdEscritorio = @IdEscritorio
                                                           OR IdEscritorio IS NULL)
                                                    ORDER BY Nome ASC";
    }

}
```
- \Juris.Cadastro\Repositories\SqlServer\VaraSqlServerRepository.cs
``` C#
using Dapper;
using Intellitouch.Infra.SQLServer;
using Juris.Cadastro.Entities;
using Juris.Cadastro.Repositories.SqlServer.Interfaces;
using Juris.Cadastro.Repositories.SqlServer.Queries;
using Juris.Infra.Repositories.SQLServer.Abstracts;

namespace Juris.Cadastro.Repositories.SqlServer
{
    public class VaraSqlServerRepository : SqlServerRepository, IVaraSqlServerRepository
    {
        public VaraSqlServerRepository(SQLServerDbContext sQLServerDbContex) : base(sQLServerDbContex)
        {

        }
        public async Task<VaraEntity> IncluirVaraAsync(VaraEntity vara)
        {
           vara.IdVara = (await QueryAsync<int>(VaraSqlServerQuery.IncluirVara, vara)).Single();
            return vara;
        }
        public async Task<VaraEntity> ConsultarVaraAsync(int idVara, int idEscritorio)
        {
            var p = new DynamicParameters();
            p.Add("@idVara", idVara);
            p.Add("@IdEscritorio", idEscritorio);

            var entity = await QueryAsync<VaraEntity>(VaraSqlServerQuery.ConsultarVara, p);
            return entity.FirstOrDefault();
        }
        public async Task AlterarVaraAsync(VaraEntity vara)
        {
            await ExecuteNonQueryAsync(VaraSqlServerQuery.AlterarVara, vara);

        }
        public async Task ExcluirVaraAsync(int idVara, int idEscritorio)
        {
            var p = new DynamicParameters();
            p.Add("@IdVara", idVara);
            p.Add("@IdEscritorio", idEscritorio);

            await ExecuteNonQueryAsync(VaraSqlServerQuery.ExcluirVara, p);

        }
        public async Task<IEnumerable<VaraEntity>> ListarVaraAsync(int idEscritorio)
        {
            var p = new DynamicParameters();
            p.Add("@IdEscritorio", idEscritorio);
            return await QueryAsync<VaraEntity>(VaraSqlServerQuery.ListarVara, p);
        }

        public async Task<VaraEntity> ConsultarNomeVaraAsync(string nomeVara, int idEscritorio)
        {
            var p = new DynamicParameters();
            p.Add("@Nome", nomeVara);
            p.Add("@IdEscritorio", idEscritorio);

            var consultarNomeVara = await QueryAsync<VaraEntity>(VaraSqlServerQuery.ConsultarNomeVara, p);
            return consultarNomeVara.FirstOrDefault();
        }
    }
}
```
- \Juris.Cadastro\Services\Interfaces\IVaraService.cs
``` C#
using Juris.Cadastro.Entities;
using Juris.Cadastro.Services.Validators;
using Juris.Infra.Services.Interfaces;
using Juris.Infra.Shared.Interfaces;

namespace Juris.Cadastro.Services.Interfaces
{
    public interface IVaraService : IService<VaraValidator>
    {
        Task<IResultadoValidacao<VaraEntity>> IncluirVaraAsync(VaraEntity vara, int idUsuario);

        Task<IResultadoValidacao<VaraEntity>> ConsultarVaraAsync(int idVara, int idEscritorio);

        Task<IResultadoValidacao> AlterarVaraAsync(VaraEntity vara, int idUsuario);

        Task<IResultadoValidacao> ExcluirVaraAsync(int idVara, int idEscritorio, int idUsuario);

        Task<IResultadoValidacao<IEnumerable<VaraEntity>>> ListarVaraAsync(int idEscritorio);

        Task<IResultadoValidacao<VaraEntity>> ConsultarNomeVaraAsync(string nomeVara, int idEscritorio);
    }
}
```
- \Juris.Cadastro\Services\Validators\VaraValidator.cs
``` C#
using Juris.Cadastro.Entities;
using Juris.Infra.Services.Validators;
using Juris.Infra.Shared;
using Juris.Infra.Shared.Interfaces;

namespace Juris.Cadastro.Services.Validators
{
    public class VaraValidator : ValidatorService
    {
        public IResultadoValidacao IncluirVaraAsync(VaraEntity vara)
        {
            if (vara == null)
                return new ResultadoValidacao("A vara não pode ser nula");

            if (string.IsNullOrEmpty(vara.Nome))
                return new ResultadoValidacao("O nome é obrigatório");

            return new ResultadoValidacao();
        }
        public IResultadoValidacao AlterarVaraAsync(VaraEntity vara)
        {
            if (vara == null)
                return new ResultadoValidacao("A vara não pode ser nula");

            if (string.IsNullOrEmpty(vara.Nome))
                return new ResultadoValidacao("O nome é obrigatório");

            if (vara.IdVara <= 0)
                return new ResultadoValidacao("O ID da vara deve ser maior que zero.");

            return ValidarEscritorio(vara.IdEscritorio);
        }
        public IResultadoValidacao ConsultarVaraAsync(int idVara, int idEscritorio)
        {
            if (idVara <= 0)
                return new ResultadoValidacao("O ID da vara deve ser maior que zero.");

            return ValidarEscritorio(idEscritorio);
        }
        public IResultadoValidacao ExcluirVaraAsync(int idVara, int idEscritorio)
        {
            if (idVara <= 0)
                return new ResultadoValidacao("O ID da vara deve ser maior que zero.");

            return ValidarEscritorio(idEscritorio);
        }

        public IResultadoValidacao ListarVara(int idEscritorio)
        {
            return ValidarEscritorio(idEscritorio);
        }

        internal IResultadoValidacao ConsultarNomeVara(string nomeVara, int idEscritorio)
        {
            if (string.IsNullOrWhiteSpace(nomeVara))
                return new ResultadoValidacao("O nome da vara é obrigatório.");

            return ValidarEscritorio(idEscritorio);
        }
    }
}
```
- \Juris.Cadastro\Services\VaraService.cs
``` C#
using Juris.Cadastro.Entities;
using Juris.Cadastro.Repositories.SqlServer.Interfaces;
using Juris.Cadastro.Services.Interfaces;
using Juris.Cadastro.Services.Validators;
using Juris.Infra.Entities;
using Juris.Infra.Services;
using Juris.Infra.Services.Interfaces;
using Juris.Infra.Shared;
using Juris.Infra.Shared.Interfaces;

namespace Juris.Cadastro.Services
{
    public class VaraService : ServiceBase<VaraValidator>, IVaraService
    {
        private readonly IVaraSqlServerRepository varaSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public VaraService(
            IVaraSqlServerRepository varaSqlServerRepository,
            IAuditoriaService auditoriaService
            )
        {
            this.varaSqlServerRepository = varaSqlServerRepository;
            this.auditoriaService = auditoriaService;
        }

        public async Task<IResultadoValidacao<VaraEntity>> IncluirVaraAsync(VaraEntity vara, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirVaraAsync(vara);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<VaraEntity>(validacao);

                vara.Cep = Common.LimparFormatacao(vara.Cep);
                vara.Telefone = Common.LimparFormatacao(vara.Telefone);
                var result = await varaSqlServerRepository.IncluirVaraAsync(vara);


                var incluirAuditoria = await auditoriaService.IncluirAuditoriaAsync(new AuditoriaEntity
                {
                    IdUsuario = idUsuario,
                    Data = DateTime.Now,
                    IdRecurso = 24,
                    TipoOperacao = "A Vara foi cadastrada.",
                }, vara);

                return new ResultadoValidacao<VaraEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<VaraEntity>(ex, "[VaraService]- Ocorreu erro ao tentar incluir a vara.");
            }
        }
        public async Task<IResultadoValidacao> AlterarVaraAsync(VaraEntity vara, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarVaraAsync(vara);
                if (validacao.OcorreuErro)
                    return (validacao);

                vara.Cep = Common.LimparFormatacao(vara.Cep);
                vara.Telefone = Common.LimparFormatacao(vara.Telefone);

                await varaSqlServerRepository.AlterarVaraAsync(vara);

                var incluirAuditoria = await auditoriaService.IncluirAuditoriaAsync(new AuditoriaEntity
                {
                    IdUsuario = idUsuario,
                    Data = DateTime.Now,
                    IdRecurso = 24,
                    TipoOperacao = "A Vara foi Alterada.",
                }, vara);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[VaraService]-Ocorreu erro ao tentar alterar a vara.");
            }
        }
        public async Task<IResultadoValidacao> ExcluirVaraAsync(int idVara, int idEscritorio, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirVaraAsync(idVara, idEscritorio);
                if (validacao.OcorreuErro)
                    return validacao;

                var vara = await varaSqlServerRepository.ConsultarVaraAsync(idVara, idEscritorio);
                if (vara == null)
                {
                    return new ResultadoValidacao("Vara não encontrada.");
                }


                await varaSqlServerRepository.ExcluirVaraAsync(idVara, idEscritorio);

                var incluirAuditoria = await auditoriaService.IncluirAuditoriaAsync(new AuditoriaEntity
                {
                    IdUsuario = idUsuario,
                    Data = DateTime.Now,
                    IdRecurso = 24,
                    TipoOperacao = "A Vara foi Excluída.",
                }, vara);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Delete statement conflicted with the REFERENCE"))
                    return new ResultadoValidacao("Não é possivél excluir o tipo de vara o mesmo está em uso por um outro produto.");

                return new ResultadoValidacao(ex, "[VaraService]-Ocorreu um erro ao tentar excluir a Vara.");
            }
        }
        public async Task<IResultadoValidacao<IEnumerable<VaraEntity>>> ListarVaraAsync(int idEscritorio)
        {
            try
            {
                var validacao = Validator.ListarVara(idEscritorio);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<VaraEntity>>(validacao);

                var result = await varaSqlServerRepository.ListarVaraAsync(idEscritorio);
                return new ResultadoValidacao<IEnumerable<VaraEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<VaraEntity>>(ex, "Ocorreu um erro ao listar a vara.");
            }
        }
        public async Task<IResultadoValidacao<VaraEntity>> ConsultarVaraAsync(int idVara, int idEscritorio)
        {
            try
            {
                var validacao = Validator.ConsultarVaraAsync(idVara, idEscritorio);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<VaraEntity>(validacao);

                var result = await varaSqlServerRepository.ConsultarVaraAsync(idVara, idEscritorio);

                return new ResultadoValidacao<VaraEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<VaraEntity>(ex, "[VaraService]-Ocorreu um erro ao tentar consultar a Vara.");
            }
        }

        public async Task<IResultadoValidacao<VaraEntity>> ConsultarNomeVaraAsync(string nomeVara, int idEscritorio)
        {
            try
            {
                var validacao = Validator.ConsultarNomeVara(nomeVara, idEscritorio);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<VaraEntity>(validacao);

                var result = await varaSqlServerRepository.ConsultarNomeVaraAsync(nomeVara, idEscritorio);
                return new ResultadoValidacao<VaraEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<VaraEntity>(ex, "[VaraService]-Ocorreu um erro ao tentar consultar a Vara.");
            }
        }
    }
}
```
- \Juris.Cadastro\IServiceCollectionExtension.cs
``` C#
using Dapper;
using Juris.Cadastro.Repositories.AWS.S3;
using Juris.Cadastro.Repositories.AWS.S3.Interfaces;
using Juris.Cadastro.Repositories.EmpresaAqui;
using Juris.Cadastro.Repositories.EmpresaAqui.Interfaces;
using Juris.Cadastro.Repositories.SqlServer;
using Juris.Cadastro.Repositories.SqlServer.Interfaces;
using Juris.Cadastro.Services;
using Juris.Cadastro.Services.Interfaces;
using Juris.Cadastro.Services.Searches;
using Juris.Cadastro.Services.Searches.Interfaces;
using Juris.Infra;
using Juris.Infra.Entities.Interfaces;
using Juris.Infra.Repositories.SqlServer;
using Juris.Infra.Repositories.SqlServer.Interfaces;
using Juris.Infra.Services;
using Juris.Infra.Services.Interfaces;
using Juris.Infra.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Juris.Cadastro
{
    public static class IServiceCollectionExtension
    {
        public static IConfiguration Configuration { get; }

        private static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration = null
        )
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Configuration
            if (configuration == null)
            {
                AppConfiguration.CreateInstance(configuration);
                _ = AppConfiguration.GetConfiguration();
            }

            // Services
            services.AddTransient<IBancoService, BancoService>();
            services.AddTransient<ICampoService, CampoService>();
            services.AddTransient<ICidadeService, CidadeService>();
            services.AddTransient<IEscritorioService, EscritorioService>();
            services.AddTransient<IEstadoCivilService, EstadoCivilService>();
            services.AddTransient<IUfService, UfService>();
            services.AddTransient<IEtiquetaService, EtiquetaService>();
            services.AddTransient<IFeriadoService, FeriadoService>();
            services.AddTransient<IFormaTratamentoService, FormaTratamentoService>();
            services.AddTransient<IForumService, ForumService>();
            services.AddTransient<IOnBoardingService, OnBoardingService>();
            services.AddTransient<IPessoaFisicaService, PessoaFisicaService>();
            services.AddTransient<IPessoaJuridicaService, PessoaJuridicaService>();
            services.AddTransient<IPessoaService, PessoaService>();
            services.AddTransient<IProfissaoService, ProfissaoService>();
            services.AddTransient<IQualificacaoProcessoService, QualificacaoProcessoService>();
            services.AddTransient<ITipoTrabalhoService, TipoTrabalhoService>();
            services.AddTransient<ITipoDocumentoService, TipoDocumentoService>();
            services.AddTransient<ITipoTribunalService, TipoTribunalService>();
            services.AddTransient<IUfService, UfService>();
            services.AddTransient<IVaraService, VaraService>();
            services.AddTransient<IAreaDireitoService, AreaDireitoService>();
            services.AddTransient<IPaisService, PaisService>();
            services.AddTransient<ITribunalService, TribunalService>();
            services.AddTransient<IPessoaSearchService, PessoaSearchService>();
            services.AddTransient<IUsuarioSearchService, UsuarioSearchService>();
            services.AddTransient<IEquipeService, EquipeService>();
            services.AddTransient<ITipoTimeSheetService, TipoTimeSheetService>();


            // Deve ser sempre o ultimo Search Service para evitar que ele seja chamado antes dos outros e trave a pesquisa generica
            services.AddTransient<ISearchService, SearchService>();
        }

        private static void ConfigureRepositories(IServiceCollection services, IConfiguration configuration = null)
        {
            // Configuration
            AppConfiguration.CreateInstance(configuration);

            services.AddTransient<IBancoSqlServerRepository, BancoSqlServerRepository>();
            services.AddTransient<ICampoSqlServerRepository, CampoSqlServerRepository>();
            services.AddTransient<ICidadeSqlServerRepository, CidadeSqlServerRepository>();
            services.AddTransient<IEscritorioSqlServerRepository, EscritorioSqlServerRepository>();
            services.AddTransient<IEscritorioS3Repository, EscritorioS3Repository>();
            services.AddTransient<IEstadoCivilSqlServerRepository, EstadoCivilSqlServerRepository>();
            services.AddTransient<IEtiquetaSqlServerRepository, EtiquetaSqlServerRepository>();
            services.AddTransient<IFeriadoSqlServerRepository, FeriadoSqlServerRepository>();
            services.AddTransient<IFormaTratamentoSqlServerRepository, FormaTratamentoSqlServerRepository>();
            services.AddTransient<IForumSqlServerRepository, ForumSqlServerRepository>();
            services.AddTransient<IOnBoardingSqlServerRepository, OnBoardingSqlServerRepository>();
            services.AddTransient<IPessoaFisicaSqlServerRepository, PessoaFisicaSqlServerRepository>();
            services.AddTransient<IPessoaJuridicaSqlServerRepository, PessoaJuridicaSqlServerRepository>();
            services.AddTransient<IPessoaSqlServerRepository, PessoaSqlServerRepository>();
            services.AddTransient<IProfissaoSqlServerRepository, ProfissaoSqlServerRepository>();
            services.AddTransient<IQualificacaoProcessoSqlServerRepository, QualificacaoProcessoSqlServerRepository>();
            services.AddTransient<ITipoTrabalhoSqlServerRepository, TipoTrabalhoSqlServerRepository>();
            services.AddTransient<ITipoDocumentoSqlServerRepository, TipoDocumentoSqlServerRepository>();
            services.AddTransient<ITipoTribunalSqlServerRepository, TipoTribunalSqlServerRepository>();
            services.AddTransient<IUfSqlServerRepository, UfSqlServerRepository>();
            services.AddTransient<IVaraSqlServerRepository, VaraSqlServerRepository>();
            services.AddTransient<IAreaDireitoSqlServerRepository, AreaDireitoSqlServerRepository>();
            services.AddTransient<IPaisSqlServerRepository, PaisSqlServerRepository>();
            services.AddTransient<IUsuarioSqlServerRepository, UsuarioSqlServerRepository>();
            services.AddTransient<IEmpresaAquiRepository, EmpresaAquiRepository>();
            services.AddTransient<ITribunalSqlServerRepository, TribunalSqlServerRepository>();
            services.AddTransient<ITipoTimeSheetSqlServerRepository, TipoTimeSheetSqlServerRepository>();
            services.AddTransient<IEquipeSqlServerRepository, EquipeSqlServerRepository>();
            services.AddTransient<IEquipeUsuarioSqlServerRepository, EquipeUsuarioSqlServerRepository>();
        }

        public static IServiceCollection ConfigureServicesCadastro(this IServiceCollection services, IConfiguration configuration = null)
        {
            //Registar tipos do SQL Server
            var types = FindAllImplementationsOfIEntity();

            foreach (var type in types)
                SqlMapper.SetTypeMap(type, new ColumnAttributeTypeMapper<IEntity>(type));

            // Services
            ConfigureServices(services, configuration);

            // Repositories
            ConfigureRepositories(services, configuration);

            return services;
        }

        public static List<Type> FindAllImplementationsOfIEntity()
        {
            var entityImplementations = new List<Type>();

            // Obtém o assembly atual ou você pode especificar um assembly diferente
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Percorre todos os tipos no assembly
            foreach (Type type in assembly.GetTypes())
            {
                // Verifica se o tipo é uma classe e implementa IEntity
                if (type.IsClass && typeof(IEntity).IsAssignableFrom(type))
                {
                    entityImplementations.Add(type);
                }
            }

            return entityImplementations;
        }
    }
}
```
- \Juris.Cadastro.Api\Controllers\VaraController.cs
``` C#
using Juris.Cadastro.Entities;
using Juris.Cadastro.Services.Interfaces;
using Juris.Infra.Controller;
using Microsoft.AspNetCore.Mvc;

namespace Juris.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class VaraController : ApiController
    {
        private readonly IVaraService varaService;

        public VaraController(IVaraService varaService)
        {
            this.varaService = varaService;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirAsync(VaraEntity vara)
        {
            vara.IdEscritorio = IdEscritorio;
            var incluir = await varaService.IncluirVaraAsync(vara, IdUsuario);
            if (incluir.OcorreuErro)
                return BadRequest(incluir.MensagemErro);

            return Ok(incluir.Retorno);
        }
        [HttpPut]
        public async Task<IActionResult> AlterarAsync(VaraEntity vara)
        {
            vara.IdEscritorio = IdEscritorio;
            var alterar = await varaService.AlterarVaraAsync(vara, IdUsuario);
            if (alterar.OcorreuErro)
                return BadRequest(alterar.MensagemErro);

            return Ok();

        }
        [HttpDelete("{idVara}")]

        public async Task<IActionResult> ExcluirAsync([FromRoute] int idVara)
        {
            var excluir = await varaService.ExcluirVaraAsync(idVara, IdEscritorio, IdUsuario);
            if (excluir.OcorreuErro)
                return BadRequest(excluir.MensagemErro);

            return Ok();
        }
        [HttpGet("{idVara}")]

        public async Task<IActionResult> ConsultarAsync([FromRoute] int idVara)
        {
            var consultar = await varaService.ConsultarVaraAsync(idVara, IdEscritorio);
            if (consultar.OcorreuErro)
                return BadRequest(consultar.MensagemErro);

            return Ok();
        }
        [HttpGet]

        public async Task<IActionResult> ListarAsync()
        {
            var listar = await varaService.ListarVaraAsync(IdEscritorio);
            if (listar.OcorreuErro)
                return BadRequest(listar.MensagemErro);

            return Ok(listar.Retorno);
        }
    }
}
```
- \Juris.Cadastro.Api\Startup.cs
``` C#
using Juris.Infra.Shared;
using Juris.Mensageria;
using Juris.Seguranca;
using Juris.Tesouraria;

namespace Juris.Cadastro.Api;

public class Startup : KosmosStartupBase
{
    public Startup(IConfiguration configuration) : base(configuration)
    {
        ApiTitle = "Kosmos - APIs de Cadastro";
        ApiVersion = "v1";
    }

    // This method gets called by the runtime. Use this method to add services to the container
    public override void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureServicesCadastro()
                .ConfigureServicesMensageria()
                .ConfigureServicesSeguranca()
                .ConfigureServicesTesouraria();
        base.ConfigureServices(services);
    }
}
```
