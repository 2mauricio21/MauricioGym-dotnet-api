using MauricioGym.Infra.Repositories.SqlServer.Interfaces;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services
{
    public class PlanoService : ServiceBase<PlanoValidator>, IPlanoService
    {
        private readonly IPlanoSqlServerRepository _planoSqlServerRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public PlanoService(
            IPlanoSqlServerRepository planoSqlServerRepository,
            ITransactionSqlServerRepository transaction)
        {
            _planoSqlServerRepository = planoSqlServerRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<PlanoEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PlanoEntity?>(validacao);

                var plano = await _planoSqlServerRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PlanoEntity?>(plano);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PlanoEntity?>(ex, "[PlanoService] - Ocorreu um erro ao obter plano por ID.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ObterTodosAsync()
        {
            try
            {
                var planos = await _planoSqlServerRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(ex, "[PlanoService] - Ocorreu um erro ao obter todos os planos.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ObterPorAcademiaIdAsync(int academiaId)
        {
            try
            {
                var validacao = Validator.ValidarId(academiaId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PlanoEntity>>(validacao);

                var planos = await _planoSqlServerRepository.ObterPorAcademiaIdAsync(academiaId);
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoEntity>>(ex, "[PlanoService] - Ocorreu um erro ao obter planos por academia.");
            }
        }

        public async Task<IResultadoValidacao<int>> IncluirPlanoAsync(PlanoEntity plano, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirPlano(plano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se já existe plano com o mesmo nome
                var planoExistente = await _planoSqlServerRepository.ExisteNomeAsync(plano.Nome, null);
                if (planoExistente != null)
                    return new ResultadoValidacao<int>("Já existe um plano com este nome.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    plano.DataInclusao = DateTime.Now;
                    plano.UsuarioInclusao = idUsuario;
                    plano.Ativo = true;

                    var id = await _planoSqlServerRepository.CriarAsync(plano);

                    await _transaction.CommitAsync();
                    return new ResultadoValidacao<int>(id);
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<int>(ex, "[PlanoService] - Ocorreu um erro ao incluir plano.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarPlanoAsync(PlanoEntity plano, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarPlano(plano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o plano existe
                var planoExistente = await _planoSqlServerRepository.ObterPorIdAsync(plano.Id);
                if (planoExistente == null)
                    return new ResultadoValidacao<bool>("Plano não encontrado.");

                // Verificar se já existe outro plano com o mesmo nome
                var existeNome = await _planoSqlServerRepository.ExisteNomeAsync(plano.Nome, plano.Id);
                if (existeNome)
                    return new ResultadoValidacao<bool>("Já existe outro plano com este nome.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    plano.DataAlteracao = DateTime.Now;
                    plano.UsuarioAlteracao = idUsuario;
                    plano.DataInclusao = planoExistente.DataInclusao;
                    plano.UsuarioInclusao = planoExistente.UsuarioInclusao;

                    var sucesso = await _planoSqlServerRepository.AtualizarAsync(plano);

                    await _transaction.CommitAsync();
                    return new ResultadoValidacao<bool>(sucesso);
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoService] - Ocorreu um erro ao alterar plano.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirPlanoAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se o plano existe
                var plano = await _planoSqlServerRepository.ObterPorIdAsync(id);
                if (plano == null)
                    return new ResultadoValidacao("Plano não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _planoSqlServerRepository.ExcluirAsync(id);
                    if (!sucesso)
                        return new ResultadoValidacao("Não foi possível excluir o plano.");

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
                if (ex.Message.Contains("Delete statement conflicted with the Reference"))
                    return new ResultadoValidacao("Não é possível excluir o plano por possuir registros associados.");

                return new ResultadoValidacao(ex, "[PlanoService] - Ocorreu um erro ao excluir plano.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _planoSqlServerRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoService] - Ocorreu um erro ao verificar existência do plano.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteNomeAsync(string nome, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ValidarNome(nome);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _planoSqlServerRepository.ExisteNomeAsync(nome, idExcluir);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoService] - Ocorreu um erro ao verificar existência do nome.");
            }
        }
    }
}
