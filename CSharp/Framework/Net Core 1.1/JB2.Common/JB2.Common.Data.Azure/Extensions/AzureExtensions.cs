using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

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
            EntityProperty prop = null;

            if (value == null || value.Equals(default(T)))
                prop = new EntityProperty(string.Empty);
            else
            {
                prop = new EntityProperty((string)(object)value.ToString());

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
                else if (typeof(T) == typeof(Byte))
                    prop = new EntityProperty(Convert.ToInt32((object)value));
            }



            if (e.Properties.ContainsKey(propertyName))
                e.Properties[propertyName] = prop;
            else
                e.Properties.Add(propertyName, prop);

            return prop;
        }

        public static DynamicTableEntity ToAzureTableEntity<TKind, TKey, TName, TTag, TUpdate>(this IObject<TKind, TKey, TName, TTag, TUpdate> o, string partitionKey, string rowKeyPropertyName = "ID", string[] propertyNames = null )
            where TKey : IComparable
            where TName : IComparable

        {
            DynamicTableEntity result = new DynamicTableEntity();

            result.PartitionKey = partitionKey;

            //set basic IObject Properties
            result.Properties.Add("ID", new EntityProperty(o.GetID().ToString()));
            result.Properties.Add("Name", new EntityProperty(o.GetName().ToString()));
            result.Properties.Add("Kind", new EntityProperty(o.GetType().ToString()));

            List<string> tags = new List<string>();

            foreach(var t in tags)
            {
                tags.Add(t.ToString());
            }

            result.Properties.Add("TagsCSV", new EntityProperty(string.Join(",", tags)));
            result.Properties.Add("LastUpdate", new EntityProperty(o.GetLastUpdate().ToString()));

            //setup the rowkey
            try
            {
                var rowValue = o.GetType().GetProperty(rowKeyPropertyName).GetValue(o, null);
                result.RowKey = rowKeyPropertyName.ToLower() + ":" + rowValue.ToString();
            }
            catch (Exception ex)
            {
                result.RowKey = "rowkey:" + JB2.Common.NewID.ShortGuid();
            }

            //get the properties
            foreach (var prop in o.GetType().GetProperties())
            {
                var obj = prop.GetValue(o, null);
                string pName = "Prop_" + prop.Name;

                var pValue = prop.GetValue(o, null);
                
                result.SetProperty<object> (pName, prop.GetValue(o, null));
            }
            return result;
        }



    }
}
