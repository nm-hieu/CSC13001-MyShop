using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShop.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        BindingList<Product> _products;
        BindingList<Category> _categories;
        int _rowsPerPage = 5;
        int _totalPages = -1;
        int _totalItems = -1;
        int _currentPage = 1;
        public ProductsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private string selectTable (string tableName)
        {
            var sql = $"select *, count(*) over() as Total from {tableName} ";
            return sql;
        }

        private string addWhere (string baseSQL, string param)
        {
            string sql = baseSQL;
            sql += "where ";
            return sql;
        }

        private void LoadCategories()
        {
            string tableName = "Products";
            var sql = selectTable(tableName);

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

        private void LoadAllProducts()
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
            productsView.ItemsSource = _products;
            // connect to itemsSource here

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            loading.IsIndeterminate = true;
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

            loading.IsIndeterminate = false;

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
            LoadCategories();
            LoadAllProducts();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
