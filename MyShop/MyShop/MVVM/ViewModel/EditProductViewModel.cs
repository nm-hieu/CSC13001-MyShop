using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.ViewModel
{
    class EditProductVM
    {
        public BindingList<Category> categories;
        public Product product;
        public EditProductVM(BindingList<Category> passedCategories)
        {
            categories = new BindingList<Category>();
            foreach (var category in passedCategories)
            {
                categories.Add(new Category()
                {
                    ID = category.ID,
                    Name = category.Name,
                });
            }
        }

    }
}
