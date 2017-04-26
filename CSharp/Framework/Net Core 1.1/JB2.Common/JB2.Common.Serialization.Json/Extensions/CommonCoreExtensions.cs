using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace JB2.Common.Serialization
{
    public static class CommonCoreExtensions
    {
        public static string ToJson(this IEnumerable<JB2.Common.Tag> tags)
        {
            var json = JsonConvert.SerializeObject(new
            {
                tags = tags
            });

            return json.ToString();
        }

        public static string ToJson(this Exception ex)
        {

            var json = JsonConvert.SerializeObject(ex);

            return json.ToString();
        }

        public static string ToJson(this IEnumerable<string> strings)
        {
            var json = JsonConvert.SerializeObject(strings);

            return json.ToString();
        }
    }
}
