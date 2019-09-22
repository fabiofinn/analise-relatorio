using AnaliseDados.Models;
using AnaliseDados.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnaliseDados.Tests
{
    [TestClass]
    public class VendedorTests
    {
        Parser parser = new Parser();
        VendedorParser vendedorParser = new VendedorParser();

        [TestMethod]
        public void parse_vendedores()
        {
            List<Vendedor> lista = new List<Vendedor>();
            for (int i = 0; i < TextoArquivo.Texto.Length; i++)
            {
                var linha = TextoArquivo.Texto[i];
                try
                {
                    var split = linha.Split(new[] { "ç" }, StringSplitOptions.RemoveEmptyEntries);
                    var parserType = parser.DetermineParseType(split);
                    if (parserType == ParseType.Vendedor)
                    {
                        var vendedor = vendedorParser.Parse(split);
                        lista.Add(vendedor);
                        Console.WriteLine($"Linha {(i + 1)}: vendedor {vendedor.Name} lido com sucesso");
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