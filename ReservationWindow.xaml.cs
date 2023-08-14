using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightReservation
{
    /// <summary>
    /// Interaction logic for ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : Window
    {
        private string name;
        private string surname;
        private int flightId;
        private DateTime departureDate;
        private string departureCity;
        private string arrivalCity;
        private decimal price;
        private string seatNumber;
        private int customerId;

        public ReservationWindow(string name, string surname, int flightId, DateTime departureDate, string departureCity, string arrivalCity, decimal price, int customerId)
        {
            InitializeComponent();
            this.name = name;
            this.surname = surname;
            this.flightId = flightId;
            this.customerId = customerId;
            this.departureDate = departureDate;
            this.departureCity = departureCity;
            this.arrivalCity = arrivalCity;
            this.price = price;


            nameLabel.Content = name;
            surnameLabel.Content = surname;
            flightIdLabel.Content = flightId;
            departureDateLabel.Content = departureDate.ToString("dd/MM/yyyy");
            departureCityLabel.Content = departureCity;
            arrivalCityLabel.Content =  arrivalCity;
            priceLabel.Content = " $" + price.ToString();

            seatNumber = GenerateSeatNumber();
            seatNumberLabel.Content = seatNumber;
        }

       
        private string GenerateSeatNumber()
        {
            Random random = new Random();
            int seatNumber = random.Next(1, 101); 
            return "A" + seatNumber.ToString(); 
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your reservation has been confirmed.");
            string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Reservations (ReservationID, FlightID, CustomerID, SeatNumber) VALUES (@ReservationID, @FlightID, @CustomerID, @SeatNumber)", connection);
                command.Parameters.AddWithValue("@ReservationID", GenerateReservationId());
                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.Parameters.AddWithValue("@FlightID", flightId);
                command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                command.ExecuteNonQuery();
            }
            this.Hide();
            Window1 dashboard = new Window1(name,surname,customerId);
            dashboard.ShowDialog();
            this.Close();

        }
        private int GenerateReservationId()
        {
           
            Random random = new Random();
            int reservationId = random.Next(100000, 1000000); 
            return reservationId;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 dashboard = new Window1(name, surname,customerId);
            dashboard.ShowDialog();
            this.Close();
           
        }
    }
}
