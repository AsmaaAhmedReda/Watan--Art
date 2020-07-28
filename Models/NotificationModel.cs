using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatanART.Models
{
    public class NotificationData
    {
        public string message { get; set; }
        public string Messageen { get; set; }
        public string title { get; set; }
        public string titleen { get; set; }
        public int type { get; set; }
        public string img { get; set; }
        public long DesID { get; set; }
        public DateTime date { get; set; }
    }

    public class Notification
    {
        public string text { get; set; }
        public string texten { get; set; }
        public string title { get; set; }
        public string titleen { get; set; }
        public int type { get; set; }
        public string img { get; set; }
        public long DesID { get; set; }
        public DateTime date { get; set; }
    }

    public class NotifcationMessagae
    {
        public string to { get; set; }
        public NotificationData data { get; set; }
        public Notification notification { get; set; }
    }

    public class NotifcationMessagaeandroid
    {
        public string to { get; set; }
        public NotificationData data { get; set; }

    }
}