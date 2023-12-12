using Microsoft.Data.SqlClient;
using MyShop.Core;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.ViewModel
{
    class HomeViewModel
    {
        int availableProductsQuantity = 0;
        int newWeekOrders = 0;
        int newMonthOtders = 0;

        public BindingList<NumberCell> _numbercells;


        public void loadData()
        {
            getProducts();
            getOrdersInWeek();
            getOrdersInMonth();
            updateCells();
        }

        private string selectTable(string tableName)
        {
            var sql = $"select *, count(*) over() as Total from {tableName}";
            return sql;
        }

        public void updateCells()
        {
            _numbercells = new BindingList<NumberCell>()
            {
                new NumberCell()
                {
                    content= "Total Available Products",
                    total = availableProductsQuantity,
                },
                new NumberCell()
                {
                    content= "Orders By Week",
                    total= newWeekOrders,
                },
                new NumberCell()
                {
                    content= "Orders By Month",
                    total=newMonthOtders,
                },
            };
        }

        public void getProducts()
        {
            string tableName = "Products";
            string sql = selectTable(tableName);
            sql += " where AvailableQuantity > 0";

            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                availableProductsQuantity = (int)reader["Total"];
            }
            reader.Close();
        }

        public void getOrdersInWeek()
        {
            string tableName = "Orders";
            string sql= selectTable(tableName);
            sql += " where Date >= DATEADD(WEEK, -1, GETDATE())";

            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                newWeekOrders = (int)reader["Total"];
            }
            reader.Close();
        }

        public void getOrdersInMonth()
        {
            string tableName = "Orders";
            string sql = selectTable(tableName);
            sql += " where MONTH(Date) = MONTH(GETDATE()) \r\n AND YEAR(Date) = YEAR(GETDATE())";

            var command = new SqlCommand(sql, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                newMonthOtders = (int)reader["Total"];
            }
            reader.Close();
        }

        public HomeViewModel() { }
    }
}
