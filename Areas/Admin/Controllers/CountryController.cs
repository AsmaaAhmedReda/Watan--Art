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
    public class CountryController : BaseController
    {
        CountryBLL _CountryBLL = new CountryBLL();
        CityBLL _CityBLL = new CityBLL();
        CurrenciesBLL OBJ = new CurrenciesBLL();
        // GET: Admin/Pattern
        public ActionResult Index()
        {

            var RESULT = _CountryBLL.SelectAllCountries(1);
            return View(RESULT);
        }



        public ActionResult New()
        {


            var obj = new CountryVM();
            ViewBag.crr = OBJ.SelectAllCurrencies(LanguageID);
            return View(obj);
        }

        public ActionResult Update(int ID)
        {

            var obj = _CountryBLL.SelectCountryById(ID,1);
            ViewBag.crr = OBJ.SelectAllCurrencies(LanguageID);
            return View(obj);
        }


        public JsonResult Insertajax(CountryVM obj)
        {
            long result = 0;
            result = _CountryBLL.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Updateajax(CountryVM obj)
        {
            long result = 0;
            result = _CountryBLL.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            try
            {
                //PatternVM _obj = _CountryBLL.SelectPatternByID(ID);

                int retValue = _CountryBLL.Delete(ID);
                if (retValue > 0)
                {
                    //General.DeleteFile(_obj.patternIcon);
                    //General.DeleteFile(_obj.PatternMobIcon);
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






        //city



        public ActionResult CityList(int Id)
        {
            ViewBag.COUNTRY = Id;
            var RESULT = _CityBLL.SelectAllCites(Id,1);
            return View(RESULT);
        }



        public ActionResult NewCity()
        {

            ViewBag.country = _CountryBLL.SelectAllCountries(LanguageID);
           
            var obj = new CityVM();
            return View(obj);
        }

        public ActionResult UpdateCity(int ID)
        {
            ViewBag.country = _CountryBLL.SelectAllCountries(LanguageID);
            
            var obj = _CityBLL.SelectCitYbYid(ID,LanguageID);
            ViewBag.CountryId = obj.CountryID;
            return View(obj);
        }


        public JsonResult InsertCityajax(CityVM obj)
        {
            long result = 0;
            result = _CityBLL.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateCityajax(CityVM obj)
        {
            long result = 0;
            result = _CityBLL.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCity(int ID)
        {
            try
            {
                //PatternVM _obj = _CountryBLL.SelectPatternByID(ID);

                int retValue = _CityBLL.Delete(ID);
                if (retValue > 0)
                {
                    //General.DeleteFile(_obj.patternIcon);
                    //General.DeleteFile(_obj.PatternMobIcon);
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