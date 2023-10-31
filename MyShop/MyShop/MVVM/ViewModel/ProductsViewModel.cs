using Microsoft.Data.SqlClient;
using MyShop.Core;
using MyShop.MVVM.Model;
using MyShop.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.MVVM.ViewModel
{
    class ProductsViewModel
    {
        public BindingList<Product> _products { get; set; }
        public BindingList<Category> _categories { get; set; }
        public int _rowsPerPage = 5;
        public int _totalPages = -1;
        public int _totalItems = -1;
        public int _currentPage = 1;
        public ProductsViewModel() {
            ConnectToDB();
        }

        public void loadData()
        {
            LoadAllProducts();
            LoadCategories();
        }

        public async void ConnectToDB ()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "HUNGLEGION\\SQLSERVER";
            builder.InitialCatalog = "Project1";
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;


            string connectionString = builder.ConnectionString;

            var connection = new SqlConnection(connectionString);
            connection = await Task.Run(() => {
                var _connection = new SqlConnection(connectionString);
                try
                {
                    _connection.Open();
                }
                catch (Exception ex)
                {

                    _connection = null;
                }
                return _connection;
            });

            if (connection != null)
            {
                DB.Instance.ConnectionString = connectionString;
            }
            else
            {
                MessageBox.Show(
                    $"Cannot connect to DB"
                );
            }
        }
        private string selectTable(string tableName)
        {
            var sql = $"select *, count(*) over() as Total from {tableName} ";
            return sql;
        }

        public void LoadAllProducts()
        {
            string tableName = "Products";
            var sql = selectTable(tableName);

            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            _products = new BindingList<Product>();
            int count = -1;

            while (reader.Read())
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];
                int price = (int)reader["Price"];
                int category = (int)reader["Category"];
                string image = (string)reader["Image"];
                string color = (string)reader["Color"];

                var product = new Product()
                {
                    ID = id,
                    Name = name,
                    Price = price,
                    Image = image,
                    Color = color,
                    Category = category
                };
                _products.Add(product);
                _totalItems = (int)reader["Total"];
                count = (int)reader["Total"];
            }
            reader.Close();
            // connect to itemsSource here

        }

        public void LoadCategories()
        {
            string tableName = "Products";
            var sql = selectTable(tableName);
            _categories = new BindingList<Category>();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];

                var category = new Category()
                {
                    ID = id,
                    Name = name,
                };
                _categories.Add(category);
            }
            reader.Close();
        }
    }
}
