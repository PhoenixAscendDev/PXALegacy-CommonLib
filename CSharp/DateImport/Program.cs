using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using JB2.Common;

namespace DateImport
{
    class Program
    {

        static CloudStorageAccount StorageAccount
        {
            get
            {
                var cstring = "DefaultEndpointsProtocol=https;AccountName=jbsquared;AccountKey=iGf7AhI5v12hig85TsVkJSuPwvB42EncTMFogXFcqGlEcVMo5oXf0PvsMkxOQQeDmM21UtqHHtwyjR3MG3Di5g==";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(cstring);

                return storageAccount;
            }
        }


        static void LoadData(int datekey)
        {
            var storageAccount = StorageAccount;

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("calendardate");
            table.CreateIfNotExists();

            double count = ( (DateTime)JB2Date.MaxDate() - (DateTime)JB2Date.MinDate()).TotalDays;

            JB2.Common.JB2Date startDate = new JB2.Common.JB2Date(datekey);
            TableBatchOperation batch = new TableBatchOperation();
            int batchCount = 0;
            string partkey = string.Empty;
            for(int i=0; i<= count; i++)
            {
                 var date =  (JB2Date)((DateTime)startDate).AddDays(i);

                 JB2DateEntry entry = new JB2DateEntry("IsWeekend", "datekey_" + date.DateKey.ToString(), date);

                 if ((batchCount == 100 || partkey != entry.PartitionKey ) && (batch.Count() > 0))
                 {

                     table.ExecuteBatch(batch);
                     batchCount = 0;
                     batch = new TableBatchOperation();
                     
                 }



                 if(entry != null && entry.IsWeekend)
                 {
                     batch.Insert(entry);
                     batchCount++;
                     partkey = entry.PartitionKey;
                 }
            }

            if(batch.Count() > 0 )
            {
                table.ExecuteBatch(batch);
                batchCount = 0;
                batch = new TableBatchOperation();
            }
            Console.WriteLine();
            Console.Write("LoadData Complete");
            //return 1;

        }

        static void LoadData1()
        {
            LoadData(JB2.Common.JB2Date.MinDate());
        }


        static void Main(string[] args)
        {

            System.Threading.Thread t = new System.Threading.Thread(LoadData1);
            t.Start();

            Console.ReadLine();
        }
    }
}
