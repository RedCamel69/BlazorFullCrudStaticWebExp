using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.HelperService
{
    internal class HelperService : IHelperService
    {
        private IConfigurationRoot _config;

        public HelperService(IFunctionsHostBuilder builder) {

        _config = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();


          
        }
        public bool GetAppSetting(string settingName)
        {
           return Convert.ToBoolean(_config["StudentService-GetStudents"]);
        }
    }
}
