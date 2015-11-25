using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Data
{
    public interface IVaultAccount
    {
        object GetObject(IVaultKey key);

        void StoreObject(IVaultKey key, object thing);
    }
}
