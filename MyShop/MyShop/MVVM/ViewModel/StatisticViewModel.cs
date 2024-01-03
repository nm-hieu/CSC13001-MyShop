using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Data.SqlClient;
using MyShop.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.MVVM.ViewModel
{
    class StatisticViewModel : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels {  get; set; }
        public Func<double, string> Formatter {  get; set; }
        public BindingList<int> MonthList = new BindingList<int>();
        public BindingList<int> YearList = new BindingList<int>();
        public string AxisXTitle {  get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        public StatisticViewModel()
        {
            SeriesCollection = new SeriesCollection();
            Labels = new List<string>();

            getYearList();
            Formatter = value => value.ToString("N");
        }

        public void getYearStatistic(int year)
        {
            getMonthList(year);
            SeriesCollection.Clear();
            Labels.Clear();
            AxisXTitle = "Tháng";

            var commandString = @$"
                    select MONTH(Date), sum(p.Price)
                    from Orders o left join OrderDetails od on o.ID = od.OrderID
			                      left join Products p on p.ID = od.ProductID
                    where YEAR(Date) = {year}
                    group by MONTH(Date)";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            SeriesCollection.Add(new ColumnSeries
            {
                Title = year.ToString(),
                Values = new ChartValues<int>()
            }) ;

            while (reader.Read())
            {
                int month = 0, revenue = 0;
                if (!reader.IsDBNull(0))
                {
                    month = reader.GetInt32(0);
                    revenue = reader.GetInt32(1);
                }

                Labels.Add($"Tháng {month}");
                SeriesCollection[0].Values.Add(revenue);
            }
            reader.Close();
        }

        public void getMonthStatistic(int month, int year)
        {
            SeriesCollection.Clear();
            Labels.Clear();
            AxisXTitle = "Ngày";
            Labels = getLabelList(month).ConvertAll(i =>  i.ToString());

            SeriesCollection.Add(new ColumnSeries
            {
                Title = $"Tháng {month}",
                Values = new ChartValues<int>()
            });

            var commandString = @$"
                    select DAY(o.Date), sum(p.Price)
                    from Orders o left join OrderDetails od on o.ID = od.OrderID
			                      left join Products p on p.ID = od.ProductID
                    where MONTH(o.Date) = {month} and YEAR(o.Date) = {year}
                    group by DAY(o.Date)";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            int index = 1;
            while (reader.Read())
            {
                int day = reader.GetInt32(0);
                int revenue = reader.GetInt32(1);

                while(index < day)
                {
                    SeriesCollection[0].Values.Add(0);
                    index++;
                }
                SeriesCollection[0].Values.Add(revenue);
                index++;
            }
            while (index < Int32.Parse(Labels.Last()))
            {
                SeriesCollection[0].Values.Add(0);
                index++;
            }
            reader.Close();
        }

        List<int> getLabelList(int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return Enumerable.Range(1, 31).ToList();

                case 4:
                case 6:
                case 9:
                case 11:
                    return Enumerable.Range(1, 30).ToList();

                case 2:
                    return Enumerable.Range(1, 28).ToList();

                default:
                    return new List<int>();
            }
        }

        void getMonthList(int year)
        {
            MonthList.Clear();

            var commandString = @$"
                    select distinct MONTH(o.Date) as Month
                    from Orders o left join OrderDetails od on o.ID = od.OrderID
			                      left join Products p on p.ID = od.ProductID
                    where YEAR(Date) = {year}";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                int month = reader.GetInt32(0);

                MonthList.Add(month);
            }
            reader.Close();
        }

        void getYearList()
        {
            YearList.Clear();

            var commandString = @$"
                    select distinct YEAR(o.Date) as Year
                    from Orders o left join OrderDetails od on o.ID = od.OrderID
			                      left join Products p on p.ID = od.ProductID";
            var command = new SqlCommand(commandString, DB.Instance.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int year = reader.GetInt32(0);

                YearList.Add(year);
            }
            reader.Close();
        }
    }
}
