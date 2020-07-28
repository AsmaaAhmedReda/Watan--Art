using Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using WatanART.BLL.ViewModels;
using WatanART.Models;

namespace WatanART.Controllers.WebApi
{
    public class FileUploaderController : ApiController
    {
        Generic _Generic = new Generic();

        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            bool obj = true;
            int State = 0;
            var rasultdata = 1;
            string outname = null;
            var UploadPath = HttpContext.Current.Server.MapPath("~/UploadedImages");
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var FileDataContent = HttpContext.Current.Request.Files[file];
                    if (FileDataContent != null && FileDataContent.ContentLength > 0)
                    {
                        // take the input stream, and save it to a temp folder using the original file.part name posted
                        var stream = FileDataContent.InputStream;
                        var fileName = Path.GetFileName(FileDataContent.FileName);
                        Directory.CreateDirectory(UploadPath);
                        string path = Path.Combine(UploadPath, fileName);
                        try
                        {
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);
                            using (var fileStream = System.IO.File.Create(path))
                            {
                                stream.CopyTo(fileStream);
                            }
                            // Once the file part is saved, see if we have enough to merge it
                            WatanART.Models.Utils UT = new WatanART.Models.Utils();
                            returntype outobj = UT.MergeFile(path);
                            outname = outobj.fileName;
                            State = outobj.state;
                        }
                        catch (IOException ex)
                        {
                            obj = false;
                            rasultdata = 0;
                            outname = FileDataContent.FileName;
                            State = 2;
                        }
                    }
                }


            }
            else
            {
                obj = false; rasultdata = 0;
                State = 1;
            }
            string RetData = JsonConvert.SerializeObject(obj, Formatting.Indented);
            string RetState = JsonConvert.SerializeObject(State, Formatting.Indented);
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent("{\"result\":" + RetData + ",\"State\":" + RetState + ",\"ISResultHasData\":" + rasultdata + ",\"fileName\":\"" + outname + "\"}", Encoding.UTF8, "application/json")
            };
            //resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return resp;


        }
        [HttpPost]
        [Route("api/FileUploader/SaveFileLst")]
        [ActionName("SaveFileLst")]
        public HttpResponseMessage SaveFileLst(UploadImageVM fileContentObject)
        {
          
            List<string> result = new List<string>();
            try
            {
                var NameandEXtion = fileContentObject.filePath.Zip(fileContentObject.imageExten, (n, w) => new { name = n, extion = w });
                foreach (var item in NameandEXtion)
                {
                    result.Add(_Generic.SaveAttach(item.name, item.extion, "UploadedImages"));
                }
                // var result = E3timad.Helper.Helper.ConvertBaseToFileURL(fileContentObject.FileBase64, fileContentObject.FileExtention);
                return _Generic.ConvertObjectToJSON(result);
            }
            catch
            {
                return _Generic.ConvertObjectToJSON(result);
            }
            
        }

    }
}