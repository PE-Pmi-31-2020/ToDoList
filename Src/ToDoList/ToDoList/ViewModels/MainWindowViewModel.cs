using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Notifications.Wpf;
using ToDoList.Annotations;
using ToDoList.BLL;
using ToDoList.BLL.DTO;
using ToDoList.BLL.Interfaces;
using ToDoList.BLL.Services;
using ToDoList.DAL.Entities;
using ToDoList.Views;
using Task = ToDoList.DAL.Entities.Task;

namespace ToDoList.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly INotificationService notificationService;
        private readonly IUserService userService;
        private readonly ITaskService taskService;
        private readonly IEventService eventService;
        private readonly LoggerService loggerService;

        public RelayCommand AddEventCommand { get; private set; }

        public RelayCommand AddTaskCommand { get; private set; }

        public RelayCommand LogoutCommand { get; private set; }

        public RelayCommand RemoveEventCommand { get; private set; }

        private string userFullName;

        public string UserFullName
        {
            get => userFullName;
            set
            {
                if (!value.Equals(this.userFullName))
                {
                    this.userFullName = value;
                    this.OnPropertyChanged(nameof(UserFullName));
                }
            }
        }

        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                if (!value.Equals(this.userName))
                {
                    this.userName = value;
                    this.OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private Event selectedEvent;

        public Event SelectedEvent
        {
            get => selectedEvent;
            set
            {
                this.selectedEvent = value;
                this.OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        private Task selectedTask;

        public Task SelectedTask
        {
            get => selectedTask;
            set
            {
                this.selectedTask = value;
                this.OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public ObservableCollection<Task> Tasks { get; set; }

        public ObservableCollection<Event> Events { get; set; }

        public MainWindowViewModel()
        {
            this.notificationService = new NotificationService();
            this.notificationService.RunNotificationKernel();
            this.userService = new UserService();
            this.taskService = new TaskService();
            this.eventService = new EventService();
            this.loggerService = new LoggerService();

            this.AddEventCommand = new RelayCommand(this.ShowAddEvent);
            this.AddTaskCommand = new RelayCommand(this.ShowAddTask);
            this.LogoutCommand = new RelayCommand(this.Logout);
            this.RemoveEventCommand = new RelayCommand(this.RemoveEvent);

            this.userFullName = this.userService.GetUserFullNameById(AppConfig.UserId);
            this.userName = this.userService.GetUserFullNameById(AppConfig.UserId);
            this.Events = new ObservableCollection<Event>(this.eventService.GetEventsByUserId(AppConfig.UserId));
            this.Tasks = new ObservableCollection<Task>(this.taskService.GetTasksByUserId(AppConfig.UserId));
        }

        public void EditTask()
        {
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

        private void ShowAddEvent()
        {
            var addEventWindow = new AddEvent();
            ((AddEventViewModel)addEventWindow.DataContext).EventAdded += () =>
            {
                foreach (var el in this.eventService.GetEventsByUserId(AppConfig.UserId))
                {
                    if (!this.Events.Select(e => e.Id).Contains(el.Id))
                    {
                        this.Events.Add(el);
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
                foreach (var el in this.taskService.GetTasksByUserId(AppConfig.UserId))
                {
                    if (!this.Tasks.Select(t => t.Id).Contains(el.Id))
                    {
                        this.Tasks.Add(el);
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
            notificationService.ShowNotification("You are logged out", NotificationType.Success, "Success");
            loggerService.LogInfo($"{this.UserName} logged out");
        }

        private void RemoveEvent()
        {
            eventService.DeleteEventAsync(SelectedEvent.Id);
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
