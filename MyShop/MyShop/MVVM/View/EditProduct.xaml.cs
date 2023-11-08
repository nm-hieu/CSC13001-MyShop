using MyShop.MVVM.Model;
using MyShop.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        EditProductVM editProductVM;
        public bool isSelectRemove;
        public Product EditedProduct {
            get
            {
                return editProductVM.product;
            }
        }

        public EditProduct(BindingList<Category> passedCategories, Product passedProduct)
        {
            InitializeComponent();
            editProductVM = new EditProductVM(passedCategories);
            editProductVM.product = (Product) passedProduct.Clone();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categorycb.ItemsSource = editProductVM.categories;
            categorycb.SelectedIndex = EditedProduct.Category -1;
            this.DataContext = EditedProduct;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var cfScreen = new Confirm();
            if (cfScreen.ShowDialog()!.Value == true)
            {
                if (cfScreen.isConfirm == true)
                {
                    editProductVM.product.Name = nametb.Text;
                    editProductVM.product.Color = colortb.Text;
                    editProductVM.product.Price = int.Parse(pricetb.Text.ToString());
                    editProductVM.product.AvailableQuantity = int.Parse(pricetb.Text.ToString());
                    editProductVM.product.MarkUpPercent = double.Parse(makeupPercent.Text.ToString());
                    editProductVM.product.Category = categorycb.SelectedIndex + 1;
                    DialogResult = true;
                }
            }
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var cfScreen = new Confirm();
            if (cfScreen.ShowDialog()!.Value == true)
            {
                isSelectRemove = true;
                DialogResult = true;
            }
                
        }
    }
}
