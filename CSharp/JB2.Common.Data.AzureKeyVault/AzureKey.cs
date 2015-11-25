using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.KeyVault.Core;


namespace JB2.Common.Data
{
    public class AzureKey : IVaultKey
    {

        private IKey _key;

        public AzureKey(IKey key)
        {
            _key = key;
        }

        public IKey Key
        {
            get
            {
                return _key;
            }
        }
    }
}
