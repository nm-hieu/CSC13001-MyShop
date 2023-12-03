using MyShop.Core;
using MyShop.MVVM.View;
using MyShop.Database;
using MyShop.MVVM.Model;
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
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace MyShop.MVVM.ViewModel
{
    class SignInViewModel: ObservableObject
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
        
        private Encrypt security;

        // Properties
        public string Username {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }
        public string Password { 
            get => _password; 
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        public string ErrorMessage { 
            get => _errorMessage; 
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
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
            set { _serverMessage = value; OnPropertyChanged(nameof(ServerMessage)); }
        }
        public bool RememberServer { 
            get => _isRememberServer; 
            set => _isRememberServer = value; 
        }
        public bool IsConnectServer { get; set; }

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
            // Validation for admin role to sign 
            var isValidUser = AuthenticateUser(new NetworkCredential(Username, Password));

            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;

                // TODO: Pass Server and Database to MainWindow
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["CurrentUserID"].Value = getCurrentUserID(Username).ToString();
                config.AppSettings.Settings["CurrentServer"].Value = Server;
                config.AppSettings.Settings["CurrentDatabase"].Value = Database;
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");

                var mainView = new MainWindow();
                mainView.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                ErrorMessage = "* Sai Username hoặc Mật khẩu";
            }
        }
        private int getCurrentUserID(string Username)
        {
            int ID = -1;
            string sql = $"select ID from [User] where Username=@Username";
            SqlCommand command = new SqlCommand(sql, DB.Instance.Connection);
            command.Parameters.AddWithValue("@Username", Username);

            var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                ID = (int)reader["ID"];
            }
            reader.Close();

            return ID;
        }
        private void ConnectToServer(string sv, string db)
        {
            if (string.IsNullOrWhiteSpace(sv) || string.IsNullOrWhiteSpace(db))
                IsConnectServer = false;

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = sv;
            builder.InitialCatalog = db;
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            
            string connectionString = builder.ConnectionString;

            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                connection = null;
            }
                
            if (connection != null)
            {
                IsConnectServer = true;
                DB.Instance.ConnectionString = connectionString;
            }
            else
            {
                IsConnectServer = false;
            }
        }
        private void ExecuteConnectServerCommand(object obj)
        {
            ConnectToServer(Server, Database);
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

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var command = new SqlCommand())
            {
                command.Connection = DB.Instance.Connection;
                command.CommandText = "select * from [User] where username=@username and [password]=@password and role=@role";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                command.Parameters.Add("@role", SqlDbType.NVarChar).Value = "admin";
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;   
        }

        //private void ExecuteShowPasswordCommand(object obj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
