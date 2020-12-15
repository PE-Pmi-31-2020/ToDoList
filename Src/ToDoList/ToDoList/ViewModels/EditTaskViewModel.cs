using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.Annotations;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;

namespace ToDoList.ViewModels
{
    public class EditTaskViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService _taskService;


        public event EventHandler<Task> TaskUpdated;

        private readonly INotificationService _notificationService;

        private readonly LoggerService _loggerService;


        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public EditTaskViewModel(Window window, Task task)
        {
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            _taskService = new TaskService();
            this._window = window;
            _notificationService = new NotificationService();
            _loggerService = new LoggerService();

            Task = task;
            Name = Task.Name;
            Deadline = Convert.ToDateTime(Task.Deadline.ToString());
        }

        private Window _window;

        private Task Task { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (!string.Equals(_name, value))
                {
                    _name = value;
                    Task.Name = value;
                    OnPropertyChanged(nameof(Name));
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
                Task.Deadline = value.TimeOfDay;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        private void Save()
        {
            _taskService.EditTaskAsync(new TaskDto {Deadline = Task.Deadline, Id = Task.Id, Name = Task.Name});
            TaskUpdated?.Invoke(this, Task);
            _notificationService.ShowNotification($"Task {Name} is successfully updated!", NotificationType.Information, "Information");
            _loggerService.LogInfo($"A new task {Name} was updated");
            _window.Close();
        }

        private void Cancel()
        {
            _window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
