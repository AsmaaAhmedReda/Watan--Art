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
    public class NewsAPIController : ApiController
    {
        NewsBLL _NewsBLL = new NewsBLL();
        Generic _Generic = new Generic();

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/NewsAPI/GetAllNews")]
        public HttpResponseMessage GetAllNews([FromBody] BasicVM Obj)
        {
            List<NewsVM> NewsLST = _NewsBLL.SelectALL(Obj.Language, Obj.CurrentPage, (int)Obj.RowsPerPage, true);
            return _Generic.ConvertObjectToJSON(NewsLST);
        }



        //{
        // NewsID:1,
        // Language:0
        //}
        [HttpPost]
        [Route("api/NewsAPI/NewsDetails")]
        public HttpResponseMessage NewsDetails([FromBody] NewsVM obj)
        {
            NewsVM NewsObj = _NewsBLL.SelectByID(obj.NewsID, obj.Language);
            return _Generic.ConvertObjectToJSON(NewsObj);
        }
    }
}
