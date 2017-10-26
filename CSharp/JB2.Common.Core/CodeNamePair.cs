using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{

    public class CodeNamePair : CodeNamePair<string,string>
    {
        public CodeNamePair()
        {

        }

        public CodeNamePair(string code, string name) : base(code,name)
        {
           
        }

        public static implicit operator KeyValuePair<string,string>(CodeNamePair cn)
        {
            var kv = new KeyValuePair<string,string>(cn.Code, cn.Name);
            return kv;
        }

        public static implicit operator CodeNamePair(KeyValuePair<string,string> kvp)
        {
            var cn = new CodeNamePair(kvp.Key, kvp.Value);
            return cn;
        }
    }
    public class CodeNamePair<TCode,TName>
    {
        public CodeNamePair()
        {

        }

        public CodeNamePair(TCode code, TName name)
        {
            Name = name;
            Code = code;
        }

        public TCode Code { get; set; }
        public TName Name { get; set; }

        public static implicit operator KeyValuePair<TCode, TName>(CodeNamePair<TCode,TName> cn)
        {
            var kv = new KeyValuePair<TCode, TName>(cn.Code, cn.Name);
            return kv;
        }

        public static implicit operator CodeNamePair<TCode, TName>(KeyValuePair<TCode,TName> kvp)
        {
            var cn = new CodeNamePair<TCode, TName>(kvp.Key, kvp.Value);
            return cn;
        }


    }
}
