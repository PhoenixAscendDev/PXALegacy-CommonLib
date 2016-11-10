using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;

namespace JB2.Common.Data
{
    public interface IContainerRepository : JB2.Common.INameProp<string>
    {

        CloudStorageAccount GetAccount();

        Enum.ContainerType GetContainerType();


    }
}
