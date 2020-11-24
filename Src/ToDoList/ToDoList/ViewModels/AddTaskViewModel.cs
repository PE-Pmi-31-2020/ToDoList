using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDoList.Logic.DTO;
using ToDoList.Logic.Interfaces;
using ToDoList.Logic.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    class AddTaskViewModel : INotifyPropertyChanged
    {
        private ITaskService _taskService;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SubmitCommand { get; private set; }
        public AddTaskViewModel()
        {
            SubmitCommand = new RelayCommand<Window>(Submit);
            _taskService = new TaskService();
        }

        private string name;
        public string Name
        {
            get { return name; }
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
            get { return deadline; }
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
            get { return userId; }
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
            _taskService.CreateTaskAsync(new TaskDto()
            {                
                Name = this.Name,
                Deadline = this.Deadline,
                UserId = this.UserId
            });
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            if (window != null)
            {
                window.Close();
            }

        }
    }
}
