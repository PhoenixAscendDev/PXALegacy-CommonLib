using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IServiceResult: IServiceResult<object>
    {

    }

    public interface IServiceResult<Tobject>
    {
        int Count { get; }

        
        List<IValidation> Validation { get; set; }
        IValidation[] ToArray();

        Tobject ToObject();

        bool ToBool();




    }
}
