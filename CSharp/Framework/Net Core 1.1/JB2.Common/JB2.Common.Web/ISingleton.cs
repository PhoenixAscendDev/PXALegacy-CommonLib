using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public interface ISingleton<Tobject>
        where Tobject : class
    {

        Tobject Instance { get; }
    }
}
