using MyShop.MVVM.Model;
using MyShop.MVVM.ViewModel;
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
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {
        OrdersViewModel ordersVM;

        public OrdersView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ordersVM = new OrdersViewModel();
            ordersVM.GetOrders();

            productsListView.ItemsSource = ordersVM.products;
            ordersListView.ItemsSource = ordersVM.orders;
            pagingComboBox.ItemsSource = ordersVM.pageNumber;
            pagingComboBox.SelectedIndex = ordersVM.currentPage - 1;
            pageRowComboBox.SelectedIndex = 0;
        }
        private void ordersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            receiptTitle.DataContext = ordersListView.SelectedItem;
            Orders selectedOrder = (Orders)ordersListView.SelectedItem;
            if (selectedOrder != null)
            {
                var temp = new BindingList<Product>(selectedOrder.Products);
                orderDetails.ItemsSource = temp;
            }

            
        }

        private void btnAddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (productsListView.SelectedItem != null && ordersListView.SelectedItem != null)
            {
                var product = (Product)productsListView.SelectedItem;
                var order = (Orders)ordersListView.SelectedItem;

                ordersVM.AddToOrder(order, product);
                orderDetails.Items.Refresh();
            }
            else if (productsListView.SelectedItem == null)
            {
                MessageBox.Show("Please select item", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (ordersListView.SelectedItem == null)
            {
                MessageBox.Show("Please select order", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            ordersVM.AddOrder();
            ordersListView.Items.Refresh();
        }

        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if(ordersListView.SelectedItems != null)
            {
                Orders order = (Orders)ordersListView.SelectedItem;
                orderDetails.ItemsSource = null;
                ordersVM.DeleteOrder(order);
            }
            ordersListView.Items.Refresh();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (ordersVM.currentPage > 1)
            {
                ordersVM.currentPage--;
                orderDetails.ItemsSource = null;
                ordersVM.GetOrders();
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (ordersVM.currentPage < ordersVM.totalPage)
            {
                ordersVM.currentPage++;
                orderDetails.ItemsSource = null;
                ordersVM.GetOrders();
            }
        }

        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PageInfo page = (PageInfo)pagingComboBox.SelectedItem;
            if (page != null)
            {
                ordersVM.currentPage = page.Page;
                orderDetails.ItemsSource = null;
                ordersVM.GetOrders();
            }
        }

        private void pageRowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)pageRowComboBox.SelectedItem;
            if (comboBoxItem != null)
            {
                int row = int.Parse(comboBoxItem.Content.ToString());
                ordersVM.pageRow = row;
                ordersVM.currentPage = 1;
                ordersListView.UnselectAll();
                orderDetails.ItemsSource = null;
                ordersVM.GetOrders();
            }
        }

        private void Button_Filter_Click(object sender, RoutedEventArgs e)
        {
            if (pickerDateFrom != null && pickerDateTo != null)
            {
                DateTime dateFrom = (DateTime)pickerDateFrom.SelectedDate;
                DateTime dateTo = (DateTime)pickerDateTo.SelectedDate;

                if (dateFrom <= dateTo)
                {
                    orderDetails.ItemsSource = null;
                    ordersVM.GetOrderWithDateFilter(dateFrom, dateTo);
                    ordersListView.ItemsSource = ordersVM.orders;
                }
                else
                {
                    MessageBox.Show("Invalid date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
