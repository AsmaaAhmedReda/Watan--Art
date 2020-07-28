using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WatanART.Controllers.WebApi
{
    public class UserRequestsController : ApiController
    {
        Generic _Generic = new Generic();
        ContactUsMessageBLL _ContactUsMessageBLL = new ContactUsMessageBLL();
        NewsLetterEmailsBLL _NewsLetterEmailsBLL = new NewsLetterEmailsBLL();
        ContactUsServicesBLL _ContactUsServicesBLL = new ContactUsServicesBLL();

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/UserRequests/GetContactUsServices")]
        public HttpResponseMessage GetContactUsServices([FromBody]BasicVM obj)
        {
            List<ContactUsServiceVM> ContactUsServicesLST = _ContactUsServicesBLL.SelectALLActive(obj.Language);
            return _Generic.ConvertObjectToJSON(ContactUsServicesLST);
        }


        //{ 
        //  FullName: "FullName",
        //  Email:"Email", 
        //  Subject:"Subject",
        //  Message:"Message",
        //  ServiceID:1
        //}
        [HttpPost]
        [Route("api/UserRequests/ContactUs")]
        public HttpResponseMessage ContactUs([FromBody]ContactUsMessageVM ContactUsMessage)
        {
            Result regresult = new Result();
            try
            {
                string res = _ContactUsMessageBLL.Add(ContactUsMessage);
                if (res == "0")
                {
                    regresult.message = "0";
                    regresult.success = false;
                    return _Generic.ConvertObjectToJSON(regresult);//server error
                }
                else
                {
                    regresult.message = res;
                    regresult.success = true;
                    return _Generic.ConvertObjectToJSON(regresult);//sucess
                }
            }
            catch (Exception ex)
            {
                regresult.message = "0";
                regresult.success = false;
                return _Generic.ConvertObjectToJSON(regresult);//server error
            }
        }


        //{ 
        //Email: "Email"
        //}
        [HttpPost]
        [Route("api/UserRequests/SubscribeNewsLetter")]
        public HttpResponseMessage SubscribeNewsLetter([FromBody]NewsLetterEmailVM NewsLetterEmail)
        {
            Result regresult = new Result();
            try
            {
                bool Used = _NewsLetterEmailsBLL.CheckIfUsed(NewsLetterEmail);
                if (!Used)
                {
                    string res = _NewsLetterEmailsBLL.Add(NewsLetterEmail);
                    if (res == "0")
                    {
                        regresult.message = "0";
                        regresult.success = false;
                        return _Generic.ConvertObjectToJSON(regresult);//server error
                    }
                    else
                    {
                        regresult.message = res;
                        regresult.success = true;
                        return _Generic.ConvertObjectToJSON(regresult);//sucess
                    }
                }
                else
                {
                    regresult.message = "-1";
                    regresult.success = false;
                    return _Generic.ConvertObjectToJSON(regresult);
                }
            }
            catch (Exception ex)
            {
                regresult.message = "0";
                regresult.success = false;
                return _Generic.ConvertObjectToJSON(regresult);//server error
            }
        }
    }
}
