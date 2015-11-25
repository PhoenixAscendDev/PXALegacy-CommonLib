using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IPlayer : JB2.Common.IPerson<string>, IMetaDatable
    {
        IMetaData GetModuleMetaData(string module, string propertyName);

        string GetMasterUsername();
        string GetMasterEmail();

        string GetjBeanAccountNumber();

        int GetBitScore();

        string GetFamilyID();

    }
}
