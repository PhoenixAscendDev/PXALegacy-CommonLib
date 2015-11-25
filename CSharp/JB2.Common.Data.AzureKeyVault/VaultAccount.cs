using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Core;


namespace JB2.Common.Data
{
    public class AzureVaultAccount : JB2.Common.Data.IVaultAccount
    {
        #region Fields
        private KeyVaultClient _azureclient;
        private KeyVaultKeyResolver _resolver;
        #endregion Fields

        #region Constructors
        public AzureVaultAccount(KeyVaultClient client, KeyVaultKeyResolver resolver)
        {
            _azureclient = client;
            _resolver = resolver;
        }

        #endregion Constructors

        #region Properties

        public object GetObject(IVaultKey key)
        {
            throw new NotImplementedException();
        }

        public void StoreObject(IVaultKey key, object thing)
        {
            throw new NotImplementedException();
        }

        public KeyVaultClient KeyVaultClient
        {
            get
            {
                return _azureclient;
            }
        }

        public KeyVaultKeyResolver KeyVaultKeyResolver
        {
            get
            {
                return _resolver;
            }
        }
        #endregion Properties

        #region Implicit Operator
        public static implicit operator KeyVaultClient(AzureVaultAccount a)
        {
            return a.KeyVaultClient;
        }

        public static implicit operator KeyVaultKeyResolver(AzureVaultAccount a)
        {
            return a.KeyVaultKeyResolver;
        }

        #endregion Implicit Operator


        public AzureKey GetKey(string keyID)
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
            IKey cloudKey = this._resolver.ResolveKeyAsync(keyID, System.Threading.CancellationToken.None).GetAwaiter().GetResult();



            return new AzureKey(cloudKey);
        }

    }
}
