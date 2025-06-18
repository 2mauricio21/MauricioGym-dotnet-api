using System.Text.RegularExpressions;

namespace MauricioGym.Infra.Databases.SQLServer.Errors
{
    internal class SqlErrorParser
    {
        internal static FieldSizeSqlErrorInfo ParseErrorMessageFieldSize(string errorMessage)
        {
            // Expressão regular para capturar a tabela, coluna e valor truncado
            var regex = new Regex(@"data would be truncated in table '(?<table>[^']+)', column '(?<column>[^']+)'. Truncated value: '(?<value>[^']+)'", RegexOptions.Compiled);

            // Aplicar a expressão regular na mensagem de erro
            var match = regex.Match(errorMessage);

            if (match.Success)
            {
                // Extrair as informações da mensagem de erro
                var table = match.Groups["table"].Value;
                var column = match.Groups["column"].Value;
                var truncatedValue = match.Groups["value"].Value;
                var truncatedValueLength = truncatedValue.Length;

                // Retornar as informações em um objeto SqlErrorInfo
                return new FieldSizeSqlErrorInfo
                {
                    Table = table,
                    Column = column,
                    TruncatedValueLength = truncatedValueLength
                };
            }

            // Se a expressão regular não encontrar um match, retornar null ou lançar uma exceção
            //throw new ArgumentException("A mensagem de erro não pôde ser analisada.", nameof(errorMessage));
            return null;
        }

        internal static DuplicateSqlErrorInfo ParseErrorMessageDuplicate(string errorMessage)
        {
            // Expressão regular para capturar a tabela, coluna e valor truncado
            string pattern = @"'dbo\.(?<table>\w+)'[\s\S]*?The duplicate key value is \((?<value>[^,]+),";

            var regex = new Regex(pattern, RegexOptions.Compiled);

            // Aplicar a expressão regular na mensagem de erro
            var match = regex.Match(errorMessage);

            if (match.Success)
            {
                // Extrair as informações da mensagem de erro
                string tableName = match.Groups[1].Value;
                string duplicateValue = match.Groups[2].Value;

                // Retornar as informações em um objeto SqlErrorInfo
                return new DuplicateSqlErrorInfo
                {
                    Table = tableName,
                    DuplicateValue = duplicateValue,
                };
            }

            // Se a expressão regular não encontrar um match, retornar null ou lançar uma exceção
            //throw new ArgumentException("A mensagem de erro não pôde ser analisada.", nameof(errorMessage));
            return null;
        }

        
    }
}
