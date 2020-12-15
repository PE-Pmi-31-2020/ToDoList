using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.Annotations;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;

namespace ToDoList.ViewModels
{
    class EditEventViewModel: INotifyPropertyChanged
    {
        private readonly IEventService _eventService;

        public event EventHandler<Event> EventUpdated;

        private readonly INotificationService _notificationService;

        private readonly LoggerService _loggerService;

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public EditEventViewModel(Window window, Event event1)
        {
            this._window = window;
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            _eventService = new EventService();
            _notificationService = new NotificationService();
            _loggerService = new LoggerService();

            Event = event1;
            Name = Event.Name;
            Description = Event.Description;
            FromTime = Convert.ToDateTime(Event.From.ToString());
            ToTime = Convert.ToDateTime(Event.To.ToString());
            RemindTime = Convert.ToDateTime(Event.RemindTime.ToString());
        }

        private Event Event { get; set; }

        private Window _window;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (!string.Equals(_name, value))
                {
                    _name = value;
                    Event.Name = value;
                    OnPropertyChanged(nameof(Name));
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
                    Event.Description = value;
                    OnPropertyChanged(nameof(Description));
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
                Event.From = value.TimeOfDay;
                OnPropertyChanged(nameof(FromTime));
            }
        }

        private DateTime _toTime;

        public DateTime ToTime
        {
            get => _toTime;
            set
            {
                _toTime = value;
                Event.To = value.TimeOfDay;
                OnPropertyChanged(nameof(ToTime));
            }
        }

        private DateTime _remindTime;

        public DateTime RemindTime
        {
            get => _remindTime;
            set
            {
                _remindTime = value;
                Event.RemindTime = value.TimeOfDay;
                OnPropertyChanged(nameof(RemindTime));
            }
        }

        private void Save()
        {
            _eventService.EditEventAsync(new EventDto
            {
                Description = Event.Description,
                From = Event.From,
                Id = Event.Id,
                To = Event.To,
                Name = Event.Name,
                RemindTime = Event.RemindTime
            });
            EventUpdated?.Invoke(this, Event);
            _notificationService.ShowNotification($"Event {Name} is successfully updated!", NotificationType.Information, "Information");
            _loggerService.LogInfo($"A new event {Name} was updated");
            _window.Close();
        }

        private void Cancel()
        {
            _window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
