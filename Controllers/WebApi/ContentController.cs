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
    public class ContentController : ApiController
    {
        AboutUsBLL _AboutUsBLL = new AboutUsBLL();
        ContactUsBLL _ContactUsBLL = new ContactUsBLL();
        ManagerWordBLL _ManagerWordBLL = new ManagerWordBLL();
        Generic _Generic = new Generic();
        SliderBLL _SliderBLL = new SliderBLL();
        SettingBLL _SettingBLL = new SettingBLL();

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/Content/GetAboutUsData")]
        public HttpResponseMessage GetAboutUsData([FromBody]BasicVM obj)
        {
            AboutUsVM AboutUs = _AboutUsBLL.SelectByID(1, obj.Language);
            var setting = _SettingBLL.SelectAboutUs(obj.Language);
            AboutUs.DescriptionAR = setting.IntroTextar;
            AboutUs.DescriptionEN = setting.IntroTexten;
            AboutUs.ContentName = setting.IntroImage;
            return _Generic.ConvertObjectToJSON(AboutUs);
        }

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/Content/GetContactUsData")]
        public HttpResponseMessage GetContactUsData([FromBody]BasicVM obj)
        {
            ContactUsVM ContactUs = _ContactUsBLL.SelectByID(2, obj.Language);
            return _Generic.ConvertObjectToJSON(ContactUs);
        }

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/Content/GetManagerWordData")]
        public HttpResponseMessage GetManagerWordData([FromBody]BasicVM obj)
        {
            ManagerWordVM ManagerWord = _ManagerWordBLL.SelectByID(3, obj.Language);
            return _Generic.ConvertObjectToJSON(ManagerWord);
        }

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/Content/GetALLSliders")]
        public HttpResponseMessage GetALLSliders([FromBody]BasicVM obj)
        {
            return _Generic.ConvertObjectToJSON(_SliderBLL.SelectALLActive(obj.Language));
        }
    }
}
