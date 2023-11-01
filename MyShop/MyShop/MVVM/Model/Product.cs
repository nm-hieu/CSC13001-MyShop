using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.Model
{
    public class Product : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public int Category { get; set; }
       
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public double MarkUpPercent { get; set; }
        public int MarkUpPrice { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
