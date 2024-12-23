using DataAccess.BussinessObjects;
using MaterialDesignThemes.Wpf;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace AssigmentPRN
{
    /// <summary>
    /// Interaction logic for FlightManager.xaml
    /// </summary>
    public partial class FlightManager : Window
    {
        private readonly IFlightService flightService;
        private readonly IAirlineService airlineService;
        private readonly IAirportService airportService;
        int currentPage = 1;
        int itemsPerPage = 15;
        private int airlineIdRepo = -1;
        private int departingAirportIDRepo = -1;
        private int arrivingAirportIDRepo = -1;
        public FlightManager()
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
            airlines.Add(new Airline { Id = 0, Name = "All" });
            airlines.AddRange(airlineService.GetAirlines());
            cmbAirlineSelect.ItemsSource = airlines;

            cmbDepartingAirport.ItemsSource = airportService.GetAirports("no").ToList();
            cmbArrivingAirport.ItemsSource = airportService.GetAirports("no").ToList();
            List<Airport> airports = new List<Airport>();
            airports.Add(new Airport { Id = 0, Name = "All" });
            airports.AddRange(airportService.GetAirports("no"));
            cmbDepartingAirportSelect.ItemsSource = airports;
            cmbArrivingAirportSelect.ItemsSource = airports;
        }

        private void loadFlights()
        {
            FlightDataGrid.ItemsSource = null;

            var flights = flightService.GetFlights()
                .Where(s => s.Status == true)
                .OrderByDescending(flight => flight.Id)
                .Select(flight => new
                {
                    flight.Id,
                    Airline = (flight.Airline != null) ? flight.Airline.Name : "",
                    AirlineCode = (flight.Airline != null) ? flight.Airline.Code : "",
                    DepartingAirport = (flight.DepartingAirportNavigation != null) ? flight.DepartingAirportNavigation.Name : "",
                    ArrivingAirport = (flight.ArrivingAirportNavigation != null) ? flight.ArrivingAirportNavigation.Name : "",
                    flight.DepartingGate,
                    flight.ArrivingGate,
                    flight.DepartureTime,
                    flight.ArrivalTime
                });

            var pagedList = flights
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            FlightDataGrid.ItemsSource = pagedList;

        }
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                filter();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            filter();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyWord = SearchTextBox.Text;

            FlightDataGrid.ItemsSource = null;

            var list = flightService.GetFlights()
                .Where(s => s.Status == true &&
                    ((s.Airline != null && s.Airline.Code.Contains(keyWord)) ||
                     (s.DepartingGate != null && s.DepartingGate.Contains(keyWord)) ||
                     (s.ArrivingGate != null && s.ArrivingGate.Contains(keyWord)) ||
                     (s.DepartingAirportNavigation != null && s.DepartingAirportNavigation.Name.Contains(keyWord)) ||
                     (s.ArrivingAirportNavigation != null && s.ArrivingAirportNavigation.Name.Contains(keyWord))))
                .OrderByDescending(flight => flight.Id).
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

        private string ValidateAndPopulateFlight(Flight flight)
        {
            string message = "";

            string? airline = cmbAirline.SelectedValue?.ToString();
            string? departingAirport = cmbDepartingAirport.SelectedValue?.ToString();
            string? arrivingAirport = cmbArrivingAirport.SelectedValue?.ToString();
            string? departingGate = txtDepartingGate.Text;
            string? arrivingGate = txtArrivingGate.Text;
            DateTime? arrivalDate = dpArrivalDate.SelectedDate;
            DateTime? arrivalTime = tpArrivalTime.SelectedTime;
            DateTime? departureDate = dpDepartureDate.SelectedDate;
            DateTime? departureTime = tpDepartureTime.SelectedTime;

            if (!string.IsNullOrEmpty(airline) && Int32.TryParse(airline, out int airlineId))
            {
                flight.AirlineId = airlineId;
            }
            else
            {
                message = "Choose airline!";
                return message;
            }
            if (!string.IsNullOrEmpty(departingAirport) && Int32.TryParse(departingAirport, out int departingAirportId))
            {
                flight.DepartingAirport = departingAirportId;
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "Choose departing airport!";
                return message;
            }
            if (!string.IsNullOrEmpty(arrivingAirport) && Int32.TryParse(arrivingAirport, out int arrivingAirportId))
            {
                flight.ArrivingAirport = arrivingAirportId;
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "Chooes arriving airport!";
                return message;
            }
            if (!string.IsNullOrEmpty(departingGate))
            {
                flight.DepartingGate = departingGate;
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "Choose departing gate!";
                return message;
            }
            if (!string.IsNullOrEmpty(arrivingGate))
            {
                flight.ArrivingGate = arrivingGate;
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "Choose arriving gate!";
                return message;
            }

            if (departureDate.HasValue && departureTime.HasValue)
            {
                flight.DepartureTime = departureDate.Value.Date.Add(departureTime.Value.TimeOfDay);
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "Choose date and time detail for departure time!";
                return message;
            }

            if (arrivalDate.HasValue && arrivalTime.HasValue)
            {
                flight.ArrivalTime = arrivalDate.Value.Date.Add(arrivalTime.Value.TimeOfDay);
            }
            else if (string.IsNullOrEmpty(message))
            {
                message = "Choose date and time detail for arrival time!";
                return message;
            }
           

            return message;
        }
        private void btn_AddClick(object sender, RoutedEventArgs e)
        {
            Flight flight = new Flight();
            flight.Id = flightService.GetFlights().Count() + 1;

            string message = ValidateAndPopulateFlight(flight);

            if (string.IsNullOrEmpty(message))
            {

                DateTime? departureDate = dpDepartureDate.SelectedDate;
                if (departureDate < DateTime.Now)
                {
                    message = "Departure date not in the pass";
                    MessageBox.Show(message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    flight.Status = true;
                    flightService.InsertFlight(flight);
                    loadFlights();
                }

            }
            else
            {
                MessageBox.Show(message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_UpdateClick(object sender, RoutedEventArgs e)
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
                        string message = ValidateAndPopulateFlight(flight);

                        if (string.IsNullOrEmpty(message))
                        {
                            MessageBoxResult result = MessageBox.Show(
                                "Are you sure to update filght?",
                                "Confirm Update",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question
                            );

                            if (result == MessageBoxResult.Yes)
                            {
                                DateTime? arrivalDate = dpArrivalDate.SelectedDate;
                                if (arrivalDate < DateTime.Now)
                                {
                                    message = "This flight is end, can not update!";
                                    MessageBox.Show(message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                {
                                    flightService.UpdateFlight(flight);
                                    MessageBox.Show("Update successfully!");
                                    loadFlights();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Operation cancelled.");
                            }
                        }
                        else
                        {
                            MessageBox.Show(message, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can not find flight!.");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid ID format. Please enter a valid number.");
                }
            }
            else
            {
                MessageBox.Show("Choose flight!");
            }
        }

        private void btn_DeleteClick(object sender, RoutedEventArgs e)
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
                            "Are you sure to delete flight?",
                            "Confirm Deletion",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question
                        );

                        // Kiểm tra kết quả của hộp thoại xác nhận
                        if (result == MessageBoxResult.Yes)
                        {
                            flight.Status = false;
                            flightService.UpdateFlight(flight);
                            MessageBox.Show("Delete successfully.");
                            loadFlights();
                        }
                        else
                        {
                            MessageBox.Show("Operation cancelled.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can not found.");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid ID format. Please enter a valid number.");
                }
            }
            else
            {
                MessageBox.Show("Choose flight!");
            }
        }



        private void btn_SearchClick(object sender, RoutedEventArgs e)
        {

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
                                        txtID.Text = flightId;
                                        cmbAirline.SelectedValue = flight?.AirlineId;
                                        cmbDepartingAirport.SelectedValue = flight?.DepartingAirport; 
                                        cmbArrivingAirport.SelectedValue = flight?.ArrivingAirport;
                                        txtDepartingGate.Text = flight?.DepartingGate;
                                        txtArrivingGate.Text = flight?.ArrivingGate;
                                        dpDepartureDate.SelectedDate = flight?.DepartureTime;
                                        tpDepartureTime.SelectedTime = flight?.DepartureTime;

                                        dpArrivalDate.SelectedDate = flight?.ArrivalTime;
                                        tpArrivalTime.SelectedTime = flight?.ArrivalTime;
                                    }
                                }
                                else
                                {
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


        private void filter()
        {
            FlightDataGrid.ItemsSource = null;

            var list = flightService.GetFlights().Where(s => s.Status == true).
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
            FlightDataGrid.ItemsSource = list.Skip((currentPage - 1) * itemsPerPage)
                                              .Take(itemsPerPage).Select(flight => new
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

        private void btn_Bin(object sender, RoutedEventArgs e)
        {
            BinFlight bin = new BinFlight();
            bin.ShowDialog();
            loadFlights();
        }


        private void dpDepartureDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateDates();
        }

        private void dpArrivalDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateDates();
        }       
       
        private void ValidateDates()
        {
            DateTime? departureDate = dpDepartureDate.SelectedDate;
            DateTime? arrivalDate = dpArrivalDate.SelectedDate;
            DateTime? departureTime = tpDepartureTime.SelectedTime;
            DateTime? arrivalTime = tpArrivalTime.SelectedTime;

            if (departureDate.HasValue && arrivalDate.HasValue)
            {
                DateTime departureDateOnly = departureDate.Value.Date;
                DateTime arrivalDateOnly = arrivalDate.Value.Date;

                if (arrivalDateOnly < departureDateOnly)
                {
                    MessageBox.Show("Arrival date can not before departure date.", "Date Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpArrivalDate.SelectedDate = null;
                    tpArrivalTime.SelectedTime = null;
                }
                else if (arrivalDateOnly == departureDateOnly)
                {
                    if (arrivalTime.HasValue && departureTime.HasValue)
                    {
                        DateTime departureDateTime = departureDateOnly.Add(new TimeSpan(departureTime.Value.Hour, departureTime.Value.Minute, departureTime.Value.Second));
                        DateTime arrivalDateTime = arrivalDateOnly.Add(new TimeSpan(arrivalTime.Value.Hour, arrivalTime.Value.Minute, arrivalTime.Value.Second));

                        if (arrivalDateTime < departureDateTime)
                        {
                            MessageBox.Show("On the same day, arrival time must be after departure time.", "Time Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            tpArrivalTime.SelectedTime = null;
                        }                       
                    }
                }
            }
        }

        private void btn_ExcelClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Thiết lập LicenseContext của EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Tạo SaveFileDialog để người dùng chọn nơi lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.FileName = "Flights.xlsx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Tạo file Excel mới
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        // Tạo một worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Flights");

                        // Lấy tiêu đề từ DataGrid
                        for (int i = 0; i < FlightDataGrid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = FlightDataGrid.Columns[i].Header.ToString();
                        }

                        // Lấy dữ liệu từ cơ sở dữ liệu
                        var flights = flightService.GetFlights()
                            .Where(s => s.Status == true)
                            .OrderByDescending(flight => flight.Id)
                            .ToList();

                        // Ghi dữ liệu vào file Excel, bắt đầu từ hàng thứ 2
                        int row = 2;
                        foreach (var flight in flights)
                        {
                            worksheet.Cells[row, 1].Value = flight.Id;
                            worksheet.Cells[row, 2].Value = flight.Airline?.Name ?? "";
                            worksheet.Cells[row, 3].Value = flight.Airline?.Code ?? "";
                            worksheet.Cells[row, 4].Value = flight.DepartingAirportNavigation?.Name ?? "";
                            worksheet.Cells[row, 5].Value = flight.ArrivingAirportNavigation?.Name ?? "";
                            worksheet.Cells[row, 6].Value = flight.DepartingGate;
                            worksheet.Cells[row, 7].Value = flight.ArrivingGate;
                            worksheet.Cells[row, 8].Value = flight.DepartureTime;
                            worksheet.Cells[row, 9].Value = flight.ArrivalTime;
                            row++;
                        }

                        // Lưu file Excel tại vị trí được chọn bởi người dùng
                        FileInfo fi = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fi);

                        MessageBox.Show("Exported to Excel successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = "";
            cmbAirline.Text = "";
            cmbDepartingAirport.Text = "";
            cmbArrivingAirport.Text = "";
            txtDepartingGate.Text = "";
            txtArrivingGate.Text = "";
            dpDepartureDate.Text = "";
            tpDepartureTime.Text = "";

            dpArrivalDate.Text = "";
            tpArrivalTime.Text = "";
        }
    }
}
