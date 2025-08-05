using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Plano.Entities;
using MauricioGym.Plano.Repositories.SqlServer.Interfaces;
using MauricioGym.Plano.Services.Interfaces;
using MauricioGym.Plano.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Plano.Services
{
    public class UsuarioPlanoService : ServiceBase<UsuarioPlanoValidator>, IUsuarioPlanoService
    {
        private readonly IUsuarioPlanoSqlServerRepository usuarioPlanoSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public UsuarioPlanoService(
            IUsuarioPlanoSqlServerRepository _usuarioPlanoRepository,
            IAuditoriaService _auditoriaService)
        {
            usuarioPlanoSqlServerRepository = _usuarioPlanoRepository;
            auditoriaService = _auditoriaService;
        }

        public async Task<IResultadoValidacao<UsuarioPlanoEntity>> IncluirUsuarioPlanoAsync(UsuarioPlanoEntity usuarioPlano, int idUsuario)
        {
            try
            {
                var validacao = await Validator.IncluirUsuarioPlanoAsync(usuarioPlano);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<UsuarioPlanoEntity>(validacao);
                
                var planoAtivo = await usuarioPlanoSqlServerRepository.ObterAtivoPorUsuarioAsync(usuarioPlano.IdUsuario);
                if (planoAtivo != null)
                    return new ResultadoValidacao<UsuarioPlanoEntity>("Usuário já possui um plano ativo");

                usuarioPlano.DataInicio = DateTime.Now;
                usuarioPlano.StatusPlano = "Ativo";

                var novoUsuarioPlano = await usuarioPlanoSqlServerRepository.IncluirAsync(usuarioPlano);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Plano {novoUsuarioPlano.IdPlano} associado ao usuário {novoUsuarioPlano.IdUsuario}");
                
                return new ResultadoValidacao<UsuarioPlanoEntity>(novoUsuarioPlano);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<UsuarioPlanoEntity>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar incluir plano para usuário.");
            }
        }

        public async Task<IResultadoValidacao<UsuarioPlanoEntity>> ConsultarUsuarioPlanoAsync(int idUsuarioPlano)
        {
            try
            {
                var usuarioPlano = await usuarioPlanoSqlServerRepository.ObterPorIdAsync(idUsuarioPlano);
                if (usuarioPlano == null)
                    return new ResultadoValidacao<UsuarioPlanoEntity>("Associação usuário-plano não encontrada");

                return new ResultadoValidacao<UsuarioPlanoEntity>(usuarioPlano);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<UsuarioPlanoEntity>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar consultar associação usuário-plano.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ConsultarPlanosPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var planos = await usuarioPlanoSqlServerRepository.ObterPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar consultar planos do usuário.");
            }
        }

        public async Task<IResultadoValidacao<UsuarioPlanoEntity>> ConsultarPlanoAtivoPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var usuarioPlano = await usuarioPlanoSqlServerRepository.ObterAtivoPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<UsuarioPlanoEntity>(usuarioPlano);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<UsuarioPlanoEntity>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar consultar plano ativo do usuário.");
            }
        }

        public async Task<IResultadoValidacao> AlterarUsuarioPlanoAsync(UsuarioPlanoEntity usuarioPlano, int idUsuario)
        {
            try
            {
                var validacao = await Validator.AlterarUsuarioPlanoAsync(usuarioPlano);
                if (validacao.OcorreuErro)
                    return validacao;

                var usuarioPlanoExistente = await usuarioPlanoSqlServerRepository.ObterAtivoPorUsuarioAsync(usuarioPlano.IdUsuarioPlano);
                if (usuarioPlanoExistente == null)
                    return new ResultadoValidacao("Associação usuário-plano não encontrada");

                await usuarioPlanoSqlServerRepository.AtualizarAsync(usuarioPlano);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Associação usuário-plano {usuarioPlano.IdUsuarioPlano} alterada com sucesso");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar alterar associação usuário-plano.");
            }
        }

        public async Task<IResultadoValidacao> CancelarUsuarioPlanoAsync(int idUsuarioPlano, int idUsuario)
        {
            try
            {
                var validacao = await Validator.CancelarUsuarioPlanoAsync(idUsuarioPlano);
                if (validacao.OcorreuErro)
                    return validacao;

                var usuarioPlano = await usuarioPlanoSqlServerRepository.ObterAtivoPorUsuarioAsync(idUsuarioPlano);
                if (usuarioPlano == null)
                    return new ResultadoValidacao("Associação usuário-plano não encontrada");

                usuarioPlano.StatusPlano = "Cancelado";
                usuarioPlano.DataFim = DateTime.Now;

                await usuarioPlanoSqlServerRepository.CancelarAsync(idUsuarioPlano);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Plano cancelado para o usuário {usuarioPlano.IdUsuario}");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar cancelar plano do usuário.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarUsuariosPlanosAsync()
        {
            try
            {
                var planos = await usuarioPlanoSqlServerRepository.ListarTodosAsync();
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar listar usuários planos.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarUsuariosPlanosAtivosAsync()
        {
            try
            {
                var planos = await usuarioPlanoSqlServerRepository.ListarAtivosAsync();
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar listar usuários planos ativos.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>> ListarUsuariosPlanosPorStatusAsync(string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(status))
                    return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>("Status não pode ser vazio ou nulo.");

                var planos = await usuarioPlanoSqlServerRepository.ListarPorStatusAsync(status);
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(planos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<UsuarioPlanoEntity>>(ex, "[UsuarioPlanoService]-Ocorreu erro ao tentar listar usuários planos por status.");
            }
        }


    }
}