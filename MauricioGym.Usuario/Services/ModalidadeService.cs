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
    public class ModalidadeService : ServiceBase<ModalidadeValidator>, IModalidadeService
    {
        private readonly IModalidadeSqlServerRepository _modalidadeSqlServerRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public ModalidadeService(
            IModalidadeSqlServerRepository modalidadeSqlServerRepository,
            ITransactionSqlServerRepository transaction)
        {
            _modalidadeSqlServerRepository = modalidadeSqlServerRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<ModalidadeEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<ModalidadeEntity?>(validacao);

                var modalidade = await _modalidadeSqlServerRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<ModalidadeEntity?>(modalidade);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<ModalidadeEntity?>(ex, "[ModalidadeService] - Ocorreu um erro ao obter modalidade por ID.");
            }
        }

        public async Task<IResultadoValidacao<ModalidadeEntity?>> ObterPorNomeAsync(string nome)
        {
            try
            {
                var validacao = Validator.ValidarNome(nome);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<ModalidadeEntity?>(validacao);

                var modalidade = await _modalidadeSqlServerRepository.ObterPorNomeAsync(nome);
                return new ResultadoValidacao<ModalidadeEntity?>(modalidade);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<ModalidadeEntity?>(ex, "[ModalidadeService] - Ocorreu um erro ao obter modalidade por nome.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<ModalidadeEntity>>> ObterTodosAsync()
        {
            try
            {
                var modalidades = await _modalidadeSqlServerRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<ModalidadeEntity>>(modalidades);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<ModalidadeEntity>>(ex, "[ModalidadeService] - Ocorreu um erro ao obter todas as modalidades.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<ModalidadeEntity>>> ObterPorAcademiaIdAsync(int academiaId)
        {
            try
            {
                var validacao = Validator.ValidarId(academiaId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<ModalidadeEntity>>(validacao);

                var modalidades = await _modalidadeSqlServerRepository.ObterPorAcademiaIdAsync(academiaId);
                return new ResultadoValidacao<IEnumerable<ModalidadeEntity>>(modalidades);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<ModalidadeEntity>>(ex, "[ModalidadeService] - Ocorreu um erro ao obter modalidades por academia.");
            }
        }

        public async Task<IResultadoValidacao<int>> IncluirModalidadeAsync(ModalidadeEntity modalidade, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirModalidade(modalidade);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se já existe modalidade com o mesmo nome
                var modalidadeExistente = await _modalidadeSqlServerRepository.ObterPorNomeAsync(modalidade.Nome);
                if (modalidadeExistente != null)
                    return new ResultadoValidacao<int>("Já existe uma modalidade com este nome.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    modalidade.DataInclusao = DateTime.Now;
                    modalidade.UsuarioInclusao = idUsuario;
                    modalidade.Ativo = true;

                    var id = await _modalidadeSqlServerRepository.CriarAsync(modalidade);

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
                return new ResultadoValidacao<int>(ex, "[ModalidadeService] - Ocorreu um erro ao incluir modalidade.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarModalidadeAsync(ModalidadeEntity modalidade, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarModalidade(modalidade);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se a modalidade existe
                var modalidadeExistente = await _modalidadeSqlServerRepository.ObterPorIdAsync(modalidade.Id);
                if (modalidadeExistente == null)
                    return new ResultadoValidacao<bool>("Modalidade não encontrada.");

                // Verificar se já existe outra modalidade com o mesmo nome
                var existeNome = await _modalidadeSqlServerRepository.ExisteNomeAsync(modalidade.Nome, modalidade.Id);
                if (existeNome)
                    return new ResultadoValidacao<bool>("Já existe outra modalidade com este nome.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    modalidade.DataAlteracao = DateTime.Now;
                    modalidade.UsuarioAlteracao = idUsuario;
                    modalidade.DataInclusao = modalidadeExistente.DataInclusao;
                    modalidade.UsuarioInclusao = modalidadeExistente.UsuarioInclusao;

                    var sucesso = await _modalidadeSqlServerRepository.AtualizarAsync(modalidade);

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
                return new ResultadoValidacao<bool>(ex, "[ModalidadeService] - Ocorreu um erro ao alterar modalidade.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirModalidadeAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se a modalidade existe
                var modalidade = await _modalidadeSqlServerRepository.ObterPorIdAsync(id);
                if (modalidade == null)
                    return new ResultadoValidacao("Modalidade não encontrada.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _modalidadeSqlServerRepository.RemoverAsync(id);
                    if (!sucesso)
                        return new ResultadoValidacao("Não foi possível excluir a modalidade.");

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
                    return new ResultadoValidacao("Não é possível excluir a modalidade por possuir registros associados.");

                return new ResultadoValidacao(ex, "[ModalidadeService] - Ocorreu um erro ao excluir modalidade.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _modalidadeSqlServerRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[ModalidadeService] - Ocorreu um erro ao verificar existência da modalidade.");
            }
        }

    }
}
