using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Academia.Entities;
using MauricioGym.Academia.Repositories.SqlServer.Interfaces;
using MauricioGym.Academia.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Academia.Services.Validators;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Academia.Services
{
    public class AcademiaService : ServiceBase<AcademiaValidator>, IAcademiaService
    {
        private readonly IAcademiaSqlServerRepository academiaSqlServerRepository;
        private readonly IAuditoriaService auditoriaService;

        public AcademiaService(
            IAcademiaSqlServerRepository academiaSqlServerRepository,
            IAuditoriaService auditoriaService
            )
        {
            this.academiaSqlServerRepository = academiaSqlServerRepository ?? throw new ArgumentNullException(nameof(academiaSqlServerRepository));
            this.auditoriaService = auditoriaService ?? throw new ArgumentNullException(nameof(auditoriaService));
        }

        public async Task<IResultadoValidacao<AcademiaEntity>> IncluirAcademiaAsync(AcademiaEntity academia, int idUsuario)
        {
            try
            {
                var validacao = Validator.IncluirAcademiaAsync(academia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcademiaEntity>(validacao);

                var result = await academiaSqlServerRepository.IncluirAcademiaAsync(academia);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, "Academia incluída.");

                return new ResultadoValidacao<AcademiaEntity>(result);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcademiaEntity>(ex, "[AcademiaService]-Ocorreu erro ao tentar incluir a academia.");
            }
        }

        public async Task<IResultadoValidacao<AcademiaEntity>> ConsultarAcademiaAsync(int idAcademia)
        {
            try
            {
                var validacao = Validator.ConsultarAcademiaAsync(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcademiaEntity>(validacao);

                var academia = await academiaSqlServerRepository.ConsultarAcademiaAsync(idAcademia);
                return new ResultadoValidacao<AcademiaEntity>(academia);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcademiaEntity>(ex, "[AcademiaService]-Ocorreu erro ao tentar consultar a academia.");
            }
        }

        public async Task<IResultadoValidacao<AcademiaEntity>> ConsultarAcademiaPorCNPJAsync(string cnpj)
        {
            try
            {
                var validacao = Validator.ConsultarAcademiaPorCNPJAsync(cnpj);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<AcademiaEntity>(validacao);

                var academia = await academiaSqlServerRepository.ConsultarAcademiaPorCNPJAsync(cnpj);
                return new ResultadoValidacao<AcademiaEntity>(academia);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<AcademiaEntity>(ex, "[AcademiaService]-Ocorreu erro ao tentar consultar a academia por CNPJ.");
            }
        }

        public async Task<IResultadoValidacao<bool>> AlterarAcademiaAsync(AcademiaEntity academia, int idUsuario)
        {
            try
            {
                var validacao = Validator.AlterarAcademiaAsync(academia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var academiaExistente = await academiaSqlServerRepository.ConsultarAcademiaAsync(academia.IdAcademia);
                if (academiaExistente == null)
                    return new ResultadoValidacao<bool>($"Academia com ID {academia.IdAcademia} não encontrada.");

                var resultado = await academiaSqlServerRepository.AlterarAcademiaAsync(academia);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Academia alterada - ID: {academia.IdAcademia}");

                return new ResultadoValidacao<bool>(resultado);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AcademiaService]-Ocorreu erro ao tentar alterar a academia.");
            }
        }

        public async Task<IResultadoValidacao<bool>> ExcluirAcademiaAsync(int idAcademia, int idUsuario)
        {
            try
            {
                var validacao = Validator.ExcluirAcademiaAsync(idAcademia);
                if (validacao.OcorreuErro)
                    return new ResultadoValidacao<bool>(validacao);

                var academia = await academiaSqlServerRepository.ConsultarAcademiaAsync(idAcademia);
                if (academia == null)
                    return new ResultadoValidacao<bool>($"Academia com ID {idAcademia} não encontrada.");

                var resultado = await academiaSqlServerRepository.ExcluirAcademiaAsync(idAcademia);

                await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Academia excluída - ID: {idAcademia}");

                return new ResultadoValidacao<bool>(resultado);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<bool>(ex, "[AcademiaService]-Ocorreu erro ao tentar excluir a academia.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ListarAcademiasAsync()
        {
            try
            {
                var academias = await academiaSqlServerRepository.ListarAcademiasAsync();
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(academias);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(ex, "[AcademiaService]-Ocorreu erro ao tentar listar as academias.");
            }
        }

        public async Task<IResultadoValidacao<IEnumerable<AcademiaEntity>>> ListarAcademiasAtivasAsync()
        {
            try
            {
                var academias = await academiaSqlServerRepository.ListarAcademiasAtivasAsync();
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(academias);
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao<IEnumerable<AcademiaEntity>>(ex, "[AcademiaService]-Ocorreu erro ao tentar listar as academias ativas.");
            }
        }
    }
}