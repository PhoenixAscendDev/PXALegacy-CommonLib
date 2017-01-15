using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Infrastructure
{
    public static class Cipher
    {
        private const string mk1 = "JGsrCuIwGnMDxnntqCInGyAzrvEtrzpLIKGpowwvAEwCryLxCAxwMBFzwJyupyGoxDKpwsztnFBAspCsyEnCtBKBxuwtzqFLoInsoFHvtHynzsMsBMnxoypzyKqzLrotLIsEMJqzBEvIAExHzFLIFoHHIszpyDJAEtMyurLwEJGAorsMwJpFtvtxMJxDvFFtFFyBqzAxnGFLHKrAKxBxMKFEBwHtpMqHorIIvzrqLJpHnwIErwMtwnqxLsIJsLoKrvGwvMMCAoLHJussIBrLxJxuMMDEttFpCCqrzzFFCLutvLIxzEKqxvsuyvvLuHuHwBzGBsKBDvvvLqurFBMnwHrrAtGFoDEBIIGMBuvnvMwBKCoMGtvFvFvILFpvxzvozALGJvDBztMpsEtuGxwtvKMIBwAFKCwDuItyMpoJGLBAwCttEsvIswFBBFAnLzGpsoFnCJzCCMuByssywLuqtqzpsJEADCyoJqtIHCszArEHFvqqvzxtsHHvLAIuMArpvCKnqBpCyoLrvnHCMxxEpEDxAzzuwEHFtsnrMrvLsHBnozwIKnrpFGBqMDsxsEswDIornArzEJzpnCGqxLCuqGJHpGnyuKAwBJJFHKvxMsGLLBnzDKpCDuAxzKqIGJCrJJDuEEnxAtDwByrCrDwKEzMvtIDGBsMsHsArHEnLoDtDHpLuFMMytHwHKywBnCEqIoyFJAxrnBwEAxKKJJMAKGAzMsozJtsAFJIAMGvwvpMMuGGyBDBnJLzpzqpMHLzrxwyyryKHGquGtosAyzCFHqErnHzBqFAwyuwquJDxLLErAFnEDJEsDBGsAxHLLoEyJrHKLMDJrsvLwDBEwzMAFosLnFqvqwqywKqEJCFqzGLvtLuGtLDAMBBFuJqvtJsvqMLBzMpIAHGACGsnpDHxIAvwAoFtCqIzHnMznKuGJDFrDzDzrwCxtoBAsJtMKKDAqxtnwLvnyEItroztyMqxqHFDBCKBIqKAvwzMMDFBuyGwwHvBwopBxMwMEIuGBFuoFJtrHyxJoJKprEqGwvJrDqLovJwEsoMBMIsvFDEIxqoMIupKnuJstrMKpFnKtHsuIysrwoDKxsIBnEoGpEBMLtAIwAGpALtntCnMrvzyqtxBJsHtqunozAnFqEMyAEEEutDBJyvprGGKzIFnpqJJKFEznDCGMtHwzwvtLMyoqAGnzJCpIrLKCttFHGtMpvEEnrtDuKGGEwAsrsLDIpnMzIyHtJAyrsyyMLpKvAIqqEDAJqDLvvpJsvJrqpuAoJoLtGArxzzJnpxyJoIKqAqoLuKpzqytvrHBpnGCLsoyJKxtrrCuArKBqLDLMrAssGLAGvrqvzGLGEMFCvCrxLEBnMyJwsqyCKnwHAztFMsFrIEJHFIKGCwHCFvxMxMGCqvBJxAKnHFMLupMKnKuMwpHwtJsBxzHJyuIsCwMrxuGzHpwwoqByAGuJvGLBFuMvLEnBKtxqxLHGDxrtuyryGrMHsnyLDGAoKIzwJMwExGJHAnBvJBxBKGJEKnBvECDBqzusoBHxKsJAAJEIoFIxypxHnzDFAszHxLvKpvuuBLLHDxquxArxzHCEEAIuFpMpCtxyqEKGxvpoGqFppCLGCGqMGpDoKvrvFIvoHwIyFxxvFBJoJILuvvAoDuJCqEJxyJywLEtpwvxquErwHpoHEutAJDtqwntwLtGJryLLwDJDJIrIDtwnCMssCDFuJuAEyInnouHynMBHCoBLypxBHKItyEpzJxnKsrIHznBFByHwwEEsFMxrEEHLDFLKsFvqnLJqsLvysoDuznyHrwowLsMtvKILrGnIHBBGLFxtorDDLxvwvtoupIxAMnuwvMstutDIEFpFuJMAyBvBCxHpqxCsMDAAuIFrIvFsHtzvtsBLqxApzwGIuMwtMEnzCKKBopqFCJuKLJMurvzCpzJMzrLnpDCDxHDvKuKCyJMnBKrFrFGIJMuKqHFJunovMntunpqMzrJyJCEBEBDBwJKEMqyMnDCCqornustIHLGunxvrqpFzpopyHDHxEFyuDrwCyFzHpyyLLttGFFDuGHJuMBBHJHstMAtuBFyCwFvyDxsIxDBzBDxunAGpyJLpEIvEsopnJMJyHzKzuJMyBHpxBMwsMFHIEKHszzpotnqxnoIMxJGKDHCqMFvMJBGIuuKBHsHFGLLxzIEotqyLJHxwLrJprEutAsIwvCByxqJDLAGLBHMpKuGquMuHDyqJoGvwpoxrGwBvAEvotDHpJwxMKytEyMuGEItGztvMxLzDGuuyIvqHtILsDAuMwqptsJrDLzzCoFCprnxnCMFrtsHqLMDxAvrJFoFyDqCqKHxHLHIwzKrxJLKCIsqxwvwAqMuvMEqrMuCzButDLnFHrMvyMCLvDyzFGICAKrBpGILpGqHECGMvvwyLnInxDGKLvJxJvuArArvHsCCoHHKsqoqyDHGMBupzFCuBoJEBAwpoyuDLEvLEDnKnpnIzDAHyFnFHMrDsowBBtEuHLDnuuFFGyMICALnLpMtqsJFGGCwtDqsvppzEzKLAHonKznFqFtxxKyEBwGCKMuFopzyMrCrsAsFAMDMMtzzwwItHrGzJuyuyMzqLwvuopLKqqBzxzyqzqvKuJqrpoprJDpICCEuKCMqDIsMxFJzGwzBFxtCHpsBAwMEKzuxGyAwDHHAMrFvuuKCzqzsyqGuxxCrpMCCGwEnotptotnwBpAAAGrowDxnAKnsowAIJMnozvryxFBrFpKoIIsxHwIwFLrGytywLsKMMxAzrtJrBKwxItEwtMLJsoKLsMxvsuIHoBpBAMnGtFuIJJMtAqxGpnFovAGwoGJMKHDAoqvsqJJMonAqBMwyBvEEInJnJsnzMqGqKpKuDvECAGxJxtquFJzqyArqyyxztHtEptrvGwwLpEJunBMEvzMBnvxCuEwFAJJoqDJusCGvtEuAGrvBxMAAAFAEszMBtoHFFqDMnEAoMDqpKwDxFFnrDKtLuvIooMCzLyKInvxEzEnnMEsyFEwortzGqEEFuL";

        public static string Encrypt2(string plainText)
        {
            var alphabet = JB2.Common.Cipher.AllAlphabet;
            var pk = JB2.Common.RNG.ThreadSafe(alphabet.Length);
            var i = pk % 3;
            var keyIndexSettingName = "JB2:Cipher:" + i.ToString();
            //Console.WriteLine(plainText);
            var mk = JB2.Configuration.GetAppSetting(keyIndexSettingName);

           // var mk = mk1;
//
            int[] ki = new int[6] { 0, 505, 1010, 1515, 2020, 2525 };

            var r = JB2.Common.RNG.ThreadSafe(ki.Count());
            int p = ki[r];

            //Console.WriteLine("p:" + p);
            string k = mk.Substring(p, 4);
           // Console.WriteLine("k:" + k);

            int k_ascii = 0;

            foreach (var c in k.ToCharArray())
            {
                k_ascii = k_ascii + (int)c;
            }

            int m = (k_ascii % 4);

            int o = ki[m];

            var key = mk.Substring(o + 4,500);

           // Console.WriteLine("key:" + key);
            //Console.WriteLine("");

            var encryptKey = cindyEncrypt(plainText, key);

            //Console.WriteLine("encrptkey:" + encryptKey);
            //Console.WriteLine("");

            var final = alphabet[pk] + encryptKey + k;

            Console.WriteLine("final:" + final);
           Console.WriteLine("");

            return final;

        }

        public static string Decrypt2(string encyptText)
        {
            char[] alphabet = JB2.Common.Cipher.AllAlphabet;
            int[] ki = new int[6] { 0, 505, 1010, 1515, 2020, 2525 };

            var s = encyptText[0];

            int pk = Array.IndexOf(alphabet, s);

            int i = pk % 3;



            var keyIndexSettingName = "JB2:Cipher:" + i;
            var mk = JB2.Configuration.GetAppSetting(keyIndexSettingName);
            //var mk = mk1;
            var k = encyptText.Substring(Math.Max(0, encyptText.Length - 4));

            int k_ascii = 0;

            foreach (var c in k.ToCharArray())
            {
                k_ascii = k_ascii + (int)c;
            }

            int m = (k_ascii % 4);

            int o = ki[m];

            var key = mk.Substring(o + 4,500);

            //Console.WriteLine("");
           // Console.WriteLine("k:" + k);

            encyptText = encyptText.Substring(1);
            encyptText = encyptText.Substring(0, encyptText.Length - 4);
            //Console.WriteLine("");
            //Console.WriteLine("encyptText:" + encyptText);

            int p = -1;

            //foreach (int o in ki)
            //{
            //    if (mk.Substring(o, 4) == k)
            //        p = o;
            //}

            //var key = mk.Substring(p);

            //Console.WriteLine("");
            //Console.WriteLine("key:" + key);

            var plainText = cindyDecrypt(encyptText, key);

            return plainText;
        }


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
