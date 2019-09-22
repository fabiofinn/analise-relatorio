using AnaliseDados.Models;
using System;

namespace AnaliseDados.Parsers
{
    public class ClienteParser
    {
        int cnpjIndex = 1;
        int nameIndex = 2;
        int businessAreaIndex = 3;

        public Cliente Parse(string[] split)
        {
            if (split.Length != Constants.SplitLength)
                throw new Exception("Dados inválidos");

            return new Cliente
            {
                Cnpj = split[cnpjIndex],
                Name = split[nameIndex],
                BusinessArea = split[businessAreaIndex]
            };
        }
    }
}