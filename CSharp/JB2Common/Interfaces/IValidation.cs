using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IValidation
    {
        string Name { get; set; }
        string Message { get; set; }
        bool IsValid { get; set; }

        Exception ToException();
    }
}
