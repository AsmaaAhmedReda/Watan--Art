using Newtonsoft.Json;
using WatanART.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WatanART.Helpers
{
    public class sendnotfction
    {


        public void sendNotify(string deviceToken, string titlear, string titleen, string NotificationMessageen, string NotificationMessage, string img, int type, long id)
        {
            try
            {


                //deviceToken = "eEwTjSRMWf0:APA91bHJH9mmdZa1Iktenw2sCu8gUlmZ9cAEMFV5SCrLuen4-qP5mwjFZBYEFF0J6hIm_nfHNZ6G6FNof9i5Llyjd_a9e5UzW1xbQMDbrrRwwAGq_18k3Kp78uIwzaWFozJppAUrQOnJ";
                string AppID = "AAAAslmXxeI:APA91bGFUW0yuTiWRPS5hYsGX2122vPcN2TIYMoSzL3Khi2uQ76wdD8WJ0v5C6BqBwKVLoUdHkyIf2Ea5VTYIc8Y9iR6eGKj1cJ35xn28NtTyYjYTYs8k4efLDgn1gRuOcYrMh4uaNVH";//ConfigurationManager.AppSettings["FcmppID"];
                string senderId = "766007297506";//ConfigurationManager.AppSettings["fcmsenderId"];
                //string AppID = "AAAArjQgW24:APA91bFe8MsJ_Aw3H68WV3dmPGlzMpnZTZ99zp0w6ylts4Ladguon2HXaoFklBsnaqNy_7I6Jd_YyqmI1uHhZ6YwrdOmHyOg0lF-pI6-IGAwIY_tsmWzh_MYIFK9t-ucVlbxEE7daGwH";
                //string senderId = "748198845294";
                const string contentType = "application/json;  charset=utf-8";
                ServicePointManager.DefaultConnectionLimit = 1000;
                CookieContainer cookies = new CookieContainer();
                HttpWebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send") as HttpWebRequest;
                WebHeaderCollection headerCollection = webRequest.Headers;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                            | SecurityProtocolType.Tls11
                            | SecurityProtocolType.Tls12
                            | SecurityProtocolType.Ssl3;
                webRequest.Method = "POST";
                webRequest.Headers["Authorization"] = "Key=" + AppID;
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                //string Data = "{\"to\":\"" + deviceToken + "\",\"data\": {\"message\": \"" + NotificationMessage + "\",\"type\": \"" + type + "\",\"id\": \"" + id + "\"}}";

                //  string Data = "{\"to\":\"" + deviceToken + "\",\"data\": {\"message\": \"" + NotificationMessage + "\",\"type\": \"" + type + "\",\"id\": \"" + id + "\"}},\"notification\": {\"text\": \"" + NotificationMessage + "\",\"type\": \"" + type + "\",\"id\": \"" + id + "\"}}";

                // deviceToken = "cgFoxBzJkTA:APA91bE08CRgRQySazavLI18YDiq293S3k66zJMuZR2Z6Vs4ZcP55FrCzZ1AJ9dI-cCgXXkeFFoE-UxR9hClkv02_vUJFqHcOHDD6icOXs9VCcFjNNJJ9qLz3lAQLFxtLrhK2RgzRln8";
                //string Data = "{\"to\":\"" + deviceToken + "\",\"data\": {\"message\": \"" + NotificationMessage + "\",\"Messageen\": \"" + NotificationMessageen + "\",\"title\": \"" + titlear + "\",\"titleen\": \"" + titleen + "\",\"type\": \"" + type + "\",\"img\":\"" + img + "\",\"DesID\": \"" + id + "\"},\"notification\": {\"text\": \"" + NotificationMessage + "\",\"texten\": \"" + NotificationMessageen + "\",\"title\": \"" + titlear + "\",\"titleen\": \"" + titleen + "\",\"type\": \"" + type + "\",\"img\":\"" + img + "\",\"DesID\": \"" + id + "\"}}";

                //string Data = "";
                //string[] list = deviceToken.Split(new string[] { "~<>" }, StringSplitOptions.None);
                //if (list != null && list.Count() > 1)
                //{
                //    deviceToken = list[0];

                //    if (int.Parse(list[1]) == 1)
                //    {
                //      var Notdata = new NotifcationMessagaeandroid()
                //        {
                //            to = deviceToken,
                //            data = new NotificationData()
                //            {
                //                DesID = id,
                //                message = NotificationMessage.Length > 150 ? NotificationMessage.Substring(0, 150) + ".." : NotificationMessage,
                //                Messageen = NotificationMessageen.Length > 150 ? NotificationMessageen.Substring(0, 150) + ".." : NotificationMessageen,
                //                title = titlear,
                //                titleen = titleen,
                //                type = type,
                //                img = img



                //            }
                //        };

                //         Data = JsonConvert.SerializeObject(Notdata);
                //    }
                //    else if (int.Parse(list[1]) == 2)
                //    {
                //        var Notdata = new NotifcationMessagae()
                //        {
                //            to = deviceToken,
                //            notification = new Notification()
                //            {
                //                DesID = id,
                //                text = NotificationMessage.Length > 150 ? NotificationMessage.Substring(0, 150) + ".." : NotificationMessage,
                //                texten = NotificationMessageen.Length > 150 ? NotificationMessageen.Substring(0, 150) + ".." : NotificationMessageen,
                //                title = titlear,
                //                titleen = titleen,
                //                type = type,
                //                img = img



                //            }
                //        };

                //        Data = JsonConvert.SerializeObject(Notdata);

                //    }
                //    else
                //    {
                //        var Notdata = new NotifcationMessagae()
                //        {
                //            to = deviceToken,
                //            data= new NotificationData(){

                //                DesID = id,
                //                message = NotificationMessage.Length > 150 ? NotificationMessage.Substring(0, 150) + ".." : NotificationMessage,
                //                Messageen = NotificationMessageen.Length > 150 ? NotificationMessageen.Substring(0, 150) + ".." : NotificationMessageen,
                //                title = titlear,
                //                titleen = titleen,
                //                type = type,
                //                img = img
                //            },
                //            notification = new Notification()
                //            {
                //                DesID = id,
                //                text = NotificationMessage.Length > 150 ? NotificationMessage.Substring(0, 150) + ".." : NotificationMessage,
                //                texten = NotificationMessageen.Length > 150 ? NotificationMessageen.Substring(0, 150) + ".." : NotificationMessageen,
                //                title = titlear,
                //                titleen = titleen,
                //                type = type,
                //                img = img



                //            }
                //        };

                //        Data = JsonConvert.SerializeObject(Notdata);
                //    }
                //}
                //else
                //{
                deviceToken = deviceToken.Replace(System.Environment.NewLine, string.Empty);
                var Notdata = new NotifcationMessagae()
                {
                    to = deviceToken,
                    data = new NotificationData()
                    {

                        DesID = id,
                        message = NotificationMessage.Length > 150 ? NotificationMessage.Substring(0, 150) + ".." : NotificationMessage,
                        Messageen = NotificationMessageen.Length > 150 ? NotificationMessageen.Substring(0, 150) + ".." : NotificationMessageen,
                        title = titlear,
                        titleen = titleen,
                        type = type,
                        img = img,
                        date=DateTime.Now
                    },
                    notification = new Notification()
                    {
                        DesID = id,
                        text = NotificationMessage.Length > 150 ? NotificationMessage.Substring(0, 150) + ".." : NotificationMessage,
                        texten = NotificationMessageen.Length > 150 ? NotificationMessageen.Substring(0, 150) + ".." : NotificationMessageen,
                        title = titlear,
                        titleen = titleen,
                        type = type,
                        img = img,
                        date = DateTime.Now



                    }
                };

                string Data = JsonConvert.SerializeObject(Notdata);
                //}






                webRequest.ContentType = contentType;
                webRequest.CookieContainer = cookies;
                webRequest.ContentLength = Data.Length;
                webRequest.SendChunked = true;
                webRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                webRequest.Accept = "text/html,application/xhtml+xml,application/json,application/xml;q=0.9,*/*;q=0.8";
                webRequest.Referer = "https://accounts.craigslist.org";
                StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
                requestWriter.Write(Data);
                requestWriter.Flush();
                StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                //string path3 = HttpRuntime.AppDomainAppPath + "/Upload/log.txt";
                //using (StreamWriter sw = File.AppendText(path3))
                //{
                //    sw.WriteLine(deviceToken + "-" + responseData);

                //}
                responseReader.Close();
                webRequest.GetResponse().Close();
            }
            catch (Exception ex)
            {
                string path2 = HttpRuntime.AppDomainAppPath + "/Upload/log.txt";
                using (StreamWriter sw = File.AppendText(path2))
                {

                    sw.WriteLine(ex.Message);
                    sw.WriteLine(ex.InnerException.Message);


                }


            }
        }

    }
}