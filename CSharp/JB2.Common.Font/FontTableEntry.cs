using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JB2.Common.Data.Azure.Entries
{
    public class FontTableEntry :  Microsoft.WindowsAzure.Storage.Table.TableEntity, JB2.Common.IIDNamePair<string, string>
    {
        public FontTableEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }


        public FontTableEntry()
        {

        }


        public string ID
        {
            get;set;
        }

        public string Name
        {
            get; set;
        }

        public string  UrlPath
        {
            get; set;
        }

        public string Notes
        {
            get; set;
        }

        public string Author
        {
            get; set;
        }

        public string Copyright
        {
            get; set;
        }

        public byte[] Content
        {
            get; set; 
        }

        public string GetID()
        {
            return ID;
        }

     
    }
}
