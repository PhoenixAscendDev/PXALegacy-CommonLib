using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Web
{
    public interface ITransportConfig
    {
        string Endpoint { get; set; }

        int Port { get; set; }
    }
}
