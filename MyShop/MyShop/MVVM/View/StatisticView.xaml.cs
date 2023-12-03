using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
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
using MyShop.MVVM.ViewModel;

namespace MyShop.MVVM.View
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : UserControl
    {
        StatisticViewModel StatisticVM;

        public StatisticView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StatisticVM = new StatisticViewModel();
            DataContext = StatisticVM;
        }


        private void monthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatisticVM.getMonthStatistic((int)monthCombobox.SelectedValue);
        }

        private void monthButton_Click(object sender, RoutedEventArgs e)
        {
            monthCombobox.ItemsSource = StatisticVM.MonthList;
            monthCombobox.SelectedIndex = 0;
            monthCombobox.IsEnabled = true;

            monthButton.Visibility = Visibility.Hidden;
            monthButton.IsEnabled = false;

            yearButton.Visibility = Visibility.Visible;
            yearButton.IsEnabled = true;

        }

        private void yearButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticVM.getYearStatistic();
            monthCombobox.IsEnabled = false;
            yearButton.Visibility = Visibility.Hidden;
            yearButton.IsEnabled = false;
            monthButton.Visibility = Visibility.Visible;
            monthButton.IsEnabled = true;
        }
    }
}
