using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class Tag: Tag<string,string>
    {
        public Tag(string name,string value) : base(name,value)
        {

        }

        public static implicit operator string(Tag t)
        {
            return t.ToString();
        }

        public static Tag FromString(string str, string seperator = ":")
        {
            try
            {
                string[] strArray = str.Split(seperator);
                return new Tag(strArray[0], strArray[1]);
            }
            catch (Exception ex)
            {
                return new Common.Tag(string.Empty, str);
            }
        }


    }
    public class Tag<TKey,TValue>
    {

        public Tag(TKey name, TValue value)
        {
            this.TagName = name;
            this.Value = value;
        }
        public TKey TagName { get; set; }
        public TValue Value { get; set; }

        public override string ToString()
        {
            return ToString(":");
        }

        public string ToString(char seperator)
        {
            return ToString(seperator.ToString());
        }

        public  string ToString( string seperator)
        {
            StringBuilder str = new StringBuilder();
            str.Append(TagName.ToString());
            str.Append(seperator);
            str.Append(Value.ToString());

            return str.ToString();
        }

        public static implicit operator KeyValuePair<TKey,TValue>(Tag<TKey, TValue> t)
        {
            var kv = new KeyValuePair<TKey, TValue>(t.TagName, t.Value);
            return kv;
        }

        public static implicit operator Tag<TKey, TValue>(KeyValuePair<TKey, TValue> kv)
        {
            var t = new Tag<TKey, TValue>(kv.Key, kv.Value);
            return kv;
        }

        public static implicit operator string (Tag<TKey,TValue> t)
        {
            return t.ToString();
        }
    }
}
