using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Core;

namespace JB2.Common.Data.AzureKeyVault
{
    public static class AzureKeyVaultHelper
    {
        private static AzureVaultAccount _client;



        public static AzureVaultAccount Account
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }

        public static string CreateRSAKey(string vaultUri,string secretName)
        {
            return CreateRSAKey(Account, vaultUri,secretName);
        }
        public static string CreateRSAKey(AzureVaultAccount client, string vaultUri,string secretName)
        {
            KeyVaultClient kvClient = client.KeyVaultClient;

            try
            {
                // Delete the secret if it exists.
                kvClient.DeleteSecretAsync(vaultUri, secretName).GetAwaiter().GetResult();
            }
            catch (KeyVaultClientException ex)
            {
                if (ex.Status != System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Unable to access the specified vault. Please confirm the KVClientId, KVClientKey, and VaultUri are valid in the app.config file.");
                    Console.WriteLine("Also ensure that the client ID has previously been granted full permissions for Key Vault secrets using the Set-AzureKeyVaultAccessPolicy command with the -PermissionsToSecrets parameter.");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadLine();
                    throw;
                }
            }
            // Create a 256bit symmetric key and convert it to Base64.
            SymmetricKey symmetricKey = new SymmetricKey(secretName, SymmetricKey.KeySize256);
            string symmetricBytes = Convert.ToBase64String(symmetricKey.Key);

            // Store the Base64 of the key in the key vault. Note that the content-type of the secret must
            // be application/octet-stream or the KeyVaultKeyResolver will not load it as a key.
            Secret cloudSecret = kvClient.SetSecretAsync(vaultUri, secretName, symmetricBytes, null, "application/octet-stream").GetAwaiter().GetResult();

            // Return the base identifier of the secret. This will be resolved to the current version of the secret.
            return cloudSecret.SecretIdentifier.BaseIdentifier;
        }




    }
}
