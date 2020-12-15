using System;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;

namespace ToDoList.ViewModels
{
    internal class AddTaskViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService _taskService;

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action TaskAdded;

        private readonly INotificationService _notificationService;

        private readonly LoggerService _loggerService;


        public RelayCommand AddCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public AddTaskViewModel(Window window)
        {
            AddCommand = new RelayCommand(Add);
            CancelCommand = new RelayCommand(Cancel);
            _taskService = new TaskService();
            this._window = window;
            _notificationService = new NotificationService();
            _loggerService = new LoggerService();
        }

        private Window _window;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (!string.Equals(_name, value))
                {
                    _name = value;
                }
            }
        }

        private DateTime _deadline;

        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                _deadline = value;
            }
        }

        private void Add()
        {
            _taskService.CreateTaskAsync(new TaskDto
            {
                Name = Name,
                Deadline = Deadline.TimeOfDay,
                UserId = (int)AppConfig.UserId,
            });
            TaskAdded?.Invoke();
            _notificationService.ShowNotification($"Task {Name} is successfully added!", NotificationType.Information, "Information");
            _loggerService.LogInfo($"A new task {Name} was added");
            _window.Close();
        }

        private void Cancel()
        {
            _window.Close();
        }
    }
}
