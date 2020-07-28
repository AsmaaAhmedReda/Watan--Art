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
    public class SliderController : BaseController
    {
        Generic _Generic = new Generic();
        SliderBLL _SliderBLL = new SliderBLL();
        // GET: Admin/Slider
        public ActionResult Index()
        {
            PaggingVM PageObj = new PaggingVM()
            {
                CurrentPage = 1
            };
            ViewBag.PageObj = PageObj;
            return View(_SliderBLL.SelectALLPagging(PageObj));
        }

        public PartialViewResult ViewSlider(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM()
            {
                CurrentPage = CurrentPage
            };
            ViewBag.PageObj = PageObj;

            var lst_res = new List<SliderVM>();

            General.ResolveRowscount<SliderVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _SliderBLL, new object[] { });

            lst_res.AddRange(_SliderBLL.SelectALLPagging(PageObj));

            return PartialView("ViewSlider", lst_res);

        }

        public ActionResult Edit(int id = 0)
        {
            return View(_SliderBLL.SelectByID(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SliderVM _SliderObj)
        {

            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    _SliderObj.SliderImage = General.SaveFile(Request.Files);

                bool _returnVal = _SliderBLL.Update(_SliderObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Index", "Slider");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_SliderObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_SliderObj);
            }
        }
        public JsonResult DeleteSlider(int SliderID)
        {
            try
            {
                SliderVM _Slider = _SliderBLL.SelectByID(SliderID);

                bool retValue = _SliderBLL.Delete(new SliderVM()
                {
                    SliderID = SliderID
                });
                if (retValue)
                {
                    General.DeleteFile(_Slider.SliderImage);
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
        public JsonResult ActiveDective(int SliderID, Boolean IsActive)
        {
            try
            {
                bool retValue = _SliderBLL.UpdateIsActive(SliderID, IsActive);
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
    }
}