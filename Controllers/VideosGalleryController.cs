using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Controllers
{
    public class VideosGalleryController : BaseController
    {
        // GET: VideosGallery
        public ActionResult Index()
        {
            return View();
        }
    }
}