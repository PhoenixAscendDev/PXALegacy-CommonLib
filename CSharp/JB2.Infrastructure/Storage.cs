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
        private const string STORAGEKEYNAME = "JB2:generalStorageKey";
        private const string BOWTIEKEYNAME = "JB2:bowtieStorageKey";
        public static StorageAccount GeneralAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[STORAGEKEYNAME];
                return StorageAccount.FromAzureStorage("jbsquared", storagekey);
                //return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }
        }

        public static StorageAccount BowtieAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[STORAGEKEYNAME];
                return StorageAccount.FromAzureStorage("jb2bowtie", storagekey);
                //return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }
        }
    }
}
