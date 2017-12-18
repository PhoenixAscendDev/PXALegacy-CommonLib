using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IClass:  IUpdateable
    {
        T GetProperity<T>(string index, T defaultValue);

        T GetProperty<T>(string index, T defaultValue);

        void SetProperty<T>(string index, T newValue, bool changeLastUpdate);

    }
}
