using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.Model
{
    public class PageInfo: INotifyCollectionChanged
    {
        public int Page { get; set; }
        public int Total { get; set; }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}
