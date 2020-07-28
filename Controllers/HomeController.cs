using WatanART.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;

namespace WatanART.Controllers
{
    public class HomeController : BaseController
    {
        SettingBLL SettingBLLoBJ = new SettingBLL();
        ModulesBLL _ModulesBLL = new ModulesBLL();
        CountryBLL CountryBLLObj = new CountryBLL();
        CityBLL CityBLLObj = new CityBLL();
        OrderBLL Orderobj = new OrderBLL();
        UsersBLL _UsersBLL = new UsersBLL();

        public ActionResult Index()
        {
            PaggingVM paggObj = new PaggingVM() { RowsPerPage = 10, CurrentPage = 1,listlang=CultureHelper.CurrentCulture };
            var d = SettingBLLoBJ.SelectIntro(LanguageID);
            ViewBag.HomeData = SettingBLLoBJ.SelectIntro(LanguageID);
            ViewBag.videoLink = d.IntroVideo;
            ViewBag.MeshCanvas = SettingBLLoBJ.SelectIntroList(LanguageID);
            ViewBag.FEATURES=_ModulesBLL.SelectALLByKey_IsActive(paggObj, "#FEATURES", true);
            ViewBag.Slider = _ModulesBLL.SelectALLByKey_IsActive(paggObj, "#Slider", true);
            var Shapes=_ModulesBLL.SelectALLByKey_IsActive(paggObj, "#Shapes", true);
            ViewBag.Shapes = _ModulesBLL.SelectALLByKey_IsActive(paggObj, "#Shapes", true);
            var r=_ModulesBLL.SelectALLByKey_IsActive(paggObj, "#REVIEWS", true);
            ViewBag.REVIEWS= _ModulesBLL.SelectALLByKey_IsActive(paggObj, "#REVIEWS", true);
            return View();
        }

        public ActionResult About()
        {


            var aboutus = SettingBLLoBJ.SelectAboutUs(CultureHelper.CurrentCulture);
            ViewBag.IntroText = aboutus.IntroText;
            ViewBag.IntroImage = aboutus.IntroImage;
            return View();
        }


        public ActionResult privacy()
        {
            //ViewBag.privacy =;
            var ObjPrivacy = SettingBLLoBJ.Selectpolicy(CultureHelper.CurrentCulture);
            ViewBag.privacy = ObjPrivacy.IntroText;
            return View();
        }

        public ActionResult FAQ()
        {
           var faq= _ModulesBLL.SelectALLByKey_IsActiveWithLang(new PaggingVM() { CurrentPage = 1, RowsPerPage = 1000 }, LanguageID, "#FAQ", true);
            ViewData["faq"] = faq;
            return View();
        }

        public ActionResult Features(long featureID)
        {
            _ModulesBLL.Language = LanguageID;
            var obj = _ModulesBLL.SelectByID(featureID);
            return View(obj);
        }

        public ActionResult register()
        {
            if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()) && Request.UrlReferrer.AbsoluteUri.Contains("/Home/Makeorder"))

            {

                ViewBag.oldlink = "/Home/shipping_order";

            }
            else
            {
                ViewBag.oldlink = "";
            }
            var allcountries= CountryBLLObj.SelectAllCountries(LanguageID);
            ViewData["allcountries"] = new SelectList(CountryBLLObj.SelectAllCountries(LanguageID), "ID", "Title");
            var ObjTerms = SettingBLLoBJ.SelecttREMS(LanguageID);
            ViewBag.Terms = ObjTerms.IntroText;
            return View();
        }

        public JsonResult ViewCities(int CountryID)
        {
            var cities= CityBLLObj.SelectAllCites(CountryID, LanguageID).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Profile()
        {
            var _UserBLL = new UsersBLL();
            var _UserVM2 = _UserBLL.GetByUserID(LoggedUserID,LanguageID);
            var allcountries = CountryBLLObj.SelectAllCountries(LanguageID);
            ViewData["allcountries"] = new SelectList(CountryBLLObj.SelectAllCountries(LanguageID), "ID", "Title");
            return View(_UserVM2);
        }

        public ActionResult Makeorder()
        {
            PatternTypeBLL _PatternTypeBLL = new PatternTypeBLL();
            ViewBag.pattern = _PatternTypeBLL.SelectAllPatternTypeswithlang(CultureHelper.CurrentCulture);
            return View();
        }

        public ActionResult order_details(long ID)
        {
            BasicVM obj = new BasicVM() { Language = LanguageID, CurrentPage = 1, RowsPerPage = 1000, UserIdValue = LoggedUserID };
            var orderObj = Orderobj.GetusersOrder(obj).ToList().Where(x=>x.ID==ID).FirstOrDefault();

            ViewBag.StateValue = orderObj.StateValue;
            ViewBag.orderData = orderObj.CreatedDate.ToString("dd/MM/yyyy");
            ViewBag.OrderNumber = ID;
            ViewBag.TotalPric = orderObj.ItemsCost;

            var result = Orderobj.GetusersOrderDetails(ID, LanguageID);
            return View(result);
        }
        [Authorize]
        public ActionResult order_history()
        {
            BasicVM obj = new BasicVM() { Language = LanguageID, CurrentPage = 1, RowsPerPage = 1000, UserIdValue = LoggedUserID }; //"f4ab7e70-db06-4452-9860-6d700c5a9617" };
            var result = Orderobj.GetusersOrder(obj);
            return View(result);
        }

        public ActionResult shipping_order()
        {
            ViewBag.shippingPrice = SettingBLLoBJ.SelectPrices(LanguageID);
            ViewData["allcountries"] = new SelectList(CountryBLLObj.SelectAllCountries(LanguageID), "ID", "Title");
            var pricesList= SettingBLLoBJ.GetLists_new(LanguageID);
            ViewBag.OnePiecePrice = pricesList.Prices.LocalPiecePrice;
            ViewBag.ThreePiecePrice = pricesList.Prices.Local3PiecePrice;
            ViewBag.Discount= pricesList.Prices.DiscountRate;
            var _UserBLL = new UsersBLL();
            var _UserVM2 = _UserBLL.GetByUserID(LoggedUserID, LanguageID);
            ViewBag.userphonenumber = (_UserVM2!=null && _UserVM2.Phone!=null)? _UserVM2.Phone :"";
            return View();
        }



        public JsonResult CheckPromoCode(string UserID, string CouponCode)
        {
            // 1 valid 
            // -1 not avalibe to use my code
            //-2  Used before
            //-3 NotValid

            int PromoCode = _UsersBLL.CheckPromoCode(UserID, CouponCode);
            return Json(PromoCode, JsonRequestBehavior.AllowGet);
        }

             
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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


        // LogOffAdmin

        public ActionResult logOFF()
        {
           
            if (Request.Cookies["UserID"] != null)
            {

                //Request.Cookies["UserID"]
                var c = new HttpCookie("UserID");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                Response.Cookies.Clear();

                HttpCookie aCookie = Request.Cookies["UserID"];
                aCookie.Values.Remove("UserID");
            }

            if (User.Identity.IsAuthenticated)
            {
                //return RedirectToAction("LogOff", "Account");
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("../" + this.Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                return RedirectToAction("../" + this.Request.UrlReferrer.AbsolutePath);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        
    }
}