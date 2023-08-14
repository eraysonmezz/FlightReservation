using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FlightReservation
{
    public partial class UpdateFlight : Window
    {
        private DataRowView selectedFlight;

        public UpdateFlight(DataRowView flight)
        {
            InitializeComponent();
            selectedFlight = flight;

            FlightIDTextBox.Text = flight["FlightID"].ToString();
            DepartureDatePicker.SelectedDate = Convert.ToDateTime(flight["DepartureTime"]);
            DepartureCityTextBox.Text = flight["DepartureCity"].ToString();
            ArrivalCityTextBox.Text = flight["ArrivalCity"].ToString();
            PriceTextBox.Text = flight["Price"].ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime newDepartureTime = DepartureDatePicker.SelectedDate.GetValueOrDefault();
            string newDepartureCity = DepartureCityTextBox.Text;
            string newArrivalCity = ArrivalCityTextBox.Text;
            decimal newPrice = decimal.Parse(PriceTextBox.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Flights SET DepartureTime = @departureTime, DepartureCity = @departureCity, ArrivalCity = @arrivalCity, Price = @price WHERE FlightID = @flightId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@departureTime", newDepartureTime);
                command.Parameters.AddWithValue("@departureCity", newDepartureCity);
                command.Parameters.AddWithValue("@arrivalCity", newArrivalCity);
                command.Parameters.AddWithValue("@price", newPrice);
                command.Parameters.AddWithValue("@flightId", selectedFlight["FlightID"]);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Flight updated successfully.");
                    this.Hide();
                    AdminPage adminPage = new AdminPage();
                    adminPage.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Flight could not be updated.");
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminPage adminPage = new AdminPage();
            adminPage.ShowDialog();
            this.Close();
        }

        private void FlightIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
