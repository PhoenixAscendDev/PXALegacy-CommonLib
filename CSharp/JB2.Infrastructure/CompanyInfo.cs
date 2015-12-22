using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2
{
    public static class Info 
    {

        public static JB2.Infrastructure.HQAddress _address;
        public static JB2.Infrastructure.Business _hq;       
        public static IBusiness HQ
        { 
            get
            {
                if(_hq == null)
                {
                    _hq = new Infrastructure.Business();
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
        public static string SupportEmail()
        {
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






    }
}
