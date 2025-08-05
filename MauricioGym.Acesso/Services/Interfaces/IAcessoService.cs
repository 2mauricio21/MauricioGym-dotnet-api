using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauricioGym.Acesso.Entities;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;

namespace MauricioGym.Acesso.Services.Interfaces
{
    public interface IAcessoService
    {
        Task<IResultadoValidacao<AcessoEntity>> IncluirAcessoAsync(AcessoEntity acesso, int idUsuario);
        Task<IResultadoValidacao<AcessoEntity>> ConsultarAcessoAsync(int idAcesso, int idAcademia);
        Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ConsultarAcessosPorUsuarioAsync(int idUsuario, int idAcademia);
        Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ConsultarAcessosPorAcademiaAsync(int idAcademia);
        Task<IResultadoValidacao<IEnumerable<AcessoEntity>>> ConsultarAcessosAtivosPorAcademiaAsync(int idAcademia);
        Task<IResultadoValidacao<AcessoEntity>> AlterarAcessoAsync(AcessoEntity acesso, int idUsuario);
        Task<IResultadoValidacao<bool>> RegistrarSaidaAsync(int idAcesso, int idUsuario);
    }
}