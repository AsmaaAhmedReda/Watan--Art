using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using WatanART.Helpers;

namespace WatanART.Areas.Admin.Controllers
{
    public class SettingsController : BaseController
    {
        // GET: Admin/Settings
        SettingBLL _SettingBLL = new SettingBLL();
        public ActionResult ContactUs()
        {
            
            return View(_SettingBLL.SelectContactUS(LanguageID));
        }

        public JsonResult Updateajax(SettingObjVM obj)
        {
            long result = 0;
            result = _SettingBLL.UpdateContactUs(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region color & style
        public ActionResult ColorSetting()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectGeneralData("Color");
            return View(RESULT);
            
        }

        public ActionResult SizeSetting()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectGeneralData("Size");
            return View(RESULT);

        }
        public ActionResult StyleSetting()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectGeneralData("Style");
            return View(RESULT);

        }
        public JsonResult InsertGeneralData(string KeyAR, string KeyEN, string Value)
        {
            GeneralDataBLL _GeneralDataBll = new GeneralDataBLL();
            int result = 0;
            result = _GeneralDataBll.insert(KeyAR,KeyEN,Value);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NewColor()
        {
            return View();
        }
        public ActionResult NewSize()
        {
            return View();
        }
        public ActionResult NewStyle()
        {
            return View();
        }
        public ActionResult UpdateColor(int ID)
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectGeneralDatabyID(ID);
            return View(RESULT);
           
        }
        public ActionResult UpdateSize(int ID)
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectGeneralDatabyID(ID);
            return View(RESULT);

        }
        public ActionResult UpdateStyle(int ID)
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectGeneralDatabyID(ID);
            return View(RESULT);

        }
        public JsonResult UpdateGeneralData(int ID,string KeyAR, string KeyEN, string Value)
        {
            GeneralDataBLL _GeneralDataBll = new GeneralDataBLL();
            int result = 0;
            result = _GeneralDataBll.Update(ID,KeyAR, KeyEN, Value);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteGeneralData(int ID)
        {
            try
            {

                GeneralDataBLL _GeneralDataBll = new GeneralDataBLL();
                int result = 0;
                result = _GeneralDataBll.Delete(ID);

                if (result > 0)
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
    
            
        
        #endregion
        public ActionResult HomeSetting()
        {

            return View(_SettingBLL.SelectIntro(LanguageID));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult HomeSettingajax(IntroVM obj)
        {
            long result = 0;
            result = _SettingBLL.UpdateIntro(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult WEBSetting()
        {

            return View(_SettingBLL.SelectwebIntro(LanguageID));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult WEBSettingSettingajax(IntroVM obj)
        {
            long result = 0;
            result = _SettingBLL.UpdatewebIntro(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Pricesettings()
        {

            return View(_SettingBLL.SelectPrices(LanguageID));
        }

        public JsonResult Pricesettingsajax(PricesSettings obj)
        {
            long result = 0;
            result = _SettingBLL.UpdatePrices(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AboutUS()
        {

            return View(_SettingBLL.SelectAboutUs(LanguageID));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult AboutUSajax(IntroVM obj)
        {
            long result = 0;
            result = _SettingBLL.UpdateAboutUs(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult termsandconditions()
        {

            return View(_SettingBLL.SelecttREMS(LanguageID));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult termsandconditionsajax(IntroVM obj)
        {
            long result = 0;
            result = _SettingBLL.UpdatetREMS(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult policy()
        {

            return View(_SettingBLL.Selectpolicy(LanguageID));
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult policyAJAX(IntroVM obj)
        {
            long result = 0;
            result = _SettingBLL.Updatepolicy(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeCurrentCulture(int id)
        {
            //  
            // Change the current culture for this user.  
            //  
            CultureHelper.CurrentCulture = id;
            //  
            // Cache the new current culture into the user HTTP session.   
            //  
            Session["CurrentCulture"] = id;
            //  
            // Redirect to the same page from where the request was made!   
            //  
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}