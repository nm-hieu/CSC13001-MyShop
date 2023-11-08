using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace MyShop.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            _connectionString = "Server=(local); Database=MVVMSignInDb; Integrated Security=true";
        }

        public RepositoryBase(string ServerName, string DatabaseName)
        {
            _connectionString = @$"Server = {ServerName};
                                Database = {DatabaseName};
                                MultipleActiveResultSets = true;
                                Trusted_Connection = yes";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlConnection CheckConnection(SqlConnection connection)
        {
            connection = GetConnection();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: " + ex.Message, "Error", MessageBoxButton.OK);
            }
            return connection;
        }
    }
}
