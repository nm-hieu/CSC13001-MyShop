using MyShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.ViewModel
{
    internal class MainViewModel: Observable
    {
        private object _currentView;
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand AnalyticsViewCommand { get; set; }  

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public SignInViewModel SignInVM { get; set; }
        public AnalyticsViewModel AnalyticsVM { get; set; }
        public object CurrentView
        {
            get { return _currentView;}
            set { _currentView = value;}
        }

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            ProductsVM = new ProductsViewModel();
            SignInVM = new SignInViewModel();
            AnalyticsVM = new AnalyticsViewModel();
            CurrentView = HomeVM;
            

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ProductsViewCommand= new RelayCommand(o =>
            {
                CurrentView = ProductsVM;
            });
            AnalyticsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AnalyticsVM;
            });

        }
    }
}