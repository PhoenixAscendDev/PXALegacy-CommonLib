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
        private const string SOFTWAREKEYNAME = "JB2:softwareStorageKey";
        private const string SPROGKEYNAME = "JB2:sprogStorageKey";
        private const string LOGKEYNAME = "JB2:logKey";

        private const string ECONOMYKEYNAME = "JB2:economyStorageKey";
        private const string ECONOMYACCOUNT = "JB2:economy";

        
        private const string ASSETTABLENAME = "assets";
        private const string JBEAN_TABLENAME = "currency-jbean";
        

        public static StorageAccount SprogAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[SPROGKEYNAME];
                return StorageAccount.FromAzureStorage("jb2sprog", storagekey);
                //return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }
        }



        public static StorageAccount GeneralAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[STORAGEKEYNAME];
                return StorageAccount.FromAzureStorage("jbsquared", storagekey);
                //return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }
        }

        public static StorageAccount SoftwareAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[SOFTWAREKEYNAME];
                return StorageAccount.FromAzureStorage("jb2softwaredata", storagekey);
                //return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }
        }

        public static StorageAccount BowtieAccount
        {
            get
            {
                var storagekey = ConfigurationManager.AppSettings[BOWTIEKEYNAME];
                return StorageAccount.FromAzureStorage("jb2bowtie", storagekey);
                //return AzureHelper.GetStorageAccount("jbsquared7", storagekey);
            }
        }

        public static StorageAccount LogAccount
        {
            get
            {
                var key = ConfigurationManager.AppSettings[LOGKEYNAME];
                return StorageAccount.FromAzureStorage("jb2log", key);
            }
        }

        public static StorageAccount EconomyAccount
        {
            get
            {
                var key = ConfigurationManager.AppSettings[ECONOMYKEYNAME];
                var account = ConfigurationManager.AppSettings[ECONOMYACCOUNT];
                return StorageAccount.FromAzureStorage(account, key);
            }
        }

        public static AzureTableRepository AssetTable
        {
            get
            {
                return GeneralAccount.GetTable(ASSETTABLENAME);
            }
        }

        public static AzureTableRepository JBeanTable
        {
            get
            {
                return BowtieAccount.GetTable(JBEAN_TABLENAME);
            }
        }
    }

}
