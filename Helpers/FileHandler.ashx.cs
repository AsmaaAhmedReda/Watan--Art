using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WatanART.Helpers
{

    /// <summary>
    /// Summary description for FileHandler
    /// </summary>
    public class FileHandler : IHttpHandler
    {
        #region ---Data Members---

        List<string> SupportedTypes = new List<string>();

        static int uniqueID = 0;

        string msg_error = string.Empty;

        static string strFileName = string.Empty;

        string strMode = string.Empty;

        string strMIMEType = string.Empty;

        #endregion

        #region --- Public Methods ---

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        if (file != null)
                            UploadFileClick(file);
                        else
                        {
                            context.Response.ContentType = "text/plain";
                            context.Response.Write("-1#-1");
                        }
                    }
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("1#" + strFileName);
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("-1#-2");
                }
            }
            catch
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("-1#-1");
            }
        }

        public void UploadFileClick(HttpPostedFile file)
        {
            try
            {
                strFileName = Path.GetFileName(file.FileName);
                string strExtension = Path.GetExtension(file.FileName).ToLower();
                strFileName = GetUniqueID() + strExtension;

                switch (strExtension)
                {
                    case ".gif":
                        strMIMEType = "image/gif";
                        break;

                    case ".jpg":
                        strMIMEType = "image/jpg";
                        break;


                    case ".jpeg":
                        strMIMEType = "image/jpeg";
                        break;


                    case ".bmp":
                        strMIMEType = "image/bmp";
                        break;

                    case ".png":
                        strMIMEType = "image/png";
                        break;

                    case ".rar":
                        strMIMEType = "image/rar";
                        break;

                    case ".zip":
                        strMIMEType = "image/zip";
                        break;

                    case ".doc":
                        strMIMEType = "file/doc";
                        break;

                    case ".docx":
                        strMIMEType = "file/docx";
                        break;

                    case ".pdf":
                        strMIMEType = "file/pdf";
                        break;

                    case ".mp3":
                        strMIMEType = "file/mp3";
                        break;

                    case ".mp4":
                        strMIMEType = "video/mp4";
                        break;
                    case ".mov":
                        strMIMEType = "video/mov";
                        break;
                    case ".m4a":
                        strMIMEType = "video/m4a";
                        break;
                    case ".ogv":
                        strMIMEType = "video/ogg";
                        break;

                    case ".webm":
                        strMIMEType = "video/webm";
                        break;

                    case ".wmv":
                        strMIMEType = "file/wmv";
                        break;
                    case ".wav":
                        strMIMEType = "video/wav";
                        break;
                    case ".3gp":
                        strMIMEType = "file/3gp";
                        break;

                    case ".wmp":
                        strMIMEType = "file/wmp";
                        break;

                    case ".flv":
                        strMIMEType = "file/flv";
                        break;


                    case ".swf":
                        strMIMEType = "file/swf";
                        break;

                    default:
                        strMIMEType = string.Empty;
                        return;
                }
                byte[] docBytes = new byte[file.InputStream.Length];
                file.InputStream.Read(docBytes, 0, docBytes.Length);
                UploadFileToServer(docBytes, strMIMEType);
            }
            catch (Exception exception) { HttpContext.Current.Response.Write("-1#-3#" + exception.Message); }
        }

        public void UploadFileToServer(byte[] bFile, string strMIMEType)
        {
            try
            {
                HttpFileCollection hfc = HttpContext.Current.Request.Files;
                HttpPostedFile hpf = hfc[0];
                hpf.SaveAs(HttpContext.Current.Server.MapPath("~\\UploadedImages") + "\\" + System.IO.Path.GetFileName(strFileName));
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("-1#-3#" + exception.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static string GetUniqueID()
        {
            object obj = new object();
            lock (obj)
            {
                uniqueID++;
            }
            return DateTime.UtcNow.ToString("yyyyMMdd") + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond + (new Random(100).Next(1, 100)) + "" + uniqueID;
        }

        #endregion
    }
}