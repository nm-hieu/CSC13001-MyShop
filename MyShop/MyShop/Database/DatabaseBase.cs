using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MyShop.Database
{
    public class DatabaseBase
    {
        private string _connectionString;
        public string server, database;
        protected SqlConnection GetConnection()
        {
            

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.InitialCatalog = database;
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            _connectionString = builder.ConnectionString;

            return new SqlConnection(_connectionString);
        }
        public bool ConnectToServer(string sv, string db)
        {
            server = sv;
            database = db;

            if (string.IsNullOrWhiteSpace(sv)|| string.IsNullOrWhiteSpace(db))
                return false;
            var connection = GetConnection();
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
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
    }
}
