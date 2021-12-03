using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace SportClub.View
{
    /// <summary>
    /// Логика взаимодействия для QueryEdit.xaml
    /// </summary>
    public partial class QueryEdit : Window
    {
        public QueryEdit()
        {
            InitializeComponent();

        }

        private void DoSQL_Click(object sender, RoutedEventArgs e)
        {
            var k = new AdoConnectionDataGrid(InputBox.Text, ResultGrid);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ResultGrid.DataContext = null;
            InputBox.Text = string.Empty;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class AdoConnectionDataGrid
    {

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SportClubDbConnectionString"].ConnectionString;
        public AdoConnectionDataGrid(string sqlQuery, DataGrid grid = null)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(_connectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(sqlQuery, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                if (grid != null) grid.DataContext = dt.DefaultView;
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }


    public class AdoConnection
    {

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SportClubDbConnectionString"].ConnectionString;

        public string SqlQuery { get; set; }

        public AdoConnection(string sqlQuery)
        {
            if (!string.IsNullOrEmpty(sqlQuery))
            {
                SqlQuery = sqlQuery;
            }
        }

        public ObservableCollection<object> ResultCollection()
        {

            var collection = new ObservableCollection<object>();

            try
            {
                SqlConnection sqlconn = new SqlConnection(_connectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(SqlQuery, sqlconn);
                DataTable dt = new DataTable();

                sqlconn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return collection;
        }
    }
}
