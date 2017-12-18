using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ApiKeySecretPair : IAPIKeySecretPair
    {
        public string APIkey
        {
            get;set;
            
        }

        public string Secret
        {
            get; set;

        }
    }
}
