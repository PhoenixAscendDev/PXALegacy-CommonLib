using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using JB2.Common.Log;

namespace JB2.Infrastructure.Test
{
    class Program
    {

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static void Main(string[] args)
        {

            string plainText = "nEyMxJJswzxIDHzswCvyBDyIMFtCHEwDHEHGKrDqoJwxotGMuyMnquuvyApAzIxnMDzrtJqHxsxzqtLLDIFLzvHDnzMIHxrvAwGrCnAwoHHzzusMsvMHsnrpDsExsAstAMxzIJLBqJsJtwMpMzJCpAsKztzwvKvMupuwEDnyCKMoqGnqyoEEwBxKBAozHxxxLsrwDyqsqvHsxxqEpLyBzoFyABLwurHEtwnGosDBEzLsMsCGGowxCLLMDCtqJsCvzntxoqGGuFBGKnDtJrFnEFHwuHDqsqvDCJqIsCoxqHJAtsMAsAsBxqEFAwxrFxrJyIwrLrzMJrKsIvJLtGDxMHKtvHLoGssAoLGIrpvBEwvKvsFDBxAoJuxyqHAFBpGIDAMzIJFEIICqFywrDCnrqtyGGBoHJzJzxzpquEpJwoxzyuKssqBBwsvtBnGyGwELJpICuEApLBGFKBMoxAoJoonrrCuKqsnBMAtKBHvnuCIuoKpJEuxE";
             plainText = "a";
            // ...

            Stopwatch sw = new Stopwatch();

            int testloop = 10;
            sw.Start();
            for (int i = 0; i < testloop; i++)
            {
                plainText = RandomString(400);
                plainText = "aaaaa";
                string encryptText = JB2.Infrastructure.Cipher.Encrypt(plainText);

                //Console.WriteLine("plain:" + "(" + plainText.Count() + ")" + plainText);
                //Console.WriteLine("");
                //Console.WriteLine("encrypt:" + "(" + encryptText.Count() + ")" + encryptText);
                //Console.WriteLine("");
                StringBuilder sb = new StringBuilder();
                //foreach (char c in encryptText)
                //    sb.AppendFormat("0x{0:X2} ", (int)c);
                //var encryptHEx = sb.ToString().Trim();
                //Console.WriteLine(JB2.Common.NewID.ProductID());
                //Console.WriteLine(plainText);
                //Console.WriteLine(encryptText);
                string decryptText = JB2.Infrastructure.Cipher.Decrypt(encryptText);
                //Console.WriteLine(plainText);
                //Console.WriteLine();
                //Console.WriteLine("decript:" + "(" + decryptText.Count() + ")" + decryptText);
                //Console.WriteLine("");
               Console.WriteLine( (plainText == decryptText) && (plainText != encryptText));
            }

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);

            sw.Reset();
            sw.Start();
            for (int i = 0; i < testloop; i++)
            {
                plainText = RandomString(400);

                var et = JB2.Infrastructure.Cipher.Encrypt2(plainText);

               //Console.WriteLine("----Decrypt----");

                var dt = JB2.Infrastructure.Cipher.Decrypt2(et);

                Console.WriteLine((plainText == dt) && (et != plainText));
            }

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);



            Console.ReadLine();
        }

       
    }
}
