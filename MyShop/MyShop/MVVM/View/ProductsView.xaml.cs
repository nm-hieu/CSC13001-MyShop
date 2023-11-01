using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using MyShop.MVVM.Model;
using MyShop.MVVM.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        ProductsViewModel ProductsVM;

        public ProductsView()
        {
            InitializeComponent();
        }

        class Row
        {
            public int value { get; set;}
        }

        class Price
        {
            public int value { get; set; }
        }

        class SearchName
        {
            public string value { get; set; } 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsVM = new ProductsViewModel();
            ProductsVM.loadData();
            productsView.ItemsSource = ProductsVM._products;
            pagingComboBox.ItemsSource = ProductsVM.pageInfos;
            pagingComboBox.SelectedIndex = ProductsVM._currentPage-1;
            RowPerPage.DataContext = new Row (){ value = ProductsVM._rowsPerPage };
            PriceFrom.DataContext = new Price() { value = ProductsVM.priceFrom };
            PriceTo.DataContext = new Price() { value = ProductsVM.priceTo };
            SearchTb.DataContext = new SearchName() { value = ProductsVM.searchQuery };
            orderComboBox.ItemsSource = ProductsVM.orderOptions;
            orderComboBox.SelectedIndex = 0;
            categorycb.ItemsSource=ProductsVM._categories; 
            categorycb.SelectedIndex = 0;
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                ProductsVM.importFromExcel(selectedFilePath);
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            pagingComboBox.SelectedIndex = ProductsVM.goToPrevious(pagingComboBox.SelectedIndex);
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {   
            pagingComboBox.SelectedIndex = ProductsVM.goToNext(pagingComboBox.SelectedIndex);
        }

        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic selected = pagingComboBox.SelectedItem;
            ProductsVM.pagingHandle(selected);
            pagingComboBox.ItemsSource = ProductsVM.pageInfos;
            pagingComboBox.SelectedIndex = ProductsVM._currentPage - 1;
        }

        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            if (RowPerPage.Text != "")
            {
                int selected = int.Parse(RowPerPage.Text);
                int priceFrom = int.Parse(PriceFrom.Text);
                int priceTo = int.Parse(PriceTo.Text);
                string seach = SearchTb.Text;
                int order = orderComboBox.SelectedIndex;
                ProductsVM.ItemPerPageHandle(selected, priceFrom, priceTo, seach, order);
            }
        }

        private void categorycb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = categorycb.SelectedIndex;
            ProductsVM.CategoryChangeHandle(selected);
        }
    }
}
