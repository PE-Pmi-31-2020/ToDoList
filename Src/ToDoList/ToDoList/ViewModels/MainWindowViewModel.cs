using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Window> AddEventCommand { get; private set; }

        public RelayCommand<Window> AddTaskCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.AddEventCommand = new RelayCommand<Window>(this.ShowAddEvent);
            this.AddTaskCommand = new RelayCommand<Window>(this.ShowAddTask);
        }

        private void ShowAddEvent(Window window)
        {
            var newWindow = new AddEvent();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }

        private void ShowAddTask(Window window)
        {
            var newWindow = new AddTask();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }
    }
}
