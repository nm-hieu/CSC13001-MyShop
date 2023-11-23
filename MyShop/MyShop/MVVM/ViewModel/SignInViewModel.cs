using MyShop.Core;
using MyShop.MVVM.View;
using MyShop.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
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
        private bool _isRememberMe = false;

        private string _server;
        private string _database;
        private string _serverMessage;
        private bool _isRememberServer = false;
        private bool _isConnectServer;
        
        private DatabaseBase DBB;
        private Encrypt security;

        // Properties
        public string Username {
            get => _username;
            set => _username = value; 
        }
        public string Password { 
            get => _password; 
            set => _password = value;
        }
        public string ErrorMessage { 
            get => _errorMessage; 
            set => _errorMessage = value;
        }
        public bool IsViewVisible { 
            get => _isViewVisible;
            set => _isViewVisible = value;
        }
        public bool RememberMe {
            get => _isRememberMe;
            set => _isRememberMe = value;
        }
        public string Server { 
            get => _server;
            set { _server = value; OnPropertyChanged(nameof(Server)); }
        }
        public string Database
        {
            get => _database;
            set { _database = value; OnPropertyChanged(nameof(Database)); }
        }
        public string ServerMessage
        {
            get => _serverMessage;
            set => _serverMessage = value;
        }
        public bool RememberServer { 
            get => _isRememberServer; 
            set => _isRememberServer = value; 
        }
        public bool IsConnectServer { 
            get => _isConnectServer; 
            set => _isConnectServer = DBB.ConnectToServer(Server, Database); 
        }

        // Command
        //public ICommand ShowPasswordCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand RememberServerCommand { get; }
        public ICommand ConnectServerCommand { get; }
        

        // Constructor
        public SignInViewModel()
        {
            SignInCommand = new RelayCommand(ExecuteSignInCommand, CanExecuteSignInCommand);
            RememberPasswordCommand = new RelayCommand(ExecuteRememberPasswordCommand);
            RememberServerCommand = new RelayCommand(ExecuteRememberServerCommand);
            ConnectServerCommand = new RelayCommand(ExecuteConnectServerCommand, CanExecuteConnectServerCommand);
            DBB = new DatabaseBase();
            security = new Encrypt();
            //ShowPasswordCommand = new RelayCommand(ExecuteShowPasswordCommand);
        }

        private bool CanExecuteConnectServerCommand(object arg)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Server) || string.IsNullOrWhiteSpace(Database))
                validData = false;
            else
                validData = true;
            return validData;
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
            // Validation for server connection
            ConnectServerCommand.Execute(obj);
            if (IsConnectServer == false)
            {
                ErrorMessage = "* Chưa kết nối với Server";
                return;
            }

            // TODO: Encrypt password to compare to SQL Server
            // Validation for admin role to sign in
            var isValidUser = DBB.AuthenticateUser(new NetworkCredential(Username, Password));
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
        private void ExecuteConnectServerCommand(object obj)
        {
            IsConnectServer = DBB.ConnectToServer(Server, Database);
            if (IsConnectServer)
            {
                ServerMessage = "* Kết nối tới Server thành công";
            }
            else
            {
                ServerMessage = "* Sai Server hoặc Database";
            }
        }

        private void ExecuteRememberPasswordCommand(object obj)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (RememberMe == true)
            {    
                config.AppSettings.Settings["Username"].Value = Username;
                config.AppSettings.Settings["Password"].Value = security.Protect(Password);
            }
            else
            {
                config.AppSettings.Settings["Username"].Value = "";
                config.AppSettings.Settings["Password"].Value = "";
            }
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void ExecuteRememberServerCommand(object obj)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (RememberServer == true)
            {
                config.AppSettings.Settings["Server"].Value = Server;
                config.AppSettings.Settings["Database"].Value = Database;
            }
            else
            {
                config.AppSettings.Settings["Server"].Value = "";
                config.AppSettings.Settings["Database"].Value = "";
            }
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
        }

        //private void ExecuteShowPasswordCommand(object obj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
