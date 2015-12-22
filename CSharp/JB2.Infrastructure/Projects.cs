using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace JB2.Infrastructure
{
    public class Projects
    {
        private const string PRODJECTCODE = "JB2:projectCode";

        public static JB2Project  Project(string projectCode)
        {
            return new JB2Project(projectCode);
        }
        public static JB2Project CurrentProject
        {
            get
            {
                return new JB2Project(ConfigurationManager.AppSettings[PRODJECTCODE]);
            }
        }
    }
}
