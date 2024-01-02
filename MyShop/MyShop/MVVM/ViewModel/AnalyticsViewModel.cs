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
        private List<int> weeklyItemIds = new List<int>();
        private List<object> weeklyProducts = new List<object>();
        public BindingList<Product> _trendingWeekyProducts = new BindingList<Product>();

        private List<int> monthlyItemIds = new List<int>();
        private List<object> monthlyProducts = new List<object>();
        public BindingList<Product> _trendingMonthProducts = new BindingList<Product>();


        private List<int> yearlyItemIds = new List<int>();
        private List<object> yearlyProducts = new List<object>();
        public BindingList<Product> _trendingYearProducts = new BindingList<Product>();

        public AnalyticsViewModel()
        {
            //ConnectToDB();
        }
        /*
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
        */
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

        private string sqlTrendingYearlyProduct()
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

        private string sqlTrendingMonthlyProduct ()
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

        private string sqlTrendingProductDetail(List<int> weeklyItemIds)
        {
            string conditionString = "";
            if (weeklyItemIds.Count() != 0)
            {
            conditionString = string.Join(" or ", weeklyItemIds.Select(id => $"ID = {id}"));

            }
            if (conditionString != "")
            {
                var sql = $"select * from Products where {conditionString} ";
                Debug.WriteLine(sql);
                return sql;
            }

            return conditionString;
            
        }

        private void getTop5WeeklyProductID ()
        {
            var sql = sqlTrendingWeeklyProduct();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            weeklyProducts.Clear();

            while (reader.Read())
            {
                int id = (int)reader["productID"];
                int quantity = (int)reader["total_quantity"];
                var item = new { id = id, quantity = quantity };
                weeklyProducts.Add(item);
                weeklyItemIds.Add(id);
            }
            reader.Close();
        }

        private void getTop5WeeklyProductDetail()
        {
            var sql = sqlTrendingProductDetail(weeklyItemIds);

            if (sql == "")
            {
                return;
            }

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

                var matchingWeeklyProduct = weeklyProducts
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
            _trendingWeekyProducts.Clear();
            foreach (var product in sortedQuantityProducts)
            {
                _trendingWeekyProducts.Add(product);
            }
        }

        private void getTop5MonthlyProductID()
        {
            var sql = sqlTrendingMonthlyProduct();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            monthlyProducts.Clear();

            while (reader.Read())
            {
                int id = (int)reader["productID"];
                int quantity = (int)reader["total_quantity"];
                var item = new { id = id, quantity = quantity };
                monthlyProducts.Add(item);
                monthlyItemIds.Add(id);
            }
            reader.Close();
        }
        private void getTop5MonthlyProductDetail()
        {
            var sql = sqlTrendingProductDetail(monthlyItemIds);
            if (sql == "")
            {
                return;
            }
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            _trendingMonthProducts.Clear();

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

                var matchingMonthlyProduct = monthlyProducts
                    .OfType<dynamic>()
                    .FirstOrDefault(wp => wp.id == id);

                if (matchingMonthlyProduct != null)
                {
                    saledQuantity = matchingMonthlyProduct.quantity;
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
                _trendingMonthProducts.Add(product);
            }
            reader.Close();

            var sortedQuantityProducts = _trendingMonthProducts.OrderByDescending(p => p.saledQuantity).ThenBy(p => p.ID).ToList();
            _trendingMonthProducts.Clear();
            foreach (var product in sortedQuantityProducts)
            {
                _trendingMonthProducts.Add(product);
            }
        }


        private void getTop5YearlyProductID()
        {
            var sql = sqlTrendingYearlyProduct();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            yearlyProducts.Clear();

            while (reader.Read())
            {
                int id = (int)reader["productID"];
                int quantity = (int)reader["total_quantity"];
                var item = new { id = id, quantity = quantity };
                yearlyProducts.Add(item);
                yearlyItemIds.Add(id);
            }
            reader.Close();
        }
        private void getTop5YearlyProductDetail()
        {
            var sql = sqlTrendingProductDetail(yearlyItemIds);
            if (sql == "")
            {
                return;
            }
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            _trendingYearProducts.Clear();

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

                var matchingYearlyProduct = yearlyProducts
                    .OfType<dynamic>()
                    .FirstOrDefault(wp => wp.id == id);

                if (matchingYearlyProduct != null)
                {
                    saledQuantity = matchingYearlyProduct.quantity;
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
                _trendingYearProducts.Add(product);
            }
            reader.Close();

            var sortedQuantityProducts = _trendingYearProducts.OrderByDescending(p => p.saledQuantity).ThenBy(p => p.ID).ToList();
            _trendingYearProducts.Clear();
            foreach (var product in sortedQuantityProducts)
            {
                _trendingYearProducts.Add(product);
            }
        }

        public void getWeeklyTrendingProducts()
        {
            getTop5WeeklyProductID();
            getTop5WeeklyProductDetail();
            getTop5MonthlyProductID();
            getTop5MonthlyProductDetail();
            getTop5YearlyProductID();
            getTop5YearlyProductDetail();
        }
    }
}
