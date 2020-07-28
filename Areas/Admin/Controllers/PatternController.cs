using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechMart.Web.Models;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;

namespace WatanART.Areas.Admin.Controllers
{
    public class PatternController : BaseController
    {
        PatternTypeBLL _PatternTypeBLL = new PatternTypeBLL();
        // GET: Admin/Pattern
        public ActionResult Index()
        {
           
            var RESULT=  _PatternTypeBLL.SelectAllPatternTypes();
            return View(RESULT);
        }



        public ActionResult New()
        {
            
           
            var obj = new PatternVM();
            return View(obj);
        }

        public ActionResult Update(int ID)
        {
           
            var obj = _PatternTypeBLL.SelectPatternByID(ID);
            return View(obj);
        }


        public JsonResult Insertajax(PatternVM obj)
        {
            long result = 0;
            result = _PatternTypeBLL.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Updateajax(PatternVM obj)
        {
            long result = 0;
            result = _PatternTypeBLL.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            try
            {
                PatternVM _obj = _PatternTypeBLL.SelectPatternByID(ID);

                int retValue = _PatternTypeBLL.Delete(ID);
                if (retValue >0 )
                {
                    General.DeleteFile(_obj.patternIcon);
                    General.DeleteFile(_obj.PatternMobIcon);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("Error", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}