using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JB2.Infrastructure.API.Controllers
{
    public class ProductController : JB2.Common.WebAPI.BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Release(string id)
        {
            return Json(id);
        }
    }
}
