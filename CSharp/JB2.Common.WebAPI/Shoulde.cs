using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Newtonsoft.Json.Serialization;

namespace JB2.Common.WebAPI
{

    public class ShouldSerializeContractResolver<T> : Newtonsoft.Json.Serialization.DefaultContractResolver
         where T : IAPIObject
    {

        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType == typeof(APIObject) || property.DeclaringType.BaseType == typeof(APIObject))
            {
                if (property.PropertyName == "serializableProperties")
                {
                    property.ShouldSerialize = instance => { return false; };
                }
                else
                {
                    property.ShouldSerialize = instance =>
                    {
                        var p = (T)instance;
                        return p.serializableProperties.Contains(property.PropertyName);
                    };
                }
            }
            return property;
        }
    }
}
