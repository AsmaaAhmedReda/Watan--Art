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
    public class CurrenciesController : BaseController
    {
        CurrenciesBLL OBJ = new CurrenciesBLL();
        public ActionResult Index()
        {

            var RESULT = OBJ.SelectAllCurrencies(LanguageID);
            return View(RESULT);
        }



        public ActionResult New()
        {


            var obj = new CurrenciesVM();
          
            return View(obj);
        }

        public ActionResult Update(int ID)
        {

            var obj = OBJ.SelectbyID(ID, 1);
           
            return View(obj);
        }


        public JsonResult Insertajax(CurrenciesVM obj)
        {
            long result = 0;
            result = OBJ.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Updateajax(CurrenciesVM obj)
        {
            long result = 0;
            result = OBJ.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

       

    }
}