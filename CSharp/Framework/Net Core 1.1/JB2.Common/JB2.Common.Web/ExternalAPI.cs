using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;

namespace JB2.Common.Web.Api
{
    public class ExternalAPI : JB2.Common.Singleton<ExternalAPI>, IExternalApi
    //where TClass : class, new()
    {

        #region Constructors

        public ExternalAPI()
        {
            //Add Http Codes to the Common Dictionary
            var values = System.Enum.GetValues(typeof(System.Net.HttpStatusCode)).Cast<System.Net.HttpStatusCode>();
            foreach(var e in values)
            {
                JB2.Dictionary.ErrorCodes.Add( new StatusCode("E-HS-" + ((int)e).ToString().PadLeft(6, '0'), e.ToString()));
            }

            JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100200", "TransportConfig not initialized"));
            JB2.Dictionary.ErrorCodes.Add(new StatusCode("E-01-100201", "General Error  in ExternalAPI executing Request"));

        }
        #endregion Constructors


        public ITransportConfig TransportConfig { get; set; }
        public ApiKeySecretPair APIKey { get; set; }
        public ILoggerAsync Logger { get; set; }
        public bool LogError { get; set; }

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

            using (System.Net.Http.HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(instance.TransportConfig.Endpoint);

                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue contentType = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);

                return  await RequestAsync(client, request, processSuccess, processFailure);
            }
                
            //setup the header
            
        }

        public async Task<APIResult> RequestAsync(System.Net.Http.HttpClient client,APIRequest request, ProcessAPIRequest processSuccess = null, ProcessAPIRequest processFailure = null)
        {
            var instance = ExternalAPI.Instance;
            APIResult result = null;
            try
            {
                if (TransportConfig == null)
                    throw new JB2Exception(JB2.Dictionary.ErrorCodes.GetDescription("E-01-100200"), "E-01-100200");

                using (System.Net.Http.HttpClient c = client)
                {
                    //client.BaseAddress = new Uri(instance.TransportConfig.Endpoint);
                    //setup the header

                    //System.Net.Http.Headers.MediaTypeWithQualityHeaderValue contentType = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
                    //client.DefaultRequestHeaders.Accept.Add(contentType);

                    //set data
                    var uri = request.MethodUrl;

                    var postParams = new Dictionary<string, string>();

                    foreach (var p in request.MethodParameters)
                    {
                        postParams.Add(p.Key, Newtonsoft.Json.JsonConvert.SerializeObject(p.Value));
                    }

                    switch (request.SendType)
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
                                catch (Exception ex)
                                {
                                    result.Validation.Add(new Validation(ex) { IsValid = false });

                                }
                                break;
                            }
                        case ApiRequestMethodType.GET:
                            using (var postContent = new FormUrlEncodedContent(postParams))
                            using (HttpResponseMessage response = await client.GetAsync(uri))
                            {
                                result = (APIResult)response;
                                try
                                {
                                    response.EnsureSuccessStatusCode(); // Throw if httpcode is an error
                                }
                                catch (Exception ex)
                                {
                                    result.Validation.Add(new Validation(ex) { IsValid = false });

                                }
                                break;
                            }

                    }


                    if (result)
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
            catch(JB2Exception ex)
            {
                if (LogError && Logger != null)
                {
                    await Logger.LogErrorAsync(ex, ex.Message, "E-01-100200");
                }
            }
            catch(Exception ex)
            {             
                var error = new JB2.Common.JB2Exception(JB2.Dictionary.ErrorCodes.GetDescription("E-01-100201"), ex, "E-01-100201");
                if(LogError && Logger != null)
                {
                    await Logger.LogErrorAsync(ex, ex.Message, "E-01-100201");
                }

                
            }
            return result;
        }


    }
}
