using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using WatanART.Models;
using Microsoft.AspNet.Identity;
using WatanART.DAL;
using System.Text;

namespace WatanART.Controllers.WebApi
{
    public class UsersController : ApiController
    {
        UsersBLL _UserBLL = new UsersBLL();
        Generic _Generic = new Generic();
        SendEMailsBLL _SendEMailsBLL = new SendEMailsBLL();
        ContactUsMessageBLL _ContactUsMessageBLL = new ContactUsMessageBLL();
        public DAL.Entities _dataBase = new Entities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
        [Route("api/Users/Login")]
        public async Task<HttpResponseMessage> Login([FromBody]UserObjVMwithPassword user)
        {
            ResultOUT regresult = new ResultOUT();
            try
            {
                //GET BY SOCIAL
                if (!(string.IsNullOrEmpty( user.FB)))
                {
                    UserObjVM _UserVM = _UserBLL.GetBySocialMediaID(user.FB);
                    if (_UserVM != null)
                    {

                      

                        if (_UserVM.IsLockedOut!=null && _UserVM.IsLockedOut.Value)
                        {
                            regresult.obj = null;
                            regresult.success = false;
                            regresult.Message = "Locked";
                            return _Generic.ConvertObjectToJSON(regresult);
                        }
                        else
                        {
                            _UserBLL.AddNewUserDevice(_UserVM.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                           // _UserBLL.UpdateFcm_Token(_UserVM.UserID, user.Fcm_Token);
                            regresult.obj = _UserVM;
                            //Session["userdata"] = _UserVM;
                            regresult.success = true;
                            return _Generic.ConvertObjectToJSON(regresult);
                           
                        }

                       
                    }
                    else
                    {
                        regresult.obj = null;
                        regresult.success = false;
                        regresult.Message = "Fail";
                        return _Generic.ConvertObjectToJSON(regresult);
                    }
                }
                else
                {
                    //GET BY EMAIL && PASSWORD
                    if (!(string.IsNullOrEmpty(user.Email)) && !(string.IsNullOrEmpty(user.Password)))
                    {
                       
                        var result = await SignInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
                        switch (result)
                        {
                            case SignInStatus.Success:
                                {

                                    var userout = await UserManager.FindAsync(user.Email, user.Password);
                                    if (userout != null)
                                    {
                                        UserObjVM _UserVM2 = _UserBLL.GetByUserID(userout.Id);
                                        if (_UserVM2 != null)
                                        {
                                            _UserBLL.AddNewUserDevice(_UserVM2.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                                            //_UserBLL.UpdateFcm_Token(_UserVM2.UserID, user.Fcm_Token);
                                            regresult.obj = _UserVM2;
                                            //Session["userdata"] = _UserVM2;
                                            regresult.success = true;
                                            return _Generic.ConvertObjectToJSON(regresult);
                                        }
                                        else
                                        {
                                            regresult.obj = null;
                                            regresult.success = false;
                                            regresult.Message = "Fail";
                                            return _Generic.ConvertObjectToJSON(regresult);
                                        }
                                    }
                                    else
                                    {
                                        regresult.obj = null;
                                        regresult.success = false;
                                        regresult.Message = "Fail";
                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }


                                }

                            case SignInStatus.Failure:
                                {
                                    regresult.obj = null;
                                    regresult.success = false;
                                    regresult.Message = "Fail";
                                    return _Generic.ConvertObjectToJSON(regresult);
                                }


                            case SignInStatus.LockedOut:
                                {
                                    regresult.obj = null;
                                    regresult.success = false;
                                    regresult.Message = "Locked";
                                    return _Generic.ConvertObjectToJSON(regresult);
                                }

                            default:
                                {
                                    regresult.obj = null;
                                    regresult.success = false;
                                    regresult.Message = "Fail";
                                    return _Generic.ConvertObjectToJSON(regresult);
                                }
                              

                        }
                        
                    }
                    else
                    {
                        regresult.obj = null;
                        regresult.success = false;
                        regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.ALLVALUESAR : CommonData.ALLVALUESEN;
                        return _Generic.ConvertObjectToJSON(regresult);

                    }
                }
            }
            catch
            {
                regresult.obj = null;
                regresult.success = false;
                return _Generic.ConvertObjectToJSON(regresult);
               
            }

        }





        [HttpPost]
        [Route("api/Users/UserData")]
        public HttpResponseMessage UserData([FromBody]BasicVM user)
        {
            ResultOUT regresult = new ResultOUT();
            try
            {
               

                var _UserVM2 = _UserBLL.GetByUserID(user.UserIdValue ,user.Language);
                if (_UserVM2!=null)
                {
                    regresult.success = true;
                    regresult.obj = _UserVM2;
                }
                else
                {
                    regresult.success = false;
                    regresult.obj = null;
                }
               
                return _Generic.ConvertObjectToJSON(regresult);
            }
            catch
            {
                regresult.success = false;
                regresult.obj = null;
                return _Generic.ConvertObjectToJSON(regresult);

            }

        }





        [HttpPost]
        [Route("api/Users/Register")]
        public async Task<HttpResponseMessage> Register([FromBody]UserObjVMwithPassword user)
        {
            ResultOUTNew regresult = new ResultOUTNew();
            //Register 
            try
            {
               
               
                if (!(string.IsNullOrEmpty(user.Email)) && (!(string.IsNullOrEmpty(user.Password))|| !(string.IsNullOrEmpty(user.FB))) &&!(string.IsNullOrEmpty(user.FullName)) && !(string.IsNullOrEmpty(user.Phone)))
                    {
                    if (user.RegisterType == 2 && (user.Password == null || user.Password == ""))
                    {
                        user.Password = CommonData.DEFAULT_PASSWORD;
                    }
                    //if (string.IsNullOrEmpty(user.UserID))
                    //{

                    //}
                    var EmailExist = _UserBLL.EmailExist(user.Email, user.FB, user.UserID);
                    if (!(EmailExist > 0))
                    {

                        var Membershipuser = new ApplicationUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone };
                        var result = await UserManager.CreateAsync(Membershipuser, user.Password);
                        if (result.Succeeded)
                        {

                            var userout = await UserManager.FindByEmailAsync(user.Email);
                            if (userout != null)
                            {

                                user.UserID = userout.Id;
                                var dbuser = _UserBLL.Insert(user);
                                if (dbuser > 0)
                                {
                                    UserObjVM _UserVM2 = _UserBLL.GetByUserID(userout.Id);
                                    if (_UserVM2 != null)
                                    {
                                        _UserBLL.AddNewUserDevice(_UserVM2.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                                       // _UserBLL.UpdateFcm_Token(_UserVM2.UserID, user.Fcm_Token);
                                        regresult.obj = _UserVM2;
                                        regresult.State = 1;
                                        regresult.success = true;
                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                    else
                                    {

                                        regresult.obj = null;
                                        regresult.success = false;
                                        regresult.State = -1;
                                        // Message = "FIAL"
                                        {

                                        }

                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                }
                                else
                                {
                                    await DeleteUser(user.UserID);
                                    regresult.obj = null;
                                    regresult.State = -1;
                                    regresult.success = false;
                                    regresult.Message = "ERROR";
                                    return _Generic.ConvertObjectToJSON(regresult);
                                }

                            }
                            else
                            {
                                regresult.obj = null;
                                regresult.success = false;
                                regresult.State = -1;
                                return _Generic.ConvertObjectToJSON(regresult);

                            }
                        }

                        else
                        {
                            regresult.obj = null;
                            regresult.success = false;
                            regresult.State = -1;
                            return _Generic.ConvertObjectToJSON(regresult);

                        }

                    }
                    else
                    {
                        regresult.obj = null;
                        regresult.success = false;
                        regresult.State = -2;
                        regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.EmailFoundar : CommonData.EmailfoundEN;
                        return _Generic.ConvertObjectToJSON(regresult);

                    }
                }
                    else
                    {
                        regresult.obj = null;
                        regresult.success = false;
                    regresult.State = -3;
                    regresult.Message =user.Language==(int)Helper.Enums.language.AR ? CommonData.ALLVALUESAR : CommonData.ALLVALUESEN;
                    return _Generic.ConvertObjectToJSON(regresult);

                    }
                
            }
            catch  (Exception EX)
            {
                regresult.obj = null;
                regresult.success = false;
                regresult.State = -4;
                regresult.Message = EX.Message;
                return _Generic.ConvertObjectToJSON(regresult);

            }

        }



        [HttpPost]
        [Route("api/Users/RegisterWebSite")]
        public async Task<HttpResponseMessage> RegisterWebSite([FromBody]UserObjVMwithPassword user)
        {
            ResultOUTNew regresult = new ResultOUTNew();
            //Register 
            try
            {
                if (!(string.IsNullOrEmpty(user.Email)) && (!(string.IsNullOrEmpty(user.Password)) || !(string.IsNullOrEmpty(user.FB))) && !(string.IsNullOrEmpty(user.FullName)) && !(string.IsNullOrEmpty(user.Phone)))
                {
                    if (user.RegisterType == 2 && (user.Password == null || user.Password == ""))
                    {
                        user.Password = CommonData.DEFAULT_PASSWORD;
                    }
                    //if (string.IsNullOrEmpty(user.UserID))
                    //{

                    //}
                    var EmailExist = _UserBLL.EmailExist(user.Email, user.FB, user.UserID);
                    if (!(EmailExist > 0))
                    {

                        var Membershipuser = new ApplicationUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone };
                        var result = await UserManager.CreateAsync(Membershipuser, user.Password);
                        if (result.Succeeded)
                        {

                            var userout = await UserManager.FindByEmailAsync(user.Email);
                            if (userout != null)
                            {

                                user.UserID = userout.Id;
                                var dbuser = _UserBLL.Insert(user);
                                if (dbuser > 0)
                                {
                                    UserObjVM _UserVM2 = _UserBLL.GetByUserID(userout.Id);
                                    if (_UserVM2 != null)
                                    {
                                        //_UserBLL.UpdateFcm_Token(_UserVM2.UserID, user.Fcm_Token);
                                        _UserBLL.AddNewUserDevice(_UserVM2.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                                        regresult.obj = _UserVM2;
                                        regresult.State = 1;
                                        regresult.success = true;

                                        var resultLogged = await SignInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
                                        switch (resultLogged)
                                        {
                                            case SignInStatus.Success:
                                                {

                                                    var useroutLoggeed = await UserManager.FindAsync(user.Email, user.Password);
                                                    if (useroutLoggeed != null)
                                                    {
                                                        UserObjVM _UserVM2Logged = _UserBLL.GetByUserID(userout.Id);
                                                        if (_UserVM2Logged != null)
                                                        {
                                                            //_UserBLL.UpdateFcm_Token(_UserVM2.UserID, user.Fcm_Token);
                                                            _UserBLL.AddNewUserDevice(_UserVM2.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                                                            regresult.obj = _UserVM2Logged;
                                                            regresult.success = true;
                                                            return _Generic.ConvertObjectToJSON(regresult);
                                                        }
                                                        else
                                                        {
                                                            regresult.obj = null;
                                                            regresult.success = false;
                                                            regresult.Message = "Fail";
                                                            return _Generic.ConvertObjectToJSON(regresult);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        regresult.obj = null;
                                                        regresult.success = false;
                                                        regresult.Message = "Fail";
                                                        return _Generic.ConvertObjectToJSON(regresult);
                                                    }


                                                }

                                            case SignInStatus.Failure:
                                                {
                                                    regresult.obj = null;
                                                    regresult.success = false;
                                                    regresult.Message = "Fail";
                                                    return _Generic.ConvertObjectToJSON(regresult);
                                                }


                                            case SignInStatus.LockedOut:
                                                {
                                                    regresult.obj = null;
                                                    regresult.success = false;
                                                    regresult.Message = "Locked";
                                                    return _Generic.ConvertObjectToJSON(regresult);
                                                }

                                            default:
                                                {
                                                    regresult.obj = null;
                                                    regresult.success = false;
                                                    regresult.Message = "Fail";
                                                    return _Generic.ConvertObjectToJSON(regresult);
                                                }


                                        }



                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                    else
                                    {

                                        regresult.obj = null;
                                        regresult.success = false;
                                        regresult.State = -1;
                                        // Message = "FIAL"
                                        {

                                        }

                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                }
                                else
                                {
                                    await DeleteUser(user.UserID);
                                    regresult.obj = null;
                                    regresult.State = -1;
                                    regresult.success = false;
                                    regresult.Message = "ERROR";
                                    return _Generic.ConvertObjectToJSON(regresult);
                                }

                            }
                            else
                            {
                                regresult.obj = null;
                                regresult.success = false;
                                regresult.State = -1;
                                return _Generic.ConvertObjectToJSON(regresult);

                            }
                        }

                        else
                        {
                            regresult.obj = null;
                            regresult.success = false;
                            regresult.State = -1;
                            return _Generic.ConvertObjectToJSON(regresult);

                        }

                    }
                    else
                    {
                        regresult.obj = null;
                        regresult.success = false;
                        regresult.State = -2;
                        regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.EmailFoundar : CommonData.EmailfoundEN;
                        return _Generic.ConvertObjectToJSON(regresult);

                    }
                }
                else
                {
                    regresult.obj = null;
                    regresult.success = false;
                    regresult.State = -3;
                    regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.ALLVALUESAR : CommonData.ALLVALUESEN;
                    return _Generic.ConvertObjectToJSON(regresult);

                }

            }
            catch (Exception EX)
            {
                regresult.obj = null;
                regresult.success = false;
                regresult.State = -4;
                regresult.Message = EX.Message;
                return _Generic.ConvertObjectToJSON(regresult);

            }

        }


        [HttpPost]
        [Route("api/Users/RegisterLoginSocial")]
        public async Task<HttpResponseMessage> RegisterLoginSocial([FromBody]UserObjVMwithPassword user)
        {
            ResultOUTNew regresult = new ResultOUTNew();
            //Register 
            try
            {


                if (!(string.IsNullOrEmpty(user.FB)))
                {

                    user.Password = CommonData.DEFAULT_PASSWORD;

                    UserObjVM _UserVM = _UserBLL.GetBySocialMediaID(user.FB);
                    if (_UserVM != null)
                    {

                        if (_UserVM.IsLockedOut != null && _UserVM.IsLockedOut.Value)
                        {
                            regresult.obj = null;
                            regresult.success = false;
                            regresult.Message = "Locked";
                            return _Generic.ConvertObjectToJSON(regresult);
                          
                        }
                        else
                        {
                            regresult.obj = _UserVM;
                            regresult.success = true;
                            _UserBLL.AddNewUserDevice(_UserVM.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                            return _Generic.ConvertObjectToJSON(regresult);
                        }
                    }
                    else
                    {

                        if (!(string.IsNullOrEmpty(user.Email)))
                        {
                            var EmailExist = await UserManager.FindByEmailAsync(user.Email);
                            if (EmailExist == null)
                            {
                                var Membershipuser = new ApplicationUser { UserName = user.Email, Email = user.Email };
                                var result = await UserManager.CreateAsync(Membershipuser, user.Password);
                                if (result.Succeeded)
                                {

                                    var userout = await UserManager.FindByEmailAsync(user.Email);
                                    if (userout != null)
                                    {

                                        user.UserID = userout.Id;
                                        var dbuser = _UserBLL.Insert(new UserObjVM { UserID = user.UserID, FB = user.FB, Email = user.Email, RegisterType = 2, FullName = user.Email,cityID=0,CountryID=0 });
                                        if (dbuser > 0)
                                        {
                                            UserObjVM _UserVM2 = _UserBLL.GetByUserID(userout.Id);
                                            _UserBLL.AddNewUserDevice(_UserVM2.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                                            // _UserBLL.UpdateFcm_Token(_UserVM2.UserID, user.Fcm_Token);
                                            regresult.obj = _UserVM2;
                                            regresult.success = true;
                                            regresult.State = 1;
                                            return _Generic.ConvertObjectToJSON(regresult);


                                        }
                                        else
                                        {
                                            regresult.obj = null;
                                            regresult.success = false;
                                            regresult.State = -3;

                                            return _Generic.ConvertObjectToJSON(regresult);
                                        }
                                    }
                                    else
                                    {
                                        regresult.obj = null;
                                        regresult.success = false;
                                        regresult.State = -3;

                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                }
                                else
                                {
                                    regresult.obj = null;
                                    regresult.success = false;
                                    regresult.State = -3;

                                    return _Generic.ConvertObjectToJSON(regresult);
                                }

                            }
                            else
                            {

                                _UserBLL.Updatesocial(user.Email, user.FB);
                               
                                //_UserBLL.UpdateFcm_Token(_UserVM.UserID, user.Fcm_Token);
                                regresult.obj = _UserBLL.GetBySocialMediaID(user.FB);
                                var userout = await UserManager.FindByEmailAsync(user.Email);
                                _UserBLL.AddNewUserDevice(userout.Id, user.DeviceID, user.DeviceToken, user.DeviceTypeID);
                                regresult.success = true;
                                regresult.State = 1;
                                return _Generic.ConvertObjectToJSON(regresult);


                            }


                        }
                        else
                        {
                            regresult.obj = null;
                            regresult.success = false;
                            regresult.State = -1; //EMAIL NOT SEND
                            regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.ALLVALUESAR : CommonData.ALLVALUESEN;
                            return _Generic.ConvertObjectToJSON(regresult);
                        }


                    }
                }
                else
                {
                    regresult.obj = null;
                    regresult.success = false;
                    regresult.State = -5; //fbid NOT SEND
                    regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.ALLVALUESAR : CommonData.ALLVALUESEN;
                    return _Generic.ConvertObjectToJSON(regresult);
                }
            }
            catch (Exception EX)
            {
                regresult.obj = null;
                regresult.success = false;
                regresult.State = -4;
                regresult.Message = EX.Message;
                return _Generic.ConvertObjectToJSON(regresult);

            }

        }



        [HttpPost]
        [Route("api/Users/UpdateProfile")]
        public HttpResponseMessage UpdateProfile([FromBody]UserObjVM user)
        {
            ResultOUTNew regresult = new ResultOUTNew();
            //Register 
            try
            {


                if (!(string.IsNullOrEmpty(user.Email)) && !(string.IsNullOrEmpty(user.FullName)) && !(string.IsNullOrEmpty(user.Phone)) && !(string.IsNullOrEmpty(user.UserID)))
                {
                  
                    var EmailExist = _UserBLL.EmailExist(user.Email, user.FB, user.UserID);
                    if (!(EmailExist > 0))
                    {
                        if (user.CountryID==null) {
                            user.CountryID = 0;
                        }

                        if (user.cityID == null)
                        {
                            user.cityID = 0;
                        }

                        var dbuser = _UserBLL.updateUser(user);
                                if (dbuser > 0)
                                {
                                    UserObjVM _UserVM2 = _UserBLL.GetByUserID(user.UserID);
                                    if (_UserVM2 != null)
                                    {
                                       
                                        regresult.obj = _UserVM2;
                                        regresult.success = true;
                                        regresult.State = 1;
                                      
                                      return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                    else
                                    {

                                        regresult.obj = null;
                                        regresult.success = false;
                                // Message = "FIAL"
                                       regresult.State = -1;

                                        return _Generic.ConvertObjectToJSON(regresult);
                                    }
                                }
                                else
                                {
                                  
                                    regresult.obj = null;
                                    regresult.success = false;
                                    regresult.State = -1;
                                    regresult.Message = "ERROR";
                                    return _Generic.ConvertObjectToJSON(regresult);
                                }

                          
                        }

                        else
                        {
                            regresult.obj = null;
                            regresult.success = false;
                        regresult.State = -2;
                        return _Generic.ConvertObjectToJSON(regresult);

                        }

                    
                }
                else
                {
                    regresult.obj = null;
                    regresult.success = false;
                    regresult.State = -3;
                    regresult.Message = user.Language == (int)Helper.Enums.language.AR ? CommonData.ALLVALUESAR : CommonData.ALLVALUESEN;
                    return _Generic.ConvertObjectToJSON(regresult);

                }

            }
            catch (Exception EX)
            {
                regresult.obj = null;
                regresult.success = false;
                regresult.State = -4;
                regresult.Message = EX.Message;
                return _Generic.ConvertObjectToJSON(regresult);

            }

        }



        [HttpPost]
        [Route("api/Users/changePassWord")]
        public async Task<HttpResponseMessage> changePassWord(ChangePasswordViewModel model)
        {
            HttpResponseMessage obj = new HttpResponseMessage();
            int STATE = 0;
            try
            {
                if (string.IsNullOrEmpty(model.OldPassword) || model.OldPassword=="_")
                {
                    model.OldPassword = CommonData.DEFAULT_PASSWORD;
                    STATE = 1;
                }
                var user = _UserBLL.GetByUserID(model.id);
                if (user != null)
                {
                    var result = await UserManager.ChangePasswordAsync(model.id, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        //if (user != null)
                        //{
                        // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        if (result.Succeeded && STATE==1)
                        {
                            _UserBLL.UpdatesoRegisterType(model.id, 3);
                        }
                        return _Generic.ConvertObjectToJSON(true);
                        //}
                       

                    }
                    else
                    {

                        return _Generic.ConvertObjectToJSON(false);
                        //if (result.Errors.FirstOrDefault() == "Incorrect password.")
                        //{

                        //    obj = obj = this.ConvertObjectToJSON(CommonData.FAIL_NO_PASSWORD);
                        //}
                        //else
                        //{
                        //    obj = obj = this.ConvertObjectToJSON(CommonData.FAIL_NO);
                        //}



                    }
                }
                else
                {

                    return _Generic.ConvertObjectToJSON(false);
                }




            }
            catch (Exception)
            {

                return _Generic.ConvertObjectToJSON(false);
            }

           
        }


        [HttpPost]
        [Route("api/Users/Updatetoken")]
        public HttpResponseMessage  Updatetoken([FromBody]UserObjVM user)
        {
            HttpResponseMessage obj = new HttpResponseMessage();
            try
            {
                if (!(string.IsNullOrEmpty(user.UserID)) && !(string.IsNullOrEmpty(user.DeviceID)) && !(string.IsNullOrEmpty(user.DeviceToken)) && !(string.IsNullOrEmpty(user.DeviceTypeID)))

                {

                    if (_UserBLL.AddNewUserDevice(user.UserID, user.DeviceID, user.DeviceToken, user.DeviceTypeID)>0)
                    {
                        return _Generic.ConvertObjectToJSON(true);
                    }
                    else
                    {
                        return _Generic.ConvertObjectToJSON(false);
                    }
                   
                    

                }
                else
                {

                    return _Generic.ConvertObjectToJSON(false);
                }



            }
            catch (Exception)
            {

                return _Generic.ConvertObjectToJSON(false);
            }


        }


        [HttpPost]
        [Route("api/Users/Logout")]
        public HttpResponseMessage Logout([FromBody]UserObjVM user)
        {
            HttpResponseMessage obj = new HttpResponseMessage();
            try
            {
                if (!(string.IsNullOrEmpty(user.UserID)))

                {

                  
                   _UserBLL.LogOut(user.UserID);
                    return _Generic.ConvertObjectToJSON(true);


                }
                else
                {

                    return _Generic.ConvertObjectToJSON(false);
                }



            }
            catch (Exception)
            {

                return _Generic.ConvertObjectToJSON(false);
            }


        }


        [HttpPost]
        [Route("api/Users/ForgetPassword")]
        public async Task<HttpResponseMessage> ForgetPassword(ResetPasswordViewModel model)
        {

            HttpResponseMessage obj = new HttpResponseMessage();
            //SP_SelectUserByEmail_Result objUsers = new SP_SelectUserByEmail_Result();
            try
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    model.Password = CreatePassword(8);
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                    var remove = await UserManager.RemovePasswordAsync(user.Id);
                    if (remove.Succeeded)
                    {
                        var add = await UserManager.AddPasswordAsync(user.Id, model.Password);
                        if (add.Succeeded)
                        {
                            
                            string Message = "لقد طلبت تغيير كلمه المرور الجديده \n New password is \n" + model.Password;
                            //_SendEMailsBLL.Add(new MailVM()
                            //{
                            //    Subject = " New Password-تم تغير كلمه المرور",
                            //    ToMail = model.Email,
                            //    MailBody = _UserBLL.GetByUserID(user.Id).FullName + "<br/>" + model.Email + "<br/>" + Message
                            //});
                            _SendEMailsBLL.Email(model.Email, " New Password-تم تغير كلمه المرور", _UserBLL.GetByUserID(user.Id).FullName + "\n" + model.Email + "\n" + Message);

                            return _Generic.ConvertObjectToJSON(true);

                        }
                        else
                        {

                            return _Generic.ConvertObjectToJSON(false);
                        }

                    }
                    else
                    {

                        return _Generic.ConvertObjectToJSON(false);
                    }

                }
                else
                {

                    return _Generic.ConvertObjectToJSON(false);
                }

            }
            catch (Exception)
            {
                return _Generic.ConvertObjectToJSON(false);

            }
         

        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        public async Task<int> DeleteUser(string id)
        {
            int x = 0;
            var user = await UserManager.FindByIdAsync(id);
            var logins = user.Logins;
            var rolesForUser = await UserManager.GetRolesAsync(id);

            using (var transaction = _dataBase.Database.BeginTransaction())
            {
                foreach (var login in logins.ToList())
                {
                    await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                }

                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                    }
                }

                await UserManager.DeleteAsync(user);
                transaction.Commit();
                x = 1;
            }

            return x;
        }
    }


    
}
