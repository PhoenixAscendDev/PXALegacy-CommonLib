using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace JB2
{
    public static class Configuration
    {
        public static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        public static string GetConnectionString(string name)
        {
            var connection = ConfigurationManager.ConnectionStrings[name];

            return (connection != null ? connection.ConnectionString : string.Empty);
        }
        
    }
}
