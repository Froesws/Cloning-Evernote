using MyEvernote.Auxiliary;
using MyEvernote.Model;
using System.Windows.Input;

namespace MyEvernote.ViewModel
{
    public class LoginViewModel
    {
        private User user;

        public User MyProperty
        {
            get { return user; }
            set { user = value; }
        }

        private ICommand registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                return registerCommand = registerCommand ?? new RelayCommand(x => RegisterButton());
            }
        }

        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand = loginCommand ?? new RelayCommand(x => LoginButton());
            }
        }

        private void LoginButton()
        {

        }
        private void RegisterButton()
        {

        }

    }
}
