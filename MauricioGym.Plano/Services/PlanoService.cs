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
  public class PlanoService : ServiceBase<PlanoValidator>, IPlanoService
  {
    private readonly IPlanoSqlServerRepository planoSqlServerRepository;
    private readonly IAuditoriaService auditoriaService;

    public PlanoService(
        IPlanoSqlServerRepository _planoRepository,
        IAuditoriaService _auditoriaService)
    {
      planoSqlServerRepository = _planoRepository;
      auditoriaService = _auditoriaService;
    }

    public async Task<IResultadoValidacao<PlanoEntity>> IncluirPlanoAsync(PlanoEntity plano, int idUsuario)
    {
      try
      {
        var validacao = await Validator.IncluirPlanoAsync(plano);
        if (validacao.OcorreuErro)
          return new ResultadoValidacao<PlanoEntity>(validacao);

        var planoExistente = await planoSqlServerRepository.ConsultarPlanoPorNomeAsync(plano.Nome, plano.IdAcademia);
        if (planoExistente != null)
          return new ResultadoValidacao<PlanoEntity>("Já existe um plano com este nome para esta academia");

        var novoPlano = await planoSqlServerRepository.IncluirPlanoAsync(plano);

        await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Plano {novoPlano.Nome} incluído com sucesso");

        return new ResultadoValidacao<PlanoEntity>(novoPlano);
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao<PlanoEntity>(ex, "[PlanoService]-Ocorreu erro ao tentar incluir plano.");
      }
    }

    public async Task<IResultadoValidacao<PlanoEntity>> ConsultarPlanoAsync(int idPlano)
    {
      try
      {
        var plano = await planoSqlServerRepository.ConsultarPlanoAsync(idPlano);
        if (plano == null)
          return new ResultadoValidacao<PlanoEntity>("Plano não encontrado");

        return new ResultadoValidacao<PlanoEntity>(plano);
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao<PlanoEntity>("Erro ao consultar plano: " + ex.Message);
      }
    }

    public async Task<IResultadoValidacao<PlanoEntity>> ConsultarPlanoPorNomeAsync(string nome, int idAcademia)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(nome))
          return new ResultadoValidacao<PlanoEntity>("Nome do plano não pode ser vazio ou nulo.");

        var plano = await planoSqlServerRepository.ConsultarPlanoPorNomeAsync(nome, idAcademia);
        return new ResultadoValidacao<PlanoEntity>(plano);
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao<PlanoEntity>("Erro ao consultar plano por nome: " + ex.Message);
      }
    }

    public async Task<IResultadoValidacao> AlterarPlanoAsync(PlanoEntity plano, int idUsuario)
    {
      try
      {
        var validacao = await Validator.AlterarPlanoAsync(plano);
        if (validacao.OcorreuErro)
          return validacao;

        var planoExistente = await planoSqlServerRepository.ConsultarPlanoPorNomeAsync(plano.Nome, plano.IdAcademia);
        if (planoExistente != null && planoExistente.IdPlano != plano.IdPlano)
          return new ResultadoValidacao("Já existe outro plano com este nome para esta academia");

        await planoSqlServerRepository.AlterarPlanoAsync(plano);

        await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Plano {plano.Nome} alterado com sucesso");

        return new ResultadoValidacao();
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao(ex, "[PlanoService]-Ocorreu erro ao tentar alterar plano.");
      }
    }

    public async Task<IResultadoValidacao> ExcluirPlanoAsync(int idPlano, int idUsuario)
    {
      try
      {
        var validacao = await Validator.ExcluirPlanoAsync(idPlano);
        if (validacao.OcorreuErro)
          return validacao;

        var plano = await planoSqlServerRepository.ConsultarPlanoAsync(idPlano);
        if (plano == null)
          return new ResultadoValidacao("Plano não encontrado");

        await planoSqlServerRepository.ExcluirPlanoAsync(idPlano);

        await auditoriaService.IncluirAuditoriaAsync(idUsuario, $"Plano {plano.Nome} excluído com sucesso");

        return new ResultadoValidacao();
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao(ex, "[PlanoService]-Ocorreu erro ao tentar excluir plano.");
      }
    }

    public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarPlanosAsync()
    {
      try
      {
        var planos = await planoSqlServerRepository.ListarPlanosAsync();
        return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao<IEnumerable<PlanoEntity>>("Erro ao listar planos: " + ex.Message);
      }
    }

    public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarPlanosPorAcademiaAsync(int idAcademia)
    {
      try
      {
        var planos = await planoSqlServerRepository.ListarPlanosPorAcademiaAsync(idAcademia);
        return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao<IEnumerable<PlanoEntity>>("Erro ao listar planos: " + ex.Message);
      }
    }

    public async Task<IResultadoValidacao<IEnumerable<PlanoEntity>>> ListarPlanosAtivosAsync()
    {
      try
      {
        var planos = await planoSqlServerRepository.ListarPlanosAtivosAsync();
        return new ResultadoValidacao<IEnumerable<PlanoEntity>>(planos);
      }
      catch (Exception ex)
      {
        return new ResultadoValidacao<IEnumerable<PlanoEntity>>("Erro ao listar planos ativos: " + ex.Message);
      }
    }


  }
}