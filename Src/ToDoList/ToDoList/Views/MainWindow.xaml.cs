using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Notifications.Wpf;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            DispatcherTimer liveTime = new DispatcherTimer();
            liveTime.Interval = TimeSpan.FromSeconds(1);
            liveTime.Tick += timer_Tick;
            liveTime.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        private void OnWindowClose(object sender, EventArgs e)
        {
            if (Application.Current.Windows.Cast<Window>().All(x => (x is NotificationsOverlayWindow)))
            {
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((MainWindowViewModel) DataContext).EditTask();

        }

        private void Control_OnMouseDoubleClick2(object sender, MouseButtonEventArgs e)
        {
            ((MainWindowViewModel)DataContext).EditEvent();
        }
    }
}
