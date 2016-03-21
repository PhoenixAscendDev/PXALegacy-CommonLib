using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Common
{
    public class Business : IBusiness<string>,IBusiness
    {
        public string ID { get; set; }
        

        public IAddress MailingAddress
        {
            get;set;
            
            
        }

        public string Name { get; set; }
        

        public IPerson<string> POC
        {
            get;set;
            
        }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
