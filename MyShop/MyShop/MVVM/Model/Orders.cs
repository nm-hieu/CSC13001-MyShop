using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.Model
{
    class Orders : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; }



        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
