using WatanART.BLL;
using WatanART.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using System.Data.Entity.Core.Objects;
using WatanART.BLL.ViewModels;

namespace WatanART.Areas.Admin.Controllers
{
    //[Authorize(Roles = "AppAdmin,SuperAdmin")]
    public class AdvertisementController : Controller
    {
        UsersBLL _UserBLL = new UsersBLL();
        MessagBLL _MessagBLL = new MessagBLL();
        NotificationsBLL _NotificationsBLL = new NotificationsBLL();
        // GET: Admin/Message
       

        public ActionResult SendNotfaction()
        {
            //SettingData();
            ObjectParameter totalCount = new ObjectParameter("TotalCount", 0);
            totalCount.Value = 0;
           var list= _UserBLL.GetUsersNew(0, int.MaxValue, totalCount, null, null, null);
            return View(list);
        }

        public JsonResult SendPushNotification(notfactionForAllVM _OBJ)
        {
            try
            {
              
                foreach (var item in _OBJ.UserList)
                {
                    _NotificationsBLL.addNewNotify(item.Userid, _OBJ.Message, item.TYPE);
                }



                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       
    }

}