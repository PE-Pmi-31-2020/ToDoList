using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.Logic.DTO;
using ToDoList.Views;
using ToDoList.Logic.Interfaces;
using ToDoList.Logic.Services;

namespace ToDoList.ViewModels
{
    class AddEventViewModel : INotifyPropertyChanged
    {
        private IEventService _eventService;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SubmitCommand { get; private set; }
        public AddEventViewModel()
        {
            SubmitCommand = new RelayCommand<Window>(Submit);
            _eventService = new EventService();
        }

        private string name;
        public string Name {
            get { return name; }
            set
            {
                if (!string.Equals(this.name, value))
                {
                    this.name = value;
                }
            }
        }
        private string description;
        public string Description {
            get { return description; }
            set
            {
                if (!string.Equals(this.description, value))
                {
                    this.description = value;
                }
            }
        }
        private TimeSpan fromTime;
        public TimeSpan FromTime
        {
            get { return fromTime; }
            set
            {
                if (!string.Equals(this.fromTime, value))
                {
                    this.fromTime = value;
                }
            }
        }
        private TimeSpan toTime;
        public TimeSpan ToTime
        {
            get { return toTime; }
            set
            {
                if (!string.Equals(this.toTime, value))
                {
                    this.toTime = value;
                }
            }
        }
        private TimeSpan remindTime;
        public TimeSpan RemindTime
        {
            get { return remindTime; }
            set
            {
                if (!string.Equals(this.remindTime, value))
                {
                    this.remindTime = value;
                }
            }
        }
        private int userId;
        public int UserId {
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
            _eventService.CreateEventAsync(new EventDto()
            {
                Name = this.Name,
                Description = this.Description,
                From=this.FromTime,
                To = this.ToTime,
                RemindTime = this.RemindTime,
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
