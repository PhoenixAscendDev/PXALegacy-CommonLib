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
            StringBuilder str = new StringBuilder();
            str.Append(TagName.ToString());
            str.Append(":");
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


    }
}
