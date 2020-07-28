using WatanART.BLL.BussinessLayer;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatanART.Models
{
    //public class SchedulerTasks
    //{
    //    public static void Start()
    //    {

    //        return;
    //        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
    //        scheduler.Start();
    //        IJobDetail job = JobBuilder.Create<NotificationsJob>().Build();
    //        ITrigger trigger = TriggerBuilder.Create()
    //             .WithIdentity("trigger1", "group1")
    //            .StartNow()
    //            .WithSimpleSchedule(x => x
    //             .WithIntervalInSeconds(50)
    //            .RepeatForever())
    //            .Build();
    //        scheduler.ScheduleJob(job, trigger);


    //    }
    //}

    public class JobScheduler
    {
        public static void Start()
        {

           
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<NotificationsJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                 .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                 .WithIntervalInSeconds(50)
                .RepeatForever())
                .Build();
            scheduler.ScheduleJob(job, trigger);


        }
    }

    public class NotificationsJob : IJob
    {
        NotificationsBLL _NotificationBLL = new NotificationsBLL();
        public void Execute(IJobExecutionContext context)
        {
            var lstAllNotifications = _NotificationBLL.ViewAllNotificationsNotSent().ToList();
            if (lstAllNotifications.Count > 0)
            {
                foreach (var item in lstAllNotifications)
                {
                    _NotificationBLL.Send(item.NotificationMessage, item.DeviceToken, (item.TypeID!=null? item.TypeID.Value:1), item.Devicetype);
                    _NotificationBLL.updateNotificationsStatus(item.ID);
                }
            }
        }
    }
}
