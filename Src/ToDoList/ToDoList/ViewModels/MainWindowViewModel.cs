using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.Annotations;
using ToDoList.BLL;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INotificationService notificationService;
        private readonly IUserService userService;

        public RelayCommand AddEventCommand { get; private set; }

        public RelayCommand AddTaskCommand { get; private set; }

        private string userFulName;

        public string UserFullName
        {
            get => userFulName;
            set
            {
                if (!value.Equals(this.userFulName))
                {
                    this.userFulName = value;
                    this.OnPropertyChanged(nameof(UserFullName));
                }
            }
        }

        public MainWindowViewModel()
        {
            this.notificationService = new NotificationService();
            this.notificationService.RunNotificationKernel();
            this.userService = new UserService();
            this.AddEventCommand = new RelayCommand(this.ShowAddEvent);
            this.AddTaskCommand = new RelayCommand(this.ShowAddTask);
            this.userFulName = this.userService.GetUserFullNameById(AppConfig.UserId);
        }

        private void ShowAddEvent()
        {
            var addEventWindow = new AddEvent();
            addEventWindow.ShowDialog();
        }

        private void ShowAddTask()
        {
            var addTaskWindow = new AddTask();
            addTaskWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
