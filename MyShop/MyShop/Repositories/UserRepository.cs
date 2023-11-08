using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Repositories
{
    public class UserRepository : RepositoryBase
    {
        // Encrpyt the password
        public string Protect(string str)
        {
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            byte[] data = Encoding.ASCII.GetBytes(str);
            string protectedData = Convert.ToBase64String(ProtectedData.Protect(data, entropy, DataProtectionScope.CurrentUser));
            return protectedData;
        }

        // Decrypt the password
        public string Unprotect(string str)
        {
            byte[] protectedData = Convert.FromBase64String(str);
            byte[] entropy = Encoding.ASCII.GetBytes(Assembly.GetExecutingAssembly().FullName);
            string data = Encoding.ASCII.GetString(ProtectedData.Unprotect(protectedData, entropy, DataProtectionScope.CurrentUser));
            return data;
        }
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User] where username=@username and [password]=@password and role=@role";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                command.Parameters.Add("@role", SqlDbType.NVarChar).Value = "admin";
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }
        
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> loadAllUser()
        {
            List<UserModel> userList = new List<UserModel>();
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from [User]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Username = reader[0].ToString(),
                            Password = string.Empty,
                            FirstName = reader[2].ToString(),
                            LastName = reader[3].ToString(),
                            Role = reader[4].ToString(),
                            Email = reader[5].ToString(),
                            Telephone = reader[6].ToString(),
                            Address = reader[7].ToString(),
                        };
                        userList.Add(user);
                    }
                    reader.Close();
                }
            }
            return userList;
        }
    }
}
