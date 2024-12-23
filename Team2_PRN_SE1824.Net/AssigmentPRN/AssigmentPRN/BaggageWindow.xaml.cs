using DataAccess.BussinessObjects;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightManagement
{
    /// <summary>
    /// Interaction logic for BaggageWindow.xaml
    /// </summary>
    public partial class BaggageWindow : Window
    {
        private readonly IBagageServices bagageServices;
        int currentPage = 1;
        int itemsPerPage = 17;
        public BaggageWindow()
        {
            bagageServices = new BaggageServices();
            InitializeComponent();
            LoadAllBookingHasNotBaggage ();
            LoadBookingPlatform();
            LoadAllAirportArrive();
            LoadAllAirportDepart();
        }

        public void LoadAllBookingHasNotBaggage()
        {

            if (lvBookingBaggage.View is GridView gridView)
            {
                int columnCount = gridView.Columns.Count;

                lvBookingBaggage.ItemsSource = null;
                var listBaggage = bagageServices.GetAllBookingHasNotBaggage();
                var pagedList = listBaggage
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();
                
                lvBookingBaggage.ItemsSource = pagedList;
            }
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Tổng chiều rộng của ListView trừ đi padding và border nếu có
            double totalWidth = lvBookingBaggage.ActualWidth - 10; // Trừ đi khoảng cách padding và border

            // Phân chia đều chiều rộng giữa các cột
            var gridView = lvBookingBaggage.View as GridView;
            if (gridView != null)
            {
                double columnWidth = totalWidth / gridView.Columns.Count;
                foreach (var column in gridView.Columns)
                {
                    column.Width = columnWidth;
                }
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadAllBookingHasNotBaggage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            LoadAllBookingHasNotBaggage();
        }

        private void Change_Button_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;
            BaggageUpdateWindow baggageWindow = new BaggageUpdateWindow
            {
                WindowState = WindowState.Maximized
            };
            baggageWindow.Show();
            baggageWindow.Closed += (s, args) =>
            {
                currentWindow.Show();
            };
            currentWindow.Hide();
        }

        public void LoadBookingPlatform()
        {
            cbBoxBookingPlatform.ItemsSource = bagageServices.GetAllBookingPlatform();
            cbBoxBookingPlatform.DisplayMemberPath = "Name";
            cbBoxBookingPlatform.SelectedValuePath = "Id";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text?.Trim();

                 
                if (!string.IsNullOrEmpty(searchText))
                {
                    if (lvBookingBaggage.View is GridView gridView)
                    {
                        int columnCount = gridView.Columns.Count;

                        lvBookingBaggage.ItemsSource = null;
                        lvBookingBaggage.ItemsSource = bagageServices.GetAllBySearch(searchText);
                        
                    }
                }
                else
                {
                    LoadAllBookingHasNotBaggage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: Cannot find baggage !\nError detail: {ex.Message}", "Error");
            }
            
        }


        private void cbBookingPlatform(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                if (cbBoxBookingPlatform.SelectedValue != null)
                {
                    
                    if (cbBoxBookingPlatform.SelectedValue is int selectedPlatformId)
                    {
                       
                        if (lvBookingBaggage.View is GridView gridView)
                        {
                            int columnCount = gridView.Columns.Count;

                            var bookingBaggage = bagageServices.GetAllByCbBookingPlatform(selectedPlatformId.ToString());

                            
                            lvBookingBaggage.ItemsSource = bookingBaggage;
                            
                        }

                        // Chuyển đổi ID thành chuỗi và gọi dịch vụ
                       
                    }
                    else
                    {
                        // Thông báo khi SelectedValue không phải kiểu int
                        MessageBox.Show($"Selected value is not an integer. It is of type {cbBoxBookingPlatform.SelectedValue.GetType()}.", "Notification");
                    }
                }
                else
                {
                    // Thông báo khi không có giá trị nào được chọn
                    MessageBox.Show("No item selected in the ComboBox.", "Notification");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết
                MessageBox.Show($"Error: Cannot find baggage !\nError detail: {ex.Message}", "Error");
            }

        }

        private void LoadAllAirportArrive()
        {
            cbBoxArriveAirport.ItemsSource = bagageServices.GetALlAirports();
            cbBoxArriveAirport.DisplayMemberPath = "Name";
            cbBoxArriveAirport.SelectedValuePath = "Id";
        }

        private void LoadAllAirportDepart()
        {
            cbBoxDepartAirport.ItemsSource = bagageServices.GetALlAirports();
            cbBoxDepartAirport.DisplayMemberPath = "Name";
            cbBoxDepartAirport.SelectedValuePath = "Id";
        }

        private void cbDepartAirport(object sender, RoutedEventArgs e)
        {
            LoadBookings();
        }

        private void cbArriveAirport(object sender, RoutedEventArgs e)
        {
            LoadBookings();
        }

        
        private void LoadBookings()
        {
            try
            {
                if (cbBoxDepartAirport.SelectedValue != null && cbBoxArriveAirport.SelectedValue != null)
                {
                    if (cbBoxDepartAirport.SelectedValue is int selectedDepartingAirport && cbBoxArriveAirport.SelectedValue is int selectedArrivingAirport)
                    {
                        // Hiển thị thông tin ID được chọn
                        if (lvBookingBaggage.View is GridView gridView)
                        {
                            int columnCount = gridView.Columns.Count;

                            var bookingBaggage = bagageServices.GetAllByCbDepartingAirportAndArrivingAirport(selectedDepartingAirport.ToString(), selectedArrivingAirport.ToString());

                            // Cập nhật ItemsSource của ListView với dữ liệu
                            lvBookingBaggage.ItemsSource = bookingBaggage;
                            //gridView.Columns[9].Width = 0;
                        }
                    }
                }
                else if (cbBoxDepartAirport.SelectedValue != null)
                {
                    if (cbBoxDepartAirport.SelectedValue is int selectedDepartingAirport)
                    {
                        // Hiển thị thông tin ID được chọn
                        if (lvBookingBaggage.View is GridView gridView)
                        {
                            int columnCount = gridView.Columns.Count;

                            var bookingBaggage = bagageServices.GetAllByCbDepartingAirport(selectedDepartingAirport.ToString());

                            // Cập nhật ItemsSource của ListView với dữ liệu
                            lvBookingBaggage.ItemsSource = bookingBaggage;
                            
                        }
                    }
                }
                else if (cbBoxArriveAirport.SelectedValue != null)
                {
                    if (cbBoxArriveAirport.SelectedValue is int selectedArrivingAirport)
                    {
                        // Hiển thị thông tin ID được chọn
                        if (lvBookingBaggage.View is GridView gridView)
                        {
                            int columnCount = gridView.Columns.Count;

                            var bookingBaggage = bagageServices.GetAllByCbArrivingAirport(selectedArrivingAirport.ToString());

                            // Cập nhật ItemsSource của ListView với dữ liệu
                            lvBookingBaggage.ItemsSource = bookingBaggage;
                            //gridView.Columns[9].Width = 0;
                        }
                    }
                }
                else
                {
                    // Không có giá trị nào được chọn, hiển thị danh sách mặc định hoặc xóa danh sách hiện tại
                    lvBookingBaggage.ItemsSource = null; // hoặc bạn có thể gọi phương thức để lấy tất cả dữ liệu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: Cannot find baggage !\nError details: {ex.Message}", "Error");
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = bagageServices.GetCurrentId();
                if (lvBookingBaggage.SelectedItem is not null)
                {
                    Baggage baggage = new Baggage
                    {
                        BookingId = Int32.Parse(txtBookingId.Text)
                    };

                    // Kiểm tra nếu có giá trị trong txtWeight
                    if (!string.IsNullOrWhiteSpace(txtWeight.Text))
                    {
                        double weight;

                        // Chấp nhận cả dấu chấm và dấu phẩy làm phân cách thập phân
                        // Thay thế dấu phẩy bằng dấu chấm để đảm bảo phân tích đúng cách
                        string normalizedInput = txtWeight.Text.Replace(',', '.');

                        if (Double.TryParse(normalizedInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out weight))
                        {
                            // Kiểm tra giá trị khối lượng hợp lệ
                            if (weight < 0)
                            {
                                MessageBox.Show("Please enter the weight greater than 0 (kg) !", "Notification");
                            }
                            else if (weight > 30)
                            {
                                MessageBox.Show("Please enter the weight less than 30 (kg) !", "Notification");
                            }
                            else
                            {
                                count++;
                                baggage.Id = count;
                                baggage.Status = true;
                                baggage.WeightInKg = (decimal?)weight;
                                bagageServices.AddBaggage(baggage);
                                MessageBox.Show("Add baggage for this booking is successfully !", "Notification");
                            }
                        }
                        //else
                        //{
                        //    MessageBox.Show("Vui lòng nhập một số hợp lệ (ví dụ: 11.5 hoặc 11) !", "Thông báo");
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Please enter weight of baggage !", "Notification");
                    }
                }
                else
                {
                    MessageBox.Show("Please seleted 1 row to add weight of baggage!", "Notification");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: cannot add new baggage !\nError details: {ex.Message}\nInner Exception: {ex.InnerException?.Message}", "Error");
            }
            finally
            {
                LoadAllBookingHasNotBaggage();
            }
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            txtFlightID.Text = "";
            txtPassengerName.Text = "";
            txtDepartingTime.Text = "";
            txtArrivingTime.Text = "";
            txtArrivingAirport.Text = "";
            txtBookingPlatform.Text = "";
            txtDepartingAirport.Text = "";
            txtWeight.Text = "";
            cbBoxBookingPlatform.SelectedIndex = -1;
            cbBoxArriveAirport.SelectedIndex = -1;
            cbBoxDepartAirport.SelectedIndex = -1;
            txtSearch.Text = "";
            LoadAllBookingHasNotBaggage() ;
        }

        private void lvBookingBaggage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvBookingBaggage.SelectedItem is not null)
            {
                var selectedItem = lvBookingBaggage.SelectedItem as dynamic;

                // Sử dụng phương thức GetPropertyValue để lấy giá trị và gán vào các TextBox
                txtBookingId.Text = GetPropertyValue(selectedItem, "BookingId") ?? "N/A";
                txtFlightID.Text = GetPropertyValue(selectedItem, "FlightCode") ?? "N/A";
                txtPassengerName.Text = GetPropertyValue(selectedItem, "PassengerName") ?? "N/A";
                txtDepartingTime.Text = GetPropertyValue(selectedItem, "DepartingTime") ?? "N/A";
                txtArrivingTime.Text = GetPropertyValue(selectedItem, "ArrivingTime") ?? "N/A";
                txtArrivingAirport.Text = GetPropertyValue(selectedItem, "ArrivingAirport") ?? "N/A";
                txtBookingPlatform.Text = GetPropertyValue(selectedItem, "PlatformName") ?? "N/A";
                txtDepartingAirport.Text = GetPropertyValue(selectedItem, "DepartingAirport") ?? "N/A";
                txtEmail.Text = GetPropertyValue(selectedItem, "Email") ?? "N/A";
            }
        }

        private string GetPropertyValue(dynamic obj, string propertyName)
        {
            try
            {
                // Kiểm tra xem obj có phải là IDictionary<string, object> không
                if (obj is IDictionary<string, object> dictionary && dictionary.ContainsKey(propertyName))
                {
                    return dictionary[propertyName]?.ToString();
                }

                // Nếu không, cố gắng lấy thuộc tính thông qua reflection
                var property = obj.GetType().GetProperty(propertyName);
                var value = property?.GetValue(obj, null);
                return value?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving property '{propertyName}': {ex.Message}", "Error");
                return null;
            }
        }


        
       
    }
}
