using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace JB2.Common.WebAPI
{
    public class BaseApiController : ApiController
    {

        protected HttpResponseMessage createResponse<T>(T o, string fields, System.Net.HttpStatusCode code) where T : IAPIObject
        {
            HttpResponseMessage result = null;

            //string[] fieldArray = fields.Split(',');

            if (fields.ToLower() != "all")
            {
                o.serializableProperties = fields.Split(',').ToList();
            }


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(o, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new ShouldSerializeContractResolver<APIObject>()
            });

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            result = Request.CreateResponse(code, obj);
            return result;
        }
    }
}
