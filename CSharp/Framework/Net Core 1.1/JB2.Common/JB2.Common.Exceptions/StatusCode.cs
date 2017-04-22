using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public struct StatusCode
    {

        #region Constructors

        public StatusCode(string code, string description)
        {
            Code = code;
            Description = description;
        }



        #endregion Constructors

        public string Code { get; set; }

        public string Description { get; set; }


        public static implicit operator KeyValuePair<string,string>(StatusCode sc)
        {
            return new System.Collections.Generic.KeyValuePair<string,string>(sc.Code, sc.Description);
        }

        public static implicit operator StatusCode(KeyValuePair<string, string> kv)
        {
            return new StatusCode() { Code = kv.Key, Description = kv.Value };
        }
    }
}
