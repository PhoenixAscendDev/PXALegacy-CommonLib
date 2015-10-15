using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Drawing.Font f = JB2.Common.FontHelper.GetFont("ffft1");

            JB2.Common.Data.AzureHelper.AccountName = "jb2bowtie";
            JB2.Common.Data.AzureHelper.AccountKey = "frIlemrNzlvAbKNhiyYCeW+otbFXBoJb0TodzbgwzF8IBEZMtifrHfx0Y+o1+jwvUL4FcAGYepHlgqrG0iCc1Q==";

            JB2.Common.Data.AzureBlobRepository cardRepo = new JB2.Common.Data.AzureBlobRepository("gameobjects");
            string link = cardRepo.GetUrl("EmptyBingoCard_5x5.png");

            var webClient = new System.Net.WebClient();
            byte[] imageBytes = webClient.DownloadData("https://jb2bowtie.blob.core.windows.net/gameobjects/EmptyBingoCard_5x5.png");

            System.IO.MemoryStream cardStream = cardRepo.GetStream("EmptyBingoCard_5x5.png");

            //byte[] cardByte = cardRepo.GetByteArray("EmptyBingoCard_5x5.png");
            JB2Image cardimage = JB2Image.FromUrl("https://jb2bowtie.blob.core.windows.net/gameobjects/EmptyBingoCard_5x5.png");
            JB2Image cardimage1 = JB2Image.FromByteArray(imageBytes);
        }
    }
}
