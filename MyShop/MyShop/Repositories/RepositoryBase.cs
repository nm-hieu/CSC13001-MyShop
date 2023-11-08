using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MyShop.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "HUNGLEGION\\SQLSERVER";
            builder.InitialCatalog = "Project1";
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            //_connectionString = "Server=(local); Database=MyShopDB; Integrated Security=true";
            _connectionString = builder.ConnectionString;

        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
