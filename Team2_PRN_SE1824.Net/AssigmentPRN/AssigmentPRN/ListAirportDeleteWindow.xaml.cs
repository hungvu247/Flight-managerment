using AssigmentPRN;
using DataAccess.BussinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AsgPRN
{
    public partial class ListAirportDeleteWindow : Window
    {
        private readonly IAirportService airportService;
        private int id = 0; // Biến id được khai báo ở cấp độ lớp

        public ListAirportDeleteWindow()
        {
            InitializeComponent();
            airportService = new AirportService();
            LoadData();
        }

        FlightManagementDbContext FlightManagementDbContext = new FlightManagementDbContext();
        int pageNumber = 1;
        int numberOfRecordings = 10;

        public void LoadData()
        {
            AirportDataGrid.ItemsSource = null;
            AirportDataGrid.ItemsSource = LoadAirport(pageNumber, numberOfRecordings);
        }

        List<Airport> LoadAirport(int pageNumber, int recordNum)
        {
            var airportsWithStatusTrue = airportService.GetAirports("yes").Skip((pageNumber - 1) * numberOfRecordings).Take(recordNum).ToList();
            return airportsWithStatusTrue;
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber - 1 > 0)
            {
                pageNumber--;
                AirportDataGrid.ItemsSource = LoadAirport(pageNumber, numberOfRecordings);
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            int totalRecord = airportService.GetAirports("yes").Where(a => a.Status == false).Count();

            if (pageNumber - 1 < totalRecord / numberOfRecordings)
            {
                pageNumber++;
                AirportDataGrid.ItemsSource = LoadAirport(pageNumber, numberOfRecordings);
            }
        }

        private void BackPage_Click(object sender, RoutedEventArgs e)
        {
            ManageAirportWindow manageAirportWindow = new ManageAirportWindow();
            this.Close();
            manageAirportWindow.Show();
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            if (id != 0) // Check if id is not zero
            {
                Airport airport = airportService.GetAirportByID(id);
                if (airport != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to restore this airport?",
                                                              "Confirm restore",
                                                              MessageBoxButton.YesNo,
                                                              MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        airport.Status = true;
                        airportService.UpdateAirport(airport);
                        FlightManagementDbContext.SaveChanges();
                        MessageBox.Show("Restore successfully");
                        LoadData();
                    }
                    // If the user selects No, do nothing (cancel restore)
                }
                else
                {
                    MessageBox.Show("No airport found for recovery.");
                }
            }
            else
            {
                MessageBox.Show("Please select an airport for recovery.");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                LoadData();
            }
            else
            {
                ComboBoxItem selectedItem = (ComboBoxItem)comboBoxSearch.SelectedItem;
                string typeOfSearch = selectedItem.Tag.ToString();
                List<Airport> airportsWithStatusTrue = new List<Airport>();

                switch (typeOfSearch)
                {
                    case "Code":
                        airportsWithStatusTrue = airportService.GetAirportByInfoCode(txtSearch.Text, "yes");
                        break;
                    case "Name":
                        airportsWithStatusTrue = airportService.GetAirportByInfoName(txtSearch.Text, "yes");
                        break;
                    case "Country":
                        airportsWithStatusTrue = airportService.GetAirportByInfoCountry(txtSearch.Text, "yes");
                        break;
                    case "City":
                        airportsWithStatusTrue = airportService.GetAirportByInfoCity(txtSearch.Text, "yes");
                        break;
                    case "State":
                        airportsWithStatusTrue = airportService.GetAirportByInfoState(txtSearch.Text, "yes");
                        break;
                }
                AirportDataGrid.ItemsSource = airportsWithStatusTrue;
            }
        }

        private void AirportDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedItem != null)
            {
                Airport selectedAirport = (Airport)dataGrid.SelectedItem;
                id = selectedAirport.Id; // Cập nhật giá trị id từ dữ liệu đã chọn
            }
        }

        private void AirportDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            // Handle the event logic here
        }
    }
}
