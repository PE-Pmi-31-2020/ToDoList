using System;
using System.Linq;
using System.Windows;
using Notifications.Wpf;
using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml.
    /// </summary>
    public partial class StartWindow
    {
        public StartWindow()
        {
            InitializeComponent();
            DataContext = new StartViewModel();
        }

        private void OnWindowClose(object sender, EventArgs e)
        {
            if (Application.Current.Windows.Cast<Window>().All(x => (x is NotificationsOverlayWindow)))
            {
                Environment.Exit(Environment.ExitCode);
            }
        }

    }
}
