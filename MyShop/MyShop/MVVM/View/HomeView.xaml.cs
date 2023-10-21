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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        class Product
        {
            public String productCode { get; set; }
            public String productName { get; set; }
            public String productCategory { get; set; }
            public float price { get; set; }
        }

        BindingList<Product> _oosProducts;

        private int totalProducts = 100; // Replace with your actual data
        private int totalOrdersByWeek = 50;     // Replace with your actual data
        private int totalOrdersByMonth = 500;     // Replace with your actual data
        public int TotalProducts
        {
            get { return totalProducts; }
            set { totalProducts = value; }
        }

        public int TotalOrdersByWeek
        {
            get { return totalOrdersByWeek; }
            set { totalOrdersByWeek = value; }
        }

        public int TotalOrdersByMonth
        {
            get { return totalOrdersByMonth; }
            set { totalOrdersByMonth = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _oosProducts = new BindingList<Product>()
            {
                new Product
                {
                    productCode= "001",
                    productCategory="Rice",
                    productName="ST25",
                    price=200000
                },
                new Product
                {
                    productCode= "002",
                    productCategory="Fruit",
                    productName="Unifarm Banana",
                    price=10000
                },
                new Product
                {
                    productCode= "003",
                    productCategory="Vegetable",
                    productName="Onion",
                    price=38000
                },
                new Product
                {
                    productCode= "004",
                    productCategory="Vegetable",
                    productName="Rocket Leaf",
                    price=154000
                },
                new Product
                {
                    productCode= "005",
                    productCategory="Vegetable",
                    productName="Thyme",
                    price=37500
                }
            };
            OosProducts.ItemsSource = _oosProducts;
            DataContext = this;
        }
    }
}
