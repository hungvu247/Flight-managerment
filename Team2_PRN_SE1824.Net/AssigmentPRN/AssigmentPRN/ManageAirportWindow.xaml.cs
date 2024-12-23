using AsgPRN;
using DataAccess.BussinessObjects;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Windows;
using Microsoft.Win32;
namespace AssigmentPRN
{
    /// <summary>
    /// Interaction logic for ManageAirportWindow.xaml
    /// </summary>
    public partial class ManageAirportWindow : Window
    {
        private readonly IAirportService airportService;
        public ManageAirportWindow()
        {
            InitializeComponent();
            airportService = new AirportService();
            LoadData();
        }

        FlightManagementDbContext FlightManagementDbContext = new FlightManagementDbContext();
        int localRecord = 0;
        int pageNumber = 1;
        int numberOfRecordings = 10;
        public void LoadData()
        {
            AirportDataGrid.ItemsSource = null;

            AirportDataGrid.ItemsSource = LoadAirport(pageNumber, numberOfRecordings);
        }
        List<Airport> LoadAirport(int pageNumber, int recordNum)
        {
            var airportsWithStatusTrue = airportService.GetAirports("no").Where(a => a.Status == true).Skip((pageNumber - 1) * numberOfRecordings).Take(recordNum).ToList();
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
            int totalRecor= airportService.GetAirports("no").Where(a => a.Status == true).Count();
            
         
            if (pageNumber -1 < totalRecor/numberOfRecordings)
            {
                pageNumber++;
                AirportDataGrid.ItemsSource = LoadAirport(pageNumber, numberOfRecordings);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Airport airport = new Airport();

            if (CheckExistCode(txtAirportCode.Text) == false)
            {
                airport.Code = txtAirportCode.Text;
                airport.Name = txtAirportName.Text;
                airport.State = txtAirportState.Text;
                airport.City = txtAirportCity.Text;
                airport.Status = true;
                airport.Country = txtAirportCountry.Text;

                airportService.InsertAirport(airport);
                FlightManagementDbContext.SaveChanges();
                System.Windows.MessageBox.Show("Add successfully");
                LoadData();
            }
            else
            {
                System.Windows.MessageBox.Show("Can not duplicate Code!");
            }
        }




        public bool CheckExistCode(string code)
        {
            var airports = airportService.GetAirports("no");

            foreach (Airport item in airports)
            {
                if (item != null && !string.IsNullOrEmpty(item.Code) && item.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = AirportDataGrid;
            if (dataGrid.ItemsSource != null)
            {
                int selectedIndex = dataGrid.SelectedIndex;
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(selectedIndex) as DataGridRow;

                if (row == null)
                {
                    System.Windows.MessageBox.Show("Please select an item to delete.");
                    return;
                }

                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string airportID = ((TextBlock)cell.Content).Text;
                if (!airportID.Equals(""))
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to delete this airport?",
                                                              "Confirm information",
                                                              MessageBoxButton.YesNo,
                                                              MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Airport airport = airportService.GetAirportByID(Int32.Parse(airportID));
                        airport.Status = false;
                        airportService.UpdateAirport(airport);
                        FlightManagementDbContext.SaveChanges();
                        System.Windows.MessageBox.Show("Delete successfully");
                        LoadData();
                    }

                }
                else
                {
                    MessageBox.Show("No airport found for recovery.");
                }
            }
            else
            {
                MessageBox.Show("No airport found for recovery.");
            }
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = AirportDataGrid;
            if (dataGrid.SelectedItem != null)
            {
                Airport selectedAirport = (Airport)dataGrid.SelectedItem;

                // Ensure selectedAirport is not null and has a valid ID
                if (selectedAirport != null && selectedAirport.Id > 0)
                {
                    Airport airport = airportService.GetAirportByID(selectedAirport.Id);

                    if (airport != null)
                    {
                        if (CheckExistCode(txtAirportCode.Text) == false)
                        {
                            airport.Code = txtAirportCode.Text;
                            airport.Name = txtAirportName.Text;
                            airport.State = txtAirportState.Text;
                            airport.City = txtAirportCity.Text;
                            airport.Status = true;
                            airport.Country = txtAirportCountry.Text;

                            airportService.UpdateAirport(airport);
                            FlightManagementDbContext.SaveChanges();

                            System.Windows.MessageBox.Show("Update successfully");
                            LoadData();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Cannot duplicate Code!");
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Airport not found!");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("No airport selected!");
                }
            }
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == null || txtSearch.Text.Length <= 0)
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
                            airportsWithStatusTrue = airportService.GetAirportByInfoCode(txtSearch.Text, "no");
                            break;
                        case "Name":
                            airportsWithStatusTrue = airportService.GetAirportByInfoName(txtSearch.Text, "no");
                            break;
                        case "Country":
                            airportsWithStatusTrue = airportService.GetAirportByInfoCountry(txtSearch.Text,"no");
                            break;
                        case "City":
                            airportsWithStatusTrue = airportService.GetAirportByInfoCity(txtSearch.Text, "no");
                            break;
                        case "State":
                            airportsWithStatusTrue = airportService.GetAirportByInfoState(txtSearch.Text, "no");
                            break;
                    }  AirportDataGrid.ItemsSource = airportsWithStatusTrue;
                

              
            }
        }

