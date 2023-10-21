using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MyShop.MVVM.View;

namespace MyShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            var signInView = new SignInView();
            signInView.Show();
            signInView.IsVisibleChanged += (s, ev) =>
            {
                if (signInView.IsVisible == false && signInView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    signInView.Close();
                }
            };
        }
    }
}
