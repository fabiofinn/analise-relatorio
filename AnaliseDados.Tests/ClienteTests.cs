using AnaliseDados.Models;
using AnaliseDados.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnaliseDados.Tests
{
    [TestClass]
    public class ClienteTests
    {
        Parser parser = new Parser();
        ClienteParser clienteParser = new ClienteParser();

        [TestMethod]
        public void parse_clientes()
        {
            List<Cliente> lista = new List<Cliente>();
            for (int i = 0; i < TextoArquivo.Texto.Length; i++)
            {
                var linha = TextoArquivo.Texto[i];
                try
                {
                    var split = linha.Split(new[] { "ç" }, StringSplitOptions.RemoveEmptyEntries);
                    var parserType = parser.DetermineParseType(split);
                    if (parserType == ParseType.Cliente)
                    {
                        var cliente = clienteParser.Parse(split);
                        lista.Add(cliente);
                        Console.WriteLine($"Linha {(i + 1)}: cliente {cliente.Name} lido com sucesso");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Linha {(i + 1)}: {ex.Message}");
                }
            }

            Assert.AreEqual(lista.Count, 2);
        }
    }
}