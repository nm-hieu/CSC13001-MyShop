using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Interaction logic for AnalyticsView.xaml
    /// </summary>'
    public partial class AnalyticsView : UserControl
    {
        public AnalyticsView()
        {
            InitializeComponent();
        }

        public BindingList<Product> _trendingWeekyProducts = new BindingList<Product>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectToDB();
        }
        public async void ConnectToDB()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "HUNGLEGION\\SQLSERVER";
            builder.InitialCatalog = "MyshopDB";
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

    }
}
