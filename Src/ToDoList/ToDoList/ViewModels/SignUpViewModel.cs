using System;
using System.ComponentModel;
using ToDoList.Logic;
namespace ToDoList.ViewModels
{
    class SignUpViewModel : INotifyPropertyChanged
    {
        private UserService userLogic;
        public event PropertyChangedEventHandler PropertyChanged;

        private void RegisterUser()
        {
            try
            {
                userLogic.CreateUser();
            }catch (Exception e)
            {
                e.Message == "Passoword Error"
            }
        }
    }
}
