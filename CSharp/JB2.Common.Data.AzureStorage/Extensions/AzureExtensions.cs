using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Common.Data
{
    public static class AzureDataExtensions
    {
        public static string PropertyStringValue(this DynamicTableEntity e, string propertyName, string defaultValue = "")
        {
            string result = e.Properties.ContainsKey(propertyName) ? e.Properties[propertyName].StringValue : defaultValue;

            return result;
        }

        public static T GetPropertyValue<T>(this DynamicTableEntity e, string propertyName, T defaultValue)
        {
            return e.Properties.ContainsKey(propertyName) ? (T)e.Properties[propertyName].PropertyAsObject : (T)defaultValue;
        }

        public static EntityProperty SetProperty<T>(this DynamicTableEntity e, string propertyName, T value)
        {

            EntityProperty prop = new EntityProperty(string.Empty);

            if (typeof(T) == typeof(String))
                prop = new EntityProperty((string)(object)value);
            else if (typeof(T) == typeof(Int16))
                prop = new EntityProperty((Int16)(object)value);
            else if (typeof(T) == typeof(Int32))
                prop = new EntityProperty((Int32)(object)value);
            else if (typeof(T) == typeof(Int64))
                prop = new EntityProperty((Int64)(object)value);
            else if (typeof(T) == typeof(bool))
                prop = new EntityProperty((bool)(object)value);
            else if (typeof(T) == typeof(DateTime))
                prop = new EntityProperty((DateTime)(object)value);


            if (e.Properties.ContainsKey(propertyName))
                e.Properties[propertyName] = prop;
            else
                e.Properties.Add(propertyName, prop);

            return prop;
        }



    }
}
