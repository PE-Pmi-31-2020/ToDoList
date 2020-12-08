using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;
using ToDoList.Annotations;
using ToDoList.BLL;
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

        public RelayCommand AddEventCommand { get; private set; }

        public RelayCommand AddTaskCommand { get; private set; }

        private string userFulName;

        public string UserFullName
        {
            get => userFulName;
            set
            {
                if (!value.Equals(this.userFulName))
                {
                    this.userFulName = value;
                    this.OnPropertyChanged(nameof(UserFullName));
                }
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

            this.AddEventCommand = new RelayCommand(this.ShowAddEvent);
            this.AddTaskCommand = new RelayCommand(this.ShowAddTask);

            this.userFulName = this.userService.GetUserFullNameById(AppConfig.UserId);
            this.Events = new ObservableCollection<Event>(this.eventService.GetEventsByUserId(AppConfig.UserId));
            this.Tasks = new ObservableCollection<Task>(this.taskService.GetTasksByUserId(AppConfig.UserId));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
