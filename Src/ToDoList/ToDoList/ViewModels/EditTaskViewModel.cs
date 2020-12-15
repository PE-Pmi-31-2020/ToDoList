using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.Annotations;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;

namespace ToDoList.ViewModels
{
    public class EditTaskViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService taskService;


        public event EventHandler<Task> TaskUpdated;

        private readonly INotificationService _notificationService;

        private readonly LoggerService loggerService;


        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public EditTaskViewModel(Window window, Task task)
        {
            this.SaveCommand = new RelayCommand(this.Save);
            this.CancelCommand = new RelayCommand(this.Cancel);
            this.taskService = new TaskService();
            this.window = window;
            _notificationService = new NotificationService();
            loggerService = new LoggerService();

            Task = task;
            Name = Task.Name;
            Deadline = Convert.ToDateTime(Task.Deadline.ToString());
        }

        private Window window;

        private Task Task { get; set; }

        private string name;

        public string Name
        {
            get => this.name;
            set
            {
                if (!string.Equals(this.name, value))
                {
                    this.name = value;
                    Task.Name = value;
                    this.OnPropertyChanged(nameof(Name));
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
                Task.Deadline = value.TimeOfDay;
                this.OnPropertyChanged(nameof(Deadline));
            }
        }

        private void Save()
        {
            this.taskService.EditTaskAsync(new TaskDto() {Deadline = Task.Deadline, Id = Task.Id, Name = Task.Name});
            TaskUpdated.Invoke(this, Task);
            this._notificationService.ShowNotification($"Task {Name} is successfully updated!", NotificationType.Information, "Information");
            loggerService.LogInfo($"A new task {Name} was updated");
            this.window.Close();
        }

        private void Cancel()
        {
            this.window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
