using Notifications.Wpf;

namespace ToDoList.BLL.Interfaces
{
    public interface INotificationService
    {
        void ShowNotification(string description, NotificationType type, string title = "Notification");
        void RunNotificationKernel();
    }
}
