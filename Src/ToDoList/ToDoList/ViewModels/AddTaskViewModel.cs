using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class AddTaskViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService taskService;

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action TaskAdded;

        private readonly INotificationService _notificationService;

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public AddTaskViewModel(Window window)
        {
            this.AddCommand = new RelayCommand(this.Add);
            this.CancelCommand = new RelayCommand(this.Cancel);
            this.taskService = new TaskService();
            this.window = window;
            _notificationService = new NotificationService();
        }

        private Window window;

        private string name;

        public string Name
        {
            get => this.name;
            set
            {
                if (!string.Equals(this.name, value))
                {
                    this.name = value;
                }
            }
        }

        private DateTime deadline;

        public DateTime Deadline
        {
            get => this.deadline;
            set
            {
                this.deadline = value;
            }
        }

        private void Add()
        {
            this.taskService.CreateTaskAsync(new TaskDto()
            {
                Name = this.Name,
                Deadline = this.Deadline.TimeOfDay,
                UserId = (int)AppConfig.UserId,
            });
            TaskAdded.Invoke();
            this._notificationService.ShowNotification($"Task {Name} is successfully added!", NotificationType.Information, "Information");
            this.window.Close();
        }

        private void Cancel()
        {
            this.window.Close();
        }
    }
}
