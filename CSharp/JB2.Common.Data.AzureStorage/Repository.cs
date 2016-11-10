using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Data
{
    public abstract class AzureRepository
    {
        #region Fields
        protected JB2.Common.Data.AzureTableRepository _table;
        protected JB2.Common.Data.AzureBlobRepository _blob;
        protected JB2.Common.Data.AzureQueueRepository _queue;
        #endregion Fields

        #region Properties

        public virtual AzureTableRepository AzureTable
        {
            get
            {
                return _table;
            }
        }

        public virtual AzureBlobRepository AzureBlob
        {
            get
            {
                return _blob;
            }
        }

        public virtual AzureQueueRepository AzureQueue
        {
            get
            {
                return _queue;
            }
        }

        #endregion Properties

    }
}
