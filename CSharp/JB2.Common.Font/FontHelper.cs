using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;


using JB2.Infrastructure;
using JB2.Common.Data;
using JB2.Common.Data.Azure.Entries;
using System.Runtime.InteropServices;




namespace JB2.Helpers
{
    public static class FontHelper
    {
        public static Font GetFont(string fontcode)
        {
            return GetFont(fontcode, 16);
        }

        public static Font GetFont(string fontcode, int fontEmSize)
        {
            PrivateFontCollection collection = new PrivateFontCollection();
            // Add the custom font families. 
            // (Alternatively use AddMemoryFont if you have the font in memory, retrieved from a database).

            FontTableEntry fontEntry = FontDB.GetEntity<FontTableEntry>("font", "font_" + fontcode);

            byte[] fontByteArray = JB2.Common.Utility.GetBinaryFromUrl(fontEntry.UrlPath);     
            //byte* ptr = fontByteArray;

            var handle = GCHandle.Alloc(fontByteArray, GCHandleType.Pinned);
            try
            {
                var ptr = Marshal.UnsafeAddrOfPinnedArrayElement(fontByteArray, 0);
                PrivateFontCollection fontCollection = new PrivateFontCollection();
                fontCollection.AddMemoryFont(ptr, fontByteArray.Length);
                Font f = new Font(fontCollection.Families.First(), fontEmSize);
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
                return Storage.AssetTable;
            }
        }


    }
}
