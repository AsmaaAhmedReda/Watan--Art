using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;

namespace WatanART.Areas.Admin.Controllers
{

   
    public class DesignController : BaseController
    {
        // GET: Admin/Design
        DesignBLL _DesignBLL = new DesignBLL();
        public ActionResult Index()
        {

            var RESULT = _DesignBLL.SelectAllDesigns(LanguageID);
            return View(RESULT);
        }

        




    }
}