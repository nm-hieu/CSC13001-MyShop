using Microsoft.Win32;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        AddProductVM addProductVM;

        public Product AddedProduct { 
            get {
                return addProductVM.product;
            }
        }
        
        public AddProduct(BindingList<Category> passedCategories)
        {
            InitializeComponent();
            addProductVM = new AddProductVM(passedCategories);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = addProductVM.product;
            categorycb.ItemsSource=addProductVM.categories;
            categorycb.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = nametb.Text;
            string color = colortb.Text;
            int price = int.Parse(pricetb.Text);
            int quantity = int.Parse(quantitytb.Text);
            double percent = double.Parse(makeupPercent.Text);
            string image = imagetb.Text;
            addProductVM.insertData(name, price, color, quantity, percent, image);

            DialogResult = true;
        }

        private void categorycb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addProductVM.handleCategoryAdded(categorycb.SelectedIndex);
        }
    }
}
