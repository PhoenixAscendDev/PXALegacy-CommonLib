using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IServiceResult
    {
        int Count { get; }
        List<IValidation> Validation { get; set; }
        IValidation[] ToArray();

        bool ToBool();


    }
}
