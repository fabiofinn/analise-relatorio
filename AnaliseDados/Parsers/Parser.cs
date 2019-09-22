using AnaliseDados.Models;
using System;

namespace AnaliseDados.Parsers
{
    public class Parser
    {
        protected int typeIndex = 0;

        public ParseType DetermineParseType(string[] split)
        {
            if (split.Length == 0)
                throw new Exception("Dados inválidos");

            switch (split[typeIndex])
            {
                case Constants.TipoVendedor:
                    return ParseType.Vendedor;
                case Constants.TipoCliente:
                    return ParseType.Cliente;
                case Constants.TipoVenda:
                    return ParseType.Venda;
                default:
                    throw new Exception("Dados inválidos");
            }
        }
    }
}