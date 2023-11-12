using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.MVVM.ViewModel
{
    class AnalyticsViewModel
    {
        private List<int> itemIds = new List<int>();
        private List<object> _weeklyProducts = new List<object>();
        public BindingList<Product> _trendingWeekyProducts = new BindingList<Product>();

        public AnalyticsViewModel()
        {
            ConnectToDB();
        }
        public async void ConnectToDB()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "HUNGLEGION\\SQLSERVER";
            builder.InitialCatalog = "MyShopDB";
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;


            string connectionString = builder.ConnectionString;

            var connection = new SqlConnection(connectionString);
            connection = await Task.Run(() => {
                var _connection = new SqlConnection(connectionString);
                try
                {
                    _connection.Open();
                }
                catch (Exception ex)
                {

                    _connection = null;
                }
                return _connection;
            });

            if (connection != null)
            {
                DB.Instance.ConnectionString = connectionString;
            }
            else
            {
                MessageBox.Show(
                    $"Cannot connect to DB"
                );
            }
        }
        private string sqlTrendingWeeklyProduct()
        {
            int week = -1;
            var sql = $@"SELECT TOP 5
                        od.productID,
                        SUM(od.OrdinalNumber) AS total_quantity
                        FROM ""Orders"" o
                        JOIN
                        ""OrderDetails"" od ON o.ID= od.OrderID
                        WHERE
                        o.Date >= DATEADD(WEEK, {week}, GETDATE())
                        GROUP BY
                        od.ProductID
                        ORDER BY
                        total_quantity DESC";
            return sql;
        }

        private string sqlTrendingYearlyProduct(int year = 1)
        {
            var sql = $@"SELECT TOP 5
                        od.ProductID,
                        SUM(od.OrdinalNumber) AS total_quantity
                        FROM ""Orders"" o
                        JOIN ""OrderDetails"" od ON o.ID = od.OrderID
                        WHERE
                        YEAR(o.Date) = YEAR(GETDATE())
                        GROUP BY
                        od.ProductID
                        ORDER BY
                        total_quantity DESC;";
            return sql;
        }

        private string sqlTrendingMonthlyProduct (int month = 1)
        {
            var sql = $@"SELECT TOP 5
                        od.ProductID,
                        SUM(od.OrdinalNumber) AS total_quantity
                        FROM ""Orders"" o
                        JOIN ""OrderDetails"" od ON o.ID = od.OrderID
                        WHERE
                        MONTH(o.Date) = MONTH(GETDATE()) 
                        AND YEAR(o.Date) = YEAR(GETDATE())
                        GROUP BY
                        od.ProductID
                        ORDER BY
                        total_quantity DESC;";
            return sql;
        }

        private string sqlTrendingWeeklyProductDetail()
        {
            string conditionString = string.Join(" or ", itemIds.Select(id => $"ID = {id}"));

            var sql = $"select * from Products where {conditionString} ";
            Debug.WriteLine( sql );
            return sql;
        }

        private void getTop5WeeklyProductID ()
        {
            var sql = sqlTrendingWeeklyProduct();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            _weeklyProducts.Clear();

            while (reader.Read())
            {
                int id = (int)reader["productID"];
                int quantity = (int)reader["total_quantity"];
                var item = new { id = id, quantity = quantity };
                _weeklyProducts.Add(item);
                itemIds.Add(id);
            }
            reader.Close();
        }

        private void getTop5WeeklyProductDetail()
        {
            var sql = sqlTrendingWeeklyProductDetail();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            _trendingWeekyProducts.Clear();

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
                int saledQuantity = 0;

                var matchingWeeklyProduct = _weeklyProducts
                    .OfType<dynamic>()
                    .FirstOrDefault(wp => wp.id == id);

                if (matchingWeeklyProduct != null)
                {
                    saledQuantity = matchingWeeklyProduct.quantity;
                }
                

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
                    saledQuantity = saledQuantity,
                };
                _trendingWeekyProducts.Add(product);
            }
            reader.Close();

            var sortedQuantityProducts = _trendingWeekyProducts.OrderByDescending(p => p.saledQuantity).ThenBy(p => p.ID).ToList();
            // Clear the existing items and add the sorted items back
            _trendingWeekyProducts.Clear();
            foreach (var product in sortedQuantityProducts)
            {
                _trendingWeekyProducts.Add(product);
            }
        }

        public void getWeeklyTrendingProducts()
        {
            getTop5WeeklyProductID();
            getTop5WeeklyProductDetail();
        }
    }
}
