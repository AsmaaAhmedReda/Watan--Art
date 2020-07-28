using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechMart.Web.Models;

namespace WatanART.Areas.Admin.Controllers
{
    public class ContentsController : BaseController
    {
        AboutUsBLL _AboutUsBLL = new AboutUsBLL();
        ContactUsBLL _ContactUsBLL = new ContactUsBLL();
        ManagerWordBLL _ManagerWordBLL = new ManagerWordBLL();
        ContactUsMessageBLL _ContactUsMessageBLL = new ContactUsMessageBLL();
        Generic _Generic = new Generic();
        ContactUsServicesBLL _ContactUsServicesBLL = new ContactUsServicesBLL();

        // GET: Admin/Contents
        public ActionResult Index()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1,RowsPerPage=int.MaxValue };
            ViewBag.PageObj = PageObj;
            var list = _ContactUsMessageBLL.SelectALLPagging(PageObj);
            return View(_ContactUsMessageBLL.SelectALLPagging(PageObj));
        }

        public PartialViewResult ViewMessages(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;
            List<ContactUsMessageVM> lst_res = new List<ContactUsMessageVM>();



            General.ResolveRowscount<ContactUsMessageVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _ContactUsMessageBLL, new object[] { });


            //General.ResolveRowscount<ContactUsMessageVM >(PageObj, currentRows, lst_res, lst_diff);

            lst_res.AddRange(_ContactUsMessageBLL.SelectALLPagging(PageObj));
            return PartialView("ViewMessages", lst_res);
        }


        public PartialViewResult ViewDetiles(long id)
        {
          
           ContactUsMessageVM lst_res = new ContactUsMessageVM();



           
            return PartialView("ViewDetiles", _ContactUsMessageBLL.Selectbyid(id));
        }


        public ActionResult ContactUs()
        {
            return View(_ContactUsBLL.SelectByID(2));
        }
        [HttpPost]
        public ActionResult ContactUs(ContactUsVM obj)
        {
            try
            {
                if (_ContactUsBLL.Update(obj))
                    ViewBag.ErrorMsg = true;
                else
                    ViewBag.ErrorMsg = false;
                return View(obj);
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(obj);
            }
        }
        public JsonResult DeleteContactUsMessage(int ServiceID)
        {
            try
            {
                bool retValue = false;// _ContactUsMessageBLL.CheckIfUsed(new ContactUsMessageVM() { MessageID =(long) ServiceID });
                if (!retValue)
                {
                    retValue = _ContactUsMessageBLL.Delete(new ContactUsMessageVM() { MessageID = (long)ServiceID });
                    if (retValue)
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    else
                        return Json("Error", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Used", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        #region ContactUsServices
        public ActionResult ContactUsServices()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_ContactUsServicesBLL.SelectALLPagging(PageObj));
        }
        public PartialViewResult ViewContactUsServices(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;
            List<ContactUsServiceVM> lst_res = new List<ContactUsServiceVM>();



            General.ResolveRowscount<ContactUsServiceVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _ContactUsServicesBLL, new object[] { });

            lst_res.AddRange(_ContactUsServicesBLL.SelectALLPagging(PageObj));
            return PartialView("ViewContactUsServices", lst_res);
        }
        public ActionResult EditContactUsService(int id = 0)
        {
            return View(_ContactUsServicesBLL.SelectByID(id));
        }
        [HttpPost]
        public ActionResult EditContactUsService(ContactUsServiceVM _ContactUsServiceObj)
        {
            try
            {
                bool _returnVal = _ContactUsServicesBLL.Update(_ContactUsServiceObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("ContactUsServices", "Contents");

                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_ContactUsServiceObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_ContactUsServiceObj);
            }
        }
        public JsonResult DeleteContactUsService(int ServiceID)
        {
            try
            {
                bool retValue = _ContactUsServicesBLL.CheckIfUsed(new ContactUsServiceVM() { ServiceID = ServiceID });
                if (!retValue)
                {
                    retValue = _ContactUsServicesBLL.Delete(new ContactUsServiceVM() { ServiceID = ServiceID });
                    if (retValue)
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    else
                        return Json("Error", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Used", JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ActiveDective(int ServiceID, Boolean IsActive)
        {
            try
            {
                bool retValue = _ContactUsServicesBLL.UpdateIsActive(ServiceID, IsActive);
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
        #endregion
    }
}