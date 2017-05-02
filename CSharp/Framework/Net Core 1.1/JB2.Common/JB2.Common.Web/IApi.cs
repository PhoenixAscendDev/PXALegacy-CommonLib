using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Web.Api
{
    public interface IExternalApi<TClass> : JB2.Common.ISingleton<IExternalApi<TClass>>
        where TClass : class, new()
    {

        #region Events
        event Action<IExternalApi<TClass>, APIResult, DateTime> RequestCompleted;
        event Action<IExternalApi<TClass>, APIRequest, DateTime> RequestSent;

        void OnRequestCompleted(IExternalApi<TClass> api, APIResult result, DateTime dateCompleted);
        void OnRequestSent(IExternalApi<TClass> api, APIRequest request, DateTime dateSent);


        #endregion Events
        ITransportConfig  TransportConfig { get; set; }

        ApiKeySecretPair APIKey { get; set; }

        Task RequestAsync(JB2.Common.Web.Api.APIRequest request,
                          JB2.Common.Web.ProcessAPIRequest processSuccess = null,
                          JB2.Common.Web.ProcessAPIRequest processFailure = null);



    }
}
