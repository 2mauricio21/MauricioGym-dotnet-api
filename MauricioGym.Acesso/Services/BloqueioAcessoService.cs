using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MauricioGym.Acesso.Entities;
using MauricioGym.Acesso.Repositories.SqlServer.Interfaces;
using MauricioGym.Acesso.Services.Interfaces;
using MauricioGym.Acesso.Services.Validators;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Acesso.Services
{
    public class BloqueioAcessoService : ServiceBase<BloqueioAcessoValidator>, IBloqueioAcessoService
    {
        private readonly IBloqueioAcessoSqlServerRepository bloqueioAcessoSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public BloqueioAcessoService(IBloqueioAcessoSqlServerRepository bloqueioAcessoRepository, IAuditoriaService auditoriaService)
        {
            bloqueioAcessoSqlServerRepository = bloqueioAcessoRepository ?? throw new ArgumentNullException(nameof(bloqueioAcessoRepository));
            this.auditoriaService = auditoriaService ?? throw new ArgumentNullException(nameof(auditoriaService));
        }

        public async Task<IResultadoValidacao<BloqueioAcessoEntity>> IncluirBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            try
            {
                var validacao = await Validator.IncluirBloqueioAcessoAsync(bloqueioAcesso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<BloqueioAcessoEntity>(validacao);

                bloqueioAcesso.DataInicioBloqueio = DateTime.Now;
                bloqueioAcesso.Ativo = true;

                var bloqueioIncluido = await bloqueioAcessoSqlServerRepository.IncluirBloqueioAcessoAsync(bloqueioAcesso);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Bloqueio de acesso incluído - ID: {bloqueioIncluido.IdBloqueioAcesso}");

                return new ResultadoValidacao<BloqueioAcessoEntity>(bloqueioIncluido);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<BloqueioAcessoEntity>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar incluir o bloqueio de acesso.");
            }
        }

        public async Task<IResultadoValidacao<BloqueioAcessoEntity>> ConsultarBloqueioAcessoAsync(int idBloqueioAcesso)
        {
            try
            {
                var validacao = await Validator.ConsultarBloqueioAcessoAsync(idBloqueioAcesso);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<BloqueioAcessoEntity>(validacao);

                var bloqueio = await bloqueioAcessoSqlServerRepository.ConsultarBloqueioAcessoAsync(idBloqueioAcesso);
                return new ResultadoValidacao<BloqueioAcessoEntity>(bloqueio);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<BloqueioAcessoEntity>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar consultar o bloqueio de acesso.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ConsultarBloqueiosPorUsuarioAsync(int idUsuario)
        {
            try
            {
                var validacao = await Validator.ConsultarBloqueiosPorUsuarioAsync(idUsuario);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(validacao);

                var bloqueios = await bloqueioAcessoSqlServerRepository.ConsultarBloqueiosPorUsuarioAsync(idUsuario);
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(bloqueios);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar consultar bloqueios por usuário.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ConsultarBloqueiosPorUsuarioAcademiaAsync(int idUsuario, int idAcademia)
        {
            try
            {
                var validacao = await Validator.ConsultarBloqueiosPorUsuarioAcademiaAsync(idUsuario, idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(validacao);

                var bloqueios = await bloqueioAcessoSqlServerRepository.ConsultarBloqueiosPorUsuarioAcademiaAsync(idUsuario, idAcademia);
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(bloqueios);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar consultar bloqueios por usuário e academia.");
            }
        }

        public async Task<IResultadoValidacao<BloqueioAcessoEntity>> ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(int idUsuario, int idAcademia)
        {
            try
            {
                var validacao = await Validator.ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(idUsuario, idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<BloqueioAcessoEntity>(validacao);

                var bloqueio = await bloqueioAcessoSqlServerRepository.ConsultarBloqueioAtivoPorUsuarioAcademiaAsync(idUsuario, idAcademia);
                return new ResultadoValidacao<BloqueioAcessoEntity>(bloqueio);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<BloqueioAcessoEntity>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar consultar bloqueio ativo por usuário e academia.");
            }
        }

        public async Task<IResultadoValidacao> AlterarBloqueioAcessoAsync(BloqueioAcessoEntity bloqueioAcesso)
        {
            try
            {
                var validacao = await Validator.AlterarBloqueioAcessoAsync(bloqueioAcesso);
                if (validacao.OcorreuErro)
                    return validacao;

                var bloqueioExistente = await bloqueioAcessoSqlServerRepository.ConsultarBloqueioAcessoAsync(bloqueioAcesso.IdBloqueioAcesso);
                if (bloqueioExistente == null)
                    return new ResultadoValidacao($"Bloqueio de acesso com ID {bloqueioAcesso.IdBloqueioAcesso} não encontrado.");

                var resultado = await bloqueioAcessoSqlServerRepository.AlterarBloqueioAcessoAsync(bloqueioAcesso);

                // Bloqueio de acesso alterado com sucesso

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar alterar o bloqueio de acesso.");
            }
        }

        public async Task<IResultadoValidacao> CancelarBloqueioAcessoAsync(int idBloqueioAcesso)
        {
            try
            {
                var validacao = await Validator.CancelarBloqueioAcessoAsync(idBloqueioAcesso);
                if (validacao.OcorreuErro)
                    return validacao;

                var bloqueio = await bloqueioAcessoSqlServerRepository.ConsultarBloqueioAcessoAsync(idBloqueioAcesso);
                if (bloqueio == null)
                    return new ResultadoValidacao($"Bloqueio de acesso com ID {idBloqueioAcesso} não encontrado.");

                bloqueio.Ativo = false;
                bloqueio.DataFimBloqueio = DateTime.Now;

                var resultado = await bloqueioAcessoSqlServerRepository.AlterarBloqueioAcessoAsync(bloqueio);

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar cancelar o bloqueio de acesso.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ListarBloqueiosAcessoAsync()
        {
            try
            {
                var validacao = Validator.ListarBloqueiosAcessoAsync();
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(validacao);

                var bloqueios = await bloqueioAcessoSqlServerRepository.ListarBloqueiosAcessoAsync();
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(bloqueios);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar listar bloqueios de acesso.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>> ListarBloqueiosAtivosAsync()
        {
            try
            {
                var validacao = Validator.ListarBloqueiosAtivosAsync();
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(validacao);

                var bloqueios = await bloqueioAcessoSqlServerRepository.ListarBloqueiosAtivosAsync();
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(bloqueios);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<BloqueioAcessoEntity>>(ex, "[BloqueioAcessoService]-Ocorreu erro ao tentar listar bloqueios ativos.");
            }
        }


    }
}