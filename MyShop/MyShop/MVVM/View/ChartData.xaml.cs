using LiveCharts;
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
using System.Windows.Shapes;

namespace MyShop.MVVM.View
{
    /// <summary>
    /// Interaction logic for ChartData.xaml
    /// </summary>
    public partial class ChartData : Window
    {
        OrdersViewModel ORdersVM;
        BindingList<Product> items;
        public int month;
        public int day;

        public ChartData(int month, int day)
        {
            InitializeComponent();

            this.month = month;
            this.day = day;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ORdersVM = new OrdersViewModel();
            List<Product> list = new List<Product>();

            if (day == -1)
            {
                ORdersVM.getOrderByMonth(month);
            }
            else
            {
                ORdersVM.getOrderByDate(month, day);
            }

            foreach (Orders order in ORdersVM.orders)
            {
                list = list.Concat(order.Products).ToList();
            }
            items = new BindingList<Product>(list);
            chartDetails.ItemsSource = items;
        }
    }
}
