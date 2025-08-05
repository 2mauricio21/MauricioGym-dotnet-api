using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Usuario.Services
{
    public class RecursoService : ServiceBase<RecursoValidator>, IRecursoService
    {
        private readonly IRecursoSqlServerRepository recursoSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public RecursoService(
            IRecursoSqlServerRepository recursoSqlServerRepository,
            IAuditoriaService auditoriaService)
        {
            this.recursoSqlServerRepository = recursoSqlServerRepository ?? throw new ArgumentNullException(nameof(recursoSqlServerRepository));
            this.auditoriaService = auditoriaService ?? throw new ArgumentNullException(nameof(auditoriaService));
        }

        public async Task<IResultadoValidacao<RecursoEntity>> IncluirRecursoAsync(RecursoEntity recurso, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirRecursoAsync(recurso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<RecursoEntity>(validacao);

                var recursoExistente = await recursoSqlServerRepository.ConsultarRecursoPorCodigoAsync(recurso.Codigo);
                if (recursoExistente != null)
                    return new ResultadoValidacao<RecursoEntity>("Já existe um recurso com este código");

                var result = await recursoSqlServerRepository.IncluirRecursoAsync(recurso);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Recurso incluído - ID: {result.IdRecurso}");

                return new ResultadoValidacao<RecursoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<RecursoEntity>(ex, "[RecursoService] - Ocorreu um erro ao incluir o recurso");
            }
        }

        public async Task<IResultadoValidacao<RecursoEntity>> ConsultarRecursoAsync(int idRecurso)
        {
            try
            {
                var validacao = Validator.ConsultarRecursoAsync(idRecurso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<RecursoEntity>(validacao);

                var result = await recursoSqlServerRepository.ConsultarRecursoAsync(idRecurso);
                return new ResultadoValidacao<RecursoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<RecursoEntity>(ex, "[RecursoService] - Ocorreu um erro ao consultar o recurso");
            }
        }

        public async Task<IResultadoValidacao<RecursoEntity>> ConsultarRecursoPorCodigoAsync(string codigo)
        {
            try
            {
                var validacao = Validator.ConsultarRecursoPorCodigoAsync(codigo);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<RecursoEntity>(validacao);

                var result = await recursoSqlServerRepository.ConsultarRecursoPorCodigoAsync(codigo);
                return new ResultadoValidacao<RecursoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<RecursoEntity>(ex, "[RecursoService] - Ocorreu um erro ao consultar o recurso por código");
            }
        }

        public async Task<IResultadoValidacao> AlterarRecursoAsync(RecursoEntity recurso, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarRecursoAsync(recurso);
                if (validacao.OcorreuErro)
                    return validacao;

                var recursoExistente = await recursoSqlServerRepository.ConsultarRecursoPorCodigoAsync(recurso.Codigo);
                if (recursoExistente != null && recursoExistente.IdRecurso != recurso.IdRecurso)
                    return new ResultadoValidacao("Já existe outro recurso com este código");

                await recursoSqlServerRepository.AlterarRecursoAsync(recurso);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Recurso alterado - ID: {recurso.IdRecurso}");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[RecursoService] - Ocorreu um erro ao alterar o recurso");
            }
        }

        public async Task<IResultadoValidacao> ExcluirRecursoAsync(int idRecurso, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirRecursoAsync(idRecurso);
                if (validacao.OcorreuErro)
                    return validacao;

                var recurso = await recursoSqlServerRepository.ConsultarRecursoAsync(idRecurso);
                if (recurso == null)
                    return new ResultadoValidacao("Recurso não encontrado");

                await recursoSqlServerRepository.ExcluirRecursoAsync(idRecurso);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Recurso excluído - ID: {idRecurso}");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                    return new ResultadoValidacao("Não é possível excluir o recurso pois ele está sendo utilizado por outras entidades");

                return new ResultadoValidacao(ex, "[RecursoService] - Ocorreu um erro ao excluir o recurso");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<RecursoEntity>>> ListarRecursosAsync()
        {
            try
            {
                var validacao = Validator.ListarRecursosAsync();
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<RecursoEntity>>(validacao);

                var result = await recursoSqlServerRepository.ListarRecursosAsync();
                return new ResultadoValidacao<IEnumerable<RecursoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<RecursoEntity>>(ex, "[RecursoService] - Ocorreu um erro ao listar os recursos");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<RecursoEntity>>> ListarRecursosAtivosAsync()
        {
            try
            {
                var validacao = Validator.ListarRecursosAtivosAsync();
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<RecursoEntity>>(validacao);

                var result = await recursoSqlServerRepository.ListarRecursosAtivosAsync();
                return new ResultadoValidacao<IEnumerable<RecursoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<RecursoEntity>>(ex, "[RecursoService] - Ocorreu um erro ao listar os recursos ativos");
            }
        }
    }
}