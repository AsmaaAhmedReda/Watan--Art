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
    public class ModulesController : BaseController
    {
        Generic _Generic = new Generic();
        ModulesBLL _ModulesBLL = new ModulesBLL();
        ModulesCatagoryBLL _ModulesCatagoryBLL = new ModulesCatagoryBLL();
        LinksBLL _LinksBLL = new LinksBLL();

        #region Modules
        // GET: Admin/Modules_CMS
        public ActionResult Index(long id = -1)
        {
            if (id > 0)
            {

                var catagoryObj = _ModulesCatagoryBLL.SelectByID(id);
                if (catagoryObj != null && catagoryObj.CatagoryID > 0)
                {
                    ViewBag.code = catagoryObj.CatagoryKey;
                    PaggingVM PageObj = new PaggingVM() { CurrentPage = 1,RowsPerPage=int.MaxValue };
                    ViewBag.PageObj = PageObj;
                    ViewBag.CatID = id;
                    if (WatanART.Helpers.CultureHelper.CurrentCulture == 2)
                        ViewBag.Title = catagoryObj.CatagoryNameEn;
                    else
                        ViewBag.Title = catagoryObj.CatagoryNameAr;

                    return View(_ModulesBLL.SelectALLByCat_IsActive(PageObj, id, null));
                }

            }

            return RedirectToAction("Index", "Shared");

        }

        public PartialViewResult ViewModules(long catID, int currentRows, int CurrentPage = 1)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;
            List<ModulesVM> lst_res = new List<ModulesVM>();



            General.ResolveRowscount<ModulesVM>(PageObj, currentRows, lst_res, "SelectALLByCat_IsActive", _ModulesBLL, new object[] { catID, null });




            lst_res.AddRange(_ModulesBLL.SelectALLByCat_IsActive(PageObj, catID, null));


            return PartialView("tbl_Modules", lst_res);

        }

        public ActionResult AddEdit(int Modid = 0, long CatID = 0)
        {
          
            ViewBag.catID = CatID;
            var catagoryObj = _ModulesCatagoryBLL.SelectByID(CatID);
            
            if (catagoryObj != null && catagoryObj.CatagoryID > 0)
            {
                ViewBag.code = catagoryObj.CatagoryKey;
                ModulesVM model = null;



                model = _ModulesBLL.SelectByID(Modid);


                if (model.ModuleCatID == 0)
                {

                    model.ModuleCatID = CatID;
                    model.LoadLinks();

                }

                if (model == null)
                {
                    model.ModuleCatID = CatID;
                    model.ModuleIsActive = false;
                }

                return View(model);
            }

            return RedirectToAction("Index", "Shared");
        }



        public JsonResult ActiveDective(int id, bool IsActive)
        {
            ModulesVM model = null;

            if (id > 0)
            {

                model = _ModulesBLL.SelectByID(id);
                model.ModuleIsActive = IsActive;

            }
            model = model == null ? new ModulesVM() : model;

            var retValue = _ModulesBLL.AddEdit(model) != "0";
            if (retValue)
            {

                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("Error", JsonRequestBehavior.AllowGet);

        }



        [ValidateInput(false)]
        public JsonResult InsertUpdate(ModulesVM _ModulesObj)
        {

            long result = 0;


            if (!String.IsNullOrEmpty(_ModulesObj.ModuleTitleAr) &&
                //!String.IsNullOrEmpty(Object.TitleEN) &&
                !String.IsNullOrEmpty(_ModulesObj.ModuleTitleEn) &&
                //!String.IsNullOrEmpty(Object.DescrptionEN)&&
                !string.IsNullOrEmpty(_ModulesObj.ModuleImage)

                )
            {
                try
                {

                    bool _returnVal = _ModulesBLL.AddEdit(_ModulesObj) != "0";
                    if (_returnVal == true)
                    {
                        //ViewBag.ErrorMsg = true;
                        //return RedirectToAction("Index", new { id = _ModulesObj.ModuleCatID });
                        result = 1;
                    }
                    else
                    {
                        //ViewBag.ErrorMsg = false;
                        //return View(_ModulesObj);
                        result = 0;
                    }
                }
                catch
                {
                    //ViewBag.ErrorMsg = false;
                    //return View(_ModulesObj);
                    result = 0;
                }

            }
            else
            {
                result = -1;
            }



            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult AddEdit(ModulesVM _ModulesObj)
        //{

        //     try
        //    {
        //        if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
        //            _ModulesObj.ModuleImage = General.SaveFile(Request.Files);


        //        bool _returnVal = _ModulesBLL.AddEdit(_ModulesObj) != "0";
        //        if (_returnVal == true)
        //        {
        //             ViewBag.ErrorMsg = true;
        //            return RedirectToAction("Index", new { id = _ModulesObj.ModuleCatID });
        //        }
        //        else
        //        {
        //             ViewBag.ErrorMsg = false;
        //            return View(_ModulesObj);
        //        }
        //    }
        //    catch
        //    {
        //         ViewBag.ErrorMsg = false;
        //        return View(_ModulesObj);
        //    }
        //}
        public JsonResult DeleteModules(Int64 ModulesID)
        {
            try
            {
                ModulesVM _Modules = _ModulesBLL.SelectByID(ModulesID);
                _Modules.ModuleImage = _Modules.ModuleImage.Replace(_Generic.GetDomainName("UploadedImages"), "");

                bool retValue = _ModulesBLL.Delete(new ModulesVM() { ModuleID = ModulesID });
                if (retValue)
                {
                    try
                    {
                        if (_Modules.ModuleImage != "No_image_available.jpg")
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/UploadedImages/"), _Modules.ModuleImage));
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

        #region ModulesCatagories
        public ActionResult ModulesTypes()
        {
            var model = _ModulesCatagoryBLL.SelectALL();


            return View(model);
        }
        public ActionResult AddEditModuleType(long id = 0)
        {
            ModulesCatagoryVM model = null;

            if (id > 0)
            {

                model = _ModulesCatagoryBLL.SelectByID(id);

            }
            model = model == null ? new ModulesCatagoryVM() : model;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEditModuleType(ModulesCatagoryVM _ModulesTypeObj)
        {
            try
            {

                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    _ModulesTypeObj.CatagoryLogo = General.SaveFile(Request.Files);

                string _returnVal = _ModulesCatagoryBLL.AddEdit(_ModulesTypeObj);
                if (_returnVal != "0")
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("ModulesTypes", "Modules");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    _ModulesTypeObj.LinksCount = 0;
                    return View(_ModulesTypeObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                _ModulesTypeObj.LinksCount = 0;
                return View(_ModulesTypeObj);
            }
        }



        public JsonResult DeleteCatagory(Int64 ModulesID)
        {
            try
            {
                ModulesCatagoryVM _Modules = _ModulesCatagoryBLL.SelectByID(ModulesID);

                bool retValue = _ModulesCatagoryBLL.Delete(new ModulesCatagoryVM() { CatagoryID = ModulesID });
                if (retValue)
                {
                    General.DeleteFile(_Modules.CatagoryLogo);
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
        #region Links
        // GET: Admin/Modules_CMS



        public JsonResult DeleteLink(Int64 ID)
        {
            try
            {
                if (ID > 0)
                {

                    var _Modules = _LinksBLL.SelectByID(ID);

                    bool retValue = _LinksBLL.Delete(_Modules);
                    if (retValue)
                    {
                        General.DeleteFile(_Modules.LinkIcon);
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json("Error", JsonRequestBehavior.AllowGet);


                }
                else
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
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