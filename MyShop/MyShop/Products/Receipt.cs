using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Products
{
    internal class Receipt : INotifyPropertyChanged
    {
        public int ID {  get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date {  get; set; }
        public List<Product> ReceiptItem { get; set; }

        public Receipt(int id, int totalPrice, DateTime date)
        {
            ID = id;
            TotalPrice = totalPrice;
            Date = date;
        }

        public void getReceiptItem(SqlConnection connection, List<Product> products)
        {
            var getReceiptItemcommand = $@"
                    select *
                    from Receipt left join ReceiptDetails on Receipt.ID = ReceiptDetails.ReceiptID
                    where Receipt.ID = {ID}";
            var readReceiptItem = new SqlCommand(getReceiptItemcommand, connection).ExecuteReader();

            ReceiptItem = new List<Product>();
            while (readReceiptItem.Read())
            {
                int itemID = readReceiptItem.GetInt32(4);
                ReceiptItem.Add(products.Find(item => item.Id == itemID));
            }
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
