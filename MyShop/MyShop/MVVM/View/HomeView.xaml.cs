using MyShop.MVVM.ViewModel;
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
        HomeViewModel homeViewModel;
        public HomeView()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            homeViewModel = new HomeViewModel();
            homeViewModel.loadData();
            NumberCellList.ItemsSource = homeViewModel._numbercells;
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
