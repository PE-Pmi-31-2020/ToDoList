using System;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class AddEventViewModel : INotifyPropertyChanged
    {
        private readonly IEventService eventService;

        public event PropertyChangedEventHandler PropertyChanged;

        public event Action EventAdded; 

        public RelayCommand AddCommand { get; private set; }

        public RelayCommand CancelCommand { get; private set; }

        public AddEventViewModel(Window window)
        {
            this.window = window;
            this.AddCommand = new RelayCommand(this.Add);
            this.CancelCommand = new RelayCommand(this.Cancel);
            this.eventService = new EventService();
        }

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

        private DateTime fromTime;

        public DateTime FromTime
        {
            get => this.fromTime;
            set
            {
                this.fromTime = value;
            }
        }

        private DateTime toTime;

        public DateTime ToTime
        {
            get => this.toTime;
            set
            {
                this.toTime = value;
            }
        }

        private DateTime remindTime;

        public DateTime RemindTime
        {
            get => this.remindTime;
            set
            {
                this.remindTime = value;
            }
        }

        private void Add()
        {
            this.eventService.CreateEventAsync(new EventDto()
            {
                Name = this.Name,
                Description = this.Description,
                From = this.FromTime.TimeOfDay,
                To = this.ToTime.TimeOfDay,
                RemindTime = this.RemindTime.TimeOfDay,
                UserId = (int)AppConfig.UserId,
            });
            EventAdded.Invoke();
            this.window.Close();
        }

        private void Cancel()
        {
            this.window.Close();
        }
    }
}
