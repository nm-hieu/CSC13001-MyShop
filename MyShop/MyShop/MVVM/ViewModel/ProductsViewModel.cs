using Microsoft.Data.SqlClient;
using MyShop.Core;
using MyShop.MVVM.Model;
using MyShop.MVVM.View;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using static MyShop.MVVM.ViewModel.ProductsViewModel;

namespace MyShop.MVVM.ViewModel
{
    class ProductsViewModel
    {
        public BindingList<Product> _products = new BindingList<Product>();
        public BindingList<PageInfo> pageInfos = new BindingList<PageInfo>();
        public BindingList<Category> _categories { get; set; }
        public int _rowsPerPage = 5;
        public int _totalPages = -1;
        public int _totalItems = -1;
        public int _currentPage = 1;

        public int priceFrom = 0;
        public int priceTo = 0;
        public string searchQuery = "";
        bool desc = false;
        public class Order: INotifyPropertyChanged
        {
            public string value { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;
        }

        string orderOption = "ID";

        public BindingList<Order> orderOptions = new BindingList<Order>();

        public ProductsViewModel() {
            ConnectToDB();
            initOrder();
        }

        private void initOrder()
        {
            orderOptions.Add(new Order() { value = "ID" });
            orderOptions.Add(new Order() { value = "Name" });
            orderOptions.Add(new Order() { value = "Price" });
        }

        public void loadData()
        {
            LoadAllProducts();
            LoadCategories();
        }

        public async void ConnectToDB ()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "HUNGLEGION\\SQLSERVER";
            builder.InitialCatalog = "Project1";
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
        private string selectTable(string tableName)
        {
            var sql = $"select *, count(*) over() as Total from {tableName} ";
            return sql;
        }

        private string addPaging ()
        {
            string orderValue = "DESC";
            if (!desc)
            {
                orderValue = "ASC";
            }

            if (orderOption != "Name")
            {
            return $"\r\n order by {orderOption} {orderValue} offset @Skip rows fetch next @Take rows only";
            }
            else
            {
                return $"\r\n order by CAST(Name AS NVARCHAR(max)) {orderValue} offset @Skip rows fetch next @Take rows only";
            }
        }

        private string addPrice()
        {
            if (priceFrom >= 0 && priceTo >= 0 && priceFrom < priceTo)
            {
                return "\r\n WHERE Price >= @PriceFrom AND Price <= @PriceTo ";
            }
            return "";
        }

        private string addNameQuery(bool isAddedWhere) 
        {
            if (searchQuery != "")
            {
                if (!isAddedWhere)
                {
                return "\r\n WHERE Name like @Keyword ";
                }
                else
                {
                    return " AND Name like @Keyword ";
                }
            }
            return "";
        }


        public void LoadAllProducts()
        {
            string tableName = "Products";
            string sql = selectTable(tableName);
            bool isAddedWhere = false;
            
            if (addPrice() != "")
            {
                isAddedWhere = true;
                sql += addPrice();
            }

            if (addNameQuery(isAddedWhere)!="")
            {
                sql += addNameQuery(isAddedWhere);
                if(!isAddedWhere)
                {
                    isAddedWhere = true;
                }
            }

            sql+= addPaging();
            
            // handle pagination
            var command = new SqlCommand(sql, DB.Instance.Connection);

            if (addPrice() != "")
            {
                command.Parameters.Add("@PriceFrom", SqlDbType.Int)
               .Value = priceFrom;
                command.Parameters.Add("@PriceTo", SqlDbType.Int)
                    .Value = priceTo;
            }

            if (addNameQuery(isAddedWhere) != "")
            {
                command.Parameters.Add("@Keyword", SqlDbType.Text)
               .Value = $"%{searchQuery}%";

            }

            command.Parameters.Add("@Skip", SqlDbType.Int)
                .Value = (_currentPage - 1) * _rowsPerPage;
            command.Parameters.Add("@Take", SqlDbType.Int)
                .Value = _rowsPerPage;
            

            //var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();
            _products.Clear();
            int count = -1;

            while (reader.Read())
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];
                int price = (int)reader["Price"];
                int category = (int)reader["Category"];
                string image = (string)reader["Image"];
                string color = (string)reader["Color"];
                int AvailableQuantity = (int)reader["AvailableQuantity"];
                double MarkUpPercent = (double)reader["MarkUpPercent"];
                int MarkupPrice = (int)Math.Round(price * (1 + MarkUpPercent));

                var product = new Product()
                {
                    ID = id,
                    Name = name,
                    Price = price,
                    Image = image,
                    Color = color,
                    Category = category,
                    AvailableQuantity = AvailableQuantity,
                    MarkUpPercent= MarkUpPercent,
                    MarkUpPrice= MarkupPrice,
                };
                _products.Add(product);
                _totalItems = (int)reader["Total"];
                count = (int)reader["Total"];
            }
            reader.Close();
            // connect to itemsSource here

