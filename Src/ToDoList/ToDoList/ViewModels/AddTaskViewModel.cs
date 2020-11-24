using System;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
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

        public RelayCommand<Window> SubmitCommand { get; private set; }

        public AddTaskViewModel()
        {
            this.SubmitCommand = new RelayCommand<Window>(this.Submit);
            this.taskService = new TaskService();
        }

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

        private TimeSpan deadline;

        public TimeSpan Deadline
        {
            get => this.deadline;
            set
            {
                if (!string.Equals(this.deadline, value))
                {
                    this.deadline = value;
                }
            }
        }

        private int userId;

        public int UserId
        {
            get => this.userId;
            set
            {
                if (!string.Equals(this.userId, value))
                {
                    this.userId = value;
                }
            }
        }

        private void Submit(Window window)
        {
            this.taskService.CreateTaskAsync(new TaskDto()
            {
                Name = this.Name,
                Deadline = this.Deadline,
                UserId = this.UserId,
            });
            var newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }
    }
}