        private void AirportDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = AirportDataGrid;

            if (dataGrid.SelectedItem != null)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator
                                      .ContainerFromItem(dataGrid.SelectedItem);

                if (row != null)
                {
                    DataGridCell cell = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;

                    if (cell != null)
                    {
                        if (cell.Content is TextBlock textBlock)
                        {
                            string airportID = textBlock.Text;

                            if (!string.IsNullOrEmpty(airportID) && int.TryParse(airportID, out int id))
                            {
                                Airport airport = airportService.GetAirportByID(id);
                                if (airport != null)
                                {
                                    txtAirportID.Text = airport.Id.ToString();
                                    txtAirportName.Text = airport.Name;
                                    txtAirportState.Text = airport.State;
                                    txtAirportCountry.Text = airport.Country;
                                    txtAirportCity.Text = airport.City;
                                    txtAirportCode.Text = airport.Code;
                                }
                            }
                        }
                        else if (cell.Content is System.Windows.Controls.TextBox textBox)
                        {
                            string airportID = textBox.Text;

                            if (!string.IsNullOrEmpty(airportID) && int.TryParse(airportID, out int id))
                            {
                                Airport airport = airportService.GetAirportByID(id);
                                if (airport != null)
                                {
                                    txtAirportID.Text = airport.Id.ToString();
                                    txtAirportName.Text = airport.Name;
                                    txtAirportState.Text = airport.State;
                                    txtAirportCountry.Text = airport.Country;
                                    txtAirportCity.Text = airport.City;
                                    txtAirportCode.Text = airport.Code;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void Reset(object sender, RoutedEventArgs e)
        {
            txtAirportID.Text = "";
            txtAirportName.Text = "";
            txtAirportState.Text = "";
            txtAirportCountry.Text = "";
            txtAirportCity.Text = "";
            txtAirportCode.Text = "";
        }

      

        private void ListDelete_Click(object sender, RoutedEventArgs e)
        {
             ListAirportDeleteWindow listAirportDeleteWindow = new ListAirportDeleteWindow();
            listAirportDeleteWindow.ShowDialog();
            LoadData();
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Thiết lập LicenseContext của EPPlus
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Tạo SaveFileDialog để người dùng chọn nơi lưu file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                saveFileDialog.FileName = "Airports.xlsx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Tạo file Excel mới
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        // Tạo một worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Airports");

                        // Lấy tiêu đề từ DataGrid
                        for (int i = 0; i < AirportDataGrid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = AirportDataGrid.Columns[i].Header.ToString();
                        }

                        // Lấy dữ liệu từ cơ sở dữ liệu
                        var airports = airportService.GetAirports("no")
                            .Where(s => s.Status == true)
                            .OrderByDescending(flight => flight.Id)
                            .ToList();

                        // Ghi dữ liệu vào file Excel, bắt đầu từ hàng thứ 2
                        int row = 2;
                        foreach (var airport in airports)
                        {
                            worksheet.Cells[row, 1].Value = airport.Id;
                            worksheet.Cells[row, 2].Value = airport.Code ?? "";
                            worksheet.Cells[row, 3].Value = airport.Name ?? "";
                            worksheet.Cells[row, 4].Value = airport.Country ?? "";
                            worksheet.Cells[row, 5].Value = airport.City ?? "";
                            worksheet.Cells[row, 6].Value = airport.State;

                            row++;
                        }

                        // Lưu file Excel tại vị trí được chọn bởi người dùng
                        FileInfo fi = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(fi);

                        System.Windows.MessageBox.Show("Exported to Excel successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
