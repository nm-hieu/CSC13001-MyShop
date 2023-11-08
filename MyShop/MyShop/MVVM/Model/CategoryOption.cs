using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.Model
{
    public class CategoryOption
    {
        public string value { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
