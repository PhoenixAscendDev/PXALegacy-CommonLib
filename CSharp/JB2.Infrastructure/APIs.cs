using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace JB2.Infrastructure
{
    public static class API
    {
        private const string IDENTITY = "JB2:api-identity";
        private const string JBEAN = "JB2:api-jbean";
        private const string BOWTIE = "JB2:api-bowtie";
        private const string INFRAST = "JB2:api-infrastructure";

        public static Uri IdentityEndpoint
        {
            get
            {
                return new Uri(ConfigurationManager.AppSettings[IDENTITY]);
            }
        }

        public static Uri jBeanEndpoint
        {
            get
            {
                return new Uri(ConfigurationManager.AppSettings[JBEAN]);
            }
        }

        public static Uri BowtieEndpoint
        {
            get
            {
                return new Uri(ConfigurationManager.AppSettings[BOWTIE]);
            }
        }

        public static Uri InfrastructureEndpoint
        {
            get
            {
                return new Uri(ConfigurationManager.AppSettings[INFRAST]);
            }
        }
    }
}
