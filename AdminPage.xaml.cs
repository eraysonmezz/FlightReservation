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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {

        public AdminPage()
        {
            InitializeComponent();

            RefreshFlights();
            string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
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
                }
                reader.Close();
            }
            FlightsDataGrid.SelectionMode = DataGridSelectionMode.Single;
        }

        private void AddFlightButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddFlightWindow addflightwindow = new AddFlightWindow();
            addflightwindow.ShowDialog();
            RefreshFlights();
            this.Close();
        }


        private void DeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;

            if (row != null)
            {
                int flightId = Convert.ToInt32(row["FlightID"]);

                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this flight?", "Delete Flight", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("DELETE FROM Flights WHERE FlightID = @flightId", connection);
                        command.Parameters.AddWithValue("@flightId", flightId);
                        int rowsDeleted = command.ExecuteNonQuery();
                        if (rowsDeleted > 0)
                        {
                            MessageBox.Show("Flight deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Flight could not be deleted.");
                        }
                    }

                    RefreshFlights();
                }
            }
            else
            {
                MessageBox.Show("Please select a flight to delete.");
            }
        }



        private void RefreshFlights()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["FlightReservation.Properties.Settings.FlightReservationConnectionString"].ConnectionString;
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
                }
                reader.Close();
            }
        }
        private void FlightsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFlight = (sender as DataGrid).SelectedItem;

            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminLoginPage adminLoginPage = new AdminLoginPage();
            adminLoginPage.ShowDialog();
            this.Close();

        }
        private void UpdateFlight_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)FlightsDataGrid.SelectedItem;

            if (row != null)
            {
                this.Hide();
                UpdateFlight updateFlight = new UpdateFlight(row);
                updateFlight.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a flight to update.");
            }
        }
        }
}