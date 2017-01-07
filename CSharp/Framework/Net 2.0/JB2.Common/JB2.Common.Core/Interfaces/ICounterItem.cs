using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface ICounterItem : ICounterItem<string>
    {

    }

    public interface ICounterItem<Tid> : IIDProp<Tid>
    {
        string GetCounterName();
        int GetCounterIndex();
    }
}
