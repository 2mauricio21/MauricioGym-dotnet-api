using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class AdministradorPapelService : ServiceBase<AdministradorPapelValidator>, IAdministradorPapelService
    {
        private readonly IAdministradorPapelSqlServerRepository _administradorPapelRepository;
        private readonly IAdministradorSqlServerRepository _administradorRepository;
        private readonly IPapelSqlServerRepository _papelRepository;

        public AdministradorPapelService(
            IAdministradorPapelSqlServerRepository administradorPapelRepository,
            IAdministradorSqlServerRepository administradorRepository,
            IPapelSqlServerRepository papelRepository)
        {
            _administradorPapelRepository = administradorPapelRepository;
            _administradorRepository = administradorRepository;
            _papelRepository = papelRepository;
        }

        public async Task<IResultadoValidacao<AdministradorPapelEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorPapelEntity?>(validacao);

                var administradorPapel = await _administradorPapelRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<AdministradorPapelEntity?>(administradorPapel);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AdministradorPapelEntity?>(ex, "[AdministradorPapelService] - Ocorreu um erro ao obter associação administrador-papel.");
            }
        }

        public async Task<IResultadoValidacao<AdministradorPapelEntity?>> ObterPorAdministradorEPapelAsync(int administradorId, int papelId)
        {
            try
            {
                var validacao = Validator.ObterPorAdministradorEPapel(administradorId, papelId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorPapelEntity?>(validacao);

                var administradorPapel = await _administradorPapelRepository.ObterPorAdministradorEPapelAsync(administradorId, papelId);
                return new ResultadoValidacao<AdministradorPapelEntity?>(administradorPapel);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AdministradorPapelEntity?>(ex, "[AdministradorPapelService] - Ocorreu um erro ao obter associação administrador-papel.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AdministradorPapelEntity>>> ObterPorAdministradorIdAsync(int administradorId)
        {
            try
            {
                var validacao = Validator.ObterPorAdministradorId(administradorId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<AdministradorPapelEntity>>(validacao);

                var administradorPapeis = await _administradorPapelRepository.ObterPorAdministradorIdAsync(administradorId);
                return new ResultadoValidacao<IEnumerable<AdministradorPapelEntity>>(administradorPapeis);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AdministradorPapelEntity>>(ex, "[AdministradorPapelService] - Ocorreu um erro ao obter papéis do administrador.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AdministradorPapelEntity>>> ObterPorPapelIdAsync(int papelId)
        {
            try
            {
                var validacao = Validator.ObterPorPapelId(papelId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<AdministradorPapelEntity>>(validacao);

                var administradorPapeis = await _administradorPapelRepository.ObterPorPapelIdAsync(papelId);
                return new ResultadoValidacao<IEnumerable<AdministradorPapelEntity>>(administradorPapeis);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AdministradorPapelEntity>>(ex, "[AdministradorPapelService] - Ocorreu um erro ao obter administradores do papel.");
            }
        }

        public async Task<IResultadoValidacao<AdministradorPapelEntity>> IncluirAdministradorPapelAsync(AdministradorPapelEntity administradorPapel, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirAdministradorPapel(administradorPapel);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorPapelEntity>(validacao);

                // Verificar se administrador existe
                var administrador = await _administradorRepository.ObterPorIdAsync(administradorPapel.AdministradorId);
                if (administrador == null)
                    return new ResultadoValidacao<AdministradorPapelEntity>("Administrador não encontrado.");

                // Verificar se papel existe
                var papel = await _papelRepository.ObterPorIdAsync(administradorPapel.PapelId);
                if (papel == null)
                    return new ResultadoValidacao<AdministradorPapelEntity>("Papel não encontrado.");

                // Verificar se associação já existe
                var associacaoExistente = await _administradorPapelRepository.ObterPorAdministradorEPapelAsync(
                    administradorPapel.AdministradorId, administradorPapel.PapelId);
                if (associacaoExistente != null)
                    return new ResultadoValidacao<AdministradorPapelEntity>("Administrador já possui este papel.");

                administradorPapel.DataInclusao = DateTime.Now;

                var resultadoCriacao = await _administradorPapelRepository.CriarAsync(administradorPapel);

                return new ResultadoValidacao<AdministradorPapelEntity>(administradorPapel);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AdministradorPapelEntity>(ex, "[AdministradorPapelService] - Ocorreu um erro ao incluir associação administrador-papel.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirAdministradorPapelAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirAdministradorPapel(id);
                if (validacao.OcorreuErro)
                    return validacao;

                var administradorPapelExistente = await _administradorPapelRepository.ObterPorIdAsync(id);
                if (administradorPapelExistente == null)
                    return new ResultadoValidacao("Associação administrador-papel não encontrada.");

                await _administradorPapelRepository.RemoverLogicamenteAsync(id);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[AdministradorPapelService] - Ocorreu um erro ao excluir associação administrador-papel.");
            }
        }

        public async Task<IResultadoValidacao> RemoverPapelDoAdministradorAsync(int administradorId, int papelId, int idUsuario)
        {
            try
            {
                var validacao = Validator.RemoverPapelDoAdministrador(administradorId, papelId);
                if (validacao.OcorreuErro)
                    return validacao;

                var associacao = await _administradorPapelRepository.ObterPorAdministradorEPapelAsync(administradorId, papelId);
                if (associacao == null)
                    return new ResultadoValidacao("Associação não encontrada.");

                await _administradorPapelRepository.RemoverLogicamenteAsync(associacao.Id);
                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[AdministradorPapelService] - Ocorreu um erro ao remover papel do administrador.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AdministradorPossuiPapelAsync(int administradorId, int papelId)
        {
            try
            {
                var validacao = Validator.AdministradorPossuiPapel(administradorId, papelId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var possui = await _administradorPapelRepository.AdministradorPossuiPapelAsync(administradorId, papelId);
                return new ResultadoValidacao<bool>(possui);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AdministradorPapelService] - Ocorreu um erro ao verificar se administrador possui papel.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.Existe(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _administradorPapelRepository.ExisteAsync(id, 0); // PapelId não é usado neste contexto
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AdministradorPapelService] - Ocorreu um erro ao verificar existência da associação.");
            }
        }

        public async Task<IResultadoValidacao> IncluirAsync(AdministradorPapelEntity administradorPapel)
        {
            var validacao = Validator.ValidarInclusao(administradorPapel);

            if (validacao.OcorreuErro)
                return validacao;

            var existe = await _administradorPapelRepository.ExisteAsync(administradorPapel.AdministradorId, administradorPapel.PapelId);

            if (existe)
                return new ResultadoValidacao("Administrador já possui este papel");


            administradorPapel.DataInclusao = DateTime.Now;

            await _administradorPapelRepository.CriarAsync(administradorPapel);

            return new ResultadoValidacao();
        }
    }
}