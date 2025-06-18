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
    public class AcademiaService : ServiceBase<AcademiaValidator>, IAcademiaService
    {
        private readonly IAcademiaSqlServerRepository _academiaSqlServerRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public AcademiaService(
            IAcademiaSqlServerRepository academiaSqlServerRepository,
            ITransactionSqlServerRepository transaction)
        {
            _academiaSqlServerRepository = academiaSqlServerRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<AcademiaEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcademiaEntity?>(validacao);

                var academia = await _academiaSqlServerRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<AcademiaEntity?>(academia);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcademiaEntity?>(ex, "[AcademiaService] - Ocorreu um erro ao obter academia por ID.");
            }
        }

        public async Task<IResultadoValidacao<AcademiaEntity?>> ObterPorCnpjAsync(string cnpj)
        {
            try
            {
                var validacao = Validator.ValidarCnpj(cnpj);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcademiaEntity?>(validacao);

                var academia = await _academiaSqlServerRepository.ObterPorCnpjAsync(cnpj);
                return new ResultadoValidacao<AcademiaEntity?>(academia);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcademiaEntity?>(ex, "[AcademiaService] - Ocorreu um erro ao obter academia por CNPJ.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ObterTodosAsync()
        {
            try
            {
                var academias = await _academiaSqlServerRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(academias);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(ex, "[AcademiaService] - Ocorreu um erro ao obter todas as academias.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ObterAtivosAsync()
        {
            try
            {
                var academias = await _academiaSqlServerRepository.ObterAtivosAsync();
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(academias);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(ex, "[AcademiaService] - Ocorreu um erro ao obter academias ativas.");
            }
        }

        public async Task<IResultadoValidacao<int>> IncluirAcademiaAsync(AcademiaEntity academia, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirAcademia(academia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se já existe academia com o mesmo CNPJ
                var academiaExistente = await _academiaSqlServerRepository.ObterPorCnpjAsync(academia.Cnpj);
                if (academiaExistente != null)
                    return new ResultadoValidacao<int>("Já existe uma academia com este CNPJ."); await _transaction.BeginTransactionAsync();

                try
                {
                    academia.DataInclusao = DateTime.Now;
                    academia.UsuarioInclusao = idUsuario;
                    academia.Ativo = true;

                    var id = await _academiaSqlServerRepository.CriarAsync(academia);

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
                return new ResultadoValidacao<int>(ex, "[AcademiaService] - Ocorreu um erro ao incluir academia.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarAcademiaAsync(AcademiaEntity academia, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarAcademia(academia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se a academia existe
                var academiaExistente = await _academiaSqlServerRepository.ObterPorIdAsync(academia.Id);
                if (academiaExistente == null)
                    return new ResultadoValidacao<bool>("Academia não encontrada.");

                // Verificar se já existe outra academia com o mesmo CNPJ
                var existeCnpj = await _academiaSqlServerRepository.ExisteCnpjAsync(academia.Cnpj, academia.Id);
                if (existeCnpj)
                    return new ResultadoValidacao<bool>("Já existe outra academia com este CNPJ.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    academia.DataAlteracao = DateTime.Now;
                    academia.UsuarioAlteracao = idUsuario;

                    var sucesso = await _academiaSqlServerRepository.AtualizarAsync(academia);

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
                return new ResultadoValidacao<bool>(ex, "[AcademiaService] - Ocorreu um erro ao alterar academia.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirAcademiaAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se a academia existe
                var academia = await _academiaSqlServerRepository.ObterPorIdAsync(id);
                if (academia == null)
                    return new ResultadoValidacao("Academia não encontrada.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _academiaSqlServerRepository.RemoverAsync(id);
                    if (!sucesso)
                        return new ResultadoValidacao("Não foi possível excluir a academia.");

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
                    return new ResultadoValidacao("Não é possível excluir a academia por possuir registros associados.");

                return new ResultadoValidacao(ex, "[AcademiaService] - Ocorreu um erro ao excluir academia.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ValidarLicencaAcademiaAsync(int idAcademia)
        {
            try
            {
                var validacao = Validator.ValidarId(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var academia = await _academiaSqlServerRepository.ObterPorIdAsync(idAcademia);
                if (academia == null)
                    return new ResultadoValidacao<bool>("Academia não encontrada.");

                // Implementar lógica de validação de licença
                // Por enquanto, considera sempre válida se a academia existe e está ativa
                return new ResultadoValidacao<bool>(academia.Ativo);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AcademiaService] - Ocorreu um erro ao validar licença da academia.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _academiaSqlServerRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AcademiaService] - Ocorreu um erro ao verificar existência da academia.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteCnpjAsync(string cnpj, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ValidarCnpj(cnpj);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _academiaSqlServerRepository.ExisteCnpjAsync(cnpj, idExcluir);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AcademiaService] - Ocorreu um erro ao verificar existência do CNPJ.");
            }
        }
    }
}