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
    internal class AddEventViewModel : INotifyPropertyChanged
    {
        private readonly IEventService eventService;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Window> SubmitCommand { get; private set; }

        public AddEventViewModel()
        {
            this.SubmitCommand = new RelayCommand<Window>(this.Submit);
            this.eventService = new EventService();
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

        private string description;

        public string Description
        {
            get => this.description;
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
            get => this.fromTime;
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
            get => this.toTime;
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
            get => this.remindTime;
            set
            {
                if (!string.Equals(this.remindTime, value))
                {
                    this.remindTime = value;
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
            this.eventService.CreateEventAsync(new EventDto()
            {
                Name = this.Name,
                Description = this.Description,
                From = this.FromTime,
                To = this.ToTime,
                RemindTime = this.RemindTime,
                UserId = this.UserId,
            });
            var newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }
    }
}
