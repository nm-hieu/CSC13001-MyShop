using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Products
{
    internal class Product : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }

        public Product(int id, string name, string manufacturer, int price, string image)
        {
            Id = id;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            Image = image;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
