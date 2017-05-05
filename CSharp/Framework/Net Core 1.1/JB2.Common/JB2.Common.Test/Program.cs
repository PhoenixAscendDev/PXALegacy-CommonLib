using System;

using Microsoft.WindowsAzure.Storage;
using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage.Table;


using System.Net;
using System.IO;

using JB2.Common;

using System.Text;

namespace JB2.Common.Test
{

    public class Dude : IDNamePair<string,Name>, IIDNamePair<string, Name>
    {
        public string PropertyName123 { get; set; }
    }
    class Program
    {

        static uint Adler32(byte[] bytesArray, int byteStart, int bytesToRead, uint checksum = 1)
        {
            int n;
            uint s1 = checksum & 0xFFFF;
            uint s2 = checksum >> 16;

            while (bytesToRead > 0)
            {
                n = (3800 > bytesToRead) ? bytesToRead : 3800;
                bytesToRead -= n;

                while (--n >= 0)
                {
                    s1 = s1 + (uint)(bytesArray[byteStart++] & 0xFF);
                    s2 = s2 + s1;
                }

                s1 %= 65521;
                s2 %= 65521;
            }

            checksum = (s2 << 16) | s1;
            return checksum;
        }


        static string ConvertToHash(string str)
        {
            byte[] sMsgBytes = Encoding.Unicode.GetBytes(str);
            uint hashnumber = Adler32(sMsgBytes, 0, sMsgBytes.Length);
            return hashnumber.ToString("X2");

        }


        static JB2.Common.ServiceResult Task1()
        {
            Console.WriteLine("In Task1");

            return true;
        }

        static JB2.Common.ServiceResult Task2()
        {
            Console.WriteLine("In Task2");

            return new ServiceResult(new Exception("This is an exception"));
        }

        static JB2.Common.ServiceResult Task3()
        {
            Console.WriteLine("In Task3");

            return true;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            //DynamicTableEntity e = new DynamicTableEntity();

            //e.SetProperty<bool>("Enabled",true);

            var workflow = new JB2.Common.Scheduler.SimpleWorkflow(JB2.Common.Log.ConsoleLogger.Instance);
            workflow.EnableLoging();

            
            workflow.Add(new JB2.Common.Scheduler.JobWork(Task1));
            workflow.Add(new JB2.Common.Scheduler.JobWork(Task2));
            workflow.Add(new JB2.Common.Scheduler.JobWork(Task3));

            workflow.StartJobsAsync().Wait();





            //for (int i = 0; i < 0; i++)
            //{
            //    var subject = JB2.Common.NewID.Guid();
            //    var urlstr = "http://id.jbsquared.com/test/?subject={0}";
            //    Console.WriteLine(subject);
            //    Console.WriteLine(JB2.Common.NewID.UriHash("PA-{0}", new Uri(string.Format(urlstr, subject))));
            //    Console.WriteLine(JB2.Common.NewID.UriHash("PA-{0}", new Uri(string.Format(urlstr, subject)),Enum.HashType.NetHashCode));
            //    Console.WriteLine(JB2.Common.NewID.UriHash("PA-{0}", new Uri(string.Format(urlstr, subject)), Enum.HashType.Adler32));
            //    Console.WriteLine(JB2.Common.NewID.UriHash("PA-{0}", new Uri(string.Format(urlstr, subject)), Enum.HashType.MD5));

            //    //var playerid = JB2.Common.NewID.UriHash("PA-{0}",new Uri(string.Format(urlstr, subject)));
            //}

            //var pair = new IDNamePair<string, string>();

            //pair.ID = "IDValue";
            //pair.Name = "NameValue";


            //var pairEntity = new DynamicTableEntity();
            //pairEntity.Properties = pair.ToEntityProperties();

            //var pair2 = pairEntity.ToObject<IDNamePair<string, string>>(new IDNamePair<string, string>() { ID = "eset" });

            //var dude = new Dude() { ID = "IDTest", Name = new Name() { First = "FirstTest", Last = "LastTest" }, PropertyName123 = "TestProperty" };

            //var dudeEntity = new DynamicTableEntity();
            //dudeEntity.Properties = dude.ToEntityProperties();


            //var dude2 = dudeEntity.ToObject<Dude>(new Dude() { ID = "default" });

            //Console.WriteLine(dude == dude2);


            //string _connectionString = "DefaultEndpointsProtocol=https;AccountName=jb2idc4ews41t736wok1;AccountKey=lrbqd8UuHgSwUQ9i+LKzs7P0ohZBVEsyTE187AopxYnPhzxU3GxoLo1eQ/gv9EcpCrCJ/P1i1gzWynM6VkcBoA==";
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            //var account = StorageAccount.FromAzureStorage(storageAccount);

            //var e =  account.GetTable("player").GetEntity<DynamicTableEntity>("testp", "testr");

            //var p = account.GetBlog("general").GetByteArray("avatar/partisan plot.jpg");

            //account.GetBlog("general").Insert(p, "avatar/new.jpg");

            //Console.WriteLine(p.Length);
            Console.ReadLine();
        }
    }
}