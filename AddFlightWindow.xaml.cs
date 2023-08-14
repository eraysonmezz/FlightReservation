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
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace FlightReservation
{
    /// <summary>
    /// Interaction logic for AddFlightWindow.xaml
    /// </summary>
    public partial class AddFlightWindow : Window
    {
        public AddFlightWindow()
        {
            InitializeComponent();

        }

        private void addFlightBtn_Click(object sender, RoutedEventArgs e)
        {


            string departureCity = departureCityTxt.Text;
            string arrivalCity = arrivalCityTxt.Text;
            DateTime departureTime = DateTime.Parse(departureTimeTxt.Text);
            DateTime arrivalTime = DateTime.Parse(arrivalTimeTxt.Text);
            string flightNumber = flightNumberTxt.Text;
            string flightID = flightIDTxt.Text;
            decimal price = decimal.Parse(priceTxt.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Flights (FlightID, FlightNumber, DepartureCity, ArrivalCity, DepartureTime, ArrivalTime, Price) VALUES (@flightID, @flightNumber, @departureCity, @arrivalCity, @departureTime, @arrivalTime, @price)", connection);
                command.Parameters.AddWithValue("@flightID", flightID);
                command.Parameters.AddWithValue("@flightNumber", flightNumber);
                command.Parameters.AddWithValue("@departureCity", departureCity);
                command.Parameters.AddWithValue("@arrivalCity", arrivalCity);
                command.Parameters.AddWithValue("@departureTime", departureTime);
                command.Parameters.AddWithValue("@arrivalTime", arrivalTime);
                command.Parameters.AddWithValue("@price", price);
                int rowsInserted = command.ExecuteNonQuery();
                if (rowsInserted > 0)
                {
                    MessageBox.Show("Flight added successfully.");
                }
                else
                {
                    MessageBox.Show("Flight could not be added.");
                }
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminPage adminPage = new AdminPage();
            adminPage.ShowDialog();
            this.Close();

        }

        private void flightIDTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
