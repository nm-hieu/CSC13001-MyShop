using MyShop.Core;
using MyShop.MVVM.Model;
using MyShop.MVVM.View;
using MyShop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyShop.MVVM.ViewModel
{
    class SignInViewModel : ObservableObject
    {
        // Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private bool _isRemember = false;
        
        // Properties
        public string Username {
            get => _username;
            set { 
                _username = value; 
                OnPropertyChanged(nameof(Username));
            } 
        }
        public string Password { 
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
        public bool RememberMe {
            get => _isRemember;
            set
            {
                _isRemember = value;
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        // Command
        public ICommand SignInCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand LoadedCommand { get; }

        private UserRepository userRepository;
        
        // Constructor
        public SignInViewModel()
        {
            userRepository = new UserRepository();
            SignInCommand = new RelayCommand(ExecuteSignInCommand, CanExecuteSignInCommand);
            ShowPasswordCommand = new RelayCommand(ExecuteShowPasswordCommand);
            RememberPasswordCommand = new RelayCommand(ExecuteRememberPasswordCommand);
            LoadedCommand = new RelayCommand(SignInViewModel_Load);
        }

        private bool CanExecuteSignInCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteSignInCommand(object obj)
        {
            // TODO: Encrypt password to save to SQL Server

            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;

                var mainView = new MainWindow();
                mainView.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                ErrorMessage = "* Sai Username hoặc Mật khẩu";
            }
        }

        private void ExecuteRememberPasswordCommand(object obj)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (RememberMe == true)
            {    
                config.AppSettings.Settings["Username"].Value = Username;
                //config.AppSettings.Settings["Password"].Value = Password;
                config.AppSettings.Settings["Password"].Value = userRepository.Protect(Password);
            }
            else
            {
                config.AppSettings.Settings["Username"].Value = "";
                config.AppSettings.Settings["Password"].Value = "";
            }
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void SignInViewModel_Load(object obj)
        {
            var PasswordInString = ConfigurationManager.AppSettings["Password"];

            if (PasswordInString.Length != 0)
            {
                Username = ConfigurationManager.AppSettings["Username"];
                //Password = PasswordInString;
                Password = userRepository.Unprotect(PasswordInString);
                RememberMe = true;
            }
        }

        private void ExecuteShowPasswordCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
