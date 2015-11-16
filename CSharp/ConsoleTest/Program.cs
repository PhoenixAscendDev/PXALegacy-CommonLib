using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.KeyVault;
using System.Threading;
using System.IO;

using JB2.Common;

namespace ConsoleTest
{
    class Program
    {


        private async static Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(
                ConfigurationManager.AppSettings["JB2:AD-clientID"],
                ConfigurationManager.AppSettings["JB2:AD-clientSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        //KeyVaultKeyResolver cloudResolver = new KeyVaultKeyResolver(GetToken);

        static void Main(string[] args)
        {

            Microsoft.Azure.KeyVault.RsaKey key = new RsaKey("private:key1");
            System.Drawing.Font f = JB2.Common.FontHelper.GetFont("ffft1");


            //JB2.Common.RGB rgb = new RGB("fff1f1");

            //Console.WriteLine(rgb.ToString());

            //JB2.Common.JB2Color color = JB2Color.FromHex("6B4106");
            Console.ReadLine();
            //JB2.Common.Data.AzureHelper.AccountName = "jb2bowtie";
            //JB2.Common.Data.AzureHelper.AccountKey = "frIlemrNzlvAbKNhiyYCeW+otbFXBoJb0TodzbgwzF8IBEZMtifrHfx0Y+o1+jwvUL4FcAGYepHlgqrG0iCc1Q==";

            //JB2.Common.Data.AzureBlobRepository cardRepo = new JB2.Common.Data.AzureBlobRepository("gameobjects");
            //string link = cardRepo.GetUrl("EmptyBingoCard_5x5.png");

            //var webClient = new System.Net.WebClient();
            //byte[] imageBytes = webClient.DownloadData("https://jb2bowtie.blob.core.windows.net/gameobjects/EmptyBingoCard_5x5.png");

            //System.IO.MemoryStream cardStream = cardRepo.GetStream("EmptyBingoCard_5x5.png");

            ////byte[] cardByte = cardRepo.GetByteArray("EmptyBingoCard_5x5.png");
            //JB2Image cardimage = JB2Image.FromUrl("https://jb2bowtie.blob.core.windows.net/gameobjects/EmptyBingoCard_5x5.png");
            //JB2Image cardimage1 = JB2Image.FromByteArray(imageBytes);
        }
    }
}
