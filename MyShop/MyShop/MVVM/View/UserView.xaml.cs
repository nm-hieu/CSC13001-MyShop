using MyShop.MVVM.Model;
using MyShop.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        UserViewModel userVM;
        public UserView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OnLoad);
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            userVM = new UserViewModel();
            userVM.loadUserData();
            this.DataContext = userVM;
        }
    }
}
