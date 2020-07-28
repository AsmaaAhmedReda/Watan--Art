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
    public class EventsAPIController : ApiController
    {
        EventBLL _EventBLL = new EventBLL();
        Generic _Generic = new Generic();

        //{
        // Language:0
        //}
        [HttpPost]
        [Route("api/EventsAPI/GetAllEvents")]
        public HttpResponseMessage GetAllEvents([FromBody]BasicVM obj)
        {
            List<EventVM> EventLST = _EventBLL.SelectALL(obj.Language, obj.CurrentPage, (int)obj.RowsPerPage, true);
            return _Generic.ConvertObjectToJSON(EventLST);
        }

        //{
        // EventID:1,
        // Language:0
        //}
        [HttpPost]
        [Route("api/EventsAPI/GetEventDetails")]
        public HttpResponseMessage GetEventDetails([FromBody]EventVM obj)
        {
            EventVM EventObj = _EventBLL.SelectByID(obj.EventID, obj.Language);
            return _Generic.ConvertObjectToJSON(EventObj);
        }

    }
}
