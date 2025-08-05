using MauricioGym.Academia.Entities;
using MauricioGym.Academia.Repositories.SqlServer.Interfaces;
using MauricioGym.Academia.Services.Interfaces;
using MauricioGym.Academia.Services.Validators;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Academia.Services
{
    public class UsuarioAcademiaService : ServiceBase<UsuarioAcademiaValidator>, IUsuarioAcademiaService
    {
        private readonly IUsuarioAcademiaSqlServerRepository usuarioAcademiaSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public UsuarioAcademiaService(
            IUsuarioAcademiaSqlServerRepository usuarioAcademiaSqlServerRepository,
            IAuditoriaService auditoriaService)
        {
            this.usuarioAcademiaSqlServerRepository = usuarioAcademiaSqlServerRepository ?? throw new ArgumentNullException(nameof(usuarioAcademiaSqlServerRepository));
            this.auditoriaService = auditoriaService ?? throw new ArgumentNullException(nameof(auditoriaService));
        }

        public async Task<IResultadoValidacao<UsuarioAcademiaEntity>> IncluirUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirUsuarioAcademiaAsync(usuarioAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<UsuarioAcademiaEntity>(validacao);

                var result = await usuarioAcademiaSqlServerRepository.IncluirUsuarioAcademiaAsync(usuarioAcademia);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Vínculo usuário-academia criado - ID: {result.IdUsuarioAcademia}");

                return new ResultadoValidacao<UsuarioAcademiaEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<UsuarioAcademiaEntity>(ex, "[UsuarioAcademiaService] - Ocorreu erro ao tentar incluir o vínculo usuário-academia.");
            }
        }

        public async Task<IResultadoValidacao> AlterarUsuarioAcademiaAsync(UsuarioAcademiaEntity usuarioAcademia, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarUsuarioAcademiaAsync(usuarioAcademia);
                if (validacao.OcorreuErro)
                    return validacao;

                await usuarioAcademiaSqlServerRepository.AlterarUsuarioAcademiaAsync(usuarioAcademia);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Vínculo usuário-academia alterado - ID: {usuarioAcademia.IdUsuarioAcademia}");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[UsuarioAcademiaService] - Ocorreu erro ao tentar alterar o vínculo usuário-academia.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirUsuarioAcademiaAsync(int idUsuarioAcademia, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirUsuarioAcademiaAsync(idUsuarioAcademia);
                if (validacao.OcorreuErro)
                    return validacao;

                var usuarioAcademia = await usuarioAcademiaSqlServerRepository.ConsultarUsuarioAcademiaAsync(idUsuarioAcademia);
                if (usuarioAcademia == null)
                {
                    return new ResultadoValidacao("Vínculo usuário-academia não encontrado.");
                }

                await usuarioAcademiaSqlServerRepository.ExcluirUsuarioAcademiaAsync(idUsuarioAcademia);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Vínculo usuário-academia excluído - ID: {idUsuarioAcademia}");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[UsuarioAcademiaService] - Ocorreu erro ao tentar excluir o vínculo usuário-academia.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>> ListarUsuarioAcademiaAsync()
        {
            try
            {
                var result = await usuarioAcademiaSqlServerRepository.ListarUsuarioAcademiaAsync();
                return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(ex, "Ocorreu um erro ao listar os vínculos usuário-academia.");
            }
        }

        public async Task<IResultadoValidacao<UsuarioAcademiaEntity>> ConsultarUsuarioAcademiaAsync(int idUsuarioAcademia)
        {
            try
            {
                var validacao = Validator.ConsultarUsuarioAcademiaAsync(idUsuarioAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<UsuarioAcademiaEntity>(validacao);

                var result = await usuarioAcademiaSqlServerRepository.ConsultarUsuarioAcademiaAsync(idUsuarioAcademia);

                return new ResultadoValidacao<UsuarioAcademiaEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<UsuarioAcademiaEntity>(ex, "[UsuarioAcademiaService] - Ocorreu um erro ao tentar consultar o vínculo usuário-academia.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>> ListarUsuarioAcademiaPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var validacao = Validator.ListarUsuarioAcademiaPorUsuarioAsync(idUsuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(validacao);

                var result = await usuarioAcademiaSqlServerRepository.ListarUsuarioAcademiaPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(ex, "Ocorreu um erro ao listar os vínculos por usuário.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>> ListarUsuarioAcademiaPorAcademiaAsync(int idAcademia)
        {
            try
            {
                var validacao = Validator.ListarUsuarioAcademiaPorAcademiaAsync(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(validacao);

                var result = await usuarioAcademiaSqlServerRepository.ListarUsuarioAcademiaPorAcademiaAsync(idAcademia);
                return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioAcademiaEntity>>(ex, "Ocorreu um erro ao listar os vínculos por academia.");
            }
        }
    }
}