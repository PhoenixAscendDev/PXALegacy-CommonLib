using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Web.Api
{
    public interface IExternalApi 
        //where TClass : IExternalApi, new()
    {

        #region Events
        event Action<IExternalApi, APIResult, DateTime> RequestCompleted;
        event Action<IExternalApi, APIRequest, DateTime> RequestSent;

        void OnRequestCompleted(IExternalApi api, APIResult result, DateTime dateCompleted);
        void OnRequestSent(IExternalApi api, APIRequest request, DateTime dateSent);


        #endregion Events
        ITransportConfig  TransportConfig { get; set; }

        ApiKeySecretPair APIKey { get; set; }

        

        JB2.Common.ILoggerAsync Logger { get; set; }
        Task<APIResult> RequestAsync(JB2.Common.Web.Api.APIRequest request,
                          JB2.Common.Web.ProcessAPIRequest processSuccess = null,
                          JB2.Common.Web.ProcessAPIRequest processFailure = null);
    }
}
