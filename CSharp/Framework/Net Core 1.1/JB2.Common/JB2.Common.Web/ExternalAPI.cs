using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace JB2.Common.Web.Api
{
    public class ExternalAPI : JB2.Common.Singleton<ExternalAPI>
    //where TClass : class, new()
    {

        #region Constructors

        public ExternalAPI()
        {

        }
        #endregion Constructors


        public ITransportConfig TransportConfig { get; set; }
        public ApiKeySecretPair APIKey { get; set; }

        public event Action<IExternalApi, APIResult, DateTime> RequestCompleted;
        public event Action<IExternalApi, APIRequest, DateTime> RequestSent;

        public void OnRequestCompleted(IExternalApi api, APIResult result, DateTime dateCompleted)
        {
            if (RequestCompleted != null)
                RequestCompleted(api, result, dateCompleted);
        }

        public void OnRequestSent(IExternalApi api, APIRequest request, DateTime dateSent)
        {
            if (RequestSent != null)
                RequestSent(api, request, dateSent);
        }

        public async Task<APIResult> RequestAsync(APIRequest request, ProcessAPIRequest processSuccess = null, ProcessAPIRequest processFailure = null)
        {
            var instance = ExternalAPI.Instance;

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(instance.TransportConfig.Endpoint);
                //setup the header
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue contentType = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                //set data
                var uri = request.MethodUrl;

                //entry.AddTag(new Tag("jb2-lc-url", uri));

                APIResult result = null;
                
 
                var postParams = new Dictionary<string, string>();

                foreach(var p in request.MethodParameters)
                {
                    postParams.Add(p.Key, Newtonsoft.Json.JsonConvert.SerializeObject(p.Value));
                }

                switch(request.SendType)
                {
                    case ApiRequestMethodType.POST:
                        using (var postContent = new FormUrlEncodedContent(postParams))
                        using (HttpResponseMessage response = await client.PostAsync(uri, postContent))
                        {
                            result = (APIResult)response;
                            try
                            {
                                response.EnsureSuccessStatusCode(); // Throw if httpcode is an error
                            }
                            catch(Exception ex)
                            {
                                result.Validation.Add(new Validation(ex) { IsValid = false });
                                
                            }
                            break;
                        }

                }


                if(result)
                {
                    if (processSuccess != null)
                        processSuccess(result);
                }
                else
                {
                    if (processFailure != null)
                        processFailure(result);
                }

                return result;
                //postParams.Add("logentry", );


            }
        }
    }
}
