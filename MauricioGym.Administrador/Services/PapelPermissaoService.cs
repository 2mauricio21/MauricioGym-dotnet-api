using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class PapelPermissaoService : ServiceBase<PapelPermissaoValidator>, IPapelPermissaoService
    {
        private readonly IPapelPermissaoSqlServerRepository _papelPermissaoRepository;
        private readonly IPapelSqlServerRepository _papelRepository;
        private readonly IPermissaoSqlServerRepository _permissaoRepository;

        public PapelPermissaoService(
            IPapelPermissaoSqlServerRepository papelPermissaoRepository,
            IPapelSqlServerRepository papelRepository,
            IPermissaoSqlServerRepository permissaoRepository)
        {
            _papelPermissaoRepository = papelPermissaoRepository;
            _papelRepository = papelRepository;
            _permissaoRepository = permissaoRepository;
        }

        public async Task<IResultadoValidacao<PapelPermissaoEntity>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PapelPermissaoEntity>(validacao);

                var papelPermissao = await _papelPermissaoRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PapelPermissaoEntity>(papelPermissao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PapelPermissaoEntity>(ex, "[PapelPermissaoService] - Ocorreu um erro ao obter associação papel-permissão.");
            }
        }

        public async Task<IResultadoValidacao<PapelPermissaoEntity>> ObterPorPapelPermissaoAsync(int papelId, int permissaoId)
        {
            try
            {
                var validacao = Validator.ObterPorPapelPermissao(papelId, permissaoId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PapelPermissaoEntity>(validacao);

                var papelPermissao = await _papelPermissaoRepository.ObterPorPapelPermissaoAsync(papelId, permissaoId);
                return new ResultadoValidacao<PapelPermissaoEntity>(papelPermissao);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PapelPermissaoEntity>(ex, "[PapelPermissaoService] - Ocorreu um erro ao obter associação papel-permissão.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PapelPermissaoEntity>>> ListarPorPapelAsync(int papelId)
        {
            try
            {
                var validacao = Validator.ListarPorPapel(papelId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PapelPermissaoEntity>>(validacao);

                var papelPermissoes = await _papelPermissaoRepository.ListarPorPapelAsync(papelId);
                return new ResultadoValidacao<IEnumerable<PapelPermissaoEntity>>(papelPermissoes);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PapelPermissaoEntity>>(ex, "[PapelPermissaoService] - Ocorreu um erro ao listar associações do papel.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PapelPermissaoEntity>>> ListarPorPermissaoAsync(int permissaoId)
        {
            try
            {
                var validacao = Validator.ListarPorPermissao(permissaoId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PapelPermissaoEntity>>(validacao);

                var papelPermissoes = await _papelPermissaoRepository.ListarPorPermissaoAsync(permissaoId);
                return new ResultadoValidacao<IEnumerable<PapelPermissaoEntity>>(papelPermissoes);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PapelPermissaoEntity>>(ex, "[PapelPermissaoService] - Ocorreu um erro ao listar associações da permissão.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ListarPermissoesDoPapelAsync(int papelId)
        {
            try
            {
                var validacao = Validator.ListarPorPapel(papelId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(validacao);

                var permissoes = await _papelPermissaoRepository.ListarPermissoesDoPapelAsync(papelId);
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(permissoes);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(ex, "[PapelPermissaoService] - Ocorreu um erro ao listar permissões do papel.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PapelEntity>>> ListarPapeisComPermissaoAsync(int permissaoId)
        {
            try
            {
                var validacao = Validator.ListarPorPermissao(permissaoId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PapelEntity>>(validacao);

                var papeis = await _papelPermissaoRepository.ListarPapeisComPermissaoAsync(permissaoId);
                return new ResultadoValidacao<IEnumerable<PapelEntity>>(papeis);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PapelEntity>>(ex, "[PapelPermissaoService] - Ocorreu um erro ao listar papéis com a permissão.");
            }
        }

        public async Task<IResultadoValidacao<int>> CriarAsync(PapelPermissaoEntity papelPermissao)
        {
            try
            {
                var validacao = Validator.IncluirPapelPermissao(papelPermissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se papel existe
                var papelExiste = await _papelRepository.ExisteAsync(papelPermissao.IdPapel);
                if (!papelExiste)
                    return new ResultadoValidacao<int>("Papel não encontrado.");

                // Verificar se permissão existe
                var permissaoExiste = await _permissaoRepository.ExisteAsync(papelPermissao.IdPermissao);
                if (!permissaoExiste)
                    return new ResultadoValidacao<int>("Permissão não encontrada.");

                // Verificar se associação já existe
                var associacaoExiste = await _papelPermissaoRepository.ExisteAsync(papelPermissao.IdPapel, papelPermissao.IdPermissao);
                if (associacaoExiste)
                    return new ResultadoValidacao<int>("Esta associação papel-permissão já existe.");

                papelPermissao.DataCriacao = DateTime.Now;
                papelPermissao.Ativo = true;

                var id = await _papelPermissaoRepository.CriarAsync(papelPermissao);

                return new ResultadoValidacao<int>(id);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<int>(ex, "[PapelPermissaoService] - Ocorreu um erro ao incluir associação papel-permissão.");
            }
        }

        public async Task<IResultadoValidacao> RemoverAsync(int id)
        {
            try
            {
                var validacao = Validator.RemoverPapelPermissao(id);
                if (validacao.OcorreuErro)
                    return validacao;

                var papelPermissao = await _papelPermissaoRepository.ObterPorIdAsync(id);
                if (papelPermissao == null)
                    return new ResultadoValidacao("Associação papel-permissão não encontrada.");

                var sucesso = await _papelPermissaoRepository.RemoverAsync(id);
                if (!sucesso)
                    return new ResultadoValidacao("Erro ao remover associação papel-permissão.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PapelPermissaoService] - Ocorreu um erro ao remover associação papel-permissão.");
            }
        }

        public async Task<IResultadoValidacao> RemoverPorPapelPermissaoAsync(int papelId, int permissaoId)
        {
            try
            {
                var validacao = Validator.RemoverPorPapelPermissao(papelId, permissaoId);
                if (validacao.OcorreuErro)
                    return validacao;

                var papelPermissao = await _papelPermissaoRepository.ObterPorPapelPermissaoAsync(papelId, permissaoId);
                if (papelPermissao == null)
                    return new ResultadoValidacao("Associação papel-permissão não encontrada.");

                var sucesso = await _papelPermissaoRepository.RemoverAsync(papelPermissao.Id);
                if (!sucesso)
                    return new ResultadoValidacao("Erro ao remover associação papel-permissão.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PapelPermissaoService] - Ocorreu um erro ao remover associação papel-permissão.");
            }
        }

        public async Task<IResultadoValidacao> AtribuirPermissaoAoPapelAsync(int papelId, int permissaoId)
        {
            try
            {
                var validacao = Validator.AtribuirPermissaoAoPapel(papelId, permissaoId);
                if (validacao.OcorreuErro)
                    return validacao;

                var papelPermissao = new PapelPermissaoEntity
                {
                    IdPapel = papelId,
                    IdPermissao = permissaoId,
                    DataCriacao = DateTime.Now,
                    Ativo = true
                };

                var id = await _papelPermissaoRepository.CriarAsync(papelPermissao);
                if (id <= 0)
                    return new ResultadoValidacao("Erro ao atribuir permissão ao papel.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PapelPermissaoService] - Ocorreu um erro ao atribuir permissão ao papel.");
            }
        }

        public async Task<IResultadoValidacao> RemoverPermissaoDoPapelAsync(int papelId, int permissaoId)
        {
            try
            {
                var validacao = Validator.RemoverPermissaoDoPapel(papelId, permissaoId);
                if (validacao.OcorreuErro)
                    return validacao;

                var papelPermissao = await _papelPermissaoRepository.ObterPorPapelPermissaoAsync(papelId, permissaoId);
                if (papelPermissao == null)
                    return new ResultadoValidacao("Associação papel-permissão não encontrada.");

                var sucesso = await _papelPermissaoRepository.RemoverAsync(papelPermissao.Id);
                if (!sucesso)
                    return new ResultadoValidacao("Erro ao remover permissão do papel.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PapelPermissaoService] - Ocorreu um erro ao remover permissão do papel.");
            }
        }
    }
}