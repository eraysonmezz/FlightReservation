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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private string name;
        private string surname;
        private int customerId;
        public Window1(string name, string surname, int customerId)
        {
            InitializeComponent();
            this.name = name;
            this.surname = surname;
            this.customerId = customerId;
            string connectionString =
               ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Flights", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    FlightsDataGrid.ItemsSource = dt.DefaultView;
                    foreach (DataRowView row in dt.DefaultView)
                    {
                        Button button = new Button();
                        button.Content = "Select";
                        button.Tag = row["FlightID"]; 
                        button.Click += SelectFlightButton_Click;
                        Grid.SetColumn(button, 2); 
                        Grid.SetRow(button, FlightsDataGrid.Items.IndexOf(row));

                    }
                }
                reader.Close();
            }

        }
        private void SelectFlightButton_Click(object sender, RoutedEventArgs e)
        {
            
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;
          
            int flightId = Convert.ToInt32(row["FlightID"]);
            DateTime departureDate = Convert.ToDateTime(row["DepartureTime"]);
            string departureCity = row["DepartureCity"].ToString();
            string arrivalCity = row["ArrivalCity"].ToString();
            decimal price = Convert.ToDecimal(row["Price"]);
            

            
            ReservationWindow reservationForm = new ReservationWindow(name, surname, flightId, departureDate, departureCity,arrivalCity, price, customerId);
            reservationForm.Show();
            this.Close();
        }

    

       

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
        }

        
    }
}
