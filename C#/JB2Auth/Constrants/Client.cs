using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constrants
{
    public static class Clients
    {
        public readonly static Client Client1 = new Client
        {
            Id = "42ff5dad3c274c97a3a7c3d44b67bb42",
            Secret = "AGKN5JestC5ZAPky86oawZUPq7y6E99XuQXMz2JwrFLfumsbyX2rbCgFphJbSNX9pw==",
            RedirectUrl = Paths.AuthorizeCodeCallBackPath
        };

        public readonly static Client Client2 = new Client
        {
            Id = "7890ab",
            Secret = "7890ab",
            RedirectUrl = Paths.ImplicitGrantCallBackPath
        };
    }

    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string RedirectUrl { get; set; }
    }
}
