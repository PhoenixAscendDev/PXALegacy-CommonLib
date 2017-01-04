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
        static void Main(string[] args)
        {

            string plainText = "ThisIsATestItwouldBeAwesomeIfThisActuallyWorksWhyDoesSometimeIfFails";

            for (int i = 0; i < 15; i++)
            {
                string encryptText = JB2.Infrastructure.Cipher.Encrypt(plainText);
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
