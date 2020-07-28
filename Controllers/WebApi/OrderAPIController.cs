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
    public class OrderAPIController : ApiController
    {
        OrderBLL Orderobj = new OrderBLL();
        UsersBLL _userbll = new UsersBLL();
        Helper.Generic _Generic = new Helper.Generic();

        [HttpPost]
        [Route("api/OrderAPI/MakeOrder")]
        [ActionName("MakeOrder")]
        public HttpResponseMessage MakeOrder([FromBody] OrderWithDetailsVM obj)
        {
            long result = 0;
           UserObjVM _UserVM2 = _userbll.GetByUserID(obj.Order.UserID); //ToDo 
            if (_UserVM2 != null)
            {

                 result = Orderobj.MakeOrder(obj);
            }
           
           
            return _Generic.ConvertObjectToJSON(result);
        }
        [HttpPost]
        [Route("api/OrderAPI/MakeNewOrder")]
        [ActionName("MakeNewOrder")]
        public HttpResponseMessage MakeNewOrder([FromBody] OrderWithDetailsNEWVM obj)
        {
            long result = 0;
            UserObjVM _UserVM2 = _userbll.GetByUserID(obj.Order.UserID); //ToDo 
            if (_UserVM2 != null)
            {

                result = Orderobj.MakeNewOrder(obj);
            }


            return _Generic.ConvertObjectToJSON(result);
        }


        [HttpPost]
        [Route("api/OrderAPI/GetALLOrderlist")]
        public HttpResponseMessage GetALLOrderlist(BasicVM obj)
        {
            var result = Orderobj.GetusersOrder(obj);

            return _Generic.ConvertObjectToJSON(result);
        }

        [HttpGet]
        [Route("api/OrderAPI/GetSamples")]
        public HttpResponseMessage GetSamples(int prod_id)
        {
            var result = Orderobj.GetAllSamples(prod_id);

            return _Generic.ConvertObjectToJSON(result);
        }

        //[HttpGet]
        //[Route("api/OrderAPI/testimg")]
        //public HttpResponseMessage testimg(string path)
        //{
        //    var result = Orderobj.Saveiamge(path);

        //    return _Generic.ConvertObjectToJSON(result);
        //}


        [HttpPost]
        [Route("api/OrderAPI/GetOrderById")]
        [ActionName("GetOrderById")]
        public HttpResponseMessage GetOrderById([FromBody]OrderVM obj)
        {
            var result = Orderobj.GetusersOrderDetails(obj.ID,obj.Language);

            return _Generic.ConvertObjectToJSON(result);
        }


        [HttpPost]
        [Route("api/OrderAPI/CheckCouponCode")]
        [ActionName("CheckCouponCode")]
        public HttpResponseMessage CheckCouponCode([FromBody]OrderVM obj)
        {
            var result = _userbll.CheckPromoCode(obj.UserID, obj.CouponCode);

            return _Generic.ConvertObjectToJSON(result);
        }



    }
   
}