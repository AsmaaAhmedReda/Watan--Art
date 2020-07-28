using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WatanART.Helpers;
using System.Web.Routing;
using WatanART.BLL.ViewModels;
using System.Web.Security;

namespace WatanART.Controllers
{
    public class BaseController : Controller
    {
        public int LanguageID { get { return Helpers.CultureHelper.CurrentCulture; } }
        public string LoggedUserID
        {
            get
            {
                try
                {
                    Helpers.CultureHelper.LoggedUserID = User.Identity.GetUserId() == null ? "-1" : User.Identity.GetUserId();
                    return Helpers.CultureHelper.LoggedUserID;
                }
                catch
                {
                    Helpers.CultureHelper.LoggedUserID = "-1";
                    return Helpers.CultureHelper.LoggedUserID;
                }

            }
        }
        private UserObjVM _LoggedUser;
        public UserObjVM LoggedUser
        {
            get
            {
                try
                {

                    return new UserObjVM() { UserID = User.Identity.GetUserId() == null ? "-1" : User.Identity.GetUserId() , role = User.Identity.GetUserId() == null ? "-1" : (User.IsInRole("Administrator") ? "admin": "User") };
                    //return _LoggedUser==null&& _LoggedUser.UserID==null? new UserObjVM() { UserID = "-1", role = "-1" }: _LoggedUser;
                }
                catch
                {
                    return new UserObjVM() { UserID = "-1", role="-1" };
                }

            }
            set
            {

                _LoggedUser = value;
            }
        }

        protected override void ExecuteCore()
        {

            int culture = 1;
            if (this.Session == null || this.Session["CurrentCulture"] == null)
            {
                HttpCookie myCookie = HttpContext.Request.Cookies["CurrentUICulture"];
                if (myCookie != null)
                {
                    culture = Convert.ToInt32(myCookie.Value == "0" ? "1" : myCookie.Value);
                }
                else
                {
                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                }

                this.Session["CurrentCulture"] = culture;
            }
            else
            {
                culture = (int)this.Session["CurrentCulture"];
            }
            CultureHelper.CurrentCulture = culture;

            base.ExecuteCore();
        }
        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }
        //protected override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (context.ActionDescriptor.ActionName != "profile")
        //    {
        //        var values = new Dictionary<string, string> {{"action", "profile"},
        //                                                 {"controller", "home"}};

        //        var routeValDict = new RouteValueDictionary(values);
        //        //redirect the request to MissingDatabase action method.
        //        context.Result = new RedirectToRouteResult(routeValDict);

        //        context.Response.Redirect("/home/profile");
        //    }
        //}

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            return base.RedirectToAction(action, controller);
        }
    }

    public class VerifySetupIsGood : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            // context.RouteData.

            var area = context.RouteData.DataTokens["area"];
            //BaseController _base = new BaseController();
            string actionName = context.ActionDescriptor.ActionName;

            string userID = null;
            string role = null;// User.IsInRole("Administrator") ? "admin" : "User")
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                userID = context.HttpContext.User.Identity.GetUserId();
                role = context.HttpContext.User.IsInRole("Administrator") ? "admin" : "User";
            }
            if (userID != null && role != null && role == "User" && area!= null)
            {

                //redirect the request to MissingDatabase action method.
                //context.ActionDescriptor =new ActionDescriptor() { "profile";
                //var controller = (Controller)context.Controller;
                //context.Result = controller.red("profile", "home");
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home",
                    area = ""
                }));

            }
            else if (userID != null && role !=null && role == "admin" && area!= "Admin" && actionName!= "LogOffAdmin" && actionName != "LoginAdmin")
            {

                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Shared",
                    area = "Admin"
                }));

                //});
                //        var controller = (Controller)context.Controller;
                //        context.Result = controller.RedirectToAction("Index", "Admin/Shared");
            }
            else
            {
                //var controller = (BaseController)context.Controller;
                //context.Result = controller.RedirectToAction("Index", "home");
            }
        }
    }


    public class BaseAccountController : Controller
    {
        public int LanguageID { get { return Helpers.CultureHelper.CurrentCulture; } }
        public string LoggedUserID
        {
            get
            {
                try
                {
                    Helpers.CultureHelper.LoggedUserID = User.Identity.GetUserId() == null ? "-1" : User.Identity.GetUserId();
                    return Helpers.CultureHelper.LoggedUserID;
                }
                catch
                {
                    Helpers.CultureHelper.LoggedUserID = "-1";
                    return Helpers.CultureHelper.LoggedUserID;

                }

            }
        }


        public UserObjVM LoggedUser
        {
            get
            {
                try
                {
                    return new UserObjVM() { UserID = User.Identity.GetUserId() == null ? "-1" : User.Identity.GetUserId(), role = User.Identity.GetUserId() == null ? "-1" : (User.IsInRole("RegisterUser") == true ? "User" : "admin") };

                }
                catch
                {
                    return new UserObjVM() { UserID = "-1", role = "-1" };
                }

            }
        }


        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            int culture = 1;
            if (this.Session == null || this.Session["CurrentCulture"] == null)
            {
                HttpCookie myCookie = HttpContext.Request.Cookies["CurrentUICulture"];
                if (myCookie != null)
                {
                    culture = Convert.ToInt32(myCookie.Value == "0" ? "1" : myCookie.Value);
                }
                else
                {
                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                }

                this.Session["CurrentCulture"] = culture;
            }
            else
            {
                culture = (int)this.Session["CurrentCulture"];
            }
            // calling CultureHelper class properties for setting  
            CultureHelper.CurrentCulture = culture;



            return base.BeginExecuteCore(callback, state);
        }
        //[System.Web.Mvc.HttpPost]
        //public JsonResult IsUniqueUserEmail(string email)
        //{
        //    return Json(new Common.BLL.UserAccounts().IsUniqueUserEmail(email), JsonRequestBehavior.AllowGet);
        //}

        //public bool ChangePassword(string UserID, string NewPassword)
        //{

        //    try
        //    {
        //        new Common.BLL.UserAccounts().ChangePassword(UserID, NewPassword);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}
    }


    public class HandleAntiForgeryError : ActionFilterAttribute, IExceptionFilter
    {
        #region IExceptionFilter Members
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception as HttpAntiForgeryException;
            if (exception != null)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Home";
                routeValues["action"] = "Index";
                filterContext.Result = new RedirectToRouteResult(routeValues);
                filterContext.ExceptionHandled = true;
            }
        }

        #endregion
    }
}