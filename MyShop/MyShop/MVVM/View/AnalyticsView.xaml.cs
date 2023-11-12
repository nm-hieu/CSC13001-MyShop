using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;
using MyShop.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        AnalyticsViewModel AnalyticsViewModel;
        public AnalyticsView()
        {
            InitializeComponent();
            AnalyticsViewModel = new AnalyticsViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AnalyticsViewModel.getWeeklyTrendingProducts();
        }
    }
}
