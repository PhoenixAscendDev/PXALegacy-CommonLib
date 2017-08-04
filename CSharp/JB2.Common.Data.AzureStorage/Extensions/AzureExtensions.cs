using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using JB2.Common.Serialization;


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


            if (typeof(T) == typeof(DateTime))
            {
                var ticks = e.Properties.ContainsKey(propertyName) ? e.Properties[propertyName].PropertyAsObject.ToString() : "0";
                DateTime dt = DateTime.MinValue;
                if (ticks.IsNumber())
                    dt = new DateTime(Convert.ToInt64(ticks));
                else
                    dt = (DateTime)e.Properties[propertyName].PropertyAsObject;

                return (T)Convert.ChangeType(dt, typeof(T));
            }
            else
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
                {
                    DateTime dt = Convert.ToDateTime(value);
                    prop = dt.ToEntityProperty();
                }
                else if (typeof(T) == typeof(Byte))
                    prop = new EntityProperty(Convert.ToInt32((object)value));
            }



            if (e.Properties.ContainsKey(propertyName))
                e.Properties[propertyName] = prop;
            else
                e.Properties.Add(propertyName, prop);

            return prop;
        }

        public static DynamicTableEntity ToAzureTableEntity<TKind, TKey, TName, TTag, TUpdate>(this IObject<TKind, TKey, TName, TTag, TUpdate> o, string partitionKey, string rowKeyPropertyName = "ID", string[] propertyNames = null)
            where TKey : IComparable
            where TName : IComparable

        {
            DynamicTableEntity result = new DynamicTableEntity();

            result.PartitionKey = partitionKey;

            //set basic IObject Properties
            result.Properties.Add("ID", new EntityProperty(o.GetID().ToString()));
            result.Properties.Add("Name", new EntityProperty(o.GetName().ToString()));
            result.Properties.Add("Kind", new EntityProperty(o.GetType().ToString()));


            var tags = o.GetTags();
            var tagList = from tag in tags select tag.ToString();


            result.Properties.Add("TagsJSON", new EntityProperty(tagList.ToJson()));
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

                result.SetProperty<object>(pName, prop.GetValue(o, null));
            }
            return result;
        }


        public static EntityProperty ToEntityProperty(this DateTime dt)
        {
            return new EntityProperty(dt.Ticks.ToString());
        }

        public static EntityProperty ToEntityProperty(this string str)
        {
            return new EntityProperty(str);
        }

        public static EntityProperty ToSerializedEntityProperty(this object obj)
        {
            string json = string.Empty;
            if (obj.GetType() == typeof(string))
                return obj.ToString().ToEntityProperty();
            else
                json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            return new EntityProperty(json);
        }

        private static IDictionary<string,EntityProperty> ToEntityProperties<TKey,TServerity>(this ILogEntry<TKey,TServerity> e, string propertyNameFormat = "{0}")
        {
            DynamicTableEntity entry = new DynamicTableEntity();

            var tagString = e.GetTags().ToJson();

            if (!propertyNameFormat.Contains("{0}"))
                propertyNameFormat += "{0}";
            
            entry.SetProperty<string>(string.Format(propertyNameFormat,"Exception"), e.Exception == null ? string.Empty : e.Exception.ToJson());
            entry.SetProperty<string>(string.Format(propertyNameFormat, "ID"), e.ID.ToString());
            //entry.SetProperty<DateTime>(string.Format(propertyNameFormat, "LogDate"), e.LogDate);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "LogDateTicks"), e.LogDate.Ticks.ToString());
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Message"), e.Message);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Serverity"), e.Serverity.ToString());
            entry.SetProperty<string>(string.Format(propertyNameFormat, "LogCode"), e.LogCode);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Tags"), tagString);

            return entry.Properties;
        }

        private static IDictionary<string, EntityProperty> ToEntityProperties<TID,TName>(this IIDNamePair<TID,TName> e, string propertyNameFormat = "{0}")
            where TID : IComparable
            where TName : IComparable
        {
            DynamicTableEntity entry = new DynamicTableEntity();

            if (typeof(TName) == typeof(string))
                entry.SetProperty<string>(string.Format(propertyNameFormat, "Name"), e.Name.ToString());
            else if (typeof(TName) == typeof(JB2.Common.Name))
            {
                var name = (Name)Convert.ChangeType(e.Name, typeof(Name));
                entry.Properties.Add(string.Format(propertyNameFormat, "Name"), e.Name.ToSerializedEntityProperty());
                entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_FirstName"), name.First);
                entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_FullName"), name.FullName);
                entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_LastName"), name.Last);
                entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_MiddleName"), name.Middle);
                entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_Salutaion"), name.Salutaion);
            }
            else
            {
                entry.Properties.Add(string.Format(propertyNameFormat, "Name"), e.Name.ToSerializedEntityProperty());
            }

            entry.Properties.Add(string.Format(propertyNameFormat, "ID"), e.ID.ToSerializedEntityProperty());

            return entry.Properties;


        }


        private static IDictionary<string, EntityProperty> ToEntityProperties(this JB2.Common.Name e, string propertyNameFormat = "{0}" )
        {
            DynamicTableEntity entry = new DynamicTableEntity();

            var name = e;
            entry.Properties.Add(string.Format(propertyNameFormat, "Name"), e.ToSerializedEntityProperty());
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_FirstName"), name.First);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_FullName"), name.FullName);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_LastName"), name.Last);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_MiddleName"), name.Middle);
            entry.SetProperty<string>(string.Format(propertyNameFormat, "Name_Salutaion"), name.Salutaion);

            return entry.Properties;

        }
        public static IDictionary<string, EntityProperty> ToEntityProperties(this object o, string propertyNameFormat = "{0}")
        {
            DynamicTableEntity entry = new DynamicTableEntity();

            if (string.IsNullOrEmpty(propertyNameFormat))
                propertyNameFormat = "{0}";

            if (!propertyNameFormat.Contains("{0}"))
                propertyNameFormat += "{0}";

            var properties = o.GetType().GetProperties();

            foreach(var prop in properties)
            {
                if (prop.PropertyType == typeof(string))
                    entry.SetProperty<string>(string.Format(propertyNameFormat, prop.Name), (string)prop.GetValue(o));

                              else if (prop.PropertyType == typeof(DateTime))
                    entry.SetProperty<DateTime>(string.Format(propertyNameFormat, prop.Name), (DateTime)prop.GetValue(o));
                else if (prop.PropertyType == typeof(int))
                    entry.SetProperty<int>(string.Format(propertyNameFormat, prop.Name), (int)prop.GetValue(o));
                else if (prop.PropertyType == typeof(long))
                    entry.SetProperty<long>(string.Format(propertyNameFormat, prop.Name), (long)prop.GetValue(o));                
                else if (prop.PropertyType == typeof(short))
                    entry.SetProperty<short>(string.Format(propertyNameFormat, prop.Name), (short)prop.GetValue(o));              
                else if (prop.PropertyType == typeof(bool))
                    entry.SetProperty<bool>(string.Format(propertyNameFormat, prop.Name), (bool)prop.GetValue(o));
                //else if (prop.PropertyType == typeof(JB2.Common.Name))
                //{
                //    var properties = (IIDNamePair)
                //}
                else
                {

                    entry.Properties.Add(string.Format(propertyNameFormat, prop.Name), prop.GetValue(o).ToSerializedEntityProperty());
                }

            }

            return entry.Properties;

        }

        public static  T ToObject<T>(this DynamicTableEntity e, T defaultValue, string propertyNameFormat = "{0}")
            where T : new()
        {
            dynamic o = new System.Dynamic.ExpandoObject();

            if (string.IsNullOrEmpty(propertyNameFormat))
                propertyNameFormat = "{0}";

            if (!propertyNameFormat.Contains("{0}"))
                propertyNameFormat += "{0}";
                

            try
            {
                //if (typeof(ILogEntry).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                //{
                //    o.ID = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "ID"), string.Empty);
                //    o.LogDate = e.GetPropertyValue<DateTime>(string.Format(propertyNameFormat, "LogDateTicks"), DateTime.MinValue);
                //    o.Message = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "ID"), string.Empty);
                //    o.LogCode = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "LogCode"), string.Empty);

                //    //exception
                //    var exjson = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Exception"), string.Empty);
                //    o.Exception = Newtonsoft.Json.JsonConvert.DeserializeObject<Exception>(exjson);

                //    //serveritytype
                //    JB2.Common.Enum.LogServerityType sertype = Common.Enum.LogServerityType.Informational;
                //    var serstring = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Serverity"), string.Empty);
                //    System.Enum.TryParse<JB2.Common.Enum.LogServerityType>(serstring, out sertype);
                //    o.Serverity = sertype;


                //}



                //if (typeof(IPerson<string>).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                //{
                //    o.DisplayName = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "DisplayName"), string.Empty);
                //}

                //if(typeof(IIDNamePair<string, string>).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                //{                 
                //    o.ID = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "ID"), string.Empty);
                //    o.Name = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Name"), string.Empty);
                //}

                //if(typeof(IIDNamePair<string, JB2.Common.Name>).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                //{
                //    o.ID = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "ID"), string.Empty);
                //    var name = new JB2.Common.Name();

                //    name.First = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Name_FirstName"), string.Empty);
                //    name.FullName = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Name_FullName"), string.Empty);
                //    name.Last = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Name_LastName"), string.Empty);
                //    name.Middle = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Name_MiddleName"), string.Empty);
                //    name.Salutaion = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Name_Salutaion"), string.Empty);

                //    o.Name = name;
                //    //o.DisplayName = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "DisplayName"), string.Empty);

                //}

                var properties = typeof(T).GetProperties();

                var oProps = (IDictionary<string, object>)o;
                foreach (var prop in properties)
                {

                    //if we already haven't define it then let's set it
                    if(!oProps.ContainsKey(prop.Name))
                    {
                        if(prop.PropertyType == typeof(string))
                            ((IDictionary<string, object>)o).Add(prop.Name, e.GetPropertyValue<string>(string.Format(propertyNameFormat, prop.Name), string.Empty));
                        else if(prop.PropertyType == typeof(DateTime))
                            ((IDictionary<string, object>)o).Add(prop.Name, e.GetPropertyValue<DateTime>(string.Format(propertyNameFormat, prop.Name),DateTime.MinValue));
                        else if(prop.PropertyType == typeof(int))
                            ((IDictionary<string, object>)o).Add(prop.Name, e.GetPropertyValue<int>(string.Format(propertyNameFormat, prop.Name), 0));
                        else if(prop.PropertyType == typeof(long))
                            ((IDictionary<string, object>)o).Add(prop.Name, e.GetPropertyValue<long>(string.Format(propertyNameFormat, prop.Name), 0));
                        else if(prop.PropertyType == typeof(short))
                            ((IDictionary<string, object>)o).Add(prop.Name, e.GetPropertyValue<short>(string.Format(propertyNameFormat, prop.Name), 0));
                        else if(prop.PropertyType == typeof(bool))
                            ((IDictionary<string, object>)o).Add(prop.Name, e.GetPropertyValue<bool>(string.Format(propertyNameFormat, prop.Name),false));
                        else
                        {
                            var seralizeString = e.GetPropertyValue<string>(string.Format(propertyNameFormat, prop.Name), string.Empty);
                            ((IDictionary<string, object>)o).Add(prop.Name, Newtonsoft.Json.JsonConvert.DeserializeObject(seralizeString));
                        }

                    }

                }
                
                var seralize = Newtonsoft.Json.JsonConvert.SerializeObject(o);


                T result = (T)Newtonsoft.Json.JsonConvert.DeserializeObject<T>(seralize); ;


                //set the Tags
                if (typeof(ITagable<JB2.Common.Tag>).GetTypeInfo().IsAssignableFrom(typeof(T).Ge‌​tTypeInfo()))
                {
                    // Tags
                    var tagsjson = e.GetPropertyValue<string>(string.Format(propertyNameFormat, "Tags"), string.Empty);
                    IEnumerable<JB2.Common.Tag> tags = new JB2.Common.Tag[0];
                    tags = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JB2.Common.Tag>>(tagsjson);

                    ((ITagable<JB2.Common.Tag>)result).LoadTags(tags);
                }

                return result;
                    //(T)Convert.ChangeType(o, typeof(T));
            }
            catch(Exception ex)
            {
                var failed = new JB2.Common.ServiceResult<T>(defaultValue);
                return (T)failed;
            }
        }

    }
}


