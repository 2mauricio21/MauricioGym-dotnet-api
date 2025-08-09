using MauricioGym.Infra.Services.Validators;
using MauricioGym.Infra.Shared;
using MauricioGym.Infra.Shared.Interfaces;
using MauricioGym.Seguranca.Entities;
using System;

namespace MauricioGym.Seguranca.Services.Validators
{
    public class AutenticacaoValidator : ValidatorService
    {
        public new IResultadoValidacao ValidarId(int? id)
        {
            return base.ValidarId(id);
        }

        public IResultadoValidacao ValidarLogin(string email, string senha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return new ResultadoValidacao("Email é obrigatório.");

                if (string.IsNullOrWhiteSpace(senha))
                    return new ResultadoValidacao("Senha é obrigatória.");

                if (!email.Contains("@"))
                    return new ResultadoValidacao("Email deve ter um formato válido.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[AutenticacaoValidator] - Erro ao validar login");
            }
        }

        public IResultadoValidacao ValidarAlteracaoSenha(string senhaAtual, string novaSenha)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(senhaAtual))
                    return new ResultadoValidacao("Senha atual é obrigatória.");

                if (string.IsNullOrWhiteSpace(novaSenha))
                    return new ResultadoValidacao("Nova senha é obrigatória.");

                if (novaSenha.Length < 6)
                    return new ResultadoValidacao("Nova senha deve ter pelo menos 6 caracteres.");

                if (senhaAtual == novaSenha)
                    return new ResultadoValidacao("A nova senha deve ser diferente da senha atual.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[AutenticacaoValidator] - Erro ao validar alteração de senha");
            }
        }

        public IResultadoValidacao ValidarToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                    return new ResultadoValidacao("Token é obrigatório.");

                return new ResultadoValidacao();
            }
            catch (Exception ex)
            {
                return new ResultadoValidacao(ex, "[AutenticacaoValidator] - Erro ao validar token");
            }
        }
    }
}