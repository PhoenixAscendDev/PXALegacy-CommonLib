using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using JB2.Infrastructure.Data;
using JB2.Common;

using JB2.Infrastructure;

namespace JB2
{
    public static class Info 
    {
        private const string PRODJECTCODE = "JB2:projectCode";
        public static JB2.Infrastructure.HQAddress _address;
        public static JB2.Common.Business _hq;  

        
        public static IBusiness HQ
        { 
            get
            {
                
                if(_hq == null)
                {
                    _hq = new Business();
                    _hq.ID = "jb2-centreville";
                    _hq.Name = "JBsquared LLC";
                    _hq.MailingAddress = new JB2.Infrastructure.HQAddress();
                    _hq.POC = JB2.Infrastructure.People.JB;
                }
                return _hq;
            }          
        }

        public static IAddress MailingAddress
        {
            get
            {
                if (_address == null)
                    _address = new JB2.Infrastructure.HQAddress();
                return _address;
            }
        }
        public static string SupportEmail
        {
            get
            {
                return "support@jbsquared.com";
            }
        }

        public static string SalesEmail
        {
            get
            {
                return "sales@jbsquared.com";
            }
        }

        public static JB2Project Project
        {
            get
            {
                JB2Project project = null;

                try
                {
                   project = (JB2Project)JB2.Infrastructure.Projects.Project(ConfigurationManager.AppSettings[PRODJECTCODE]);

                    if (project == null)
                        throw new NullReferenceException();
                }
                catch(Exception ex)
                {
                    project = JB2Project.Empty;
                }


                return project;
            }
        }






    }
}
