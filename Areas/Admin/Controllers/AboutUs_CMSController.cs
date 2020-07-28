using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Areas.Admin.Controllers
{
    public class AboutUs_CMSController : BaseController
    {
        AboutUsBLL _AboutUsBLL = new AboutUsBLL();
        Generic _Generic = new Generic();

        // GET: Admin/AboutUs

        public ActionResult Index()
        {
            return View(_AboutUsBLL.SelectByID(1));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AboutUsVM obj)
        {
            try
            {
                if (_AboutUsBLL.Update(obj))
                    ViewBag.ErrorMsg = true;
                else
                    ViewBag.ErrorMsg = false;
                return View(obj);
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(obj);
            }
        }
    }
}