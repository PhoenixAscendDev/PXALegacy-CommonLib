using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public static class SettingExtensions
    {

        public static T To<T>(this ISetting setting)
        {
            var type = typeof(T);
            if (type == setting.Value.GetType())
                return (T)setting.Value;
            else
                return default(T);
        }      
    }
}
