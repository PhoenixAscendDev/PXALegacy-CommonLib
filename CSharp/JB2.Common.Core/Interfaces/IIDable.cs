using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IIDable<T>
    {

        T GetID();
    }
}
