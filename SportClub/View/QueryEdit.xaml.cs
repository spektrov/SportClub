using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Configuration;


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

        private string _connectionString = ConfigurationManager.ConnectionStrings["SportClubDbConnectionString"].ConnectionString;

        private void DoSQL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(_connectionString);
                sqlconn.Open();
                SqlDataAdapter oda = new SqlDataAdapter(InputBox.Text, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                ResultGrid.DataContext = dt.DefaultView;
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
