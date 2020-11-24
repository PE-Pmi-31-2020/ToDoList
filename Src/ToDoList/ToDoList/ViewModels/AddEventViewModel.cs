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
        private readonly IEventService _eventService;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<Window> SubmitCommand { get; private set; }
        public AddEventViewModel()
        {
            SubmitCommand = new RelayCommand<Window>(Submit);
            _eventService = new EventService();
        }

        private string _name;
        public string Name {
            get => _name;
            set
            {
                if (!string.Equals(this._name, value))
                {
                    this._name = value;
                }
            }
        }
        private string _description;
        public string Description {
            get => _description;
            set
            {
                if (!string.Equals(this._description, value))
                {
                    this._description = value;
                }
            }
        }
        private TimeSpan _fromTime;
        public TimeSpan FromTime
        {
            get => _fromTime;
            set
            {
                if (!string.Equals(this._fromTime, value))
                {
                    this._fromTime = value;
                }
            }
        }
        private TimeSpan _toTime;
        public TimeSpan ToTime
        {
            get => _toTime;
            set
            {
                if (!string.Equals(this._toTime, value))
                {
                    this._toTime = value;
                }
            }
        }
        private TimeSpan _remindTime;
        public TimeSpan RemindTime
        {
            get => _remindTime;
            set
            {
                if (!string.Equals(this._remindTime, value))
                {
                    this._remindTime = value;
                }
            }
        }
        private int _userId;
        public int UserId {
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
            _eventService.CreateEventAsync(new EventDto()
            {
                Name = this.Name,
                Description = this.Description,
                From=this.FromTime,
                To = this.ToTime,
                RemindTime = this.RemindTime,
                UserId = this.UserId
            });
            var newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            window?.Close();
        }
    }
}
