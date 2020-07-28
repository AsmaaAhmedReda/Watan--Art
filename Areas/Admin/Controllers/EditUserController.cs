using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Areas.Admin.Controllers
{
    public class EditUserController : BaseController
    {
        public UsersBLL _UsersBLL = new UsersBLL();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public EditUserController()
        {
        }

        public EditUserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Admin/EditUser
        public ActionResult EditUser()
        {
            WatanART.Models.ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            UsersVM _UserData = new UsersVM();
            _UserData.Email = user.Email;
            _UserData.Password = user.PasswordHash;
            return View(_UserData);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditUser(UsersVM _UserData)
        {
            try
            {
                _UserData.UserID = User.Identity.GetUserId();
                _UserData.Password = UserManager.PasswordHasher.HashPassword(_UserData.Password);
                _UsersBLL.Update_Login_Data(_UserData);
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("LoginAdmin", "Account", new { area = "" });
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_UserData);
            }
        }
    }
}