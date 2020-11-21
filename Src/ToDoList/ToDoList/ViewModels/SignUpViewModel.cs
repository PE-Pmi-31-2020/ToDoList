using System;
using System.ComponentModel;
using ToDoList.Logic;
namespace ToDoList.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        private UserService userService;
        public event PropertyChangedEventHandler PropertyChanged;

        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.userName, value))
                {
                    this.userName = value;
                }
            }
        }

        private string password1;

        public string Password1
        {
            get { return password1; }
            set
            {
                if(!string.Equals(this.password1, value))
                {
                    this.password1 = value;
                }
            }
        }

        private void RegisterUser()
        {
            try
            {
                userService.CreateUser(UserName, Password1, password2)
            }catch (Exception e)
            {
                e.Message == "Passoword Error"
            }
        }
    }
}