                _totalItems = count;
                _totalPages = (_totalItems / _rowsPerPage) +
                    (((_totalItems % _rowsPerPage) == 0) ? 0 : 1);

                // Tạo thông tin phân trang cho combobox
                pageInfos.Clear();

                for (int i = 1; i <= _totalPages; i++)
                {
                    pageInfos.Add(new PageInfo()
                    {
                        Page = i,
                        Total = _totalPages
                    });
                };

        }

        public void LoadCategories()
        {
            string tableName = "Products";
            var sql = selectTable(tableName);
            _categories = new BindingList<Category>();
            var command = new SqlCommand(sql, DB.Instance.Connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["ID"];
                string name = (string)reader["Name"];

                var category = new Category()
                {
                    ID = id,
                    Name = name,
                };
                _categories.Add(category);
            }
            reader.Close();
        }

        private int getLastID(string tableName)
        {
            var sql = $"SELECT MAX(ID) AS LastID\r\nFROM {tableName}";
            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();
            int lastID = -1;
            while (reader.Read())
            {
                lastID = (int)reader["LastID"];
            }
            reader.Close();
            return lastID;
        }

        public List<Product> ExtractExcelProducts(string filePath)
        {
            List<Product> products= new List<Product>();
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming you want to read the first worksheet.

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    int ID = 0;
                    string Name = "";
                    int Price = 0;
                    string Image ="";
                    string Color = "";
                    int Category = 0;

                    for (int col = 1; col <= colCount; col++)
                    {
                        // Access cell value using worksheet.Cells[row, col].Text
                        string cellValue = worksheet.Cells[row, col].Text;

                        // case ID
                        
                        switch (col) { 
                            case 1:
                                ID = int.Parse(cellValue);
                                break;
                            case 2:
                                Name = cellValue;
                                break;
                            case 3:
                                Price = int.Parse(cellValue);
                                break;
                            case 4:
                                Color = cellValue;
                                break;
                            case 5:
                                Category = int.Parse(cellValue);
                                break;
                            case 6:
                                Image = cellValue;
                                break;
                            default:
                            break;
                        }
                        // Process or display the cell value as needed
                    }

                    var product = new Product()
                    {
                        ID = ID,
                        Name = Name,
                        Price = Price,
                        Image = Image,
                        Color = Color,
                        Category = Category,
                    };
                    products.Add(product);

                }
            }
            return products;
        }

        private int insertProduct(Product product)
        {
            string sql = @"INSERT INTO Products (ID, Name, Price, Category, Color, Image)
                VALUES (@ID, @Name, @Price, @Category, @Color, @Image);";

            SqlCommand insertCommand = new SqlCommand(sql, DB.Instance.Connection);

            insertCommand.Parameters.AddWithValue("@ID", product.ID);
            insertCommand.Parameters.AddWithValue("@Name", product.Name);
            insertCommand.Parameters.AddWithValue("@Price", product.Price);
            insertCommand.Parameters.AddWithValue("@Category", product.Category);
            insertCommand.Parameters.AddWithValue("@Color", product.Color);
            insertCommand.Parameters.AddWithValue("@Image", product.Image);

            int rowsAffected = insertCommand.ExecuteNonQuery();

            // < 0 is failed
            return rowsAffected;
        }

        private int insertProductsToDB (List<Product> products)
        {
            int updatedProducts = 0;
            foreach (Product item in products)
            {
                int rs = insertProduct(item);
                if(rs > 0)
                {
                    updatedProducts++;
                }
            }
            return updatedProducts;
        }

        public void importFromExcel (string filePath)
        {
            Console.WriteLine("products.Count");
            List<Product> products = ExtractExcelProducts(filePath);
            int updatedProducts = insertProductsToDB(products);
            MessageBox.Show($"Added {updatedProducts} products from Excel file {filePath}");
            LoadAllProducts();
            return;
        }

        public int goToPrevious(int selectedIndex)
        {
            if (selectedIndex > 0)
            {
                int rs = selectedIndex - 1;
                return rs;
            }
            return 0;
        }

        public int goToNext(int selectedIndex)
        {
            if (selectedIndex < _totalPages)
            {
                int rs = selectedIndex + 1;
                return rs;
            }
            return 0;
        }

        public void pagingHandle(dynamic selected)
        {
            if (selected != null)
            {
                if (selected?.Page != _currentPage)
                {
                    _currentPage = selected?.Page;
                    LoadAllProducts();
                }
            }
        }

        public void ItemPerPageHandle(int selected, int _priceFrom, int _priceTo, string search, int orderOps)
        {
            desc = !desc;
            orderOption =  orderOptions[orderOps].value;
            _rowsPerPage = selected;
            _currentPage = 1;
            priceFrom = _priceFrom;
            priceTo= _priceTo;
            searchQuery=search;
            LoadAllProducts();
        }

    }
}
