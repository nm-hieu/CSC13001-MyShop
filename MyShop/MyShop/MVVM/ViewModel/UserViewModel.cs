using MyShop.Core;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyShop.Database;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Data;
using MyShop.MVVM.View;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace MyShop.MVVM.ViewModel
{
    class UserViewModel
    {
        public ICommand EditUserCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public RelayCommand AddUserCommand { get; set; }
        
        public UserViewModel()
        {
            loadUserData();

            EditUserCommand = new RelayCommand(ExecuteEditUserCommmand);
            AddUserCommand = new RelayCommand(o =>
            {
                var addUser = new AddUser();
                addUser.Show();
            });
            ReloadCommand = new RelayCommand(o =>
            {
                loadUserData();
            });
        }

        private ObservableCollection<UserModel> _userList = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> UserList { get => _userList; set => _userList = value; }
        public void loadUserData()
        {
            using (var connection = new SqlConnection(DatabaseBase.Instance.ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *, count(*) over() as Total from [User]";
                var reader = command.ExecuteReader();
                UserList.Clear();

                while (reader.Read())
                {
                    int id = (int)reader["ID"];
                    string username = (string)reader["Username"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    string role = (string)reader["Role"];
                    string email = (string)reader["Email"];
                    string telephone = (string)reader["Telephone"];
                    string address = (string)reader["Address"];
                    string avatar = (string)reader["Avatar"];

                    var user = new UserModel()
                    {
                        ID = id,
                        Username = username,
                        FirstName = firstName,
                        LastName = lastName,
                        FullName = firstName + " " + lastName,
                        Role = role,
                        Email = email,
                        Telephone = telephone,
                        Address = address,
                        Avatar = avatar,
                    };
                    UserList.Add(user);
                }
                reader.Close();
            }
        }

        private void ExecuteEditUserCommmand(object obj)
        {
            int userID = int.Parse(obj.ToString());
            var editUser = new EditUser(this.getUserByID(userID));
            editUser.Show();
        }

        public UserModel getUserByID(int id)
        {
            int index = -1;

            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].ID == id)
                {
                    index = i; break;
                }
            }
            return UserList[index];
        }
    }
}
