﻿using MyShop.Core;
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
        public RelayCommand UserViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ProductsViewModel ProductsVM { get; set; }
        public AnalyticsViewModel AnalyticsVM { get; set; }
        public UserViewModel UserVM { get; set; }
        public object CurrentView
        {
            get { return _currentView;}
            set { _currentView = value;}
        }

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            ProductsVM = new ProductsViewModel();
            AnalyticsVM = new AnalyticsViewModel();
            UserVM = new UserViewModel();
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
            UserViewCommand = new RelayCommand(o =>
            {
                CurrentView = UserVM;
            });
        }
    }
}