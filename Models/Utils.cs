using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace WatanART.Models
{
    class Utils
    {
        public string FileName { get; set; }
        public string TempFolder { get; set; }
        public int MaxFileSizeMB { get; set; }
        public static int uniqueID = 0;
        public List<String> FileParts { get; set; }


        public static string GetUniqueID()
        {
            object obj = new object();
            lock (obj)
            {
                uniqueID++;
            }
            return DateTime.UtcNow.ToString("yyyyMMdd") + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + "" + DateTime.Now.Millisecond + (new Random(100).Next(1, 100)) + "" ;
        }
        public Utils()
        {
            FileParts = new List<string>();
        }



        

        /// <summary>
        /// original name + ".part_N.X" (N = file part number, X = total files)
        /// Objective = enumerate files in folder, look for all matching parts of split file. If found, merge and return true.
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public returntype MergeFile(string FileName)
        {
           
            returntype objout = new returntype();
            objout.Result = false;
            // parse out the different tokens from the filename according to the convention
            string partToken = ".part_";
            string baseFileName = FileName.Substring(0, FileName.IndexOf(partToken));
            string trailingTokens = FileName.Substring(FileName.IndexOf(partToken) + partToken.Length);
            int FileIndex = 0;
            int FileCount = 0;
            int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
            int.TryParse(trailingTokens.Substring(trailingTokens.IndexOf(".") + 1), out FileCount);
            // get a list of all file parts in the temp folder
            string Searchpattern = Path.GetFileName(baseFileName) + partToken + "*";
            string[] FilesList = Directory.GetFiles(Path.GetDirectoryName(FileName), Searchpattern);
            List<string> fileswithoutfullpath = new List<string>();

            foreach (var item in FilesList)
            {
                fileswithoutfullpath.Add(Path.GetFileName(item));
            }
           
            //  merge .. improvement would be to confirm individual parts are there / correctly in sequence, a security check would also be important
            // only proceed if we have received all the file chunks
            if (FilesList.Count() == FileCount) 
            {
                // use a singleton to stop overlapping processes
                if (!MergeFileManager.Instance.InUse(baseFileName))
                {
                    MergeFileManager.Instance.AddFile(baseFileName);
                    if (File.Exists(baseFileName))
                        File.Delete(baseFileName);
                    // add each file located to a list so we can get them into 
                    // the correct order for rebuilding the file
                    List<SortedFile> MergeList = new List<SortedFile>();
                    foreach (string File in FilesList)
                    {
                        SortedFile sFile = new SortedFile();
                        sFile.FileName = File;
                        ////////sFile.FileName = GetUniqueID();
                        baseFileName = File.Substring(0, File.IndexOf(partToken));
                        //baseFileName = new Guid().ToString();
                        trailingTokens = File.Substring(File.IndexOf(partToken) + partToken.Length);
                        int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out FileIndex);
                        sFile.FileOrder = FileIndex;
                        MergeList.Add(sFile);
                    }
                    // sort by the file-part number to ensure we merge back in the correct order
                    var MergeOrder = MergeList.OrderBy(s => s.FileOrder).ToList();


                    string strExtension = Path.GetExtension(baseFileName).ToLower();
                   var filenewname=GetUniqueID() + strExtension;
                    var newfilename= HttpContext.Current.Server.MapPath("~\\UploadedImages") + "\\" + filenewname;
                    //objout.fileName = GetUniqueID() + strExtension;
                    using (FileStream FS = new FileStream(newfilename, FileMode.Create))
                    {
                        // merge each file chunk back into one contiguous file stream
                        foreach (var chunk in MergeOrder)
                        {
                            try
                            {
                                using (FileStream fileChunk = new FileStream(chunk.FileName, FileMode.Open))
                                {
                                    fileChunk.CopyTo(FS);
                                }
                            }
                            catch (IOException ex)
                            {
                                objout.Result = false;
                                objout.state = 4;
                                objout.fileName = ex.Message;
                            }
                        }
                    }
                    objout.Result = true;
                    objout.fileName = filenewname;
                    objout.state = 5;


                    foreach (var item in FilesList)
                    {
                        if (System.IO.File.Exists(item))
                            System.IO.File.Delete(item);
                    }
                    //rslt = true;
                    // unlock the file from singleton

                    var image = baseFileName;
                    MergeFileManager.Instance.RemoveFile(baseFileName);
                }

            }
            else
            {
                objout.Result = true;
                objout.state = 3;
                objout.fileName = string.Join(",", fileswithoutfullpath);
            }
            return objout;
        }


    }

    public struct SortedFile
    {
        public int FileOrder { get; set; }
        public String FileName { get; set; }
    }

    public class MergeFileManager
    {
        private static MergeFileManager instance;
        private List<string> MergeFileList;

        private MergeFileManager()
        {
            try
            {
                MergeFileList = new List<string>();
            }
            catch { }
        }

        public static MergeFileManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MergeFileManager();
                return instance;
            }
        }

        public void AddFile(string BaseFileName)
        {
            MergeFileList.Add(BaseFileName);
        }

        public bool InUse(string BaseFileName)
        {
            return MergeFileList.Contains(BaseFileName);
        }

        public bool RemoveFile(string BaseFileName)
        {
            return MergeFileList.Remove(BaseFileName);
        }

      
    }

    public class returntype
    {
        public string  fileName { get; set; }
        public bool Result { get; set; }

        public int state { get; set; }
    }

  
   

}



