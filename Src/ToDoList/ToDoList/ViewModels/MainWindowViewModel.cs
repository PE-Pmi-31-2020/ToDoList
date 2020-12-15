using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.Annotations;
using ToDoList.BLL;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INotificationService _notificationService;
        private readonly ITaskService _taskService;
        private readonly IEventService _eventService;
        private readonly LoggerService _loggerService;

        public RelayCommand AddEventCommand { get; private set; }

        public RelayCommand AddTaskCommand { get; private set; }

        public RelayCommand LogoutCommand { get; private set; }

        public RelayCommand RemoveEventCommand { get; private set; }

        private string _userFullName;

        public string UserFullName
        {
            get => _userFullName;
            set
            {
                if (!value.Equals(_userFullName))
                {
                    _userFullName = value;
                    OnPropertyChanged(nameof(UserFullName));
                }
            }
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (!value.Equals(_userName))
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private Event _selectedEvent;

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        private Task _selectedTask;

        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public ObservableCollection<Task> Tasks { get; set; }

        public ObservableCollection<Event> Events { get; set; }

        public MainWindowViewModel()
        {
            _notificationService = new NotificationService();
            _notificationService.RunNotificationKernel();
            IUserService userService = new UserService();
            _taskService = new TaskService();
            _eventService = new EventService();
            _loggerService = new LoggerService();

            AddEventCommand = new RelayCommand(ShowAddEvent);
            AddTaskCommand = new RelayCommand(ShowAddTask);
            LogoutCommand = new RelayCommand(Logout);
            RemoveEventCommand = new RelayCommand(RemoveEvent);

            _userFullName = userService.GetUserFullNameById(AppConfig.UserId);
            _userName = userService.GetUserFullNameById(AppConfig.UserId);
            Events = new ObservableCollection<Event>(_eventService.GetEventsByUserId(AppConfig.UserId));
            Tasks = new ObservableCollection<Task>(_taskService.GetTasksByUserId(AppConfig.UserId));
        }

        public void EditTask()
        {
            if (SelectedTask == null)
            {
                return;
            }
            var editTaskwindow = new EditTask();
            var viewModel = new EditTaskViewModel(editTaskwindow, SelectedTask);
            viewModel.TaskUpdated += (sender, x) =>
            {
                var taskToUpdate = Tasks.Where(t => t.Id == x.Id).FirstOrDefault();
                Tasks.Remove(taskToUpdate);
                Tasks.Add(x);
            };
            editTaskwindow.DataContext = viewModel;
            editTaskwindow.ShowDialog();
        }

        public void EditEvent()
        {
            if (SelectedEvent == null)
            {
                return;
            }
            var editEventwindow = new EditEvent();
            var viewModel = new EditEventViewModel(editEventwindow, SelectedEvent);
            viewModel.EventUpdated += (sender, x) =>
            {
                var eventToUpdate = Events.Where(e => e.Id == x.Id).FirstOrDefault();
                Events.Remove(eventToUpdate);
                Events.Add(x);
            };
            editEventwindow.DataContext = viewModel;
            editEventwindow.ShowDialog();
        }

        private void ShowAddEvent()
        {
            var addEventWindow = new AddEvent();
            ((AddEventViewModel)addEventWindow.DataContext).EventAdded += () =>
            {
                foreach (var el in _eventService.GetEventsByUserId(AppConfig.UserId))
                {
                    if (!Events.Select(e => e.Id).Contains(el.Id))
                    {
                        Events.Add(el);
                    }
                }
            };
            addEventWindow.ShowDialog();
        }

        private void ShowAddTask()
        {
            var addTaskWindow = new AddTask();
            ((AddTaskViewModel)addTaskWindow.DataContext).TaskAdded += () =>
            {
                foreach (var el in _taskService.GetTasksByUserId(AppConfig.UserId))
                {
                    if (!Tasks.Select(t => t.Id).Contains(el.Id))
                    {
                        Tasks.Add(el);
                    }
                }
            };
            addTaskWindow.ShowDialog();
        }

        private void Logout()
        {
            AppConfig.UserId = null;
            var newWindow = new StartWindow();
            Application.Current.MainWindow?.Close();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
            _notificationService.ShowNotification("You are logged out", NotificationType.Success, "Success");
            _loggerService.LogInfo($"{UserName} logged out");
        }

        private void RemoveEvent()
        {
            _eventService.DeleteEventAsync(SelectedEvent.Id);
            Events.Remove(SelectedEvent);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
