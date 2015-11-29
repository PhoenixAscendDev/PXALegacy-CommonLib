using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.WebAPI
{
    public interface IAPIObject
    {
        List<string> serializableProperties { get; set; }

        void SetSerializableProperties(string fields);
    }
}
