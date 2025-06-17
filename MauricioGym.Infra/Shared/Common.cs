using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MauricioGym.Infra.Shared
{
    public static class Common
    {
        #region [ Métodos Públicos ]

        public static string LimparFormatacao(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            return new Regex(@"[^\w\s]").Replace(value, "").Replace(" ", "");
        }

        public static string LimparFormatacaoHtml(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            value = Regex.Replace(value, @"</?(p|br|div)[^>]*>", " ", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"<[^>]+>", "", RegexOptions.IgnoreCase);
            value = Regex.Replace(value, @"\s+", " ", RegexOptions.IgnoreCase);
            return value.Trim();
        }

        public static string RemoverAcentos(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            StringBuilder stringBuilder = new StringBuilder();
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            
            foreach (char character in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(character);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = LimparFormatacao(cpf);

            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(d => d == cpf[0]))
                return false;

            // Validação do primeiro dígito verificador
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);

            var remainder = sum % 11;
            var digit1 = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != digit1)
                return false;

            // Validação do segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);

            remainder = sum % 11;
            var digit2 = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[10].ToString()) == digit2;
        }

        #endregion
    }
}
