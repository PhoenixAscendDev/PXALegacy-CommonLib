using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace JB2.MediaWeb.Controllers
{
    public class AvatarImageController : Controller
    {

        
        // GET: Avatar
        public ActionResult EmailHash(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                id = "NotFound";
            }
            var webClient = new WebClient();
            var url = JB2.Gravatar.GetImageUrl(id, 80, "http://jbsquared.blob.core.windows.net/images/genericProfile_80.jpg");
            
            byte[] imageBytes = webClient.DownloadData(url);
               
            string contentType = "image/jpeg"; 
            return this.Image(imageBytes, contentType);
        }
    }
}