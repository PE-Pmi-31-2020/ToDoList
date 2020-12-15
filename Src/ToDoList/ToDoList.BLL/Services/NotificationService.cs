using System;
using System.Linq;
using System.Threading;
using Notifications.Wpf;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class NotificationService: INotificationService
    {
        private IUnitOfWork _database;
        private INotificationManager _notificationManager;

        public NotificationService()
        {
            _database = new EfUnitOfWork(); 
            _notificationManager = new NotificationManager();
        }
        public NotificationService(IUnitOfWork database, INotificationManager notification)
        {
            _database = database;
            _notificationManager = notification;
        }
        public void ShowNotification(string description, NotificationType type, string title = "Notification")
        {
            _notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = description,
                Type = type
            });
        }

        public void RunNotificationKernel()
        {
            Thread thread = new Thread(Check);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Check()
        {
            TimeSpan currentTime;
            TimeSpan prevTime = new TimeSpan();
            while (true)
            {
                currentTime = DateTime.Now.TimeOfDay;
                if (currentTime.Minutes != prevTime.Minutes)
                {
                    var events = _database.Events.GetAll()
                        .Where(t => t.RemindTime.Minutes == DateTime.Now.TimeOfDay.Minutes &&
                                        t.RemindTime.Hours == DateTime.Now.TimeOfDay.Hours &&
                                        t.UserId == AppConfig.UserId).ToList();
                    var tasks = _database.Tasks.GetAll()
                        .Where(t => t.Deadline.Minutes == DateTime.Now.TimeOfDay.Minutes &&
                                    t.Deadline.Hours == DateTime.Now.TimeOfDay.Hours && !t.IsDone &&
                                    t.UserId == AppConfig.UserId).ToList();
                    foreach (var e in events)
                    {
                        ShowNotification($"Deadline of {e.Name} horyt", NotificationType.Warning);
                    }

                    foreach (var t in tasks)
                    {
                        ShowNotification($"Deadline of {t.Name} horyt", NotificationType.Warning);
                    }
                }
                prevTime = currentTime;
            }
        }
    }
}
