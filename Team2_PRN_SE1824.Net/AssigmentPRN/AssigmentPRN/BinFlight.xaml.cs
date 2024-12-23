using DataAccess.BussinessObjects;
using Services;
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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace AssigmentPRN
{
    /// <summary>
    /// Interaction logic for BinFlight.xaml
    /// </summary>
    public partial class BinFlight : Window
    {
        private readonly IFlightService flightService;
        private readonly IAirlineService airlineService;
        private readonly IAirportService airportService;


        private int airlineIdRepo = -1;
        private int departingAirportIDRepo = -1;
        private int arrivingAirportIDRepo = -1;
        public BinFlight()
        {
            InitializeComponent();
            flightService = new FlightService();
            airlineService = new AirlineService();
            airportService = new AirportService();
            loadFlights();
            loadComboBox();
        }

        private void loadComboBox()
        {
            cmbAirline.ItemsSource = airlineService.GetAirlines().ToList();
            List<Airline> airlines = new List<Airline>();
            airlines.Add(new Airline { Id = 0, Name = "Tất cả" });
            airlines.AddRange(airlineService.GetAirlines());
            cmbAirlineSelect.ItemsSource = airlines;

            cmbDepartingAirport.ItemsSource = airportService.GetAirports("no").ToList();
            cmbArrivingAirport.ItemsSource = airportService.GetAirports("no").ToList();
            List<Airport> airports = new List<Airport>();
            airports.Add(new Airport { Id = 0, Name = "Tất cả" });
            airports.AddRange(airportService.GetAirports("no"));
            cmbDepartingAirportSelect.ItemsSource = airports;
            cmbArrivingAirportSelect.ItemsSource = airports;
        }

        private void loadFlights()
        {
            FlightDataGrid.ItemsSource = null;

            var list = flightService.GetFlights().
                Where(s => s.Status == false).
                OrderByDescending(flight => flight.Id).
                Select(flight => new
                {
                    flight.Id,
                    Airline = (flight.Airline is not null) ? flight.Airline.Name : "",
                    AirlineCode = (flight.Airline is not null) ? flight.Airline.Code : "",
                    DepartingAirport = (flight.DepartingAirportNavigation is not null) ? flight.DepartingAirportNavigation.Name : "",
                    ArrivingAirport = (flight.ArrivingAirportNavigation is not null) ? flight.ArrivingAirportNavigation.Name : "",
                    flight.DepartingGate,
                    flight.ArrivingGate,
                    flight.DepartureTime,
                    flight.ArrivalTime
                }).ToList();
            FlightDataGrid.ItemsSource = list;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyWord = SearchTextBox.Text;

        }




        private void FlightDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = FlightDataGrid;
            if (dataGrid.ItemsSource != null && dataGrid.SelectedItem != null)
            {
                // Ensure the selected item index is valid
                if (dataGrid.SelectedIndex >= 0)
                {
                    DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                        if (cell != null && cell.Content is TextBlock textBlock)
                        {
                            string flightId = textBlock.Text;
                            if (!string.IsNullOrEmpty(flightId))
                            {
                                if (int.TryParse(flightId, out int parsedFlightId))
                                {
                                    Flight? flight = flightService.GetFlightByID(parsedFlightId);
                                    if (flight != null)
                                    {
                                        txtID.Text = flightId; // flightId là một chuỗi, đảm bảo rằng nó không null
                                        cmbAirline.SelectedValue = flight?.AirlineId; // Kiểm tra flight không null trước khi gọi AirlineId.ToString()
                                        cmbDepartingAirport.SelectedValue = flight?.DepartingAirport; // Kiểm tra DepartingAirport không null
                                        cmbArrivingAirport.SelectedValue = flight?.ArrivingAirport; // Kiểm tra ArrivingAirport không null
                                        txtDepartingGate.Text = flight?.DepartingGate; // Kiểm tra DepartingGate không null
                                        txtArrivingGate.Text = flight?.ArrivingGate; // Kiểm tra ArrivingGate không null
                                        dpDepartureDate.SelectedDate = flight?.DepartureTime; // Kiểm tra DepartureTime không null
                                        tpDepartureTime.SelectedTime = flight?.DepartureTime;

                                        dpArrivalDate.SelectedDate = flight?.ArrivalTime;
                                        tpArrivalTime.SelectedTime = flight?.ArrivalTime;
                                    }
                                }
                                else
                                {
                                    // Handle the case where the flight ID is not a valid integer
                                    MessageBox.Show("Invalid flight ID format.");
                                }
                            }
                        }
                    }
                }
            }
        }


        private void btn_ResetFilterClick(object sender, RoutedEventArgs e)
        {
            airlineIdRepo = -1;
            departingAirportIDRepo = -1;
            arrivingAirportIDRepo = -1;
            loadFlights();
            loadComboBox();
        }

        private void SearchTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void filter()
        {
            FlightDataGrid.ItemsSource = null;

            var list = flightService.GetFlights().Where(s => s.Status == false).
                OrderByDescending(flight => flight.Id).ToList();
            if (airlineIdRepo != -1)
            {
                if (airlineIdRepo == 0)
                {
                    list = list.Where(f => f.Id > 0).ToList();
                }
                else
                {
                    list = list.Where(f => f.AirlineId == airlineIdRepo).ToList();
                }
            }

            if (departingAirportIDRepo != -1)
            {
                if (departingAirportIDRepo == 0)
                {
                    list = list.Where(f => f.DepartingAirport > 0).ToList();
                }
                else
                {
                    list = list.Where(f => f.DepartingAirport == departingAirportIDRepo).ToList();
                }
            }

            if (arrivingAirportIDRepo != -1)
            {
                if (arrivingAirportIDRepo == 0)
                {
                    list = list.Where(f => f.ArrivingAirport > 0).ToList();
                }
                else
                {
                    list = list.Where(f => f.ArrivingAirport == arrivingAirportIDRepo).ToList();
                }
            }
            FlightDataGrid.ItemsSource = list.Select(flight => new
            {
                flight.Id,
                Airline = (flight.Airline is not null) ? flight.Airline.Name : "",
                AirlineCode = (flight.Airline is not null) ? flight.Airline.Code : "",
                DepartingAirport = (flight.DepartingAirportNavigation is not null) ? flight.DepartingAirportNavigation.Name : "",
                ArrivingAirport = (flight.ArrivingAirportNavigation is not null) ? flight.ArrivingAirportNavigation.Name : "",
                flight.DepartingGate,
                flight.ArrivingGate,
                flight.DepartureTime,
                flight.ArrivalTime
            }).ToList();
        }

        private void cmbAirlineSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string airlineId = cmbAirlineSelect.SelectedValue?.ToString();
            if (airlineId is not null)
            {
                int airlineIdParse = Int32.Parse(airlineId);
                airlineIdRepo = airlineIdParse;
            }
            filter();

        }

        private void cmbDepartingAirportSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string departingAirport = cmbDepartingAirportSelect.SelectedValue?.ToString();
            if (departingAirport is not null)
            {
                int departingAirportParse = Int32.Parse(departingAirport);
                departingAirportIDRepo = departingAirportParse;
            }
            filter();
        }

        private void cmbArrivingAirportSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string arrivingAirport = cmbArrivingAirportSelect.SelectedValue?.ToString();
            if (arrivingAirport is not null)
            {
                int arrivingAirportParse = Int32.Parse(arrivingAirport);
                arrivingAirportIDRepo = arrivingAirportParse;

            }
            filter();
        }

        private void btn_UndoDelete(object sender, RoutedEventArgs e)
        {
            string id = txtID.Text;
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    int idParse = Int32.Parse(id);
                    Flight flight = flightService.GetFlightByID(idParse);
                    if (flight != null)
                    {
                        // Hiển thị hộp thoại xác nhận
                        MessageBoxResult result = MessageBox.Show(
                            "Are you sure you want to undo delete this flight?",
                            "Confirm Undo Deletion",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question
                        );

                        // Kiểm tra kết quả của hộp thoại xác nhận
                        if (result == MessageBoxResult.Yes)
                        {
                            flight.Status = true;
                            flightService.UpdateFlight(flight);
                            MessageBox.Show("Flight deleted successfully.");
                            loadFlights();
                        }
                        else
                        {
                            MessageBox.Show("Operation cancelled.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Flight not found.");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid ID format. Please enter a valid number.");
                }
            }
            else
            {
                MessageBox.Show("Please choose a flight!");
            }
        }

        private void btn_UndoDeleteAll(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                           "Are you sure you want to undo delete all flight?",
                           "Confirm Deletion",
                           MessageBoxButton.YesNo,
                           MessageBoxImage.Question
                       );
            if (result == MessageBoxResult.Yes)
            {
                List<Flight> list = flightService.GetFlights().Where(s => s.Status == false).ToList();
                foreach (Flight flight in list)
                {
                    flight.Status = true;
                    flightService.UpdateFlight(flight);
                }
                MessageBox.Show("Flight deleted successfully.");
                loadFlights();
            }
            else
            {
                MessageBox.Show("Operation cancelled.");
            }
        }
    }
}
