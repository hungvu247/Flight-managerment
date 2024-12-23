using DataAccess.BussinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Shapes;

namespace Team2_SE1824_FlightManager
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BinBooking : Window
    {
        private readonly IBookingService bookingService;
        private readonly FlightManagementDbContext db = new FlightManagementDbContext();
        public BinBooking()
        {
            bookingService = new BookingService();
            InitializeComponent();
            LoadBooking();
            LoadBookingPlatform();
            LoadPassenger();
            LoadFlight();
        }

        public void LoadBooking()
        {
            dgData.ItemsSource = null;
            dgData.ItemsSource = bookingService.GetAllBookingsRemoved();
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
                DataGridCell cell = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;

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
                // 
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int bookingId = Int32.Parse(txtID.Text);

                    // before remove this booking then remove baggage
                    Booking? booking = bookingService.GetBookingById(bookingId);
                    foreach (var item in booking.Baggages)
                    {
                        db.Baggages.Remove(item);
                    }
                    bookingService.RemoveBookingFromBin(booking);
                    MessageBox.Show("Remove booking successfully!");
                    LoadBooking();
                }
            }
            else
            {
                MessageBox.Show("Please select the booking you want to delete!");
            }

        }

        private bool FlightHasTakenOff(int? flightId)
        {
            return db.Flights.Find(flightId).DepartureTime != null;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = "";
            cboPassenger.Text = "";
            cboBookingPlat.Text = "";
            cboFlight.Text = "";
            txtBookingTime.Text = "";
        }

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            txtSearchFlight.Text = "";
            txtSearchP.Text = "";
            cboSearchBP.Text = "";
        }

        private void txtSearchP_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgData.ItemsSource = null;
            dgData.ItemsSource = FilterBooking();
        }

        private void txtSearchFlight_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgData.ItemsSource = null;
            dgData.ItemsSource = FilterBooking();
        }

        private List<Booking> FilterBooking()
        {
            List<Booking> list = bookingService.GetAllBookingsRemoved();

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
            dgData.ItemsSource = null;
            dgData.ItemsSource = FilterBooking();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            int bookingId = Int32.Parse(txtID.Text.ToString());
            Booking bookingRestore = bookingService.GetBookingById(bookingId);  
            if (bookingRestore != null)
            {
                bookingRestore.Status = false;
                // 
                MessageBoxResult result = MessageBox.Show("Are you sure you want to restore this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bookingService.UpdateBooking(bookingRestore);
                    MessageBox.Show("Restore booking successfully!");
                    LoadBooking();
                }
               
            }
        }

        private void btnRestoreAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restore all the booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                List<Booking> list = bookingService.GetAllBookingsRemoved();
                foreach (var item in list)
                {
                    item.Status = false;
                    bookingService.UpdateBooking(item);
                }
                MessageBox.Show("Restore all booking successfully!");
                LoadBooking();
            }          
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete all the booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                List<Booking> list = bookingService.GetAllBookingsRemoved();
                foreach (var item in list)
                {
                    // before remove this booking then remove baggage
                    Booking? booking = bookingService.GetBookingById(item.Id);
                    foreach (var b in booking.Baggages)
                    {
                        db.Baggages.Remove(b);
                    }
                    bookingService.RemoveBookingFromBin(item);
                }
                MessageBox.Show("Delete all booking successfully!");
                LoadBooking();
            }
        }
    }
}
