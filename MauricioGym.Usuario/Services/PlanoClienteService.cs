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
    public class PlanoClienteService : ServiceBase<PlanoClienteValidator>, IPlanoClienteService
    {
        private readonly IPlanoClienteSqlServerRepository _planoClienteSqlServerRepository;
        private readonly IClienteSqlServerRepository _clienteSqlServerRepository;
        private readonly ITransactionSqlServerRepository _transaction;

        public PlanoClienteService(
            IPlanoClienteSqlServerRepository planoClienteSqlServerRepository,
            IClienteSqlServerRepository clienteSqlServerRepository,
            ITransactionSqlServerRepository transaction)
        {
            _planoClienteSqlServerRepository = planoClienteSqlServerRepository;
            _clienteSqlServerRepository = clienteSqlServerRepository;
            _transaction = transaction;
        }

        public async Task<IResultadoValidacao<PlanoClienteEntity?>> ObterPorIdAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<PlanoClienteEntity?>(validacao);

                var planoCliente = await _planoClienteSqlServerRepository.ObterPorIdAsync(id);
                return new ResultadoValidacao<PlanoClienteEntity?>(planoCliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PlanoClienteEntity?>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter plano do cliente por ID.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPorClienteIdAsync(int clienteId)
        {
            try
            {
                var validacao = Validator.ValidarId(clienteId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(validacao);

                var planosCliente = await _planoClienteSqlServerRepository.ObterPorClienteIdAsync(clienteId);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planosCliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos do cliente por ID do cliente.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPorPlanoIdAsync(int planoId)
        {
            try
            {
                var validacao = Validator.ValidarId(planoId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(validacao);

                var planosCliente = await _planoClienteSqlServerRepository.ObterPorPlanoIdAsync(planoId);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planosCliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos do cliente por ID do plano.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterAtivosPorClienteIdAsync(int clienteId)
        {
            try
            {
                var validacao = Validator.ValidarId(clienteId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(validacao);

                var planosCliente = await _planoClienteSqlServerRepository.ObterAtivosPorClienteIdAsync(clienteId);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planosCliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos ativos do cliente.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterVencidosPorClienteIdAsync(int clienteId)
        {
            try
            {
                var validacao = Validator.ValidarId(clienteId);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(validacao);

                var planosCliente = await _planoClienteSqlServerRepository.ObterVencidosPorClienteIdAsync(clienteId);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planosCliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos vencidos do cliente.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterVencendoAsync(int diasAntecedencia = 7)
        {
            try
            {
                if (diasAntecedencia < 0)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>("Dias de antecedência deve ser maior ou igual a zero.");

                var planosCliente = await _planoClienteSqlServerRepository.ObterVencendoAsync(diasAntecedencia);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planosCliente);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos vencendo.");
            }
        }

        public async Task<IResultadoValidacao<int>> IncluirPlanoClienteAsync(PlanoClienteEntity planoCliente, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirPlanoCliente(planoCliente);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<int>(validacao);

                // Verificar se o cliente já possui este plano ativo
                var possuiPlanoAtivo = await _planoClienteSqlServerRepository.ClientePossuiPlanoAtivoAsync(planoCliente.ClienteId, planoCliente.PlanoId);
                if (possuiPlanoAtivo)
                    return new ResultadoValidacao<int>("Cliente já possui este plano ativo.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    planoCliente.DataInclusao = DateTime.Now;
                    planoCliente.UsuarioInclusao = idUsuario;
                    planoCliente.Ativo = true;

                    var id = await _planoClienteSqlServerRepository.CriarAsync(planoCliente);

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
                return new ResultadoValidacao<int>(ex, "[PlanoClienteService] - Ocorreu um erro ao incluir plano do cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarPlanoClienteAsync(PlanoClienteEntity planoCliente, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarPlanoCliente(planoCliente);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                // Verificar se o plano do cliente existe
                var planoClienteExistente = await _planoClienteSqlServerRepository.ObterPorIdAsync(planoCliente.Id);
                if (planoClienteExistente == null)
                    return new ResultadoValidacao<bool>("Plano do cliente não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    planoCliente.DataAlteracao = DateTime.Now;
                    planoCliente.UsuarioAlteracao = idUsuario;
                    planoCliente.DataInclusao = planoClienteExistente.DataInclusao;
                    planoCliente.UsuarioInclusao = planoClienteExistente.UsuarioInclusao;

                    var sucesso = await _planoClienteSqlServerRepository.AtualizarAsync(planoCliente);

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
                return new ResultadoValidacao<bool>(ex, "[PlanoClienteService] - Ocorreu um erro ao alterar plano do cliente.");
            }
        }

        public async Task<IResultadoValidacao> ExcluirPlanoClienteAsync(int id, int idUsuario)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return validacao;

                // Verificar se o plano do cliente existe
                var planoCliente = await _planoClienteSqlServerRepository.ObterPorIdAsync(id);
                if (planoCliente == null)
                    return new ResultadoValidacao("Plano do cliente não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _planoClienteSqlServerRepository.RemoverAsync(id);
                    if (!sucesso)
                        return new ResultadoValidacao("Não foi possível excluir o plano do cliente.");

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
                    return new ResultadoValidacao("Não é possível excluir o plano do cliente por possuir registros associados.");

                return new ResultadoValidacao(ex, "[PlanoClienteService] - Ocorreu um erro ao excluir plano do cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExisteAsync(int id)
        {
            try
            {
                var validacao = Validator.ValidarId(id);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var existe = await _planoClienteSqlServerRepository.ExisteAsync(id);
                return new ResultadoValidacao<bool>(existe);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoClienteService] - Ocorreu um erro ao verificar existência do plano do cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ClientePossuiPlanoAtivoAsync(int clienteId, int planoId)
        {
            try
            {
                if (clienteId <= 0 || planoId <= 0)
                    return new ResultadoValidacao<bool>("ID do cliente e ID do plano devem ser maiores que zero.");

                var possuiPlano = await _planoClienteSqlServerRepository.ClientePossuiPlanoAtivoAsync(clienteId, planoId);
                return new ResultadoValidacao<bool>(possuiPlano);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoClienteService] - Ocorreu um erro ao verificar se cliente possui plano ativo.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPlanosDoClienteAsync(string cpfCliente, int idAcademia)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpfCliente))
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>("CPF do cliente não pode ser nulo ou vazio.");

                if (idAcademia <= 0)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>("ID da academia deve ser maior que zero.");

                var planos = await _planoClienteSqlServerRepository.ObterPorClienteAsync(cpfCliente, idAcademia);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos do cliente por CPF.");
            }
        }

        public async Task<IResultadoValidacao<PlanoClienteEntity?>> ObterPlanoAtivoAsync(string cpfCliente, int idAcademia)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpfCliente))
                    return new ResultadoValidacao<PlanoClienteEntity?>("CPF do cliente não pode ser nulo ou vazio.");

                if (idAcademia <= 0)
                    return new ResultadoValidacao<PlanoClienteEntity?>("ID da academia deve ser maior que zero.");

                var plano = await _planoClienteSqlServerRepository.ObterPlanoAtivoAsync(cpfCliente, idAcademia);
                return new ResultadoValidacao<PlanoClienteEntity?>(plano);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<PlanoClienteEntity?>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter plano ativo do cliente.");
            }
        }

        public async Task<IResultadoValidacao<int>> ContratarPlanoAsync(string cpfCliente, int idPlano, int idAcademia, int meses, decimal valorPago, int idUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpfCliente))
                    return new ResultadoValidacao<int>("CPF do cliente não pode ser nulo ou vazio.");

                if (idPlano <= 0)
                    return new ResultadoValidacao<int>("ID do plano deve ser maior que zero.");

                if (idAcademia <= 0)
                    return new ResultadoValidacao<int>("ID da academia deve ser maior que zero.");

                if (meses <= 0)
                    return new ResultadoValidacao<int>("Quantidade de meses deve ser maior que zero.");

                if (valorPago <= 0)
                    return new ResultadoValidacao<int>("Valor pago deve ser maior que zero.");

                // Buscar cliente por CPF
                var cliente = await _clienteSqlServerRepository.ObterPorCpfAsync(cpfCliente);
                if (cliente == null)
                    return new ResultadoValidacao<int>("Cliente não encontrado.");

                var planoCliente = new PlanoClienteEntity
                {
                    ClienteId = cliente.Id,
                    PlanoId = idPlano,
                    AcademiaId = idAcademia,
                    Valor = valorPago,
                    DataInicio = DateTime.Now.Date,
                    DataVencimento = DateTime.Now.Date.AddMonths(meses),
                    Ativo = true,
                    DataInclusao = DateTime.Now,
                    UsuarioInclusao = idUsuario
                };

                return await IncluirPlanoClienteAsync(planoCliente, idUsuario);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<int>(ex, "[PlanoClienteService] - Ocorreu um erro ao contratar plano.");
            }
        }

        public async Task<IResultadoValidacao<bool>> RenovarPlanoAsync(string cpfCliente, int idAcademia, int mesesAdicionais, decimal valorPago, int idUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpfCliente))
                    return new ResultadoValidacao<bool>("CPF do cliente não pode ser nulo ou vazio.");

                if (idAcademia <= 0)
                    return new ResultadoValidacao<bool>("ID da academia deve ser maior que zero.");

                if (mesesAdicionais <= 0)
                    return new ResultadoValidacao<bool>("Quantidade de meses adicionais deve ser maior que zero.");

                if (valorPago <= 0)
                    return new ResultadoValidacao<bool>("Valor pago deve ser maior que zero.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _planoClienteSqlServerRepository.RenovarPlanoAsync(cpfCliente, idAcademia, mesesAdicionais, valorPago);

                    if (sucesso)
                    {
                        // Buscar cliente por CPF para registro de auditoria
                        var cliente = await _clienteSqlServerRepository.ObterPorCpfAsync(cpfCliente);

                        await _transaction.CommitAsync();
                        return new ResultadoValidacao<bool>(true);
                    }

                    await _transaction.RollbackAsync();
                    return new ResultadoValidacao<bool>("Não foi possível renovar o plano. Cliente não possui plano ativo.");
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoClienteService] - Ocorreu um erro ao renovar plano do cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> CancelarPlanoAsync(int idPlanoCliente, string motivo, int idUsuario)
        {
            try
            {
                if (idPlanoCliente <= 0)
                    return new ResultadoValidacao<bool>("ID do plano cliente deve ser maior que zero.");

                if (string.IsNullOrWhiteSpace(motivo))
                    return new ResultadoValidacao<bool>("Motivo do cancelamento não pode ser nulo ou vazio.");

                var planoCliente = await _planoClienteSqlServerRepository.ObterPorIdAsync(idPlanoCliente);
                if (planoCliente == null)
                    return new ResultadoValidacao<bool>("Plano do cliente não encontrado.");

                await _transaction.BeginTransactionAsync();

                try
                {
                    var sucesso = await _planoClienteSqlServerRepository.CancelarPlanoAsync(idPlanoCliente, motivo);

                    if (sucesso)
                    {
                        await _transaction.CommitAsync();
                        return new ResultadoValidacao<bool>(true);
                    }

                    await _transaction.RollbackAsync();
                    return new ResultadoValidacao<bool>("Não foi possível cancelar o plano.");
                }
                catch
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoClienteService] - Ocorreu um erro ao cancelar plano do cliente.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ClientePodeEfetuarCheckInAsync(string cpfCliente, int idAcademia)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpfCliente))
                    return new ResultadoValidacao<bool>("CPF do cliente não pode ser nulo ou vazio.");

                if (idAcademia <= 0)
                    return new ResultadoValidacao<bool>("ID da academia deve ser maior que zero.");

                var podeEfetuarCheckIn = await _planoClienteSqlServerRepository.ClientePossuiPlanoAtivoAsync(cpfCliente, idAcademia);
                return new ResultadoValidacao<bool>(podeEfetuarCheckIn);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[PlanoClienteService] - Ocorreu um erro ao verificar se cliente pode efetuar check-in.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<PlanoClienteEntity>>> ObterPlanosVencendoAsync(int idAcademia, int dias = 7)
        {
            try
            {
                if (idAcademia <= 0)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>("ID da academia deve ser maior que zero.");

                if (dias < 0)
                    return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>("Dias deve ser maior ou igual a zero.");

                var planos = await _planoClienteSqlServerRepository.ObterPlanosVencendoAsync(idAcademia, dias);
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<PlanoClienteEntity>>(ex, "[PlanoClienteService] - Ocorreu um erro ao obter planos vencendo.");
            }
        }
    }
}
