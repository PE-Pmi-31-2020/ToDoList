using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Windows;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    class AddTaskViewModel : INotifyPropertyChanged
    {
        private readonly ITaskService _taskService;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SubmitCommand { get; private set; }
        public AddTaskViewModel()
        {
            SubmitCommand = new RelayCommand<Window>(Submit);
            _taskService = new TaskService();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (!string.Equals(this._name, value))
                {
                    this._name = value;
                }
            }
        }
        
        private TimeSpan _deadline;
        public TimeSpan Deadline
        {
            get => _deadline;
            set
            {
                if (!string.Equals(this._deadline, value))
                {
                    this._deadline = value;
                }
            }
        }
        
        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                if (!string.Equals(this._userId, value))
                {
                    this._userId = value;
                }
            }
        }


        private void Submit(Window window)
        {
            _taskService.CreateTaskAsync(new TaskDto()
            {                
                Name = this.Name,
                Deadline = this.Deadline,
                UserId = this.UserId
            });
            var newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();

        }
    }
}
