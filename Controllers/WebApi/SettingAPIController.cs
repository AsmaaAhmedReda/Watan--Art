using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;

namespace WatanART.Controllers.WebApi
{
    public class SettingAPIController : ApiController
    {
        SettingBLL SettingBLLoBJ = new SettingBLL();
        ModulesBLL _ModulesBLL = new ModulesBLL();
        CategoryBLL _CategoryBLL = new CategoryBLL();
        Helper.Generic _Generic = new Helper.Generic();


        [HttpPost]
        [Route("api/SettingApi/SellectLists")]
        [ActionName("SellectLists")]
        public HttpResponseMessage SellectLists(BasicVM OBJ )
        {
            var result = SettingBLLoBJ.GetLists_new(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);
       
        }


        [HttpPost]
        [Route("api/SettingApi/GetContactUsData")]
        [ActionName("GetContactUsData")]
        public HttpResponseMessage GetContactUsData(BasicVM OBJ)
        {
            var result = SettingBLLoBJ.SelectContactUS(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);

        }

      
        [HttpPost]
        [Route("api/SettingApi/GetIntro")]
        [ActionName("GetIntro")]
        public HttpResponseMessage GetIntro(BasicVM OBJ)
        {
            var result = SettingBLLoBJ.SelectIntro(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);

        }
        [HttpPost]
        [Route("api/SettingApi/GetSubs")]
        [ActionName("GetSubs")]
        public HttpResponseMessage GetSubs(BasicVM OBJ,int CatID)
        {
            var result = _CategoryBLL.SelectCategoriesbyParent(OBJ.Language,CatID);
            return _Generic.ConvertObjectToJSON(result);

        }
        [HttpPost]
        [Route("api/SettingApi/GeneralData")]
        [ActionName("GeneralData")]
        public HttpResponseMessage GeneralData(BasicVM OBJ)
        {
            var result = _CategoryBLL.SelectGeneralData(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);

        }


        [HttpPost]
        [Route("api/SettingApi/GetGetIntroList")]
        [ActionName("GetGetIntroList")]
        public HttpResponseMessage GetGetIntroList(BasicVM OBJ)
        {
            var result = SettingBLLoBJ.SelectIntroList(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);

        }



        [HttpPost]
        [Route("api/SettingApi/GetALLFQA")]
        [ActionName("GetALLFQA")]

        public HttpResponseMessage GetALLFQA(BasicVM OBJ)
        {
            var result = _ModulesBLL.SelectALLByKey_IsActiveWithLang(new PaggingVM() { CurrentPage = OBJ.CurrentPage ,RowsPerPage=OBJ.RowsPerPage},OBJ.Language,"#FAQ", true);
            return _Generic.ConvertObjectToJSON(result);
        }


        [HttpPost]
        [Route("api/SettingApi/TermsAndConditions")]
        [ActionName("TermsAndConditions")]
        public HttpResponseMessage TermsAndConditions(BasicVM OBJ)
        {
            var result = SettingBLLoBJ.SelecttREMS(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);

        }



        [HttpPost]
        [Route("api/SettingApi/Policy")]
        [ActionName("Policy")]
        public HttpResponseMessage Policy(BasicVM OBJ)
        {
            var result = SettingBLLoBJ.Selectpolicy(OBJ.Language);
            return _Generic.ConvertObjectToJSON(result);

        }



    }
}
