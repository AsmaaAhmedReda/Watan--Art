using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WatanART.Models;
using Twitterizer;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using WatanART.BLL.ViewModels;
using WatanART.BLL.BussinessLayer;
using System.Web.Security;
using System.Security.Principal;

namespace WatanART.Controllers
{
    [Authorize]
    public class AccountController : BaseAccountController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            HttpContext.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        UsersBLL _UserBLL = new UsersBLL();
        Generic _Generic = new Generic();
        public async Task<ActionResult> TwiiterLogged2()
        {
            try
            {
                string DeviceID = "";
                var consumer_key = "yxsUk7nXFNvl8DXU7j7IjdaIb";
                var consumer_secret = "p6WdLENKhzurd8RqMKXkCVJomaQZ1iXh0dd7P7kX9uPbg6Y8KM";
                if (Request["oauth_token"] == null)
                {
                    OAuthTokenResponse reqToken = OAuthUtility.GetRequestToken(consumer_key, consumer_secret, Request.Url.AbsoluteUri);
                    // return Json(string.Format("http://twitter.com/oauth/authorize?oauth_token={0}", reqToken.Token), JsonRequestBehavior.AllowGet);
                    string uri = string.Format("http://twitter.com/oauth/authorize?oauth_token={0}", reqToken.Token);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Method = "Get";

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader responseStream = new StreamReader(response.GetResponseStream());
                    string resultado = responseStream.ReadToEnd();
                    return Content(resultado);
                }
                else
                {
                    string requestToken = Request["oauth_token"].ToString();
                    string pin = Request["oauth_verifier"].ToString();
                    var tokens = OAuthUtility.GetAccessToken(consumer_key, consumer_secret, requestToken, pin);
                    OAuthTokens accessTokens = new OAuthTokens()
                    {
                        AccessToken = tokens.Token,
                        AccessTokenSecret = tokens.TokenSecret,
                        ConsumerKey = consumer_key,
                        ConsumerSecret = consumer_secret
                    };
                    //TwitterResponse<TwitterStatus> response = TwitterStatus.Update(accessTokens, "done");
                    // TwitterResponse<TwitterStatus> response = TwitterStatus.Update(accessTokens, "Testing!! It works (hopefully)2.", new StatusUpdateOptions() { UseSSL = true, APIBaseAddress = "http://api.twitter.com/1.1/" });
                    UserTimelineOptions userOptions = new UserTimelineOptions();
                    userOptions.IncludeRetweets = false;
                    userOptions.ScreenName = tokens.ScreenName;
                    userOptions.UseSSL = true;
                    userOptions.Count = 20;
                    TwitterResponse<TwitterUser> showUserResponse = TwitterUser.Show(accessTokens, tokens.ScreenName, new StatusUpdateOptions() { UseSSL = true, APIBaseAddress = "http://api.twitter.com/1.1/" });
                    var cc = showUserResponse.Content;
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    TwitterSocialVM user = jss.Deserialize<TwitterSocialVM>(cc);
                    UserObjVM _UserVM = _UserBLL.GetBySocialMediaID(user.id_str);
                    if (_UserVM != null)
                    {
                        //update name
                        //user.UserId = _UserVM.UserId;
                        //_UserBLL.Update(user);
                        //var result = await SignInManager.PasswordSignInAsync(_UserVM.FaceBookID, "Pa$$w0rd123456", true, shouldLockout: false);
                        var _user = await UserManager.FindByIdAsync(_UserVM.UserID);
                        var identity = await UserManager.CreateIdentityAsync(_user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                        var authenticationManager = HttpContext.GetOwinContext().Authentication;
                        authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var _user = new WatanART.Models.ApplicationUser { UserName = user.id.ToString(), Email = user.id_str + "@twitter.com" };
                        var result = await UserManager.CreateAsync(_user, "Pa$$w0rd123456");
                        if (result.Succeeded)
                        {
                            //_UserVM = _UserBLL.GetBySocialMediaID(_user.UserName);
                            _UserBLL.Insert(new UserObjVM() { UserID = _user.Id, FB = user.id_str, FullName = user.name });

                            var identity = await UserManager.CreateIdentityAsync(_user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
                            var authenticationManager = HttpContext.GetOwinContext().Authentication;
                            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                        }
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult GetUserName()
        {
            if (User.Identity.GetUserId() != null)
            {
                UserObjVM _UserVM = _UserBLL.GetByUserID(User.Identity.GetUserId());
                if (_UserVM == null)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    //return RedirectToAction("Index", "Home");
                    return Content("");
                }
                else
                {
                    return Content(_UserVM.FullName);
                }
            }
            else
                return Content("");
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("Account/Register_Login_Website")]
        //public async Task<JsonResult> Register_Login_Website([System.Web.Http.FromBody]UsersVM user)
        //{
        //    Result regresult = new Result();
        //    try
        //    {
        //        UsersVM _UserVM = _UserBLL.GetBySocialMediaID(user.SocialMediaID);
        //        if (_UserVM != null)
        //        {
        //            regresult.message = _UserVM.UserID;
        //            regresult.success = true;
        //            var _user = await UserManager.FindByIdAsync(_UserVM.UserID);
        //            var identity = await UserManager.CreateIdentityAsync(_user, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
        //            var authenticationManager = HttpContext.GetOwinContext().Authentication;
        //            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
        //            return Json(_Generic.ConvertStringToJSONForRegister(_UserVM.UserID), JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            if (user.SocialMediaType == 2)
        //            {
        //                user.Email = user.SocialMediaID + "@gmail.com";
        //                user.SocialMediaName = "TW";
        //            }
        //            else
        //            {
        //                user.SocialMediaName = "FB";
        //            }

        //            var _user = new WatanART.Models.ApplicationUser { UserName = user.SocialMediaID, Email = user.Email };
        //            var result = await UserManager.CreateAsync(_user, "Pa$$w0rd123456");
        //            if (result.Succeeded)
        //            {
        //                _UserBLL.Insert(new UsersVM() { UserID = _user.Id, SocialMediaID = user.SocialMediaID, SocialMediaName = user.SocialMediaName, Name = user.Name });
        //                user.DeviceID = "";
        //                regresult.message = _user.Id;
        //                regresult.success = true;
        //                return Json(_Generic.ConvertStringToJSONForRegister(_user.Id), JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                regresult.message = "0";
        //                regresult.success = false;
        //                return Json(_Generic.ConvertStringToJSONForRegister(regresult.message), JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        regresult.message = "0";
        //        regresult.success = false;
        //        return Json(_Generic.ConvertObjectToJSON(regresult), JsonRequestBehavior.AllowGet);
        //    }
        //}


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult LoginAdmin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAdmin(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password,true, false);
            switch (result)
            {
                case SignInStatus.Success:
                    {
                        try
                        {
                            var user = await UserManager.FindAsync(model.Email, model.Password);
                            string roleName = UserManager.GetRoles(user.Id)[0].ToString();
                            if (roleName == "Administrator")
                            {
                                return RedirectToLocal(Url.Action("Index", "Shared", new { area = "Admin" }));
                            }
                            else
                            {
                                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                                return RedirectToLocal(returnUrl);
                            }
                        }
                        catch
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            return RedirectToLocal(returnUrl);
                        }
                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                    {
                        ViewBag.Failure = false;
                        return View(model);
                    }
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        //private ApplicationUserManager _userManager;

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOffAdmin()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

          
            HttpContext.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);
            return RedirectToAction("LoginAdmin", "Account");
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public JsonResult Logoff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);


            HttpContext.User =
                new GenericPrincipal(new GenericIdentity(string.Empty), null);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
    }
}