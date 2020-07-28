using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;

namespace WatanART.Controllers.WebApi
{
    public class PlatformAPIController : ApiController
    {


        ProductBLL _ProductBLL = new ProductBLL();
        Helper.Generic _Generic = new Helper.Generic();
        [HttpGet]
        [Route("api/PlatformAPI/Home")]
        public HttpResponseMessage Home()
        {
            var lang = Request.Headers.AcceptLanguage;
            int Language = 1;
            if (lang.ToString() == "ar")
            {
                Language = 1;
            }
            else if (lang.ToString() == "en")

            {
                Language = 2;
            }
            var result = _ProductBLL.GetHomeProduct(Language);

            return _Generic.ConvertObjectToJSON(result);
        }

       

    }
   
}