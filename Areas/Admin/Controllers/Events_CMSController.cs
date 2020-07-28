using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechMart.Web.Models;

namespace WatanART.Areas.Admin.Controllers
{
    public class Events_CMSController : BaseController
    {
        EventBLL _EventBLL = new EventBLL();
        Generic _Generic = new Generic();

        // GET: Admin/Events_CMS
        public ActionResult Index()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_EventBLL.SelectALL_CMSPagging(PageObj, Helpers.CultureHelper.CurrentCulture));
        }
        public PartialViewResult ViewEvents(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            var lst_res = new List<EventVM>();

            General.ResolveRowscount<EventVM>(PageObj, currentRows, lst_res, "SelectALL_CMSPagging", _EventBLL, new object[] { Helpers.CultureHelper.CurrentCulture });


            lst_res.AddRange(_EventBLL.SelectALL_CMSPagging(PageObj, Helpers.CultureHelper.CurrentCulture));

            return PartialView("ViewEvents", lst_res);

        }
        public JsonResult DeleteEvent(Int64 EventID)
        {
            try
            {
                EventVM _event = _EventBLL.SelectByID(EventID);

                bool retValue = _EventBLL.Delete(new EventVM() { EventID = EventID });
                if (retValue)
                {
                    General.DeleteFile(_event.EventImage);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("Error", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ActiveDective(Int64 EventID, Boolean IsActive)
        {
            try
            {
                bool retValue = _EventBLL.UpdateIsActive(EventID, IsActive);
                if (retValue)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("Error", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Edit(long id = 0)
        {
            return View(_EventBLL.SelectByID_CMS(id, Helpers.CultureHelper.CurrentCulture));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EventVM _eventObj)
        {

            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    _eventObj.EventImage = General.SaveFile(Request.Files);


                DateTime EventStartTime = DateTime.ParseExact(_eventObj.EventStartTime_Str, "h:mm tt", CultureInfo.InvariantCulture);
                _eventObj.EventStartTime = EventStartTime.TimeOfDay;

                DateTime EventEndTime = DateTime.ParseExact(_eventObj.EventEndTime_Str, "h:mm tt", CultureInfo.InvariantCulture);
                _eventObj.EventEndTime = EventEndTime.TimeOfDay;

                bool _returnVal = _EventBLL.Update(_eventObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_eventObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_eventObj);
            }
        }
    }
}