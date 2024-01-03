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
        string currentChart = "year";

        public StatisticView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            StatisticVM = new StatisticViewModel();
            DataContext = StatisticVM;
            yearCombobox.ItemsSource = StatisticVM.YearList;
            monthCombobox.ItemsSource = StatisticVM.MonthList;
            yearButton_Click(sender, e);
        }

        private void yearCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (yearCombobox.Items.Count > 0)
            {
                int year = (int)yearCombobox.SelectedValue;
                StatisticVM.getYearStatistic(year);
            }
        }
        private void monthCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (monthCombobox.Items.Count > 0)
            {
                int year = (int)yearCombobox.SelectedValue;
                int month = (int)monthCombobox.SelectedValue;
                StatisticVM.getMonthStatistic(month, year);
            }
        }

        private void yearButton_Click(object sender, RoutedEventArgs e)
        {
            //yearCombobox.ItemsSource = StatisticVM.YearList;
            yearCombobox.SelectedIndex = 0;
            StatisticVM.getYearStatistic((int)yearCombobox.SelectedValue);
            yearCombobox.IsEnabled = true;
            monthCombobox.IsEnabled = false;
            
            yearButton.IsEnabled = false;
            monthButton.IsEnabled = true;
            
            monthButton.Visibility = Visibility.Visible;
            yearButton.Visibility = Visibility.Hidden;


            currentChart = "year";
        }
        private void monthButton_Click(object sender, RoutedEventArgs e)
        {
            //monthCombobox.ItemsSource = StatisticVM.MonthList;
            monthCombobox.SelectedIndex = 0;
            monthCombobox.IsEnabled = true;
            yearCombobox.IsEnabled = false;
            
            monthButton.IsEnabled = false;
            yearButton.IsEnabled = true;

            monthButton.Visibility = Visibility.Hidden;
            yearButton.Visibility = Visibility.Visible;

            currentChart = "month";
        }

        private void lvchart_DataClick(object sender, ChartPoint chartPoint)
        {
            int month= - 1;
            int day = - 1;
            if (String.Equals(currentChart, "month"))
            {
                month = (int)monthCombobox.SelectedValue;
                string getDate = xBar.Labels[(int)chartPoint.X];
                day = Int32.Parse(getDate);
            }
            else if (String.Equals(currentChart, "year"))
            {
                string getDate = xBar.Labels[(int)chartPoint.X];
                string[] temp = getDate.Split(' ');
                month = Int32.Parse(temp[1]);
            }

            var ChartDataWindow = new ChartData(month, day);
            ChartDataWindow.Show();
        }
    }
}
