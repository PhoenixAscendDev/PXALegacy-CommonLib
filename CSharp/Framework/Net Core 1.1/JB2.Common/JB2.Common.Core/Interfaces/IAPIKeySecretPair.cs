using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IAPIKeySecretPair
    {
        string APIkey { get; set; }
        string Secret { get; set; }
    }
}
