using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Web.Api
{
    public struct APIRequest
    {
        

        public string MethodUrl { get; set; }
        public ApiRequestMethodType SendType { get; set; }
        public IDictionary<string, object> MethodParameters { get; set; }

    }
}
