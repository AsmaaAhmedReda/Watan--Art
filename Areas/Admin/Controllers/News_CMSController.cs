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
    public class News_CMSController : BaseController
    {
        Generic _Generic = new Generic();
        NewsBLL _NewsBLL = new NewsBLL();
        NewsTypeBLL _NewsTypeBLL = new NewsTypeBLL();

        #region News
        // GET: Admin/News_CMS
        public ActionResult Index()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_NewsBLL.SelectALLPagging(PageObj));
        }

        public PartialViewResult ViewNews(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;
            var lst_res = new List<NewsVM>();

            General.ResolveRowscount<NewsVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _NewsBLL, new object[] { });



            lst_res.AddRange(_NewsBLL.SelectALLPagging(PageObj));

            return PartialView("ViewNews", lst_res);

        }

        public ActionResult Edit(int id = 0)
        {
            ViewBag.NewsTypes = new SelectList(_NewsTypeBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "NewsTypeID", "NameAR");
            return View(_NewsBLL.SelectByID(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsVM _newsObj)
        {
            NewsVM _newsOldObj = new NewsVM();
            ViewBag.NewsTypes = new SelectList(_NewsTypeBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "NewsTypeID", "NameAR");
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    _newsObj.NewsImage = General.SaveFile(Request.Files);

                bool _returnVal = _NewsBLL.Update(_newsObj);
                if (_returnVal == true)
                {

                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Index", "News_CMS");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_newsObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_newsObj);
            }
        }
        public JsonResult DeleteNews(Int64 NewsID)
        {
            try
            {
                NewsVM _news = _NewsBLL.SelectByID(NewsID);

                bool retValue = _NewsBLL.Delete(new NewsVM() { NewsID = NewsID });
                if (retValue)
                {
                    General.DeleteFile(_news.NewsImage);
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
        public JsonResult ActiveDective(Int64 NewsID, Boolean IsActive)
        {
            try
            {
                bool retValue = _NewsBLL.UpdateIsActive(NewsID, IsActive);
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

        #region NewsTypes
        public ActionResult NewsTypes()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            var lst_res = _NewsTypeBLL.SelectALLPagging(PageObj);
            return View(lst_res);
        }

        public PartialViewResult ViewNewsTypes(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            var lst_res = new List<NewsTypeVM>();

            General.ResolveRowscount<NewsTypeVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _NewsTypeBLL, new object[] { });
            lst_res.AddRange(_NewsTypeBLL.SelectALLPagging(PageObj));


            return PartialView("ViewNewsTypes", lst_res);

        }

        public ActionResult EditNewsType(int id = 0)
        {
            return View(_NewsTypeBLL.SelectByID(id));
        }
        [HttpPost]
        public ActionResult EditNewsType(NewsTypeVM _newsTypeObj)
        {
            try
            {
                bool _returnVal = _NewsTypeBLL.Update(_newsTypeObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("NewsTypes", "News_CMS");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_newsTypeObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_newsTypeObj);
            }
        }
        public JsonResult DeleteNewsType(int NewsTypeID)
        {
            try
            {
                bool retValue = _NewsTypeBLL.CheckIfUsed(new NewsTypeVM() { NewsTypeID = NewsTypeID });
                if (!retValue)
                {
                    retValue = _NewsTypeBLL.Delete(new NewsTypeVM() { NewsTypeID = NewsTypeID });
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
        #endregion

    }
}