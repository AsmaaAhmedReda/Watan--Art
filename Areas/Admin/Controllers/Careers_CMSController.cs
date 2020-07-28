using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using System.IO;
using TechMart.Web.Models;

namespace WatanART.Areas.Admin.Controllers
{
    public class Careers_CMSController : BaseController
    {
        CareersPositionsBLL _CareersPositionsBLL = new CareersPositionsBLL();
        WorkTypesBLL _WorkTypesBLL = new WorkTypesBLL();
        CareersBLL _CareersBLL = new CareersBLL();
        GenderBLL _GenderBLL = new GenderBLL();
        ApplyCareerBLL _ApplyCareerBLL = new ApplyCareerBLL();
        Generic _Generic = new Generic();

        #region Careers
        // GET: Admin/Careers_CMS
        public ActionResult Index()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_CareersBLL.SelectALLPagging(PageObj));
        }
        public PartialViewResult ViewCareers(int currentRows, int CurrentPage = 1)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            List<CareerVM> lst_res = new List<CareerVM>();



            General.ResolveRowscount<CareerVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _CareersBLL, new object[] { });




            lst_res.AddRange(_CareersBLL.SelectALLPagging(PageObj));
            return PartialView("ViewCareers", lst_res);
        }
        public ActionResult Edit(int id = 0)
        {
            LockUp();
            return View(_CareersBLL.SelectByID(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CareerVM _CareerObj)
        {
            LockUp();
            try
            {
                bool _returnVal = _CareersBLL.Update(_CareerObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Index", "Careers_CMS");

                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_CareerObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_CareerObj);
            }
        }
        public JsonResult DeleteCareer(Int64 CareerID)
        {
            try
            {
                bool retValue = _CareersBLL.CheckIfUsed(new CareerVM() { CareerID = CareerID });
                if (!retValue)
                {
                    retValue = _CareersBLL.Delete(new CareerVM() { CareerID = CareerID });
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
        public JsonResult ActiveDectiveCareer(Int64 CareerID, Boolean IsActive)
        {
            try
            {
                bool retValue = _CareersBLL.UpdateIsActive(CareerID, IsActive);
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

        void LockUp()
        {
            ViewBag.CareersPositions = new SelectList(_CareersPositionsBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "CareerPositionID", "CareerPositionAR");
            ViewBag.WorkTypes = new SelectList(_WorkTypesBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "WorkTypeID", "WorkTypeAR");
            ViewBag.Gender = new SelectList(_GenderBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "GenderID", "GenderAR");
        }

        #endregion

        #region CareersApplicants
        public ActionResult CareersApplicants(long id)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_ApplyCareerBLL.SelectALLPagging(PageObj, Helpers.CultureHelper.CurrentCulture, id));
        }
        public PartialViewResult ViewCareersApplicants(long CareerID, int currentRows, int CurrentPage = 1)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;
            List<ApplyCareerVM> lst_res = new List<ApplyCareerVM>();



            General.ResolveRowscount<ApplyCareerVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _ApplyCareerBLL, new object[] { Helpers.CultureHelper.CurrentCulture, CareerID });

            lst_res.AddRange(_ApplyCareerBLL.SelectALLPagging(PageObj, Helpers.CultureHelper.CurrentCulture, CareerID));
            return PartialView("ViewCareersApplicants", lst_res);
        }
        public JsonResult DeleteCareerApplicant(Int64 ApplyCareerID)
        {
            try
            {
                ApplyCareerVM _ApplyCareer = _ApplyCareerBLL.SelectByID(ApplyCareerID);
                _ApplyCareer.CVFile = _ApplyCareer.CVFile.Replace(_Generic.GetDomainName("UploadedImages"), "");
                bool retValue = _ApplyCareerBLL.Delete(new ApplyCareerVM() { ApplyCareerID = ApplyCareerID });

                if (retValue)
                {
                    try
                    {
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedImages/"), _ApplyCareer.CVFile));
                    }
                    catch
                    {

                    }
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
        #region CareersPositions

        public ActionResult CareersPositions()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_CareersPositionsBLL.SelectALLPagging(PageObj));
        }

        public PartialViewResult ViewCareersPositions(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            List<CareerPositionVM> lst_res = new List<CareerPositionVM>(); ;



            General.ResolveRowscount<CareerPositionVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _CareersPositionsBLL, new object[] { });

            lst_res.AddRange(_CareersPositionsBLL.SelectALLPagging(PageObj));

            return PartialView("ViewCareersPositions", lst_res);

        }
        public ActionResult EditCareerPosition(int id = 0)
        {
            return View(_CareersPositionsBLL.SelectByID(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditCareerPosition(CareerPositionVM _CareerPositionObj)
        {
            try
            {
                bool _returnVal = _CareersPositionsBLL.Update(_CareerPositionObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("CareersPositions", "Careers_CMS");

                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_CareerPositionObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_CareerPositionObj);
            }
        }
        public JsonResult DeleteCareerPosition(int CareerPositionID)
        {
            try
            {
                bool retValue = _CareersPositionsBLL.CheckIfUsed(new CareerPositionVM() { CareerPositionID = CareerPositionID });
                if (!retValue)
                {
                    retValue = _CareersPositionsBLL.Delete(new CareerPositionVM() { CareerPositionID = CareerPositionID });
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
        public JsonResult ActiveDectiveCareerPosition(int CareerPositionID, Boolean IsActive)
        {
            try
            {
                bool retValue = _CareersPositionsBLL.UpdateIsActive(CareerPositionID, IsActive);
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
        #region WorkTypes

        public ActionResult WorkTypes()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_WorkTypesBLL.SelectALLPagging(PageObj));
        }
        public PartialViewResult ViewWorkTypes(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            List<WorkTypeVM> lst_res = new List<WorkTypeVM>(); ;



            General.ResolveRowscount<WorkTypeVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _WorkTypesBLL, new object[] { });

            lst_res.AddRange(_WorkTypesBLL.SelectALLPagging(PageObj));

            return PartialView("ViewWorkTypes", lst_res);

        }
        public ActionResult EditWorkType(int id = 0)
        {
            return View(_WorkTypesBLL.SelectByID(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWorkType(WorkTypeVM _WorkTypeObj)
        {
            try
            {
                bool _returnVal = _WorkTypesBLL.Update(_WorkTypeObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("WorkTypes", "Careers_CMS");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_WorkTypeObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_WorkTypeObj);
            }
        }
        public JsonResult DeleteWorkType(int WorkTypeID)
        {
            try
            {
                bool retValue = _WorkTypesBLL.CheckIfUsed(new WorkTypeVM() { WorkTypeID = WorkTypeID });
                if (!retValue)
                {
                    retValue = _WorkTypesBLL.Delete(new WorkTypeVM() { WorkTypeID = WorkTypeID });
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
        public JsonResult ActiveDectiveWorkType(int WorkTypeID, Boolean IsActive)
        {
            try
            {
                bool retValue = _WorkTypesBLL.UpdateIsActive(WorkTypeID, IsActive);
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