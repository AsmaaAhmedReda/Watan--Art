using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechMart.Web.Models;

namespace WatanART.Areas.Admin.Controllers
{
    public class NewsLettersController : BaseController
    {
        NewsLetterEmailsBLL _NewsLetterEmailsBLL = new NewsLetterEmailsBLL();
        // GET: Admin/NewsLetters
        public ActionResult Index()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_NewsLetterEmailsBLL.SelectALLPagging(PageObj));
        }

        public PartialViewResult ViewSubscribeEmail(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            var lst_res = _NewsLetterEmailsBLL.SelectALLPagging(PageObj);
            return PartialView("ViewSubscribeEmail", lst_res);
        }
    }
}