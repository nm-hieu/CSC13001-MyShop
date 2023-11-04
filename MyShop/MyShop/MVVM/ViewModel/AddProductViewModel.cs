using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.ViewModel
{
    
    class AddProductVM
    {
        public Product product = new Product();
        public BindingList<Category> categories;
        public void addImage(string path)
        {
            product.Image = path;
        }
        public void insertData(string name, int price, string color, int quantity, double percent, string image)
        {
            product.Price = price;
            product.Color = color;
            product.Name = name;
            product.AvailableQuantity = quantity;
            product.MarkUpPercent = percent;
            product.Image = image;
        }

        public AddProductVM(BindingList<Category> passedCategories) {
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

        public void handleCategoryAdded(int index)
        {
            product.Category = categories[index].ID;
        }
    }
}
