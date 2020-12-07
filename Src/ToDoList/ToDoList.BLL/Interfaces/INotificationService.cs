using System;
using System.Collections.Generic;
using System.Text;
using Notifications.Wpf;

namespace ToDoList.Logic.Interfaces
{
    public interface INotificationService
    {
        void ShowNotification(string description, NotificationType type, string title = "Notification");
        void RunNotificationKernel();
    }
}
