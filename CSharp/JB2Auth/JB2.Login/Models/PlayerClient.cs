using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Login
{
    public class PlayerClient
    {
        public string PlayerID { get; set; }
        public string ClientID { get; set; }
        public Enum.ClaimScope[] Scope { get; set; }
    }
}
