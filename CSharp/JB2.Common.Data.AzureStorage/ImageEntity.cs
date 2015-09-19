using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Data
{
    public class ImageEntity : Microsoft.WindowsAzure.Storage.Table.TableEntity
    {
        public ImageEntity(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }

        public ImageEntity()
        {

        }

        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

    }
}
