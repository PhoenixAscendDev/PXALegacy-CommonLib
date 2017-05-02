using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Web.Api
{
    public class APIResult : JB2.Common.ServiceResult<System.Net.Http.HttpContent>
    {
        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; set; }

        public string ReasonPhrase { get; set; }

        public System.Net.Http.HttpRequestMessage RequestMessage { get; set; }

        public System.Net.HttpStatusCode StatusCode { get; set; }

    }
}
