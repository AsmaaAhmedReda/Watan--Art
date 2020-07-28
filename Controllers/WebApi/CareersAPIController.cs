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
    public class CareersAPIController : ApiController
    {
        CareersBLL _CareersBLL = new CareersBLL();
        ApplyCareerBLL _ApplyCareerBLL = new ApplyCareerBLL();


        Generic _Generic = new Generic();

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/CareersAPI/GetAllCareers")]
        public HttpResponseMessage GetAllCareers([FromBody]BasicVM obj)
        {
            List<CareerVM> CareerLST = _CareersBLL.SelectALL(obj.Language, obj.CurrentPage, (int)obj.RowsPerPage, true);
            return _Generic.ConvertObjectToJSON(CareerLST);
        }

        //{
        // CareerID:1,
        // FullName:"FullName",
        // PhoneNumber:"PhoneNumber",
        // EMail:"EMail",
        // Address:"Address",
        // Base64:"CV File Base64",
        // Extension:"CV File Extension '.doc,.pdf'"
        //}
        [HttpPost]
        [Route("api/CareersAPI/ApplyCareer")]
        public HttpResponseMessage ApplyCareer([FromBody]ApplyCareerVM obj)
        {
            obj.CVFile = _Generic.SaveAttach(obj.Base64, obj.Extension, "UploadedImages");

            string ApplyCareerID = _ApplyCareerBLL.Add(obj);
            return _Generic.ConvertBoolToJSON(ApplyCareerID == "0" ? false : true);
        }
    }
}
