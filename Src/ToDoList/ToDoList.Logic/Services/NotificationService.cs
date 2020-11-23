using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using ToDoList.Database.Repositories;
using ToDoList.Logic.Interfaces;

namespace ToDoList.Logic.Services
{
    public class NotificationService: INotificationService
    {
        private EFUnitOfWork _database;

        public NotificationService()
        {
            _database = new EFUnitOfWork();
        }
        public void ShowNotification(string description, string title = "Notification")
        {
            string xml = $@"<toast>
                      <visual>
                        <binding template='ToastGeneric'>
                          <text>{title}</text>
                          <text>Description: {description} </text>
                        </binding>
                      </visual>
                    </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        public void RunNotificationKernel()
        {
            Task task = Task.Run(Check);
        }

        private void Check()
        {
            while (true)
            {
                var events = _database.Events.GetAll().Where(t => t.RemindTime == DateTime.Now.TimeOfDay).ToList();
                var tasks = _database.Tasks.GetAll().Where(t => t.Deadline == DateTime.Now.TimeOfDay).ToList();
                foreach (var e in events)
                {
                   ShowNotification($"Deadline of {e.Name} horyt");
                }
                foreach (var t in tasks)
                {
                    ShowNotification($"Deadline of {t.Name} horyt");
                }

            }
            

        }
    }
}
