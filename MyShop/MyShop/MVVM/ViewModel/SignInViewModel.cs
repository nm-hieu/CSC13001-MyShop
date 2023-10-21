using MyShop.Core;
using MyShop.MVVM.Model;
using MyShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.MVVM.ViewModel
{
    class SignInViewModel : ObservableObject
    {
        // Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;

        // Properties
        public string Username {
            get => _username;
            set { 
                _username = value; 
                OnPropertyChanged(nameof(Username));
            } 
        }
        public SecureString Password { 
            get => _password; 
            set { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage { 
            get => _errorMessage; 
            set { 
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible { 
            get => _isViewVisible;
            set { 
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // Command
        public ICommand SignInCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructor
        public SignInViewModel()
        {
            userRepository = new UserRepository();
            SignInCommand = new RelayCommand(ExecuteSignInCommand, CanExecuteSignInCommand);
        }

        private bool CanExecuteSignInCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteSignInCommand(object obj)
        {
            //var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }
    }
}
