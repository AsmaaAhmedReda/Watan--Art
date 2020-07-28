using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatanART.BLL;
using WatanART.BLL.ViewModels;
using System.Reflection;
using WatanART.BLL.BussinessLayer;
using System.IO;

namespace TechMart.Web.Models
{
    public delegate List<T> MyMethodDelegate<T, T2, T3, T4>(T2 arg1, T3 arg2, T4 arg3);
    public class General
    {
        //public static int? RowsCount = null;



        public static Dictionary<string, string> Roles
        {
            get
            {
                return new Dictionary<string, string>() { { "Admins", "Admin" }, { "Approver", "Approver" }, { "Moderator", "Moderator" }, { "AppUesr", "Application User" } };
            }
        }







        public static void ResolveRowscount<T>(PaggingVM PageObj, int currentRows, List<T> lst_res, string MethodName, object model, object[] Args)
        {
            lst_res = lst_res == null ? new List<T>() : lst_res;


            if (PageObj.CurrentPage > 1)
            {
                int? OldRows = PageObj.RowsPerPage * (PageObj.CurrentPage - 1);
                PaggingVM diffObject = new PaggingVM() { CurrentPage = (PageObj.CurrentPage - 1) };

                if (OldRows > currentRows)
                {
                    var ArgsList = Args.ToList();
                    if (!ArgsList.Exists(x => x is PaggingVM))
                    {
                        ArgsList.Insert(0, diffObject);
                    }

                    var diffrence = OldRows - currentRows;
                    Type t = model.GetType();
                    MethodInfo method = t.GetMethod(MethodName);
                    List<T> lst_diff = (List<T>)method.Invoke(model, ArgsList.ToArray());
                    lst_diff.Reverse();
                    lst_diff = lst_diff.Take((int)diffrence).ToList();
                    lst_diff.Reverse();
                    ;
                    lst_res.AddRange(lst_diff);

                }
            }
        }
        public static void DeleteFile(string path)
        {
            try
            {
                if (path != null && path != "No_image_available.jpg")
                    System.IO.File.Delete(Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedImages/"), path));
            }
            catch { }
        }

        public static string SaveFile(HttpFileCollectionBase File)
        {
            var _fileName = "";
            var SliderImageFile = File[0];
            if (SliderImageFile.ContentLength > 0)
            {

                if (SliderImageFile != null && SliderImageFile.ContentLength > 0)
                {
                    _fileName = Guid.NewGuid() + string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + Path.GetExtension(SliderImageFile.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedImages/"), _fileName);
                    SliderImageFile.SaveAs(path);
                }
            }
            return _fileName;
        }


        public static string SaveThumb(string _fileName)
        {
            if (!string.IsNullOrEmpty(_fileName))
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/UploadedImages/") + _fileName);
                if (img != null)
                {
                    System.Drawing.Image thumb = img.GetThumbnailImage(120, 120, null, IntPtr.Zero);
                    img.Dispose();
                    thumb.Save(Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedImages/" + "Thumb" + _fileName)));
                }
                return "Thumb" + _fileName;

            }
            else return "";
        }
    }
}





