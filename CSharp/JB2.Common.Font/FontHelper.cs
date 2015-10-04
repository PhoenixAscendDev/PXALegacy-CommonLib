using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;

using JB2.Common.Data;

using System.Runtime.InteropServices;




namespace JB2.Common
{
    public static class FontHelper
    {

        public static Font GetFont(string fontcode)
        {
            PrivateFontCollection collection = new PrivateFontCollection();
            // Add the custom font families. 
            // (Alternatively use AddMemoryFont if you have the font in memory, retrieved from a database).

            FontTableEntry fontEntry = FontDB.GetEntity<FontTableEntry>("font", "font_ffft");

            byte[] fontByteArray = JB2.Common.Utility.GetBinaryFromUrl(fontEntry.UrlPath);     
            //byte* ptr = fontByteArray;

            var handle = GCHandle.Alloc(fontByteArray, GCHandleType.Pinned);
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(fontByteArray, 0);
                PrivateFontCollection fontCollection = new PrivateFontCollection();
                fontCollection.AddMemoryFont(ptr, fontByteArray.Length);
                Font f = new Font(collection.Families.First(), 16);
                return f;
            }
            finally
            {
                // don't forget to unpin the array!
                handle.Free();
            }
            
           // collection.AddMemoryFont(new IntPtr(ptr), fontByteArray.Length);
           // return f;
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
