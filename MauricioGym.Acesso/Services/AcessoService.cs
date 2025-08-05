using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MauricioGym.Acesso.Entities;
using MauricioGym.Acesso.Repositories.SqlServer.Interfaces;
using MauricioGym.Acesso.Services.Interfaces;
using MauricioGym.Acesso.Services.Validators;
using MauricioGym.Infra.Entities;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Acesso.Services
{
    public class AcessoService : ServiceBase<AcessoValidator>, IAcessoService
    {
        private readonly IAcessoSqlServerRepository acessoSqlServerRepository;
        private readonly IBloqueioAcessoSqlServerRepository bloqueioAcessoSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public AcessoService(
            IAcessoSqlServerRepository acessoSqlServerRepository,
            IBloqueioAcessoSqlServerRepository bloqueioAcessoSqlServerRepository,
            IAuditoriaService auditoriaService)
        {
            this.acessoSqlServerRepository = acessoSqlServerRepository;
            this.bloqueioAcessoSqlServerRepository = bloqueioAcessoSqlServerRepository;
            this.auditoriaService = auditoriaService;
        }

        public async Task<IResultadoValidacao<AcessoEntity>> IncluirAcessoAsync(AcessoEntity acesso, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirAcesso(acesso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcessoEntity>(validacao);

                // Verificar se usuário está bloqueado
                var bloqueioAtivo = await bloqueioAcessoSqlServerRepository.ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(acesso.IdUsuario, acesso.IdAcademia);
                if (bloqueioAtivo != null)
                    return new ResultadoValidacao<AcessoEntity>($"Usuário está bloqueado para acesso nesta academia. Motivo: {bloqueioAtivo.MotivoBloqueio}");

                acesso.DataHoraEntrada = DateTime.Now;
                acesso.ObservacaoAcesso = "Ativo";

                var result = await acessoSqlServerRepository.IncluirAcessoAsync(acesso);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Acesso cadastrado - ID: {result.IdAcesso}");

                return new ResultadoValidacao<AcessoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcessoEntity>(ex, "[AcessoService]- Ocorreu erro ao tentar incluir o acesso.");
            }
        }

        public async Task<IResultadoValidacao<AcessoEntity>> ConsultarAcessoAsync(int idAcesso, int idAcademia)
        {
            try
            {
                var validacao = Validator.ConsultarAcesso(idAcesso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcessoEntity>(validacao);

                var result = await acessoSqlServerRepository.ConsultarAcessoAsync(idAcesso);
                return new ResultadoValidacao<AcessoEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcessoEntity>(ex, "[AcessoService]-Ocorreu um erro ao tentar consultar o acesso.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ConsultarAcessosPorUsuarioAsync(int idUsuario, int idAcademia)
        {
            try
            {
                var validacao = Validator.ConsultarAcessosPorUsuario(idUsuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<AcessoEntity>>(validacao);

                var result = await acessoSqlServerRepository.ConsultarAcessosPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(ex, "[AcessoService]-Ocorreu um erro ao tentar consultar acessos por usuário.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ConsultarAcessosPorAcademiaAsync(int idAcademia)
        {
            try
            {
                var validacao = Validator.ConsultarAcessosPorAcademia(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<AcessoEntity>>(validacao);

                var result = await acessoSqlServerRepository.ConsultarAcessosPorAcademiaAsync(idAcademia);
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(ex, "[AcessoService]-Ocorreu um erro ao tentar consultar acessos por academia.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ConsultarAcessosAtivosPorAcademiaAsync(int idAcademia)
        {
            try
            {
                var validacao = Validator.ConsultarAcessosAtivosPorAcademia(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<AcessoEntity>>(validacao);

                var result = await acessoSqlServerRepository.ConsultarAcessosAtivosPorAcademiaAsync(idAcademia);
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(ex, "[AcessoService]-Ocorreu um erro ao tentar consultar acessos ativos por academia.");
            }
        }

        public async Task<IResultadoValidacao<AcessoEntity>> AlterarAcessoAsync(AcessoEntity acesso, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarAcesso(acesso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcessoEntity>(validacao);

                var acessoExistente = await acessoSqlServerRepository.ConsultarAcessoAsync(acesso.IdAcesso);
                if (acessoExistente == null)
                    return new ResultadoValidacao<AcessoEntity>(null, "Acesso não encontrado.");

                var result = await acessoSqlServerRepository.AlterarAcessoAsync(acesso);
                
                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Acesso alterado - ID: {acesso.IdAcesso}");
                
                return new ResultadoValidacao<AcessoEntity>(acesso);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcessoEntity>(ex, "[AcessoService]-Ocorreu um erro ao tentar alterar o acesso.");
            }
        }

        public async Task<IResultadoValidacao<bool>> RegistrarSaidaAsync(int idAcesso, int idUsuario)
        {
            try
            {
                var validacao = Validator.RegistrarSaida(idAcesso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var acesso = await acessoSqlServerRepository.ConsultarAcessoAsync(idAcesso);
                if (acesso == null)
                    return new ResultadoValidacao<bool>("Acesso não encontrado.");

                if (acesso.ObservacaoAcesso == "Finalizado")
                    return new ResultadoValidacao<bool>("Saída já registrada.");

                acesso.DataHoraSaida = DateTime.Now;
                var result = await acessoSqlServerRepository.AlterarAcessoAsync(acesso);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Saída registrada - ID Acesso: {idAcesso}");

                return new ResultadoValidacao<bool>(result != null);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AcessoService]-Ocorreu um erro ao tentar registrar saída.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ListarAcessosAsync()
        {
            try
            {
                var validacao = Validator.ListarAcessosAsync();
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<AcessoEntity>>(validacao);

                var acessos = await acessoSqlServerRepository.ListarAcessosAsync();
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(acessos);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcessoEntity>>(ex, "[AcessoService]-Ocorreu erro ao tentar listar acessos.");
            }
        }

        private async Task ValidarAcessoAsync(AcessoEntity acesso)
        {
            if (acesso == null)
                throw new ArgumentNullException(nameof(acesso));

            if (acesso.IdUsuario <= 0)
                throw new ArgumentException("ID do usuário é obrigatório.", nameof(acesso.IdUsuario));

            if (acesso.IdAcademia <= 0)
                throw new ArgumentException("ID da academia é obrigatório.", nameof(acesso.IdAcademia));

            if (string.IsNullOrWhiteSpace(acesso.TipoAcesso))
                throw new ArgumentException("Tipo do acesso é obrigatório.", nameof(acesso.TipoAcesso));
        }
    }
}