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
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> AddEventCommand { get; private set; }
        public RelayCommand<Window> AddTaskCommand { get; private set; }

        public MainWindowViewModel()
        {
            AddEventCommand = new RelayCommand<Window>(ShowAddEvent);
            AddTaskCommand = new RelayCommand<Window>(ShowAddTask);
        }

        private void ShowAddEvent(Window window)
        {
            AddEvent newWindow = new AddEvent();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            if (window != null)
            {
                window.Close();
            }

        }

        private void ShowAddTask(Window window)
        {
            AddTask newWindow = new AddTask();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            if (window != null)
            {
                window.Close();
            }

        }
    }
}
