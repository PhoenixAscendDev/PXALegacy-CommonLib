using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common.Web.Api
{
    public class APIResult : JB2.Common.ServiceResult<System.Net.Http.HttpContent>
    {

        #region Constructor

        public APIResult() : base()
        {

        }

        public APIResult(Exception ex) : base(ex)
        {

        }

        protected APIResult(System.Net.Http.HttpResponseMessage msg) : base(msg.Content)
        {
            this.Headers = msg.Headers;
            this.ReasonPhrase = msg.ReasonPhrase;
            this.StatusCode = msg.StatusCode;
            this.RequestUri = msg.RequestMessage.RequestUri.ToString();

            this.Validation.Add(new Validation("HttpStatusCode", msg.StatusCode.ToString()) { IsValid = msg.IsSuccessStatusCode });
        }


        #endregion Constructor

        public System.Net.Http.Headers.HttpResponseHeaders Headers { get; set; }

        public string RequestUri { get; set; }

        public string ReasonPhrase { get; set; }


        public System.Net.HttpStatusCode StatusCode { get; set; }


        public static implicit operator APIResult(System.Net.Http.HttpResponseMessage msg)
        {
            APIResult result = new APIResult(msg);

            return result;
        }

        public static APIResult FromHttpReponse(System.Net.Http.HttpResponseMessage msg)
        {
            return (APIResult)msg;
        }

    }
}
