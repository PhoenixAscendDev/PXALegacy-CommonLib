using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Common;
namespace JB2.Infrastructure
{
    public interface IPlayer : JB2.Common.IPerson<string> , IMetaDatable
    {
        string MasterUsername { get; set; }
        Email MasterEmail { get; set; }
        string jBeanWalletID { get; set; }
        int BitScore { get; set; }
        string FamilyID { get; set; }

             
    }
}
