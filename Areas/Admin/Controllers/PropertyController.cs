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
    public class PropertyController : BaseController
    {
        // GET: Admin/Property
        PropertyBLL _PropertyBLL = new PropertyBLL();
        public ActionResult Index()
        {
            var RESULT = _PropertyBLL.SelectAllProperties(LanguageID);
            return View(RESULT);
        }
        public ActionResult New()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
           var obj = new PropertiesVM();
            ViewBag.cat = _CategoryBLL.SelectCategoriesbyParent(LanguageID, 0);
            return View(obj);
          
        }
        public ActionResult Update(int ID)
        {

            var obj = _PropertyBLL.SelectPropertyById(ID, LanguageID);
        
            return View(obj);
        }
        public JsonResult Insertajax(PropertiesVM obj)
        {
            long result = 0;
              result = _PropertyBLL.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Updateajax(PropertiesVM obj)
        {
            long result = 0;
            result = _PropertyBLL.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            try
            {

                int retValue = _PropertyBLL.Delete(ID);
                if (retValue > 0)
                {

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

        public JsonResult DeleteOption(int ID)
        {
            try
            {

                int retValue = _PropertyBLL.DeleteOptions(ID);
                if (retValue > 0)
                {

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