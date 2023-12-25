using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyShop.MVVM.ViewModel
{
    class OrdersViewModel
    {
        public BindingList<Product> products = new BindingList<Product>();
        public BindingList<Orders> orders = new BindingList<Orders>();
        public BindingList<PageInfo> pageNumber = new BindingList<PageInfo>();

        public int totalItem = 0;
        public int totalPage = 0;
        public int currentPage = 1;
        public int pageRow = 5;



        private void GetProduct()
        {
            var commandString = "select * from Products";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];
                int price = (int)reader["Price"];
                int category = (int)reader["Category"];
                string image = (string)reader["Image"];
                string color = (string)reader["Color"];
                int AvailableQuantity = 0;
                double MarkUpPercent = 0;

                if (reader["AvailableQuantity"] != DBNull.Value)
                {
                    AvailableQuantity = (int)reader["AvailableQuantity"];
                }

                if (reader["MarkUpPercent"] != DBNull.Value)
                {
                    MarkUpPercent = (double)reader["MarkUpPercent"];
                }

                int MarkupPrice = (int)Math.Round(price * (1 + MarkUpPercent));

                var product = new Product()
                {
                    ID = id,
                    Name = name.Replace("\n", "").Trim(),
                    Price = price,
                    Image = image,
                    Color = color,
                    Category = category,
                    AvailableQuantity = AvailableQuantity,
                    MarkUpPercent = MarkUpPercent,
                    MarkUpPrice = MarkupPrice,
                };

                products.Add(product);
            }
            reader.Close();
        }

        private List<Product> GetOrderItem(int orderID)
        {
            var commandString = @$"
                    select ProductID
                    from Orders left join OrderDetails on Orders.ID = OrderDetails.OrderID
                    where Orders.ID = {orderID}";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();
            List<Product> list = new List<Product>();
            List<Product> temp = products.ToList();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    int itemID = reader.GetInt32(0);
                    list.Add(temp.Find(item => item.ID == itemID));
                }
            }
            reader.Close();

            return list;
        }

        private int GetTotalPrice(List<Product> list)
        {
            int total = 0;
            foreach(Product product in list)
            {
                total += product.Price;
            }

            return total;
        }




        public void GetOrders()
        {
            products.Clear();
            orders.Clear();

            GetProduct();
            var commandString = @$"
                select *, count(*) over()
                from Orders
                order by ID
                offset {(currentPage - 1) * pageRow} rows fetch next {pageRow} rows only";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int orderID = reader.GetInt32(0);
                DateTime date = reader.GetDateTime(1);
                
                var order = new Orders()
                {
                    ID = orderID,
                    Date = date
                };
                orders.Add(order);
                totalItem = reader.GetInt32(2);
            }
            reader.Close();

            if (totalItem % pageRow != 0)
            {
                totalPage = totalItem / pageRow + 1;
            }
            else
            {
                totalPage = totalItem / pageRow;
            }


            pageNumber.Clear();
            for (int i = 1; i <= totalPage; i++)
            {
                pageNumber.Add(new PageInfo()
                {
                    Page = i,
                    Total = totalPage
                });
            }


            foreach (Orders order in orders)
            {
                order.Products = GetOrderItem(order.ID);
                order.TotalPrice = GetTotalPrice(order.Products);
            }
        }

        public void AddToOrder(Orders order, Product product)
        {
            Orders temp = orders.SingleOrDefault(o => o.ID == order.ID);
            if (temp != null)
            {
                temp.Products.Add(product);
                temp.TotalPrice += product.Price;

                int ordinalNumb = -1;
                var selectString = @$"
                    select max(OrdinalNumber)
                    from OrderDetails
                    where OrderID = {temp.ID}";
                var commandReader = new SqlCommand(selectString, DB.Instance.Connection);
                var reader = commandReader.ExecuteReader();

                while (reader.Read())
                {
                    ordinalNumb = reader.GetInt32(0) + 1;
                }
                reader.Close();

                if (ordinalNumb > 0)
                {
                    var insertString = @$"
                        insert into OrderDetails
                        values (@STT, @OrderID, @ProductID)";
                    var insertCommand = new SqlCommand(insertString, DB.Instance.Connection);
                    insertCommand.Parameters.AddWithValue("@STT", ordinalNumb);
                    insertCommand.Parameters.AddWithValue("@OrderID", order.ID);
                    insertCommand.Parameters.AddWithValue("@ProductID", product.ID);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public void AddOrder()
        {
            int id = -1;
            var selectString = @$"
                    select max(ID)
                    from Orders";
            var commandReader = new SqlCommand(selectString, DB.Instance.Connection);
            var reader = commandReader.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt32(0) + 1;
            }
            reader.Close();

            if (id > 0)
            {
                var insertString = @$"
                            insert into Orders
                            values (@ID, @Date)";
                var insertCommand = new SqlCommand(insertString, DB.Instance.Connection);
                insertCommand.Parameters.AddWithValue("@ID", id);
                insertCommand.Parameters.AddWithValue("@Date", DateTime.Today);

                insertCommand.ExecuteNonQuery();
            }

            GetOrders();
        }

        public void DeleteOrder(Orders order)
        {
            var deleteString = @$"
                            delete from OrderDetails
                            where OrderID = @ID
                            delete from Orders
                            where ID = @ID";
            var deleteCommand = new SqlCommand(deleteString, DB.Instance.Connection);
            deleteCommand.Parameters.AddWithValue("@ID", order.ID);

            deleteCommand.ExecuteNonQuery();

            GetOrders();
        }

        public void GetOrderWithDateFilter(DateTime from, DateTime to)
        {
            GetOrders();
            string dateFrom = from.ToString("yyyy-MM-dd");
            string dateTo = to.ToString("yyyy-MM-dd");
            List<Orders> orderCopied = orders.ToList();
            BindingList<Orders> orderTempList = new BindingList<Orders>();
            currentPage = 1;

            var commandString = @$"
                select *, count(*) over()
                from Orders
                where Date >= '{dateFrom}' and Date <= '{dateTo}'
                order by ID
                offset {(currentPage - 1) * pageRow} rows fetch next {pageRow} rows only";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    orderTempList.Add(orderCopied.Find(o => o.ID == reader.GetInt32(0)));
                    totalItem = reader.GetInt32(2);
                }
            }
            reader.Close();

            if (totalItem % pageRow != 0)
            {
                totalPage = totalItem / pageRow + 1;
            }
            else
            {
                totalPage = totalItem / pageRow;
            }

            orders.Clear();
            orders = orderTempList;
        }

        public void getOrderByMonth(int month)
        {
            products.Clear();
            orders.Clear();

            GetProduct();
            var commandString = @$"
                select *
                from Orders
                where month(Date) = {month}
                order by ID";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int orderID = reader.GetInt32(0);
                DateTime date = reader.GetDateTime(1);

                var order = new Orders()
                {
                    ID = orderID,
                    Date = date
                };
                orders.Add(order);
            }
            reader.Close();

            foreach (Orders order in orders)
            {
                order.Products = GetOrderItem(order.ID);
                order.TotalPrice = GetTotalPrice(order.Products);
            }
        }
        public void getOrderByDate(int month, int day)
        {
            products.Clear();
            orders.Clear();

            GetProduct();
            var commandString = @$"
                select *
                from Orders
                where month(Date) = {month} and day(Date) = {day}
                order by ID";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int orderID = reader.GetInt32(0);
                DateTime date = reader.GetDateTime(1);

                var order = new Orders()
                {
                    ID = orderID,
                    Date = date
                };
                orders.Add(order);
            }
            reader.Close();

            foreach (Orders order in orders)
            {
                order.Products = GetOrderItem(order.ID);
                order.TotalPrice = GetTotalPrice(order.Products);
            }
        }
    }
}
