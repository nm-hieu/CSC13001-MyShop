using MyShop.Products;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace MyShop.Repositories
{
    internal class ProductRepositories : RepositoryBase
    {
        public ProductRepositories(string serverName, string databaseName) : base(serverName, databaseName) { }

        public List<Product> GetProduct()
        {
            SqlConnection connection = null;
            connection = CheckConnection(connection);

            var commandString = "select * from Product";
            var command = new SqlCommand(commandString, connection);
            var reader = command.ExecuteReader();
            var productList = new List<Product>();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string manufacturer = reader.GetString(2);
                int price = reader.GetInt32(3);
                string img = reader.GetString(4);

                productList.Add(new Product(id, name, manufacturer, price, img));
            }

            return productList;
        }

        public List<Receipt> GetReceipt()
        {
            SqlConnection connection = null;
            connection = CheckConnection(connection);

            var commandString = "select * from Receipt";
            var command = new SqlCommand(commandString, connection);
            var reader = command.ExecuteReader();
            var receiptList = new List<Receipt>();
            var productList = GetProduct();

            while (reader.Read())
            {
                int receiptID = reader.GetInt32(0);
                DateTime date = reader.GetDateTime(1);

                var totalCommand = $@"
                    select sum(Product.Price)
                    from Receipt join ReceiptDetails on Receipt.ID = ReceiptDetails.ReceiptID
			                     join Product on ReceiptDetails.ProductID = Product.ID
                    where Receipt.ID = {receiptID}
                    group by Receipt.ID";
                var readTotalPrice = new SqlCommand(totalCommand, connection).ExecuteReader();
                readTotalPrice.Read();
                int total = readTotalPrice.GetInt32(0);

                receiptList.Add(new Receipt(receiptID, total, date));
            }

            foreach (var receipt in receiptList)
            {
                receipt.getReceiptItem(connection, productList);
            }

            return receiptList;
        }
    }
}
