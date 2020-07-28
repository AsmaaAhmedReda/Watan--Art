using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatanART.Areas.Admin.Controllers
{
    public class ManagerWordController : BaseController
    {
        ManagerWordBLL _ManagerWordBLL = new ManagerWordBLL();
        Generic _Generic = new Generic();
        // GET: Admin/ManagerWord

        public ActionResult Index()
        {
            return View(_ManagerWordBLL.SelectByID(3));
        }

        [HttpPost]
        public ActionResult Index(ManagerWordVM obj)
        {
            try
            {
                ManagerWordVM ManagerWordObj = new ManagerWordVM();
                ManagerWordObj = _ManagerWordBLL.SelectByID(3);

                var ManagerPhotoFile = Request.Files[0];
                if (ManagerPhotoFile.ContentLength > 0)
                {
                    var _fileName = "";
                    if (ManagerPhotoFile != null && ManagerPhotoFile.ContentLength > 0)
                    {
                        _fileName = Guid.NewGuid() + string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + Path.GetExtension(ManagerPhotoFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/UploadedImages/"), _fileName);
                        ManagerPhotoFile.SaveAs(path);
                        obj.ManagerPhoto = _fileName;
                    }
                    try
                    {
                        if (ManagerWordObj.ManagerPhoto.Replace(_Generic.GetDomainName("UploadedImages"), "") != "No_image_available.jpg")
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedImages/"), ManagerWordObj.ManagerPhoto.Replace(_Generic.GetDomainName("UploadedImages"), "")));
                    }
                    catch { }
                }
                else
                {
                    obj.ManagerPhoto = String.Empty;
                    obj.ManagerPhoto = ManagerWordObj.ManagerPhoto.Replace(_Generic.GetDomainName("UploadedImages"), "");
                }
                if (_ManagerWordBLL.Update(obj))
                    ViewBag.ErrorMsg = true;
                else
                    ViewBag.ErrorMsg = false;

                obj.ManagerPhoto = _Generic.GetFilePath("UploadedImages", obj.ManagerPhoto);
                return View(obj);
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(obj);
            }
        }
    }
}