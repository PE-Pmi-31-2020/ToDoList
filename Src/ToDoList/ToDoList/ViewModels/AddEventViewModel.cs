using System;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;

namespace ToDoList.ViewModels
{
    internal class AddEventViewModel : INotifyPropertyChanged
    {
        private readonly IEventService _eventService;

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action EventAdded;

        private readonly INotificationService _notificationService;

        private readonly LoggerService _loggerService;

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public AddEventViewModel(Window window)
        {
            _window = window;
            AddCommand = new RelayCommand(Add);
            CancelCommand = new RelayCommand(Cancel);
            _eventService = new EventService();
            _notificationService = new NotificationService();
            _loggerService = new LoggerService();
        }

        private readonly Window _window;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (!string.Equals(_name, value))
                {
                    _name = value;
                }
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                if (!string.Equals(_description, value))
                {
                    _description = value;
                }
            }
        }

        private DateTime _fromTime;

        public DateTime FromTime
        {
            get => _fromTime;
            set
            {
                _fromTime = value;
            }
        }

        private DateTime _toTime;

        public DateTime ToTime
        {
            get => _toTime;
            set
            {
                _toTime = value;
            }
        }

        private DateTime _remindTime;

        public DateTime RemindTime
        {
            get => _remindTime;
            set
            {
                _remindTime = value;
            }
        }

        private void Add()
        {
            _eventService.CreateEventAsync(new EventDto
            {
                Name = Name,
                Description = Description,
                From = FromTime.TimeOfDay,
                To = ToTime.TimeOfDay,
                RemindTime = RemindTime.TimeOfDay,
                UserId = (int)AppConfig.UserId,
            });
            EventAdded?.Invoke();
            _notificationService.ShowNotification($"Event {Name} is successfully added!", NotificationType.Information, "Information");
            _loggerService.LogInfo($"A new event {Name} was added");
            _window.Close();
        }

        private void Cancel()
        {
            _window.Close();
        }
    }
}
