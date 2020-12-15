using ToDoList.ViewModels;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for AddTask.xaml.
    /// </summary>
    public partial class AddTask
    {
        public AddTask()
        {
            InitializeComponent();
            DataContext = new AddTaskViewModel(this);
        }
    }
}
