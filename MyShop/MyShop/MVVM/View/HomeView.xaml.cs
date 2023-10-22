using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        class NumberCell
        {
            public String content { get; set; }
            public int total { get; set; }
        }

        BindingList<Product> _oosProducts;
        BindingList<NumberCell> _numbercells;
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
            _numbercells = new BindingList<NumberCell>()
            {
                new NumberCell()
                {
                    content= "Products",
                    total=100
                },
                new NumberCell()
                {
                    content= "Orders By Week",
                    total=50
                },
                new NumberCell()
                {
                    content= "Orders By Month",
                    total=500
                },
            };
            OosProducts.ItemsSource = _oosProducts;
            NumberCellList.ItemsSource = _numbercells;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int cellsPerRow = 4;
            
            if (this.ActualWidth < 1200 && this.ActualWidth >= 900)
            {
                cellsPerRow = 3;
            }
            else if (this.ActualWidth >= 600 && this.ActualWidth < 900)
            {
                cellsPerRow = 2;
            }

            GridView gridView = new GridView();
            for (int i = 0; i < cellsPerRow; i++)
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Product " + (i + 1),
                    Width = 150, // Set the desired width for each cell
                    DisplayMemberBinding = new Binding("ProductName") // Adjust the binding path as needed
                });
            }
        }
    }
}
