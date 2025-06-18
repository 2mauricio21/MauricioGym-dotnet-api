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
    public class ClienteService : ServiceBase<ClienteValidator>, IClienteService
    {
        private readonly IClienteSqlServerRepository _clienteSqlServerRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public ClienteService(
            IClienteSqlServerRepository clienteSqlServerRepository,
            ITransactionSqlServerRepository transaction)
        {
            _clienteSqlServerRepository = clienteSqlServerRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<ClienteEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<ClienteEntity?>(validacao);

                var cliente = await _clienteSqlServerRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<ClienteEntity?>(cliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<ClienteEntity?>(ex, "[ClienteService] - Ocorreu um erro ao obter cliente por ID.");
            }
        }

        public async Task<IResultadoValidacao<ClienteEntity?>> ObterPorCpfAsync(string cpf)
        {
            try
            {
                var validacao = Validator.ValidarCpf(cpf);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<ClienteEntity?>(validacao);

                var cliente = await _clienteSqlServerRepository.ObterPorCpfAsync(cpf);
                return new ResultadoValidacao<ClienteEntity?>(cliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<ClienteEntity?>(ex, "[ClienteService] - Ocorreu um erro ao obter cliente por CPF.");
            }
        }

        public async Task<IResultadoValidacao<ClienteEntity?>> ObterPorEmailAsync(string email)
        {
            try
            {
                var validacao = Validator.ValidarEmail(email);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<ClienteEntity?>(validacao);

                var cliente = await _clienteSqlServerRepository.ObterPorEmailAsync(email);
                return new ResultadoValidacao<ClienteEntity?>(cliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<ClienteEntity?>(ex, "[ClienteService] - Ocorreu um erro ao obter cliente por email.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<ClienteEntity>>> ObterTodosAsync()
        {
            try
            {
                var clientes = await _clienteSqlServerRepository.ObterTodosAsync();
                return new ResultadoValidacao<IEnumerable<ClienteEntity>>(clientes);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<ClienteEntity>>(ex, "[ClienteService] - Ocorreu um erro ao obter todos os clientes.");
            }
        }

        public async Task<IResultadoValidacao<int>> IncluirClienteAsync(ClienteEntity cliente, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirCliente(cliente);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se já existe cliente com o mesmo CPF
                var clienteExistente = await _clienteSqlServerRepository.ObterPorCpfAsync(cliente.Cpf);
                if (clienteExistente != null)
                    return new ResultadoValidacao<int>("Já existe um cliente com este CPF.");

                // Verificar se já existe cliente com o mesmo e-mail
                var clienteEmailExistente = await _clienteSqlServerRepository.ObterPorEmailAsync(cliente.Email);
                if (clienteEmailExistente != null)
                    return new ResultadoValidacao<int>("Já existe um cliente com este e-mail.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    cliente.DataInclusao = DateTime.Now;
                    cliente.UsuarioInclusao = idUsuario;
                    cliente.Ativo = true;

                    var id = await _clienteSqlServerRepository.CriarAsync(cliente);

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
                return new ResultadoValidacao<int>(ex, "[ClienteService] - Ocorreu um erro ao incluir cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarClienteAsync(ClienteEntity cliente, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarCliente(cliente);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o cliente existe
                var clienteExistente = await _clienteSqlServerRepository.ObterPorIdAsync(cliente.Id);
                if (clienteExistente == null)
                    return new ResultadoValidacao<bool>("Cliente não encontrado.");

                // Verificar se já existe outro cliente com o mesmo CPF
                var existeCpf = await _clienteSqlServerRepository.ExisteCpfAsync(cliente.Cpf, cliente.Id);
                if (existeCpf)
                    return new ResultadoValidacao<bool>("Já existe outro cliente com este CPF.");

                // Verificar se já existe outro cliente com o mesmo e-mail
                var existeEmail = await _clienteSqlServerRepository.ExisteEmailAsync(cliente.Email, cliente.Id);
                if (existeEmail)
                    return new ResultadoValidacao<bool>("Já existe outro cliente com este e-mail.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    cliente.DataAlteracao = DateTime.Now;
                    cliente.UsuarioAlteracao = idUsuario;
                    cliente.DataInclusao = clienteExistente.DataInclusao;
                    cliente.UsuarioInclusao = clienteExistente.UsuarioInclusao;

                    var sucesso = await _clienteSqlServerRepository.AtualizarAsync(cliente);

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
                return new ResultadoValidacao<bool>(ex, "[ClienteService] - Ocorreu um erro ao alterar cliente.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirClienteAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se o cliente existe
                var cliente = await _clienteSqlServerRepository.ObterPorIdAsync(id);
                if (cliente == null)
                    return new ResultadoValidacao("Cliente não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _clienteSqlServerRepository.RemoverAsync(id);
                    if (!sucesso)
                        return new ResultadoValidacao("Não foi possível excluir o cliente.");

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
                    return new ResultadoValidacao("Não é possível excluir o cliente por possuir registros associados.");

                return new ResultadoValidacao(ex, "[ClienteService] - Ocorreu um erro ao excluir cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _clienteSqlServerRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[ClienteService] - Ocorreu um erro ao verificar existência do cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteCpfAsync(string cpf, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ValidarCpf(cpf);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _clienteSqlServerRepository.ExisteCpfAsync(cpf, idExcluir);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[ClienteService] - Ocorreu um erro ao verificar existência do CPF.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteEmailAsync(string email, int? idExcluir = null)
        {
            try
            {
                var validacao = Validator.ValidarEmail(email);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _clienteSqlServerRepository.ExisteEmailAsync(email, idExcluir);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[ClienteService] - Ocorreu um erro ao verificar existência do e-mail.");
            }
        }
    }
}
