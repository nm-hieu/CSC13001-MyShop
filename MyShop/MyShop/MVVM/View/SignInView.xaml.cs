using MyShop.MVVM.ViewModel;
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
using System.Configuration;
using MyShop.Database;

namespace MyShop.MVVM.View
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        Encrypt security = new Encrypt();
        public SignInView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OnLoad);
            this.DataContext = new SignInViewModel();
        }
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            // Load for Server & Database
            Server.Text = ConfigurationManager.AppSettings["Server"];
            Database.Text = ConfigurationManager.AppSettings["Database"];
            if (Server.Text.Length != 0 && Database.Text.Length != 0)
            {
                RememberServer.IsChecked = true;
                ServerMessage.Text = "* Kết nối tới Server thành công";
            }

            // Load for Username & Password
            var EncryptedPassword = ConfigurationManager.AppSettings["Password"];
            if (EncryptedPassword.Length != 0)
            {
                Username.Text = ConfigurationManager.AppSettings["Username"];
                Password.Password = security.Unprotect(EncryptedPassword);
                RememberMe.IsChecked = true;
            }
        }
    }
}
