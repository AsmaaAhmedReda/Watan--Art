using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatanART.BLL.BussinessLayer;
using WatanART.BLL.ViewModels;
using WatanART.Controllers;

namespace WatanART.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        ProductBLL _ProductBLL = new ProductBLL();
        public ActionResult Index()
        {
           
            var RESULT = _ProductBLL.SelectAllProducts(LanguageID);
          
            return View(RESULT);
        }
        public ActionResult Products(int Cat_ID)
        {

            var RESULT = _ProductBLL.SelectProductByCatId(Cat_ID,LanguageID);

            return View(RESULT);
        }
        public ActionResult New()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            //PropertyBLL _PropertyBLL = new PropertyBLL();
            var obj = new ProductVM();
            ViewBag.cat = _CategoryBLL.SelectCategoriesbyParent(LanguageID, 0);
           
            ViewBag.Color = _CategoryBLL.SelectGeneralData("Color");
            ViewBag.Style = _CategoryBLL.SelectGeneralData("Style");
            ViewBag.Size = _CategoryBLL.SelectGeneralData("Size");
            //  ViewBag.Property = _PropertyBLL.SelectAllProperties(LanguageID);
            return View(obj);
        }
        public ActionResult New2()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
            //PropertyBLL _PropertyBLL = new PropertyBLL();
            var obj = new ProductVM();
            ViewBag.cat = _CategoryBLL.SelectCategoriesbyParent(LanguageID, 0);
            //  ViewBag.Property = _PropertyBLL.SelectAllProperties(LanguageID);
            return View(obj);
        }
        public ActionResult Update(int ID)
        {

            CategoryBLL _CategoryBLL = new CategoryBLL();
            PropertyBLL _PropertyBLL = new PropertyBLL();
          
            var obj = _ProductBLL.SelectProductById(ID, LanguageID);
            ViewBag.cat = _CategoryBLL.SelectCategoriesbyParent(LanguageID, 0);
            ViewBag.subcat = _CategoryBLL.SelectCategoriesbyParent(LanguageID,int.Parse(obj.parent.ToString()));
            // ViewBag.Property = _PropertyBLL.SelectAllProperties(LanguageID);

            ViewBag.Color = _ProductBLL.ColorForProductALL(ID);
            ViewBag.Style = _ProductBLL.StyleForProductALL(ID);
            ViewBag.Size = _ProductBLL.SizeForProductALL(ID);
            //ViewBag.Product_Property = _ProductBLL.SelectProduct_Property(LanguageID,ID);
            return View(obj);
        }
        public JsonResult SubList(int Parent)
        {

            CategoryBLL _CategoryBLL = new CategoryBLL();
            var RESULT = _CategoryBLL.SelectCategoriesbyParent( LanguageID,Parent);
      
            

            return Json(RESULT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insertajax(ProductVM obj)
        {
            long result = 0;
            result = _ProductBLL.insert(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Updateajax(ProductVM obj)
        {
            long result = 0;
            result = _ProductBLL.Update(obj);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            try
            {

                int retValue = _ProductBLL.Delete(ID);
                if (retValue > 0)
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

       
        public ActionResult GetPartialview(int Cat_ID)
        {
            PropertyBLL _PropertyBLL = new PropertyBLL();
            List<PropertiesVM> list = new List<PropertiesVM>();
             list = _PropertyBLL.SelectProperties(LanguageID, Cat_ID);
            ViewBag.Property = list;
        
            return PartialView("_demoPartial");
        }


        public ActionResult Sample()
        {
            CategoryBLL _CategoryBLL = new CategoryBLL();
           
            var obj = new SampleVM();
            ViewBag.cat = _CategoryBLL.SelectCategoriessub(LanguageID);
            
            return View(obj);
        }
        public ActionResult SaveAttachment(HttpPostedFileBase Attachment)
        {
            Guid newId = Guid.NewGuid();


            var Name = Attachment.FileName;
            string NameWithoutExtension = Path.GetFileNameWithoutExtension(Name);
            string ext = Path.GetExtension(Name);
            var path = NameWithoutExtension + newId + ext;
            if (!Directory.Exists(Server.MapPath("~/UploadedImages/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/UploadedImages"));
            }
            var filepath = Path.Combine(Server.MapPath("~/UploadedImages/"), path);
            Attachment.SaveAs(filepath);
            string[] Myfile = { Name, path };

            return Json(Myfile, JsonRequestBehavior.AllowGet);
        }

    }
}