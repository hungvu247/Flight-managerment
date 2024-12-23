using FlightManagement;
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
using Team2_SE1824_FlightManager;

namespace AssigmentPRN
{
    /// <summary>
    /// Interaction logic for StaffHome.xaml
    /// </summary>
    public partial class StaffHome : Window
    {
        public StaffHome()
        {
            InitializeComponent();
        }              
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PassengerManagement passengerManagement = new PassengerManagement();
            passengerManagement.ShowDialog();
        }

        
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            BaggageWindow baggageWindow = new BaggageWindow();
            baggageWindow.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.ShowDialog();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            ReportChartFlightWindow reportChartFlightWindow = new ReportChartFlightWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
