using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Infrastructure
{
    public static class Cipher
    {

        public static string Encrypt(string plainText)
        {
            //get key index
            string encryptText;
            string keyIndex;
            string key;
            try
            {
                var keyIndexSettingName = "JB2:Cipher:keys";
                var keys = JB2.Configuration.GetAppSetting(keyIndexSettingName).Split(',');

                var randomIndex = JB2.Common.RNG.ThreadSafe(keys.Count());
                keyIndex = keys[randomIndex];

                //keyIndex = "aaaa";
                key = getkeybyRef(keyIndex);
                //key = getkey(randomIndex);

                encryptText = cindyEncrypt(plainText, key.Substring(0,plainText.Length));

                encryptText = "a" + keyIndex + encryptText;

                //Console.WriteLine("encrypt keyIndex:" + keyIndex);
                //Console.WriteLine("encrypt presalt:" + encryptText);
                //embed the keyIndex into the final text
 

                //var positions = JB2.Configuration.GetAppSetting(keyPositionSettingName).Split(',');
                //for (int i = 0; i < positions.Count(); i++)
                //{
                //    int position = i;
                //    int.TryParse(positions[i], out position);

                //    if (position > encryptText.Length)
                //        position = 0;

                //    encryptText = encryptText.Insert(position, keyIndex[i].ToString());
                //}
                //Console.WriteLine("encrypt final:" + encryptText);
            }
            catch (Exception ex)
            {
                //do something
                encryptText = plainText;
            }
            return encryptText;
        }


        public static string Decrypt(string encryptedText)
        {
            

            try
            {
                //Step 1 obtain the embedded keyindex
                


                var keyPositionSettingName = "JB2:Cipher:position-A";
                var positions = JB2.Configuration.GetAppSetting(keyPositionSettingName).Split(',');
                string keyIndex = string.Empty;

                keyIndex = encryptedText.Substring(1, 4);

                //for (int i = 0; i < positions.Count(); i++)
                //{
                //    int position = i;
                //    int.TryParse(positions[i], out position);

                //    if (position > encryptedText.Length)
                //        position = 0;
                //    keyIndex = keyIndex + encryptedText[position];
                //}
                //keyIndex = "haot";

                //Console.WriteLine("decrypt keyIndex:" + keyIndex);
                //Console.WriteLine("decrypt presalt:" + encryptedText);
                //remove the keyindexs

                //for (int i = positions.Count()-1; i >= 0; i--)
                //{
                //    int position = i;
                //    int.TryParse(positions[i], out position);

                //    if (position > encryptedText.Length)
                //        position = 0;
                //    encryptedText = encryptedText.Remove(position, 1);
                //}

                //Step 2 obtain the key

                encryptedText = encryptedText.Remove(0, 5);
                var key = getkeybyRef(keyIndex);

                //Setp 3 decrypt the key
                var plainText = cindyDecrypt(encryptedText, key.Substring(0, encryptedText.Length));
                //Console.WriteLine("decript final:" + encryptedText);

                return plainText;
                
            }
            catch(Exception ex)
            {
                return encryptedText;
            }



        }

        private static string getkey(int index)
        {
            var keyIndexSettingName = "JB2:Cipher:keys";
            try
            {
                var keys = JB2.Configuration.GetAppSetting(keyIndexSettingName).Split(',');

                if (index >= keys.Count())
                    index = 0;

                string keyIndex = keys[index];

                var keySettingName = "JB2:Cipher:{0}";

                var key = JB2.Configuration.GetAppSetting(string.Format(keySettingName, keyIndex));

                return key;
            }
            catch(Exception ex)
            {
                //do something

                return JB2.Common.NewID.Base62();
            }
        }

        private static string getkeybyRef(string keyRef)
        {
            var keySettingName = "JB2:Cipher:{0}";

            var key = JB2.Configuration.GetAppSetting(string.Format(keySettingName, keyRef));

            return key;
        }

        private static int getkeyCount()
        {
            var keyIndexSettingName = "JB2:Cipher:keys";
            try
            {
                var keys = JB2.Configuration.GetAppSetting(keyIndexSettingName).Split(',');
                return keys.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private static string cindyEncrypt(string plainText,string key)
        {
            return JB2.Common.Cipher.Cindy(plainText, key);
        }

        private static string cindyDecrypt(string encryptedText, string key)
        {
            return JB2.Common.Cipher.Decrypt(new Common.CipherResult(encryptedText, Common.Enum.CipherMode.Cindy), key);
        }
    }
}
