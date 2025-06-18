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
    public class AdministradorService : ServiceBase<AdministradorValidator>, IAdministradorService
    {
        private readonly IAdministradorSqlServerRepository _administradorSqlServerRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public AdministradorService(
            IAdministradorSqlServerRepository administradorSqlServerRepository,
            ITransactionSqlServerRepository transaction)
        {
            _administradorSqlServerRepository = administradorSqlServerRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<AdministradorEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorEntity?>(validacao);

                var administrador = await _administradorSqlServerRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<AdministradorEntity?>(administrador);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AdministradorEntity?>(ex, "[AdministradorService] - Ocorreu um erro ao obter administrador por ID.");
            }
        }

        public async Task<IResultadoValidacao<AdministradorEntity?>> ObterPorEmailAsync(string email)
        {
            try
            {
                var validacao = Validator.ValidarEmail(email);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorEntity?>(validacao);

                var administrador = await _administradorSqlServerRepository.ObterPorEmailAsync(email);
                return new ResultadoValidacao<AdministradorEntity?>(administrador);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AdministradorEntity?>(ex, "[AdministradorService] - Ocorreu um erro ao obter administrador por e-mail.");
            }
        }

        public async Task<IResultadoValidacao<AdministradorEntity?>> ObterPorCpfAsync(string cpf)
        {
            try
            {
                var validacao = Validator.ValidarCpf(cpf);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AdministradorEntity?>(validacao);

                var administrador = await _administradorSqlServerRepository.ObterPorCpfAsync(cpf);
                return new ResultadoValidacao<AdministradorEntity?>(administrador);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AdministradorEntity?>(ex, "[AdministradorService] - Ocorreu um erro ao obter administrador por CPF.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AdministradorEntity>>> ObterTodosAsync()
        {
            try
            {
                var administradores = await _administradorSqlServerRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<AdministradorEntity>>(administradores);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AdministradorEntity>>(ex, "[AdministradorService] - Ocorreu um erro ao obter todos os administradores.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AdministradorEntity>>> ObterAtivosAsync()
        {
            try
            {
                var administradores = await _administradorSqlServerRepository.ObterAtivosAsync();
                return new ResultadoValidacao<IEnumerable<AdministradorEntity>>(administradores);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AdministradorEntity>>(ex, "[AdministradorService] - Ocorreu um erro ao obter administradores ativos.");
            }
        }

        public async Task<IResultadoValidacao<int>> IncluirAdministradorAsync(AdministradorEntity administrador, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirAdministrador(administrador);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se já existe administrador com o mesmo e-mail
                var administradorExistente = await _administradorSqlServerRepository.ObterPorEmailAsync(administrador.Email);
                if (administradorExistente != null)
                    return new ResultadoValidacao<int>("Já existe um administrador com este e-mail.");

                // Verificar se já existe administrador com o mesmo CPF
                administradorExistente = await _administradorSqlServerRepository.ObterPorCpfAsync(administrador.Cpf);
                if (administradorExistente != null)
                    return new ResultadoValidacao<int>("Já existe um administrador com este CPF.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    administrador.DataInclusao = DateTime.Now;
                    administrador.UsuarioInclusao = idUsuario;
                    administrador.Ativo = true;

                    // Implementar hash de senha em produção
                    // administrador.Senha = HashHelper.HashPassword(administrador.Senha);

                    var id = await _administradorSqlServerRepository.CriarAsync(administrador);
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
                return new ResultadoValidacao<int>(ex, "[AdministradorService] - Ocorreu um erro ao incluir administrador.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarAdministradorAsync(AdministradorEntity administrador, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarAdministrador(administrador);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o administrador existe
                var administradorExistente = await _administradorSqlServerRepository.ObterPorIdAsync(administrador.Id);
                if (administradorExistente == null)
                    return new ResultadoValidacao<bool>("Administrador não encontrado.");

                // Verificar se já existe outro administrador com o mesmo e-mail
                var existeEmail = await _administradorSqlServerRepository.ExistePorEmailAsync(administrador.Email, administrador.Id);
                if (existeEmail)
                    return new ResultadoValidacao<bool>("Já existe outro administrador com este e-mail.");

                // Verificar se já existe outro administrador com o mesmo CPF
                var existeCpf = await _administradorSqlServerRepository.ExistePorCpfAsync(administrador.Cpf, administrador.Id);
                if (existeCpf)
                    return new ResultadoValidacao<bool>("Já existe outro administrador com este CPF.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    administrador.DataAlteracao = DateTime.Now;
                    administrador.UsuarioAlteracao = idUsuario;

                    // Se estiver alterando a senha, aplicar hash
                    if (!string.IsNullOrEmpty(administrador.Senha) && administrador.Senha != administradorExistente.Senha)
                    {
                        // Implementar hash de senha em produção
                        // administrador.Senha = HashHelper.HashPassword(administrador.Senha);
                    }
                    else
                    {
                        // Manter a senha atual se não for alterada
                        administrador.Senha = administradorExistente.Senha;
                    }

                    var sucesso = await _administradorSqlServerRepository.AtualizarAsync(administrador);

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
                return new ResultadoValidacao<bool>(ex, "[AdministradorService] - Ocorreu um erro ao alterar administrador.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirAdministradorAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se o administrador existe
                var administrador = await _administradorSqlServerRepository.ObterPorIdAsync(id);
                if (administrador == null)
                    return new ResultadoValidacao("Administrador não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _administradorSqlServerRepository.ExcluirAsync(id, idUsuario);
                    if (!sucesso)
                        return new ResultadoValidacao("Não foi possível excluir o administrador.");

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
                    return new ResultadoValidacao("Não é possível excluir o administrador por possuir registros associados.");

                return new ResultadoValidacao(ex, "[AdministradorService] - Ocorreu um erro ao excluir administrador.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _administradorSqlServerRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AdministradorService] - Ocorreu um erro ao verificar existência do administrador.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExistePorEmailAsync(string email, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ValidarEmail(email);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _administradorSqlServerRepository.ExistePorEmailAsync(email, idExcluir);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AdministradorService] - Ocorreu um erro ao verificar existência do e-mail.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExistePorCpfAsync(string cpf, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ValidarCpf(cpf);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _administradorSqlServerRepository.ExistePorCpfAsync(cpf, idExcluir);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AdministradorService] - Ocorreu um erro ao verificar existência do CPF.");
            }
        }
    }
}
