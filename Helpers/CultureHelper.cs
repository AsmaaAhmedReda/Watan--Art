using WatanART.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace WatanART.Helpers
{
    public class CultureHelper
    {
        protected HttpSessionState session;

        //constructor   
        public CultureHelper(HttpSessionState httpSessionState)
        {
            session = httpSessionState;
        }
        // Properties  
        public static int CurrentCulture
        {
            get
            {
                if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                {
                    return 0;
                }
                else if (Thread.CurrentThread.CurrentUICulture.Name == "ar-AE")
                {
                    return 1;
                }
                else
                {
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies["CurrentUICulture"];
                    if (myCookie != null)
                    {
                        return Convert.ToInt32(myCookie.Value);
                    }
                    else
                        return 1;
                }
            }
            set
            {

                if (value == 0)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                }
                else if (value == 1)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-AE");
                }
                else
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                }

                var userCookie = new HttpCookie("CurrentUICulture", value.ToString());
                userCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(userCookie);
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }
        public static string LoggedUserID { get; set; }

        //public static List<SettingVM> Settings { get; set; }


        //public static List<ModulesCatagoryVM> DynamicModules { get; set; }


    }
}