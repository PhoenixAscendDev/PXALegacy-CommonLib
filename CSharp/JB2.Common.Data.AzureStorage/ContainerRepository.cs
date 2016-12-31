using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;

namespace JB2.Common.Data
{
    public abstract class ContainerRepository : IContainerRepository
    {
        #region Fields
        protected CloudStorageAccount _account;
        #endregion Fields

        #region Constructors

        public ContainerRepository(CloudStorageAccount account)
        {
            _account = account;
        }

        #endregion Constructors


        public abstract string GetName();

        public abstract Enum.ContainerType GetContainerType();

        public CloudStorageAccount GetAccount()
        {
            return _account;
        }
    }
}
