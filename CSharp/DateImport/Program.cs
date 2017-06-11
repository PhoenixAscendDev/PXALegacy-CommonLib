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
                var cstring = "DefaultEndpointsProtocol=https;AccountName=jb2sprog;AccountKey=leo2MGKYV+F3HOdAejr7NPHGVaYyM4u+bUKQCMv35KSirfZEv1CCtDiWQdZFZoChLlQa2a+bBC50cST3PQgcJA==";
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


        static void LoadTime()
        {
            var storageAccount = StorageAccount;

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("time");
            table.CreateIfNotExists();

            TableBatchOperation primaryBatch = new TableBatchOperation();
            int primarybatchCount = 0;
            string primarypartkey = string.Empty;


            for (int i = 86399; i <= 86399;i++)
            {
                var t = JB2Time.FromSecondsInDay(i);

                JB2TimeEntry entry = new JB2TimeEntry("time", "timekey_" + t.TimeKey.ToString(), t);

                if ((primarybatchCount == 100 || primarypartkey != entry.PartitionKey) && (primaryBatch.Count() > 0))
                {

                    table.ExecuteBatch(primaryBatch);
                    primarybatchCount = 0;
                    primaryBatch = new TableBatchOperation();
                }




                //if ((daybatchCount == 100 || daypartkey != entry.PartitionKey) && (dayBatch.Count() > 0))
                //{

                //    table.ExecuteBatch(dayBatch);
                //    daybatchCount = 0;
                //    dayBatch = new TableBatchOperation();
                //}


                if (entry != null)
                {
                    primaryBatch.InsertOrReplace(entry);
                    primarybatchCount++;
                    primarypartkey = entry.PartitionKey;
                }

                //if (daynightEntry != null)
                //{
                //    dayBatch.Insert(daynightEntry);
                //    daybatchCount++;
                //    daypartkey = daynightEntry.PartitionKey;
                //}

                //JB2TimeEntry tentry = new JB2TimeEntry("timekey_" + t.TimeKey.ToString(), "" + t.TimeKey.ToString(), t);

                //JB2TimeEntry hentry = new JB2TimeEntry("time:hour:" + t.Hour.ToString(), "timekey_" + t.TimeKey.ToString(), t);

                //JB2TimeEntry daynightEntry = new JB2TimeEntry("time:daynight:" + t.DayNight, "timekey_" + t.TimeKey.ToString(), t);

                //JB2TimeEntry ampmEntry = new JB2TimeEntry("time:ampm:" + t.AmPm, "timekey_" + t.TimeKey.ToString(), t);


                //TableOperation insertOperation = TableOperation.InsertOrReplace(tentry);
                //table.Execute(insertOperation);

                //insertOperation = TableOperation.InsertOrReplace(hentry);
                //table.Execute(insertOperation);

                //insertOperation = TableOperation.InsertOrReplace(daynightEntry);
                //table.Execute(insertOperation);

                //insertOperation = TableOperation.InsertOrReplace(ampmEntry);
                //table.Execute(insertOperation);



            }

            if (primaryBatch.Count() > 0)
            {
                table.ExecuteBatch(primaryBatch);
                primarybatchCount = 0;
                primaryBatch = new TableBatchOperation();
            }


            Console.WriteLine();
            Console.Write("LoadData Complete");

        }


        static void LoadTemp()
        {

            var storageAccount = StorageAccount;

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("temperature");
            table.CreateIfNotExists();

            TableBatchOperation primaryBatch = new TableBatchOperation();
            int primarybatchCount = 0;
            string primarypartkey = string.Empty;



            decimal t = 0.000m;
            for (int i = 0; i <= 900000; i++)
            {
                var temp = new JB2.Common.Temperature(Convert.ToDouble(t));

                var entry = new JB2TempEntry("temperature", "tempkey_", temp);
                entry.RowKey = "tempkey_" + entry.TempKey;

                if ((primarybatchCount == 100 || primarypartkey != entry.PartitionKey) && (primaryBatch.Count() > 0))
                {


                    table.ExecuteBatch(primaryBatch);
                    primarybatchCount = 0;
                    primaryBatch = new TableBatchOperation();
                }


                if (entry != null)
                {
                    primaryBatch.InsertOrReplace(entry);
                    primarybatchCount++;
                    primarypartkey = entry.PartitionKey;
                }


                Console.WriteLine(entry.RowKey);



                t = decimal.Add(t, 0.001m);
                //Console.WriteLine(t.ToString());
            }
            if (primaryBatch.Count() > 0)
            {
                table.ExecuteBatch(primaryBatch);
                primarybatchCount = 0;
                primaryBatch = new TableBatchOperation();
            }


            Console.WriteLine();
            Console.Write("LoadData Complete");


        }



        static void Main(string[] args)
        {

            System.Threading.Thread t = new System.Threading.Thread(LoadTemp);
            t.Start();

            Console.ReadLine();
        }
    }
}
