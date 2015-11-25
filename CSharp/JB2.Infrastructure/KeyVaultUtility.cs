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


//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Blob;

namespace JB2.Infrastructure
{
    public class KeyVaultUtility
    {
        private const string ADCLIENTID = "JB2:AD-clientID";
        private const string ADCLIENTKEY = "JB2:AD-clientSecret";
        private const string VAULTURI = "JB2:vaultUri";
        private const string RSAKeyID1 = "JB2:RSAKey1-ID";

        public static string CreateRSAKey(string secretName)
        {
            KeyVaultClient cloudVault = JB2KeyVaultClient;
            string vaultUri = CloudConfigurationManager.GetSetting(VAULTURI);

            try
            {
                // Delete the secret if it exists.
                cloudVault.DeleteSecretAsync(vaultUri, secretName).GetAwaiter().GetResult();
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
            Secret cloudSecret = cloudVault.SetSecretAsync(vaultUri, secretName, symmetricBytes, null, "application/octet-stream").GetAwaiter().GetResult();

            // Return the base identifier of the secret. This will be resolved to the current version of the secret.
            return cloudSecret.SecretIdentifier.BaseIdentifier;
        }

        public static KeyVaultClient JB2KeyVaultClient
        {
            get
            {
                var client =  new KeyVaultClient(GetAccessToken);
                return client;
            }
        }
        public static KeyVaultKeyResolver Resolver
        {
            get
            {
                return new KeyVaultKeyResolver(GetAccessToken);
            }
        }

        public static IKey RSAKey1
        {
            get
            {
                return GetKey(CloudConfigurationManager.GetSetting(RSAKeyID1));
            }
        }

        public static IKey GetKey(string keyID)
        {
            // If there are multiple key sources like Azure Key Vault and local KMS, set up an aggregate resolver as follows.
            // This helps users to define a plug-in model for all the different key providers they support.
           // AggregateKeyResolver aggregateResolver = new AggregateKeyResolver()
           //     .Add(Resolver);

            // Set up a caching resolver so the secrets can be cached on the client. This is the recommended usage
            // pattern since the throttling targets for Storage and Key Vault services are orders of magnitude
            // different.
            //CachingKeyResolver cachingResolver = new CachingKeyResolver(2, aggregateResolver);

            // Create a key instance corresponding to the key ID. This will cache the secret.
            IKey cloudKey = Resolver.ResolveKeyAsync(keyID, CancellationToken.None).GetAwaiter().GetResult();

            return cloudKey;
        }

        public static async Task<string> GetAccessToken(string authority, string resource, string scope)
        {

            ClientCredential credential = new ClientCredential(CloudConfigurationManager.GetSetting(ADCLIENTID),
                                                CloudConfigurationManager.GetSetting(ADCLIENTKEY));

            AuthenticationContext ctx = new AuthenticationContext(new Uri(authority).AbsoluteUri, false);
            AuthenticationResult result = await ctx.AcquireTokenAsync(resource, credential);

            return result.AccessToken;
        }

    }
}
