using AnaliseDados.Models;
using AnaliseDados.Parsers;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnaliseDados
{
    public class RelatorioListener
    {
        private FileSystemWatcher watcher;

        private Parser parser = new Parser();
        private VendedorParser vendedorParser = new VendedorParser();
        private ClienteParser clienteParser = new ClienteParser();
        private VendaParser vendaParser = new VendaParser();

        public RelatorioListener()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = Program.AppConfig.DataIn;
            watcher.Created += Watcher_Created;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                ProcessarArquivo(e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ProcessarArquivo(FileSystemEventArgs e)
        {
            Console.WriteLine($"Processando arquivo {e.FullPath}...");
            var relatorio = LerArquivo(e.FullPath);

            Console.WriteLine($"Dados lidos. Gerando relatório na pasta de saída...");

            var dirOut = Program.AppConfig.DataOut;
            var dataOut = Path.Combine(dirOut, $"{DateTime.Now:yyyyMMddHHmmssfff}-{e.Name}");
            GravarRelatorio(relatorio, dataOut);

            Console.WriteLine($"Relatório gerado no arquivo {dataOut}");
        }

        private void GravarRelatorio(Relatorio relatorio, string dataOut)
        {
            var saida = new List<string>();

            saida.Add($"Quantidade de clientes no arquivo de entrada: {relatorio.Clientes.Count}");
            saida.Add($"Quantidade de vendedores no arquivo de entrada: {relatorio.Vendedores.Count}");
            saida.Add($"ID da venda mais cara: {relatorio.GetIdVendaMaisCara()}");
            saida.Add($"O pior vendedor: {relatorio.GetPiorVendedor()}");

            File.WriteAllLines(dataOut, saida);
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
        }

        private Relatorio LerArquivo(string fullPath)
        {
            var relatorio = new Relatorio();
            var data = File.ReadAllLines(fullPath);

            foreach (var item in data)
            {
                var split = item.Split(new[] { Constants.SplitCharacter }, StringSplitOptions.RemoveEmptyEntries);
                switch (parser.DetermineParseType(split))
                {
                    case ParseType.Vendedor:
                        relatorio.Vendedores.Add(vendedorParser.Parse(split));
                        break;
                    case ParseType.Cliente:
                        relatorio.Clientes.Add(clienteParser.Parse(split));
                        break;
                    case ParseType.Venda:
                        relatorio.Vendas.Add(vendaParser.Parse(split));
                        break;
                    default:
                        throw new Exception("Arquivo com dados inválidos");
                }
            }

            return relatorio;
        }
    }
}