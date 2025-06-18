using MauricioGym.Administrador.Entities;
using MauricioGym.Administrador.Repositories.SqlServer.Interfaces;
using MauricioGym.Administrador.Services.Interfaces;
using MauricioGym.Administrador.Services.Validators;
using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Administrador.Services
{
    public class PapelService : ServiceBase<PapelValidator>, IPapelService
    {
        private readonly IPapelSqlServerRepository _papelRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public PapelService(
            IPapelSqlServerRepository papelRepository,
            ITransactionSqlServerRepository transaction)
        {
            _papelRepository = papelRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<PapelEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ObterPorId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PapelEntity?>(validacao);

                var papel = await _papelRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PapelEntity?>(papel);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PapelEntity?>(ex, "[PapelService] - Ocorreu um erro ao obter papel.");
            }
        }

        public async Task<IResultadoValidacao<PapelEntity?>> ObterPorNomeAsync(string nome)
        {
            try
            {
                var validacao = Validator.ObterPorNome(nome);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PapelEntity?>(validacao);

                var papel = await _papelRepository.ObterPorNomeAsync(nome);
                return new ResultadoValidacao<PapelEntity?>(papel);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PapelEntity?>(ex, "[PapelService] - Ocorreu um erro ao obter papel por nome.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PapelEntity>>> ObterTodosAsync()
        {
            try
            {
                var papeis = await _papelRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<PapelEntity>>(papeis);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PapelEntity>>(ex, "[PapelService] - Ocorreu um erro ao obter papéis.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PapelEntity>>> ObterPorAdministradorIdAsync(int administradorId)
        {
            try
            {
                var validacao = Validator.ObterPorAdministradorId(administradorId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PapelEntity>>(validacao);

                var papeis = await _papelRepository.ObterPorAdministradorIdAsync(administradorId);
                return new ResultadoValidacao<IEnumerable<PapelEntity>>(papeis);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PapelEntity>>(ex, "[PapelService] - Ocorreu um erro ao obter papéis do administrador.");
            }
        }

        public async Task<IResultadoValidacao<PapelEntity>> IncluirPapelAsync(PapelEntity papel, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirPapel(papel);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PapelEntity>(validacao);

                // Verificar se já existe papel com o mesmo nome
                var papelExistente = await _papelRepository.ObterPorNomeAsync(papel.Nome);
                if (papelExistente != null)
                    return new ResultadoValidacao<PapelEntity>("Já existe um papel com este nome.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    papel.DataInclusao = DateTime.Now;
                    papel.UsuarioInclusao = idUsuario;
                    papel.Ativo = true;

                    var id = await _papelRepository.CriarAsync(papel);
                    papel.Id = id.Id;

                    await _transaction.CommitAsync();
                    return new ResultadoValidacao<PapelEntity>(papel);
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PapelEntity>(ex, "[PapelService] - Ocorreu um erro ao incluir papel.");
            }
        }

        public async Task<IResultadoValidacao<PapelEntity>> AlterarPapelAsync(PapelEntity papel, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarPapel(papel);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PapelEntity>(validacao);

                // Verificar se o papel existe
                var papelExistente = await _papelRepository.ObterPorIdAsync(papel.Id);
                if (papelExistente == null)
                    return new ResultadoValidacao<PapelEntity>("Papel não encontrado.");

                // Verificar se já existe outro papel com o mesmo nome
                var existeNome = await _papelRepository.ExisteNomeAsync(papel.Nome);
                if (existeNome)
                    return new ResultadoValidacao<PapelEntity>("Já existe outro papel com este nome.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    papel.DataAlteracao = DateTime.Now;
                    papel.UsuarioAlteracao = idUsuario;

                    var sucesso = await _papelRepository.AtualizarAsync(papel);

                    if (sucesso == new PapelEntity())
                        return new ResultadoValidacao<PapelEntity>("Erro ao alterar papel.");

                    await _transaction.CommitAsync();
                    return new ResultadoValidacao<PapelEntity>(papel);
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PapelEntity>(ex, "[PapelService] - Ocorreu um erro ao alterar papel.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirPapelAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirPapel(id);
                if (validacao.OcorreuErro)
                    return validacao;

                var papelExistente = await _papelRepository.ObterPorIdAsync(id);
                if (papelExistente == null)
                    return new ResultadoValidacao("Papel não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    await _papelRepository.RemoverLogicamenteAsync(id);

                    await _transaction.CommitAsync();
                    return new ResultadoValidacao();
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[PapelService] - Ocorreu um erro ao excluir papel.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ExcluirPapel(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _papelRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PapelService] - Ocorreu um erro ao verificar existência do papel.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteNomeAsync(string nome, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ObterPorNome(nome);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _papelRepository.ExisteNomeAsync(nome);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PapelService] - Ocorreu um erro ao verificar existência do nome do papel.");
            }
        }
    }
}