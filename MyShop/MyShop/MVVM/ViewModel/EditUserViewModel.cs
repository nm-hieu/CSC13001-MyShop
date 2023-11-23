using MyShop.MVVM.Model;
using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Input;
using MyShop.MVVM.View;
using static Azure.Core.HttpHeader;
using Microsoft.Data.SqlClient;
using MyShop.Database;
using System.ComponentModel;

namespace MyShop.MVVM.ViewModel
{
    class EditUserViewModel : ObservableObject
    {
        /*
        private string _username;
        private string _firstname;
        private string _lastname;
        private string _role;
        private string _email;
        private string _telephone;
        private string _address;
        
        public string Username { get => _username; set => _username = value; }
        public string FirstName { get => _firstname; set { _firstname = value; OnPropertyChanged(nameof(FirstName)); } }
        public string LastName { get => _lastname; set => _lastname = value; }
        public string Role { get => _role; set => _role = value; }
        public string Email { get => _email; set => _email = value; }
        public string Telephone { get => _telephone; set => _telephone = value; }
        public string Address { get => _address; set => _address = value; }
        */
        
        public string Username {  get; set; }
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string Role {  get; set; }
        public string Email {  get; set; }
        public string Telephone {  get; set; }
        public string Address {  get; set; }
        
        string[] getAvatarList()
        {
            string dir = "assets/images/avatar/";

            string[] files = Directory.GetFiles(dir);
            //string[] avatar = new string[files.Length];
            //for (int i = 0; i < files.Length; i++)
            //{
            //    avatar[i] = Path.GetFileName(files[i]);
            //}
            
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
            foreach(string s in a)
            {
                _avatarList.Add(s);
            }
            AvatarList = _avatarList;
        }
        public void loadUserData(UserModel user)
        {
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role;
            Email = user.Email;
            Telephone = user.Telephone;
            Address = user.Address;
        }
        public void getIndex(UserModel user)
        {
            for (int i = 0; i < AvatarList.Count; i++)
            {
                if (user.Avatar == AvatarList[i])
                {
                    AvatarIndex = i; break;
                }
            }

            for (int i = 0; i < RoleList.Count; i++)
            {
                if (user.Role == RoleList[i])
                {
                    RoleIndex = i; break;
                }
            }

            
        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private UserModel _user;
        
        public EditUserViewModel(UserModel user)
        {
            _user = user;
            loadRoleData();
            loadAvatarData();
            loadUserData(user);
            getIndex(user);

            ConfirmCommand = new RelayCommand(ExecuteConfirmCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
        }

        private void ExecuteConfirmCommand(object obj)
        {
            var cfScreen = new Confirm();
            if (cfScreen.ShowDialog()!.Value == true)
            {
                if (cfScreen.isConfirm == true)
                {
                    // TODO:
                    // 1. Check if duplicate field in database
                    // 2. Reload ListView auto
                    if (EditUserData(_user) == true)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    } else
                    {
                        MessageBox.Show("Không có cập nhật");
                    }
                } 
            }
        }

        public bool EditUserData(UserModel user)
        {
            string sql = @"UPDATE [User] Set Avatar = @Avatar, Username = @Username, FirstName = @FirstName, LastName = @LastName,
                                            Role = @Role, Email = @Email, Telephone = @Telephone, Address = @Address where ID = @ID";

            using (var connection = new SqlConnection(DatabaseBase.Instance.ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@ID", user.ID);
                command.Parameters.AddWithValue("@Avatar", AvatarList[AvatarIndex]);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Role", RoleList[RoleIndex]);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Telephone", Telephone);
                command.Parameters.AddWithValue("@Address", Address);

                int rowsAffected = command.ExecuteNonQuery();

                // < 0 is failed
                return rowsAffected > 0;
            }
        }

        private void ExecuteDeleteCommand(object obj)
        {
            var cfScreen = new Confirm();
            if (cfScreen.ShowDialog()!.Value == true)
            {
                if (cfScreen.isConfirm == true)
                {
                    // TODO:
                    // 1. Check if duplicate field in database
                    // 2. Reload ListView auto
                    if (DeleteUserData(_user) == true)
                    {
                        MessageBox.Show("Xoá User thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại");
                    }
                }
            }
        }

        public bool DeleteUserData(UserModel user)
        {
            string sql = @"DELETE FROM [User] where ID = @ID";

            using (var connection = new SqlConnection(DatabaseBase.Instance.ConnectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@ID", user.ID);
                
                int rowsAffected = command.ExecuteNonQuery();

                // < 0 is failed
                return rowsAffected > 0;
            }
        }
    }
}
