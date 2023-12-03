using Microsoft.Data.SqlClient;
using MyShop.Core;
using MyShop.Database;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.MVVM.ViewModel
{
    internal class MainViewModel: Observable
    {
        private object _currentView;
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand UserViewCommand { get; set; }
        public RelayCommand AnalyticsViewCommand { get; set; }  
        public RelayCommand OrdersViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public UserViewModel UserVM { get; set; }
        public AnalyticsViewModel AnalyticsVM { get; set; }
        public OrdersViewModel OrdersVM { get; set; }
        public object CurrentView
        {
            get { return _currentView;}
            set { _currentView = value;}
        }

        private void ConnectToServer()
        {
            string sv = ConfigurationManager.AppSettings["CurrentServer"];
            string db = ConfigurationManager.AppSettings["CurrentDatabase"];

            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = sv;
            builder.InitialCatalog = db;
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;

            string connectionString = builder.ConnectionString;

            var connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                connection = null;
            }

            if (connection != null)
            {
                DB.Instance.ConnectionString = connectionString;
            }
            else
            {
                MessageBox.Show("Can't connect to DB");   
            }
        }

        public MainViewModel() 
        {
            ConnectToServer();
            HomeVM = new HomeViewModel();
            ProductsVM = new ProductsViewModel();
            UserVM = new UserViewModel();
            AnalyticsVM = new AnalyticsViewModel();
            OrdersVM = new OrdersViewModel();
            CurrentView = HomeVM;
            
            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ProductsViewCommand= new RelayCommand(o =>
            {
                CurrentView = ProductsVM;
            });
            UserViewCommand = new RelayCommand(o =>
            {
                CurrentView = UserVM;
            });
            AnalyticsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AnalyticsVM;
            });
            OrdersViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrdersVM;
            });
        }
    }
}