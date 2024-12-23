using DataAccess.BussinessObjects;
using FlightManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team2_SE1824_FlightManager;

namespace AssigmentPRN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_ManageFlight(object sender, RoutedEventArgs e)
        {
            FlightManager flightManager = new FlightManager();
            flightManager.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManageAirportWindow manageAirportWindow = new ManageAirportWindow();
            manageAirportWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AirlineManagement airlineManagement = new AirlineManagement();
            airlineManagement.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PassengerManagement passengerManagement = new PassengerManagement();
            passengerManagement.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;


            BookingPlatformWindow bookingPlatformWindow = new BookingPlatformWindow()
            {
                WindowState = WindowState.Maximized,

            };
            bookingPlatformWindow.Show();


            bookingPlatformWindow.Closed += (s, args) =>
            {

                currentWindow.Show();
            };

            currentWindow.Hide();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;


            BaggageWindow baggageWindow = new BaggageWindow
            {
                WindowState = WindowState.Maximized,

            };
            baggageWindow.Show();
            baggageWindow.Closed += (s, args) =>
            {

                currentWindow.Show();
            };


            currentWindow.Hide();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.ShowDialog();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            ReportChartFlightWindow reportChartFlightWindow = new ReportChartFlightWindow();
            reportChartFlightWindow.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}