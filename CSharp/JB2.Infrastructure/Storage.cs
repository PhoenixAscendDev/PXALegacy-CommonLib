using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using JB2.Common;
using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage;

namespace JB2.Infrastructure
{
    public static class Storage
    {
        private const string STORAGEKEYNAME = "JB2-StorageKey";
        public static CloudStorageAccount GeneralCloudAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[STORAGEKEYNAME];

                return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }

        }
    }
}
