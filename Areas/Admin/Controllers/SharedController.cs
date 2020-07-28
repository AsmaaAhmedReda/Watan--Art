using WatanART.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Areas.Admin.Controllers
{
    public class SharedController : BaseController
    {
        // GET: Admin/Shared
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult DeleteImageajax(string ImageName)
        {

            var resultout = 0;

            //var emp = _EmployeesBLL.UpdateUsers(model);

            //if (emp > 0)
            //{
            //    resultout = 1;//done
            //}
            //else
            //{
            //    // await DeleteUser(user.Id);
            //    resultout = 0;//error
            //}
            if (!(string.IsNullOrEmpty(ImageName)))
            {


                try
                {
                    string fullPath = Request.MapPath("~/UploadedImages/" + ImageName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);


                    }
                    resultout = 1;
                }
                catch (Exception)
                {

                    resultout = 0;
                }


            }
            else
            {
                resultout = 1;
            }
            return Json(resultout, JsonRequestBehavior.AllowGet);
            // If we got this far, something failed, redisplay form

        }


    }
}