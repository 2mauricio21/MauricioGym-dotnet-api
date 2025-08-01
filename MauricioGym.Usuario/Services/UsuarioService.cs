using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MauricioGym.Infra.Services;
using MauricioGym.Infra.Services.Interfaces;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Usuario.Entities;
using MauricioGym.Usuario.Repositories.SqlServer.Interfaces;
using MauricioGym.Usuario.Services.Interfaces;
using MauricioGym.Usuario.Services.Validators;

namespace MauricioGym.Usuario.Services
{
    public class UsuarioService : ServiceBase<UsuarioValidator>, IUsuarioService
    {
        private readonly IUsuarioSqlServerRepository _usuarioRepository;
        private readonly IAuditoriaService _auditoriaService;

        public UsuarioService(
            IUsuarioSqlServerRepository usuarioRepository,
            IAuditoriaService auditoriaService)
        {
            _usuarioRepository = usuarioRepository;
            _auditoriaService = auditoriaService;
        }

        public async Task<IResultadoValidacao<int>> IncluirUsuarioAsync(UsuarioEntity usuario)
        {
            var validacao = Validator.CriarUsuario(usuario);
            if (validacao.OcorreuErro)
                return new ResultadoValidacao<int>(validacao);

            usuario.DataCadastro = DateTime.Now;
            usuario.Ativo = true;
            
            var usuarioCriado = await _usuarioRepository.IncluirUsuarioAsync(usuario);
            
            await _auditoriaService.RegistrarAuditoriaAsync(
                usuarioCriado.IdUsuario, 
                $"Usuário criado: {usuarioCriado.Nome} ({usuarioCriado.Email})");

            return new ResultadoValidacao<int>(usuarioCriado.IdUsuario);
        }

        public async Task<IResultadoValidacao<UsuarioEntity>> ConsultarUsuarioAsync(int idUsuario)
        {
            var validacao = Validator.ConsultarUsuario(idUsuario);
            if (validacao.OcorreuErro)
                return new ResultadoValidacao<UsuarioEntity>(validacao);

            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            if (usuario == null)
                return new ResultadoValidacao<UsuarioEntity>("Usuário não encontrado.");

            return new ResultadoValidacao<UsuarioEntity>(usuario);
        }

        public async Task<IResultadoValidacao<UsuarioEntity>> ConsultarUsuarioPorEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ResultadoValidacao<UsuarioEntity>("Email é obrigatório.");

            var usuario = await _usuarioRepository.ConsultarUsuarioPorEmailAsync(email);
            if (usuario == null)
                return new ResultadoValidacao<UsuarioEntity>("Usuário não encontrado.");

            return new ResultadoValidacao<UsuarioEntity>(usuario);
        }

        public async Task<IResultadoValidacao<UsuarioEntity>> ConsultarUsuarioPorCPFAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return new ResultadoValidacao<UsuarioEntity>("CPF é obrigatório.");

            var usuario = await _usuarioRepository.ConsultarUsuarioPorCPFAsync(cpf);
            if (usuario == null)
                return new ResultadoValidacao<UsuarioEntity>("Usuário não encontrado.");

            return new ResultadoValidacao<UsuarioEntity>(usuario);
        }

        public async Task<IResultadoValidacao> AlterarUsuarioAsync(UsuarioEntity usuario)
        {
            var validacao = Validator.AtualizarUsuario(usuario);
            if (validacao.OcorreuErro)
                return validacao;

            var usuarioExistente = await _usuarioRepository.ConsultarUsuarioAsync(usuario.IdUsuario);
            if (usuarioExistente == null)
                return new ResultadoValidacao("Usuário não encontrado.");

            await _usuarioRepository.AlterarUsuarioAsync(usuario);
            
            await _auditoriaService.RegistrarAuditoriaAsync(
                usuario.IdUsuario, 
                $"Usuário alterado: {usuario.Nome} ({usuario.Email})");

            return new ResultadoValidacao();
        }

        public async Task<IResultadoValidacao> ExcluirUsuarioAsync(int idUsuario)
        {
            var validacao = Validator.ConsultarUsuario(idUsuario);
            if (validacao.OcorreuErro)
                return validacao;

            var usuario = await _usuarioRepository.ConsultarUsuarioAsync(idUsuario);
            if (usuario == null)
                return new ResultadoValidacao("Usuário não encontrado.");

            await _usuarioRepository.ExcluirUsuarioAsync(idUsuario);
            
            await _auditoriaService.RegistrarAuditoriaAsync(
                idUsuario, 
                $"Usuário excluído: {usuario.Nome} ({usuario.Email})");

            return new ResultadoValidacao();
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioEntity>>> ListarUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.ListarUsuariosAsync();
            return new ResultadoValidacao<IEnumerable<UsuarioEntity>>(usuarios);
        }

        public async Task<IResultadoValidacao<IEnumerable<UsuarioEntity>>> ListarUsuariosAtivosAsync()
        {
            var usuarios = await _usuarioRepository.ListarUsuariosAtivosAsync();
            return new ResultadoValidacao<IEnumerable<UsuarioEntity>>(usuarios);
        }

        public async Task<IResultadoValidacao<bool>> ValidarLoginAsync(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new ResultadoValidacao<bool>("Email é obrigatório.");

            if (string.IsNullOrWhiteSpace(senha))
                return new ResultadoValidacao<bool>("Senha é obrigatória.");

            var usuario = await _usuarioRepository.ConsultarUsuarioPorEmailAsync(email);
            if (usuario == null || !usuario.Ativo)
                return new ResultadoValidacao<bool>("Usuário não encontrado ou inativo.");

            // Em produção, usar hash de senha
            var senhaValida = usuario.Senha == senha;
            
            if (senhaValida)
            {
                usuario.DataUltimoLogin = DateTime.Now;
                await _usuarioRepository.AlterarUsuarioAsync(usuario);
                
                await _auditoriaService.RegistrarAuditoriaAsync(
                    usuario.IdUsuario, 
                    "Login realizado com sucesso");
            }
            else
            {
                await _auditoriaService.RegistrarAuditoriaAsync(
                    usuario.IdUsuario, 
                    "Tentativa de login falhou");
            }

            return new ResultadoValidacao<bool>(senhaValida);
        }
    }
}