using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace MauricioGym.Administrador.Services
{
    public class PermissaoService : ServiceBase<PermissaoValidator>, IPermissaoService
    {
        private readonly IPermissaoSqlServerRepository _permissaoRepository;
        private readonly ILogger<PermissaoService> _logger;

        public PermissaoService(
            IPermissaoSqlServerRepository permissaoRepository,
            ILogger<PermissaoService> logger)
        {
            _permissaoRepository = permissaoRepository;
            _logger = logger;
        }

        public async Task<IResultadoValidacao<PermissaoEntity>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PermissaoEntity>(validacao);

                var consulta = await _permissaoRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PermissaoEntity>(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter permissão por ID {Id}", id);
                return new ResultadoValidacao<PermissaoEntity>(ex, "Erro ao obter permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<PermissaoEntity>> ObterPorNomeAsync(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return new ResultadoValidacao<PermissaoEntity>("Nome da permissão não informado.");

                var consulta = await _permissaoRepository.ObterPorNomeAsync(nome);
                return new ResultadoValidacao<PermissaoEntity>(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter permissão por nome {Nome}", nome);
                return new ResultadoValidacao<PermissaoEntity>(ex, "Erro ao obter permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ObterTodosAsync()
        {
            try
            {
                var consulta = await _permissaoRepository.ListarAsync();
                if (consulta == null)
                    return new ResultadoValidacao<IEnumerable<PermissaoEntity>>("Nenhuma permissão encontrada.");

                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as permissões");
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(ex, "Erro ao obter permissões. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ObterPorPapelIdAsync(int papelId)
        {
            try
            {
                if (papelId <= 0)
                    return new ResultadoValidacao<IEnumerable<PermissaoEntity>>("ID do papel deve ser maior que zero.");

                var consulta = await _permissaoRepository.ObterPorPapelIdAsync(papelId);
                if (consulta == null)
                    return new ResultadoValidacao<IEnumerable<PermissaoEntity>>("Nenhuma permissão encontrada.");

                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter permissões do papel {PapelId}", papelId);
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(ex, "Erro ao obter permissões do papel. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ObterPorRecursoAsync(string recurso)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(recurso))
                    return new ResultadoValidacao<IEnumerable<PermissaoEntity>>("Recurso não informado.");

                var consulta = await _permissaoRepository.ObterPorRecursoAsync(recurso);
                if (consulta == null)
                    return new ResultadoValidacao<IEnumerable<PermissaoEntity>>("Nenhuma permissão encontrada para o recurso especificado.");

                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter permissões por recurso {Recurso}", recurso);
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(ex, "Erro ao obter permissões por recurso. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<PermissaoEntity>> IncluirPermissaoAsync(PermissaoEntity permissao, int idUsuario)
        {
            try
            {
                var validacao = Validator.Validar(permissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PermissaoEntity>(validacao);

                // Verificar se já existe permissão com o mesmo nome
                var existeNome = await _permissaoRepository.ExisteNomeAsync(permissao.Nome);
                if (existeNome)
                    return new ResultadoValidacao<PermissaoEntity>("Já existe uma permissão com este nome.");

                // Preparar dados
                permissao.DataInclusao = DateTime.Now;
                permissao.Ativo = true;

                // Criar permissão
                var resultado = await _permissaoRepository.CriarAsync(permissao);

                return new ResultadoValidacao<PermissaoEntity>(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao incluir permissão {Nome}", permissao?.Nome);
                return new ResultadoValidacao<PermissaoEntity>(ex, "Erro ao incluir permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<PermissaoEntity>> AlterarPermissaoAsync(PermissaoEntity permissao, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarAtualizacao(permissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PermissaoEntity>(validacao);

                // Verificar se permissão existe
                var permissaoExistente = await _permissaoRepository.ObterPorIdAsync(permissao.Id);
                if (permissaoExistente == null)
                    return new ResultadoValidacao<PermissaoEntity>("Permissão não encontrada.");

                // Verificar se já existe permissão com o mesmo nome (excluindo a atual)
                var existeNome = await _permissaoRepository.ExistePorNomeAsync(permissao.Nome, permissao.Id);
                if (existeNome)
                    return new ResultadoValidacao<PermissaoEntity>("Já existe uma permissão com este nome.");

                // Preparar dados
                permissao.DataAlteracao = DateTime.Now;

                // Atualizar permissão
                var sucesso = await _permissaoRepository.AtualizarAsync(permissao);
                if (sucesso.Id > 0)
                    return new ResultadoValidacao<PermissaoEntity>(sucesso);
                else
                    return new ResultadoValidacao<PermissaoEntity>("Erro ao atualizar permissão.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao alterar permissão {Id}", permissao?.Id);
                return new ResultadoValidacao<PermissaoEntity>(ex, "Erro ao alterar permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirPermissaoAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se permissão existe
                var permissaoExistente = await _permissaoRepository.ObterPorIdAsync(id);
                if (permissaoExistente == null)
                    return new ResultadoValidacao("Permissão não encontrada.");

                // Excluir permissão
                await _permissaoRepository.ExcluirAsync(id);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir permissão {Id}", id);
                return new ResultadoValidacao(ex, "Erro ao excluir permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ResultadoValidacao<bool>("ID deve ser maior que zero.");

                var consulta = await _permissaoRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(consulta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência da permissão {Id}", id);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência da permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteNomeAsync(string nome, int? idExcluir = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return new ResultadoValidacao<bool>("Nome da permissão não informado.");

                if (idExcluir.HasValue && idExcluir.Value <= 0)
                    return new ResultadoValidacao<bool>("ID para exclusão deve ser maior que zero.");

                bool existe;
                if (idExcluir.HasValue)
                {
                    existe = await _permissaoRepository.ExistePorNomeAsync(nome, idExcluir.Value);
                }
                else
                {
                    existe = await _permissaoRepository.ExisteNomeAsync(nome);
                }

                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar existência do nome da permissão {Nome}", nome);
                return new ResultadoValidacao<bool>(ex, "Erro ao verificar existência do nome da permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PermissaoEntity>>> ListarAsync()
        {
            try
            {
                var dados = await _permissaoRepository.ListarAsync();
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(dados);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar permissões");
                return new ResultadoValidacao<IEnumerable<PermissaoEntity>>(ex, "Erro ao listar permissões. Tente novamente mais tarde.");
            }
        }


        public async Task<IResultadoValidacao<int>> CriarAsync(PermissaoEntity permissao)
        {
            try
            {
                var validacao = Validator.Validar(permissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se já existe permissão com o mesmo nome
                var existeNome = await _permissaoRepository.ExisteNomeAsync(permissao.Nome);
                if (existeNome)
                    return new ResultadoValidacao<int>("Já existe uma permissão com este nome.");

                // Preparar dados
                permissao.DataInclusao = DateTime.Now;
                permissao.Ativo = true;

                // Criar permissão
                var id = await _permissaoRepository.CriarAsync(permissao);
                return new ResultadoValidacao<int>(id.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar permissão {Nome}", permissao?.Nome);
                return new ResultadoValidacao<int>(ex, "Erro ao criar permissão. Tente novamente mais tarde.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AtualizarAsync(PermissaoEntity permissao)
        {
            try
            {
                var validacao = Validator.ValidarAtualizacao(permissao);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se a permissão existe
                var permissaoExistente = await _permissaoRepository.ObterPorIdAsync(permissao.Id);
                if (permissaoExistente == null)
                    return new ResultadoValidacao<bool>("Permissão não encontrada.");

                // Verificar se já existe outra permissão com o mesmo nome
                var existeNome = await _permissaoRepository.ExistePorNomeAsync(permissao.Nome, permissao.Id);
                if (existeNome)
                    return new ResultadoValidacao<bool>("Já existe outra permissão com este nome.");

                // Preparar dados
                permissao.DataAlteracao = DateTime.Now;

                // Atualizar permissão
                var sucesso = await _permissaoRepository.AtualizarAsync(permissao);
                if (sucesso.Id > 0)
                    return new ResultadoValidacao<bool>(true);

                return new ResultadoValidacao<bool>("Erro ao atualizar permissão.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar permissão {Id}", permissao?.Id);
                return new ResultadoValidacao<bool>(ex, "Erro ao atualizar permissão. Tente novamente mais tarde.");
            }
        }

    }
}