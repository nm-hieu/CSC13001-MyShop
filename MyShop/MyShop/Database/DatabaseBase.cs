using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;

namespace MyShop.Database
{
    public class DatabaseBase
    {
        public string Server, Database;
        public string ConnectionString { get; set; } = "";
        private SqlConnection _connection = null;
        private static DatabaseBase? _instance = null;

        public SqlConnection? Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(ConnectionString); ;
                    _connection.Open();
                }

                return _connection;
            }
        }
        public static DatabaseBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseBase();
                }
                return _instance;
            }
        }
        public bool ConnectToServer(string sv, string db)
        {
            Server = sv; Database = db;
            if (string.IsNullOrWhiteSpace(sv)|| string.IsNullOrWhiteSpace(db))
                return false;

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = sv;
            builder.InitialCatalog = db;
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            ConnectionString = builder.ConnectionString;

            try
            {
                var connection = Connection;
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
