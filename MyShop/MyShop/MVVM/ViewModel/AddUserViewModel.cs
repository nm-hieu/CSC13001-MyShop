using MyShop.Core;
using MyShop.Database;
using MyShop.MVVM.Model;
using MyShop.MVVM.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace MyShop.MVVM.ViewModel
{
    class AddUserViewModel : ObservableObject
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        
        string[] getAvatarList()
        {
            string dir = "assets/images/avatar/";

            string[] files = Directory.GetFiles(dir);

            return files;
        }
        private List<string> _roleList = new List<string>();
        private List<string> _avatarList = new List<string>();
        private int _avatarIndex;
        private int _roleIndex;
        public List<string> RoleList { get => _roleList; set => _roleList = value; }
        public List<string> AvatarList { get => _avatarList; set => _avatarList = value; }
        public int AvatarIndex { get => _avatarIndex; set => _avatarIndex = value; }
        public int RoleIndex { get => _roleIndex; set => _roleIndex = value; }

        public void loadRoleData()
        {
            _roleList.Clear();
            _roleList.Add("admin");
            _roleList.Add("user");
            RoleList = _roleList;
        }
        public void loadAvatarData()
        {
            _avatarList.Clear();
            string[] a = getAvatarList();
            foreach (string s in a)
            {
                _avatarList.Add(s);
            }
            AvatarList = _avatarList;
        }
        public void addUserData()
        {
            _user = new UserModel
            {
                Avatar = AvatarList[AvatarIndex],
                Username = Username,
                FirstName = FirstName,
                LastName = LastName,
                Role = RoleList[RoleIndex],
                Email = Email,
                Telephone = Telephone,
                Address = Address,
            };
        }
        
        public ICommand AddUserCommand { get; set; }
        private UserModel _user;

        
        public AddUserViewModel()
        {
            loadRoleData();
            loadAvatarData();
            addUserData();
            AddUserCommand = new RelayCommand(ExecuteAddUserCommand);
        }
        private int getLastID(string tableName)
        {
            var sql = $"SELECT MAX(ID) AS LastID\r\nFROM [{tableName}]";
            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();
            int lastID = -1;
            while (reader.Read())
            {
                lastID = (int)reader["LastID"];
            }
            reader.Close();
            return lastID;
        }
        private void ExecuteAddUserCommand(object obj)
        {
            var cfScreen = new Confirm();
            if (cfScreen.ShowDialog()!.Value == true)
            {
                if (cfScreen.isConfirm == true)
                {
                    int ID = getLastID("User") + 1;
                    if (AddUserData(_user, ID) == true)
                    {
                        MessageBox.Show("Thêm User thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không thêm được User");
                    }
                }
            }
        }

        public bool AddUserData(UserModel user, int ID = -1)
        {
            string sql = @"INSERT INTO [User] (  ID,  Avatar,  Username,  Password,  FirstName,  LastName,  Role,  Email,  Telephone,  Address)
                                        VALUES (@ID, @Avatar, @Username, @Password, @FirstName, @LastName, @Role, @Email, @Telephone, @Address);";

            using (var connection = new SqlConnection(DatabaseBase.Instance.ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = sql;
                if (ID == -1)
                {
                    command.Parameters.AddWithValue("@ID", user.ID);
                }
                else
                {
                    command.Parameters.AddWithValue("@ID", ID);
                }
                command.Parameters.AddWithValue("@Avatar", AvatarList[AvatarIndex]);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", "password");
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Role", RoleList[RoleIndex]);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Telephone", Telephone);
                command.Parameters.AddWithValue("@Address", Address);

                int rowsAffected = 0;
                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    Message = "Username/Email/Telephone đã được sử dụng";
                }

                // < 0 is failed
                return rowsAffected > 0;
            }
        }
    }
}
