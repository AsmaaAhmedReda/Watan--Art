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
    public class GalleriesController : BaseController
    {
        AlbumsBLL _AlbumsBLL = new AlbumsBLL();
        AlbumGalleryBLL _AlbumGalleryBLL = new AlbumGalleryBLL();
        Generic _Generic = new Generic();
        ManageMedia _ManageMedia = new ManageMedia();
        // GET: Admin/Galleries
        public ActionResult Index()
        {
            return View();
        }
        #region Videos
        public ActionResult Videos()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_AlbumGalleryBLL.SelectALLPagging(PageObj, MediaTypeEnum.Video));
        }

        public PartialViewResult ViewVideos(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            List<AlbumGalleryVM> lst_res = new List<AlbumGalleryVM>();


            General.ResolveRowscount<AlbumGalleryVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _AlbumGalleryBLL, new object[] { MediaTypeEnum.Video });

            lst_res.AddRange(_AlbumGalleryBLL.SelectALLPagging(PageObj, MediaTypeEnum.Video));

            return PartialView("ViewVideos", lst_res);

        }


        public ActionResult EditVideo(Int64 id = 0)
        {
            ViewBag.Albums = new SelectList(_AlbumsBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "AlbumID", "AlbumNameAR");
            return View(_AlbumGalleryBLL.SelectByID(id, MediaTypeEnum.Video));
        }
        [HttpPost]
        public ActionResult EditVideo(AlbumGalleryVM _VideoObj)
        {
            _VideoObj.MediaType = (int)MediaTypeEnum.Video;
            ViewBag.Albums = new SelectList(_AlbumsBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "AlbumID", "AlbumNameAR");
            try
            {
                if (!_VideoObj.IsYoutube)
                {
                    var ImageFile = Request.Files[0];


                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {

                        _VideoObj.AttachedFile = General.SaveFile(Request.Files);

                        try
                        {
                            _ManageMedia.GetThumbnail(Server.MapPath("~/UploadedImages/" + _VideoObj.AttachedFile), Path.Combine(Server.MapPath("~/UploadedImages/"), _VideoObj.AttachedFile.Replace(".", "") + ".jpg"), "80x80");
                            _VideoObj.ThumbFile = _VideoObj.AttachedFile.Replace(".", "") + ".jpg";
                        }
                        catch (Exception ex)
                        {
                            _VideoObj.ThumbFile = _VideoObj.AttachedFile.Replace(".", "") + ".jpg";
                        }
                    }


                }
                else
                {
                    _VideoObj.AttachedFile = _VideoObj.YoutubeURL;
                    _VideoObj.ThumbFile = _ManageMedia.getYouTubeThumbnail(_VideoObj.AttachedFile);
                }

                bool _returnVal = _AlbumGalleryBLL.Update(_VideoObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Videos");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_VideoObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_VideoObj);
            }
        }

        public JsonResult DeleteVideo(Int64 MediaID)
        {
            try
            {
                AlbumGalleryVM _Gallery = _AlbumGalleryBLL.SelectByID(MediaID, MediaTypeEnum.Video);

                bool retValue = _AlbumGalleryBLL.Delete(new AlbumGalleryVM() { MediaID = MediaID });
                if (retValue)
                {
                    General.DeleteFile(_Gallery.AttachedFile);
                    General.DeleteFile(_Gallery.ThumbFile);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("Error", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Photos
        public ActionResult Photos()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_AlbumGalleryBLL.SelectALLPagging(PageObj, MediaTypeEnum.Image));
        }
        public PartialViewResult ViewPhotos(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;

            List<AlbumGalleryVM> lst_res = new List<AlbumGalleryVM>();


            General.ResolveRowscount<AlbumGalleryVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _AlbumGalleryBLL, new object[] { MediaTypeEnum.Image });


            lst_res.AddRange(_AlbumGalleryBLL.SelectALLPagging(PageObj, MediaTypeEnum.Image));

            return PartialView("ViewPhotos", lst_res);

        }

        public ActionResult EditPhoto(Int64 id = 0)
        {
            ViewBag.Albums = new SelectList(_AlbumsBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "AlbumID", "AlbumNameAR");
            return View(_AlbumGalleryBLL.SelectByID(id, MediaTypeEnum.Image));
        }
        [HttpPost]
        public ActionResult EditPhoto(AlbumGalleryVM _PhotoObj)
        {
            _PhotoObj.MediaType = (int)MediaTypeEnum.Image;
            _PhotoObj.IsYoutube = false;
            ViewBag.Albums = new SelectList(_AlbumsBLL.SelectALL(Helpers.CultureHelper.CurrentCulture), "AlbumID", "AlbumNameAR");
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    _PhotoObj.AttachedFile = General.SaveFile(Request.Files);

                _PhotoObj.ThumbFile = General.SaveThumb(_PhotoObj.AttachedFile);
                bool _returnVal = _AlbumGalleryBLL.Update(_PhotoObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Photos");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_PhotoObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_PhotoObj);
            }
        }

        public JsonResult DeletePhoto(Int64 MediaID)
        {
            try
            {
                AlbumGalleryVM _Gallery = _AlbumGalleryBLL.SelectByID(MediaID, MediaTypeEnum.Image);

                bool retValue = _AlbumGalleryBLL.Delete(_Gallery);
                if (retValue)
                {
                    General.DeleteFile(_Gallery.AttachedFile);
                    General.DeleteFile(_Gallery.ThumbFile);
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json("Error", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Albums
        public ActionResult Albums()
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = 1 };
            ViewBag.PageObj = PageObj;
            return View(_AlbumsBLL.SelectALLPagging(PageObj));
        }
        public PartialViewResult ViewAlbums(int CurrentPage = 1, int currentRows = 0)
        {
            PaggingVM PageObj = new PaggingVM() { CurrentPage = CurrentPage };
            ViewBag.PageObj = PageObj;
            List<AlbumVM> lst_res = new List<AlbumVM>();


            General.ResolveRowscount<AlbumVM>(PageObj, currentRows, lst_res, "SelectALLPagging", _AlbumsBLL, new object[] { });


            lst_res.AddRange(_AlbumsBLL.SelectALLPagging(PageObj));

            return PartialView("ViewAlbums", lst_res);

        }

        public ActionResult EditAlbum(int id = 0)
        {
            return View(_AlbumsBLL.SelectByID(id));
        }
        [HttpPost]
        public ActionResult EditAlbum(AlbumVM _AlbumObj)
        {
            AlbumVM _AlbumOldObj = new AlbumVM();
            _AlbumOldObj = _AlbumsBLL.SelectByID(_AlbumObj.AlbumID);
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    _AlbumObj.AlbumImage = General.SaveFile(Request.Files);

                bool _returnVal = _AlbumsBLL.Update(_AlbumObj);
                if (_returnVal == true)
                {
                    ViewBag.ErrorMsg = true;
                    return RedirectToAction("Albums");
                }
                else
                {
                    ViewBag.ErrorMsg = false;
                    return View(_AlbumObj);
                }
            }
            catch
            {
                ViewBag.ErrorMsg = false;
                return View(_AlbumObj);
            }
        }

        public JsonResult ActiveDectiveAlbum(int AlbumID, Boolean IsActive)
        {
            try
            {
                bool retValue = _AlbumsBLL.UpdateIsActive(AlbumID, IsActive);
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
        public JsonResult DeleteAlbum(int AlbumID)
        {
            try
            {
                bool retValue = _AlbumsBLL.CheckIfUsed(new AlbumVM() { AlbumID = AlbumID });
                if (!retValue)
                {
                    retValue = _AlbumsBLL.Delete(new AlbumVM() { AlbumID = AlbumID });
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