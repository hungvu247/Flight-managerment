using DataAccess.BussinessObjects;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace Team2_SE1824_FlightManager
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private readonly IBookingService bookingService;
        private readonly FlightManagementDbContext db = new FlightManagementDbContext();
        private int currentPage;
        private readonly int pageSize = 25;
        public BookingWindow()
        {
            bookingService = new BookingService();
            currentPage = 1;
            InitializeComponent();
            LoadBooking();
            LoadBookingPlatform();
            LoadPassenger();
            LoadFlight();
        }

        public void LoadBooking()
        {
            dgData.ItemsSource = null;
            List<Booking> list = bookingService.GetAllBookings()
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            dgData.ItemsSource = list;
        }

        public void LoadBookingPlatform()
        {
            cboBookingPlat.ItemsSource = null;
            cboBookingPlat.ItemsSource = db.BookingPlatforms.ToList();
            cboBookingPlat.SelectedValuePath = "Id";
            cboBookingPlat.DisplayMemberPath = "Name";

            cboSearchBP.ItemsSource = null;
            cboSearchBP.ItemsSource = db.BookingPlatforms.ToList();
            cboSearchBP.SelectedValuePath = "Id";
            cboSearchBP.DisplayMemberPath = "Name";
        }

        public void LoadPassenger()
        {
            cboPassenger.ItemsSource = null;
            cboPassenger.ItemsSource = db.Passengers.ToList();
            cboPassenger.SelectedValuePath = "Id";
            cboPassenger.DisplayMemberPath = "FullName";
        }

        public void LoadFlight()
        {
            cboFlight.ItemsSource = null;

            // chỉ load những Flight chưa cất cánh
            List<Flight> list = db.Flights
                .Where(f => f.DepartureTime == null && f.ArrivalTime == null)
                .ToList();
            cboFlight.ItemsSource = list;
            cboFlight.SelectedValuePath = "Id";
            cboFlight.DisplayMemberPath = "InforFlight";
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.ItemsSource != null)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string bookingId = ((TextBlock)cell.Content).Text;

                if (!bookingId.Equals(""))
                {
                    Booking booking = bookingService.GetBookingById(Int32.Parse(bookingId));
                    txtID.Text = booking.Id.ToString();

                    // name of passenger
                    Passenger passenger = db.Passengers.Find(booking.PassengerId);
                    cboPassenger.SelectedValue = passenger.Id;

                    // 
                    Flight flight = db.Flights.Find(booking.FlightId);
                    cboFlight.SelectedValue = flight.Id;

                    // 
                    BookingPlatform bp = db.BookingPlatforms.Find(booking.BookingPlatformId);
                    cboBookingPlat.SelectedValue = bp.Id;

                    // 
                    txtBookingTime.Text = booking.BookingTime.ToString();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!txtID.Text.IsNullOrEmpty())
            {
                int bookingId = Int32.Parse(txtID.Text);
                Booking? booking = bookingService.GetBookingById(bookingId);
                // 
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bookingService.RemoveBooking(booking);
                    MessageBox.Show("Remove booking successfully!");
                    ClearField();
                    LoadBooking();
                }
            }
            else
            {
                MessageBox.Show("Please select the booking you want to delete!");
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cboPassenger.SelectedValue == null || cboFlight.SelectedValue == null || cboBookingPlat.SelectedValue == null)
            {
                MessageBox.Show("Please fill in all information when adding a new booking!");
            }
            else
            {
                int bookingId = bookingService.GetBookingWithLargestId().Id + 1;
                int passengerId = Int32.Parse(cboPassenger.SelectedValue.ToString());
                int flightId = Int32.Parse(cboFlight.SelectedValue.ToString());
                int bookingFlatformId = Int32.Parse(cboBookingPlat.SelectedValue.ToString());
                DateTime currentDateTime = DateTime.Now;
                Booking booking = new Booking()
                {
                    Id = bookingId,
                    PassengerId = passengerId,
                    FlightId = flightId,
                    BookingPlatformId = bookingFlatformId,
                    BookingTime = currentDateTime,
                };
                // 
                MessageBoxResult result = MessageBox.Show("Are you sure you want to create this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bookingService.AddBooking(booking);
                    MessageBox.Show("Create booking successfully!");
                    LoadBooking();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!txtID.Text.IsNullOrEmpty())
            {
                int bookingId = Int32.Parse(txtID.Text);
                Booking booking = bookingService.GetBookingById(bookingId);

                // chỉ dc update những booking có chuyến bay chưa xuất phát
                // nếu cập nhật chuyến bay thì chỉ được cập nhật chuyến bay chưa xuất phát trong booking
                if (FlightHasTakenOff(booking.FlightId))
                {
                    MessageBox.Show("The flight has taken off so the booking cannot be updated!");
                }
                else
                {
                    int passengerId = Int32.Parse(cboPassenger.SelectedValue.ToString());
                    int flightId = Int32.Parse(cboFlight.SelectedValue.ToString());
                    int bookingFlatformId = Int32.Parse(cboBookingPlat.SelectedValue.ToString());

                    booking.PassengerId = passengerId;
                    booking.FlightId = flightId;
                    booking.BookingPlatformId = bookingFlatformId;
                    // 
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to update this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bookingService.UpdateBooking(booking);
                        MessageBox.Show("Remove booking successfully!");
                        LoadBooking();
                    }
                }
            }
        }

        private bool FlightHasTakenOff(int? flightId)
        {
            return db.Flights.Find(flightId).DepartureTime != null;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearField();
        }

        public void ClearField()
        {
            txtID.Text = "";
            searchPassenger.Text = "";
            cboPassenger.Text = "";
            cboBookingPlat.Text = "";
            cboFlight.Text = "";
            txtBookingTime.Text = "";
        }

        private void searchPassenger_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchPass = searchPassenger.Text.ToString().ToLower();
            List<Passenger> listPassenger = db.Passengers
                .Where(p => p.FirstName.ToLower().Contains(searchPass) ||
                p.LastName.ToLower().Contains(searchPass))
                .ToList();
            cboPassenger.ItemsSource = listPassenger;
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            txtSearchFlight.Text = "";
            txtSearchP.Text = "";
            cboSearchBP.Text = "";
        }

        private void txtSearchP_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentPage = 1;
            dgData.ItemsSource = null;
            dgData.ItemsSource = FilterBooking()
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private void txtSearchFlight_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentPage = 1;
            dgData.ItemsSource = null;
            dgData.ItemsSource = FilterBooking()
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private List<Booking> FilterBooking()
        {        
            List<Booking> list = bookingService.GetAllBookings();

            if (!txtSearchP.Text.IsNullOrEmpty())
            {
                string searchPassenger = txtSearchP.Text.ToString().ToLower();
                list = list
                    .Where(b => b.Passenger.FullName.ToLower().Contains(searchPassenger))
                    .ToList();
            }

            if (!txtSearchFlight.Text.IsNullOrEmpty())
            {
                string searchFlight = txtSearchFlight.Text.ToString().ToLower();
                list = list
                    .Where(b => b.Flight.InforFlight.ToLower().Contains(searchFlight))
                    .ToList();
            }

            if (cboSearchBP.SelectedValue != null)
            {
                int bookingPlatformId = Int32.Parse(cboSearchBP.SelectedValue.ToString());
                list = list
                    .Where(b => b.BookingPlatformId == bookingPlatformId)
                    .ToList();
            }

            list = list.OrderByDescending(b => b.BookingTime).ToList();

            return list;
        }

        private void cboSearchBP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentPage = 1;
            dgData.ItemsSource = null;
            dgData.ItemsSource = FilterBooking()
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        private void btnBin_Click(object sender, RoutedEventArgs e)
        {
            BinBooking binBooking = new BinBooking();
            binBooking.Show();
            LoadBooking();
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            dgData.ItemsSource = null;

            List<Booking> list = bookingService.GetAllBookings()
                .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            if (!txtSearchP.Text.IsNullOrEmpty() || !txtSearchFlight.Text.IsNullOrEmpty() ||
                    cboSearchBP.SelectedValue != null)
            {
                list = FilterBooking()
                    .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            dgData.ItemsSource = list;
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgData.ItemsSource = null;
                List<Booking> list = bookingService.GetAllBookings()
                    .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                if (!txtSearchP.Text.IsNullOrEmpty() || !txtSearchFlight.Text.IsNullOrEmpty() ||
                    cboSearchBP.SelectedValue != null)
                {
                    list = FilterBooking()
                        .Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                }
                dgData.ItemsSource = list;
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
                saveFileDialog.FileName = "Bookings.xlsx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Tạo file Excel mới
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        // Tạo một worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Bookings");

                        // Lấy tiêu đề từ DataGrid
                        for (int i = 0; i < dgData.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dgData.Columns[i].Header.ToString();
                        }

                        // Lấy dữ liệu từ cơ sở dữ liệu
                        var bookings = bookingService.GetAllBookings()
                            .Where(b => b.Status == false || b.Status == null)
                            .OrderByDescending(b => b.BookingTime)
                            .ToList();

                        // Ghi dữ liệu vào file Excel, bắt đầu từ hàng thứ 2
                        int row = 2;
                        foreach (var booking in bookings)
                        {
                            worksheet.Cells[row, 1].Value = booking.Id;
                            worksheet.Cells[row, 2].Value = booking.Passenger?.FullName ?? "";
                            worksheet.Cells[row, 3].Value = booking.Flight?.InforFlight ?? "";
                            worksheet.Cells[row, 4].Value = booking.BookingPlatform?.Name ?? "";
                            worksheet.Cells[row, 5].Value = booking.BookingTime;
                            worksheet.Cells[row, 5].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
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
    }
}
