using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Log;

namespace JB2.Infrastructure.Test
{
    class Program
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static void Main(string[] args)
        {

            string plainText = "nEyMxJJswzxIDHzswCvyBDyIMFtCHEwDHEHGKrDqoJwxotGMuyMnquuvyApAzIxnMDzrtJqHxsxzqtLLDIFLzvHDnzMIHxrvAwGrCnAwoHHzzusMsvMHsnrpDsExsAstAMxzIJLBqJsJtwMpMzJCpAsKztzwvKvMupuwEDnyCKMoqGnqyoEEwBxKBAozHxxxLsrwDyqsqvHsxxqEpLyBzoFyABLwurHEtwnGosDBEzLsMsCGGowxCLLMDCtqJsCvzntxoqGGuFBGKnDtJrFnEFHwuHDqsqvDCJqIsCoxqHJAtsMAsAsBxqEFAwxrFxrJyIwrLrzMJrKsIvJLtGDxMHKtvHLoGssAoLGIrpvBEwvKvsFDBxAoJuxyqHAFBpGIDAMzIJFEIICqFywrDCnrqtyGGBoHJzJzxzpquEpJwoxzyuKssqBBwsvtBnGyGwELJpICuEApLBGFKBMoxAoJoonrrCuKqsnBMAtKBHvnuCIuoKpJEuxE";

            for (int i = 0; i < 2000; i++)
            {
                plainText = RandomString(400);
                string encryptText = JB2.Infrastructure.Cipher.Encrypt(plainText);
                StringBuilder sb = new StringBuilder();
                foreach (char c in encryptText)
                    sb.AppendFormat("0x{0:X2} ", (int)c);
                var encryptHEx = sb.ToString().Trim();
                //Console.WriteLine(JB2.Common.NewID.ProductID());
                //Console.WriteLine(plainText);
                //Console.WriteLine(encryptText);
                string decryptText = JB2.Infrastructure.Cipher.Decrypt(encryptText);
                //Console.WriteLine(plainText);
                //Console.WriteLine(encryptText);
                //Console.WriteLine(decryptText);
                Console.WriteLine(plainText == decryptText);
            }
            Console.ReadLine();
        }

       
    }
}
