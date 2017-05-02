using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.Mvc;
using System.IO;

using JB2.Common.Web;

namespace JB2.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static ImageResult Image(this Controller controller, Stream imageStream, string contentType)
        {
            return new ImageResult(imageStream, contentType);
        }

        public static ImageResult Image(this Controller controller, byte[] imageBytes, string contentType)
        {
            return new ImageResult(new MemoryStream(imageBytes), contentType);
        }

        public static System.Net.Http.HttpResponseMessage Image(this System.Web.Http.ApiController controller, MemoryStream imageStream, string contentType)
        {
            System.Net.Http.HttpResponseMessage httpResponseMessage = new System.Net.Http.HttpResponseMessage();
            //httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            //MemoryStream memoryStream = new MemoryStream(imageBytes);

            httpResponseMessage.Content = new System.Net.Http.ByteArrayContent(imageStream.ToArray());
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            //httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            //

            return httpResponseMessage;
        }





        public static System.Net.Http.HttpResponseMessage Image(this System.Web.Http.ApiController controller, byte[] imageBytes, string contentType)
        {
            return Image(controller,new MemoryStream(imageBytes), contentType);
        }
    }
}
