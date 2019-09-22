using AnaliseDados.Models;
using AnaliseDados.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnaliseDados.Tests
{
    [TestClass]
    public class VendaTests
    {
        Parser parser = new Parser();
        VendaParser vendaParser = new VendaParser();

        [TestMethod]
        public void parse_vendas()
        {
            List<Venda> lista = new List<Venda>();
            for (int i = 0; i < TextoArquivo.Texto.Length; i++)
            {
                var linha = TextoArquivo.Texto[i];
                try
                {
                    var split = linha.Split(new[] { "ç" }, StringSplitOptions.RemoveEmptyEntries);
                    var parserType = parser.DetermineParseType(split);
                    if (parserType == ParseType.Venda)
                    {
                        var venda = vendaParser.Parse(split);
                        lista.Add(venda);
                        Console.WriteLine($"Linha {(i + 1)}: venda lida com sucesso");
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