using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

using JB2.Common.Data;




namespace JB2.Common
{
    public static class FontHelper
    {

        public static Font GetFont(string fontcode)
        {
            PrivateFontCollection collection = new PrivateFontCollection();
            // Add the custom font families. 
            // (Alternatively use AddMemoryFont if you have the font in memory, retrieved from a database).
           
            collection.AddFontFile(@"E:\Downloads\actest.ttf");
            Font f = new Font(collection.Families.First(), 16);

            return f;
        }

        private static AzureTableRepository FontDB
        {
            get
            {
                Microsoft.WindowsAzure.Storage.CloudStorageAccount account =  JB2.Common.Data.AzureHelper.GetStorageAccount("jbsquared", "iGf7AhI5v12hig85TsVkJSuPwvB42EncTMFogXFcqGlEcVMo5oXf0PvsMkxOQQeDmM21UtqHHtwyjR3MG3Di5g==");

                return new AzureTableRepository(account, "assets");
                
               

            }
        }


    }
}
