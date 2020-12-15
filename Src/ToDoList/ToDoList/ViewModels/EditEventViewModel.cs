using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.Annotations;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;

namespace ToDoList.ViewModels
{
    class EditEventViewModel: INotifyPropertyChanged
    {
        private readonly IEventService eventService;

        public event EventHandler<Event> EventUpdated;

        private readonly INotificationService _notificationService;

        private readonly LoggerService loggerService;

        public RelayCommand SaveCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public EditEventViewModel(Window window, Event event1)
        {
            this.window = window;
            this.SaveCommand = new RelayCommand(this.Save);
            this.CancelCommand = new RelayCommand(this.Cancel);
            this.eventService = new EventService();
            this._notificationService = new NotificationService();
            loggerService = new LoggerService();

            Event = event1;
            Name = Event.Name;
            Description = Event.Description;
            FromTime = Convert.ToDateTime(Event.From.ToString());
            ToTime = Convert.ToDateTime(Event.To.ToString());
            RemindTime = Convert.ToDateTime(Event.RemindTime.ToString());
        }

        private Event Event { get; set; }

        private Window window;

        private string name;

        public string Name
        {
            get => this.name;
            set
            {
                if (!string.Equals(this.name, value))
                {
                    this.name = value;
                    this.Event.Name = value;
                    this.OnPropertyChanged(nameof(Name));
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
                    this.Event.Description = value;
                    this.OnPropertyChanged(nameof(Description));
                }
            }
        }

        private DateTime fromTime;

        public DateTime FromTime
        {
            get => this.fromTime;
            set
            {
                this.fromTime = value;
                this.Event.From = value.TimeOfDay;
                this.OnPropertyChanged(nameof(FromTime));
            }
        }

        private DateTime toTime;

        public DateTime ToTime
        {
            get => this.toTime;
            set
            {
                this.toTime = value;
                this.Event.To = value.TimeOfDay;
                this.OnPropertyChanged(nameof(ToTime));
            }
        }

        private DateTime remindTime;

        public DateTime RemindTime
        {
            get => this.remindTime;
            set
            {
                this.remindTime = value;
                this.Event.RemindTime = value.TimeOfDay;
                this.OnPropertyChanged(nameof(RemindTime));
            }
        }

        private void Save()
        {
            this.eventService.EditEventAsync(new EventDto()
            {
                Description = this.Event.Description,
                From = this.Event.From,
                Id = this.Event.Id,
                To = this.Event.To,
                Name = this.Event.Name,
                RemindTime = this.Event.RemindTime
            });
            EventUpdated.Invoke(this, Event);
            this._notificationService.ShowNotification($"Event {Name} is successfully updated!", NotificationType.Information, "Information");
            loggerService.LogInfo($"A new event {Name} was updated");
            this.window.Close();
        }

        private void Cancel()
        {
            this.window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
