using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Controllers
{
    public class AlbumsGalleryController : BaseController
    {
        // GET: AlbumsGallery
        public ActionResult Index()
        {
            return View();
        }
    }
}