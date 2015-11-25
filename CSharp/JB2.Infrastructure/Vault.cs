using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

using Microsoft.Azure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Core;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

using JB2.Common.Data;

namespace JB2.Infrastructure
{
    public static class Vault
    {
        private const string RSAKeyID1 = "JB2:RSAKey1-ID";
        private const string JB2VaultURI = "JB2:vaultUri";

        public  static JB2.Common.Data.AzureVaultAccount GeneralVault
        {
            get
            {
                var client = new KeyVaultClient(KeyVaultUtility.GetAccessToken);
                var resolver = new KeyVaultKeyResolver(KeyVaultUtility.GetAccessToken);

                return new AzureVaultAccount(client, resolver);
            }
        }

        public static IKey RSAKey1
        {
            get
            {
                return GeneralVault.GetKey(CloudConfigurationManager.GetSetting(RSAKeyID1)).Key;
            }
        }










    }
}
