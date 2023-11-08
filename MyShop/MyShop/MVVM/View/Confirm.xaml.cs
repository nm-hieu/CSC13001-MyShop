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
using System.Windows.Shapes;

namespace MyShop.MVVM.View
{
    /// <summary>
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public bool isConfirm;
        public Confirm()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            isConfirm = true;
            DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            isConfirm = false;
            DialogResult = true;
        }
    }
}
