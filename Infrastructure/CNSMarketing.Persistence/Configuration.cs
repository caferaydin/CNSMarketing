using System;
using Microsoft.Extensions.Configuration;

namespace CNSMarketing.Persistence;

static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                try
                {
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/CNSMarketing.API"));
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                    configurationManager.AddJsonFile("appsettings.Development.json");
                }

                return configurationManager.GetConnectionString("SqlServer");
            }
        }
    }
