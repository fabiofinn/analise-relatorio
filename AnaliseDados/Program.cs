using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AnaliseDados
{
    class Program
    {
        public static AppConfig AppConfig;

        private static RelatorioListener listener;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            AppConfig = new AppConfig(configuration);

            if (!Directory.Exists(AppConfig.DataIn))
                Directory.CreateDirectory(AppConfig.DataIn);

            if (!Directory.Exists(AppConfig.DataOut))
                Directory.CreateDirectory(AppConfig.DataOut);

            Console.WriteLine("Escutando arquivos da pasta: " + AppConfig.DataIn);
            Console.WriteLine("Pasta de destino do relatório: " + AppConfig.DataOut);

            listener = new RelatorioListener();
            listener.Start();

            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();

            listener.Stop();
        }
    }
}