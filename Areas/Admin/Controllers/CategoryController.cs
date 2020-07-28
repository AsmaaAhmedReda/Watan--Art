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

   
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        CategoryBLL _CategoryBLL = new CategoryBLL();
        public ActionResult Index()
        {

            var RESULT = _CategoryBLL.SelectAllCategories(LanguageID);
            return View(RESULT);
        }

        public ActionResult New()
        {

            var obj = new CategoryVM();
            ViewBag.cat = _CategoryBLL.SelectCategoriesbyParent(LanguageID,0);
            return View(obj);
        }
        public ActionResult Update(int ID)
        {

            var obj = _CategoryBLL.SelectCategoryById(ID, LanguageID);
            ViewBag.cat = _CategoryBLL.SelectCategoriesbyParent(LanguageID,0);
            return View(obj);
        }
        public JsonResult Insertajax(CategoryVM obj)
        {
            long result = 0;
            result = _CategoryBLL.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Updateajax(CategoryVM obj)
        {
            long result = 0;
            result = _CategoryBLL.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            try
            {
                //PatternVM _obj = _CountryBLL.SelectPatternByID(ID);

                int retValue = _CategoryBLL.Delete(ID);
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