using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Common.Data
{
    public static class AzureHelper
    {
        private static readonly string _cstring = "DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}";

        public static string AccountName
        {
            get;
            set;
        }

        public static string AccountKey
        {
            get;
            set;
        }

        public static CloudStorageAccount StorageAccount
        {
            get
            {
              
                var name = AccountName;
                var key = AccountKey;
                if (string.IsNullOrEmpty(name))
                    name = "jbsquared";
                if (string.IsNullOrEmpty(key))
                    key = "";
                return GetStorageAccount(name,key);
            }
        }


        public static CloudStorageAccount GetStorageAccount(string accountName, string accountKey)
        {
            var cstring = string.Format(_cstring, accountName, accountKey);
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(cstring);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return storageAccount;

        }
    }
}