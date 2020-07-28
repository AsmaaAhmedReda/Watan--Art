using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Controllers
{
    public class ImagesGalleryController : BaseController
    {
        // GET: ImagesGallery
        public ActionResult Index()
        {
            return View();
        }
    }
}