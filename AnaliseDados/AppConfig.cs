using Microsoft.Extensions.Configuration;
using System;

namespace AnaliseDados
{
    public class AppConfig
    {
        public AppConfig(IConfigurationRoot configurationRoot)
        {
            var homepath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            DataIn = string.Concat(homepath, configurationRoot.GetSection("AppConfig")["DataIn"]);
            DataOut = string.Concat(homepath, configurationRoot.GetSection("AppConfig")["DataOut"]);
        }

        public string DataIn { get; private set; }
        public string DataOut { get; private set; }
    }
}