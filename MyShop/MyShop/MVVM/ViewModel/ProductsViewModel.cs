using Microsoft.Data.SqlClient;
using MyShop.Core;
using MyShop.MVVM.Model;
using MyShop.MVVM.View;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyShop.MVVM.ViewModel
{
    class ProductsViewModel
    {
        public BindingList<Product> _products = new BindingList<Product>();
        public BindingList<Category> _categories { get; set; }
        public int _rowsPerPage = 5;
        public int _totalPages = -1;
        public int _totalItems = -1;
        public int _currentPage = 1;
        public ProductsViewModel() {
            ConnectToDB();
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

        public void LoadAllProducts()
        {
            string tableName = "Products";
            var sql = selectTable(tableName);

            var command = new SqlCommand(sql, DB.Instance.Connection);

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

                var product = new Product()
                {
                    ID = id,
                    Name = name,
                    Price = price,
                    Image = image,
                    Color = color,
                    Category = category
                };
                _products.Add(product);
                _totalItems = (int)reader["Total"];
                count = (int)reader["Total"];
            }
            reader.Close();
            // connect to itemsSource here

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
    }
}
