using MyShop.Products;
using MyShop.Repositories;
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
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        BindingList<Product> products;
        BindingList<Receipt> receipts;
        ProductRepositories repository;

        string SERVER_NAME = "LAPTOP-9RV6F0G8\\SQLEXPRESS";
        string DATABASE_NAME = "MyShop";

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            repository = new ProductRepositories(SERVER_NAME, DATABASE_NAME);

            products = new BindingList<Product>(repository.GetProduct());
            phoneListView.ItemsSource = products;

            receipts = new BindingList<Receipt>(repository.GetReceipt());
            receiptListView.ItemsSource = receipts;
        }

        private void phoneListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           itemInformation.DataContext = phoneListView.SelectedItem;
        }

        private void receiptListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            receiptTitle.DataContext = receiptListView.SelectedItem;
            var selectedReceipt = receiptListView.SelectedItem as Receipt;


            var temp = new BindingList<Product>(selectedReceipt.ReceiptItem);
            receiptDetails.ItemsSource = temp;
        }

        private void btnAddToReceipt_Click(object sender, RoutedEventArgs e)
        {
            if(phoneListView.SelectedItem != null && receiptListView.SelectedItem != null)
            {
                var product = (Product) phoneListView.SelectedItem;
                var receipt = (Receipt) receiptListView.SelectedItem;

                receipt.ReceiptItem.Add(product);
                receipt.TotalPrice += product.Price;

                receiptDetails.Items.Refresh();
            }
            else if (phoneListView.SelectedItem == null)
            {
                MessageBox.Show("Please select item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (receiptListView.SelectedItem == null)
            {
                MessageBox.Show("Please select receipt", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddReceipt_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDeleteReceipt_Click(object sender, RoutedEventArgs e)
        {
            if (receiptListView.SelectedItem != null)
            {
                var receipt = (Receipt) receiptListView.SelectedItem;

                receipts.Remove(receipt);
            }
            else if (receiptListView.SelectedItem == null)
            {
                MessageBox.Show("Please select item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
