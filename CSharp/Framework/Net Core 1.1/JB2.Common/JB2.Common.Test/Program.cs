using System;

using Microsoft.WindowsAzure.Storage;
using JB2.Common.Data;
using Microsoft.WindowsAzure.Storage.Table;

using System.Text;

namespace JB2.Common.Test
{
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

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            for(int i=0;i < 10; i++)
            {
                var subject = "203cd762-a99e-4126-9b62-7c80f044a557";
                var urlstr = "http://id.jbsquared.com/test/?subject={0}";
                //var playerid = JB2.Common.NewID.UriHash("PA-{0}",new Uri(string.Format(urlstr, subject)));

                var playerid = "PA-" + ConvertToHash(string.Format(urlstr, subject));

                Console.WriteLine(playerid);
            }

            string _connectionString = "DefaultEndpointsProtocol=https;AccountName=jb2idc4ews41t736wok1;AccountKey=lrbqd8UuHgSwUQ9i+LKzs7P0ohZBVEsyTE187AopxYnPhzxU3GxoLo1eQ/gv9EcpCrCJ/P1i1gzWynM6VkcBoA==";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connectionString);
            var account = StorageAccount.FromAzureStorage(storageAccount);

            var e =  account.GetTable("player").GetEntity<DynamicTableEntity>("testp", "testr");
            Console.WriteLine(e);
            Console.ReadLine();
        }
    }
}