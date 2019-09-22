using AnaliseDados.Models;
using System;

namespace AnaliseDados.Parsers
{
    public class VendedorParser
    {
        int cpfIndex = 1;
        int nameIndex = 2;
        int salaryIndex = 3;

        public Vendedor Parse(string[] split)
        {
            if (split.Length != Constants.SplitLength)
                throw new Exception("Dados inválidos");

            var salaryParseOk = decimal.TryParse(split[salaryIndex], out decimal salary);
            if (!salaryParseOk)
                throw new Exception("Erro ao ler o salário do Vendedor");

            return new Vendedor
            {
                Cpf = split[cpfIndex],
                Name = split[nameIndex],
                Salary = salary
            };
        }
    }
}