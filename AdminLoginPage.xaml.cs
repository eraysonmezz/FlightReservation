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

namespace FlightReservation
{
    /// <summary>
    /// Interaction logic for AdminLoginPage.xaml
    /// </summary>
    public partial class AdminLoginPage : Window
    {
        public AdminLoginPage()
        {
            InitializeComponent();
            


        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "admin" && passwordBox.Password == "password")
            {
                this.Hide();
                AdminPage adminPage = new AdminPage();
                adminPage.ShowDialog();
                this.Close();
                
            }
            else
            {
                MessageBox.Show("Username or password is wrong.");
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow  = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();

        }
    }
}
