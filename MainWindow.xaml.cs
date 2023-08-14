using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace FlightReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
           

        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
            string email = emailTextBox.Text;
            string password = passwordBox.Password;
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE email = @email AND Passwords = @password", connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    command = new SqlCommand("SELECT firstname, lastname, customerId FROM Customers WHERE email = @email", connection);
                    command.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                       string name = reader.GetString(0);
                       string surname = reader.GetString(1);
                       int customerId = reader.GetInt32(2);
                        this.Hide();
                        Window1 dashboard = new Window1(name, surname, customerId);
                        dashboard.Show();
                        this.Close();
                    }
                   
                }
                else
                {
                    MessageBox.Show("E-mail or password is wrong.");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminLoginPage adminLoginPage= new AdminLoginPage();
            adminLoginPage.ShowDialog();
            this.Close();
        }

        private void emailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    }

