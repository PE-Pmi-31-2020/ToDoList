using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Logic.Interfaces
{
    public interface INotificationService
    {
        void ShowNotification(string description);
        void RunNotificationKernel();
    }
}
