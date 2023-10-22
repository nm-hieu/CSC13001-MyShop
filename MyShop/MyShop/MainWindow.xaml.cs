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

namespace MyShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            sidebar.Visibility = Visibility.Collapsed;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            sidebar.Visibility = Visibility.Visible;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int cellsPerRow = 4;
            toggleButton.IsChecked = (false) ; // show sidebar

            if (this.ActualWidth < 1200 && this.ActualWidth >= 900)
            {
                toggleButton.IsChecked = (false); // show sidebar
                cellsPerRow = 3;
            } else if (this.ActualWidth >= 600 && this.ActualWidth < 900)
            {
                cellsPerRow = 2;
                toggleButton.IsChecked = (true); // hidden sidebar
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
