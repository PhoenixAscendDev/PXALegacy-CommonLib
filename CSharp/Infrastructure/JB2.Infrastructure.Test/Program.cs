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

            string plainText = "nEyMxJJswzxIDHzswCvyBDyIMFtCHEwDHEHGKrDqoJwxotGMuyMnquuvyApAzIxnMDzrtJqHxsxzqtLLDIFLzvHDnzMIHxrvAwGrCnAwoHHzzusMsvMHsnrpDsExsAstAMxzIJLBqJsJtwMpMzJCpAsKztzwvKvMupuwEDnyCKMoqGnqyoEEwBxKBAozHxxxLsrwDyqsqvHsxxqEpLyBzoFyABLwurHEtwnGosDBEzLsMsCGGowxCLLMDCtqJsCvzntxoqGGuFBGKnDtJrFnEFHwuHDqsqvDCJqIsCoxqHJAtsMAsAsBxqEFAwxrFxrJyIwrLrzMJrKsIvJLtGDxMHKtvHLoGssAoLGIrpvBEwvKvsFDBxAoJuxyqHAFBpGIDAMzIJFEIICqFywrDCnrqtyGGBoHJzJzxzpquEpJwoxzyuKssqBBwsvtBnGyGwELJpICuEApLBGFKBMoxAoJoonrrCuKqsnBMAtKBHvnuCIuoKpJEuxE";

            for (int i = 0; i < 2000; i++)
            {
                plainText = JB2.Common.NewID.ProductID().Replace("-","");
                string encryptText = JB2.Infrastructure.Cipher.Encrypt(plainText);
                StringBuilder sb = new StringBuilder();
                foreach (char c in encryptText)
                    sb.AppendFormat("0x{0:X2} ", (int)c);
                var encryptHEx = sb.ToString().Trim();
                //Console.WriteLine(JB2.Common.NewID.ProductID());
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
