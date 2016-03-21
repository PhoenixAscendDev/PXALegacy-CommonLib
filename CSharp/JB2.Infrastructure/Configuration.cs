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
        private const string CURRENCYJBEAN = "JB2:jbeanCurrencyID";
        public static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        public static string GetConnectionString(string name)
        {
            var connection = ConfigurationManager.ConnectionStrings[name];

            return (connection != null ? connection.ConnectionString : string.Empty);
        }

        public static string GetjBeanCurrencyID()
        {

            return GetAppSetting(CURRENCYJBEAN);
            //var demoinations = new JB2.Economy.IDenomination[3] {  new JB2.Economy.JBeanDenomination(JB2.Economy.Enum.JBeanTokenType.Kidney),
            //                                                new JB2.Economy.JBeanDenomination(JB2.Economy.Enum.JBeanTokenType.Navy),
            //                                                new JB2.Economy.JBeanDenomination(JB2.Economy.Enum.JBeanTokenType.Pinto)
            //                                              };
            //JB2.Economy.ICurrency jBeanCurrency = new JB2.Economy.JBean()
            //{
            //    ID = GetAppSetting(CURRENCYJBEAN),
            //    Denominations = demoinations,
            //    Name = "jBean",
            //    PluralName = "jBeans"
            //};

            //return jBeanCurrency;


        }
        
    }
}
