using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using Microsoft.WindowsAzure.Storage;



namespace JB2.Infrastructure.Data
{
    public class ReleaseNote : JB2.Common.Data.AzureTableEntity, JB2.Common.IReleaseNote
    {
        public ReleaseNote(string partitionKey, string rowKey) : base(partitionKey,rowKey)
        {
            ID = JB2.Common.NewID.ShortGuid();
        }

        public ReleaseNote() : base()
        {
            ID = JB2.Common.NewID.ShortGuid();
        }

        public string ProductCode { get; set; }
        public string Detail { get; set; }
        public string Title { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool isPublic { get; set; }
        public string VersionNumber { get; set;}


        #region IReleaseNote
        public IPerson<string> GetAuthor()
        {
            return JB2.Infrastructure.People.JB;
        }

        public string GetHtmlText()
        {
            return Detail;
        }

        public DateTime GetLastUpdate()
        {
            return DateUpdated;
        }

        public string GetPlainText()
        {
            return Detail;
        }

        public bool IsInternal()
        {
            return !isPublic;
        }
        #endregion IReleaseNote
    }
}
