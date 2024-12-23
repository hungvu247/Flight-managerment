using DataAccess.BussinessObjects;

using Microsoft.Win32;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace FlightManagement
{
    /// <summary>
    /// Interaction logic for BaggageUpdateWindow.xaml
    /// </summary>
    public partial class BaggageUpdateWindow : Window
    {
        private readonly IBagageServices bagageServices;
        int currentPage = 1;
        int itemsPerPage = 17;
        public BaggageUpdateWindow()
        {
            bagageServices = new BaggageServices();
            InitializeComponent();
            LoadAllBookingHasBaggage();
            LoadBookingPlatform();
            LoadAllAirportArrive();
            LoadAllAirportDepart();
        }

        public void LoadAllBookingHasBaggage()
        {

            if (lvBookingBaggage.View is GridView gridView)
            {
                int columnCount = gridView.Columns.Count;

                lvBookingBaggage.ItemsSource = null;
                var listBaggage = bagageServices.GetAllBookingHasBaggage();
                //gridView.Columns[10].Width = 0;
                //gridView.Columns[11].Width = 0;
                var pagedList = listBaggage
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();
                //gridView.Columns[9].Width = 0;
                lvBookingBaggage.ItemsSource = pagedList;
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadAllBookingHasBaggage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            LoadAllBookingHasBaggage();
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
                        lvBookingBaggage.ItemsSource = bagageServices.GetAllBySearchUpdate(searchText);
                        
                    }
                }
                else
                {
                    LoadAllBookingHasBaggage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: cannnot find baggage !\nError detail: {ex.Message}", "Error");
            }

        }


        private void cbBookingPlatform(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Kiểm tra nếu có giá trị được chọn trong ComboBox
                if (cbBoxBookingPlatform.SelectedValue != null)
                {
                    // Kiểm tra kiểu dữ liệu của SelectedValue
                    if (cbBoxBookingPlatform.SelectedValue is int selectedPlatformId)
                    {
                        // Hiển thị thông tin ID được chọn
                        if (lvBookingBaggage.View is GridView gridView)
                        {
                            int columnCount = gridView.Columns.Count;

                            var bookingBaggage = bagageServices.GetAllByCbBookingPlatformUpdate(selectedPlatformId.ToString());

                            
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
               
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết
                MessageBox.Show($"Error: cannot find the baggage !\nError detail: {ex.Message}", "Error");
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

                            var bookingBaggage = bagageServices.GetAllByCbDepartingAirportAndArrivingAirportUpdate(selectedDepartingAirport.ToString(), selectedArrivingAirport.ToString());

                            // Cập nhật ItemsSource của ListView với dữ liệu
                            lvBookingBaggage.ItemsSource = bookingBaggage;
                            
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

                            var bookingBaggage = bagageServices.GetAllByCbDepartingAirportUpdate(selectedDepartingAirport.ToString());

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

                            var bookingBaggage = bagageServices.GetAllByCbArrivingAirportUpdate(selectedArrivingAirport.ToString());

                            // Cập nhật ItemsSource của ListView với dữ liệu
                            lvBookingBaggage.ItemsSource = bookingBaggage;
                            
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
                MessageBox.Show($"Error: cannot find the baggage !\nError detail: {ex.Message}", "Error");
            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (lvBookingBaggage.SelectedItem is not null)
                {
                    Baggage baggage = new Baggage
                    {
                        BookingId = Int32.Parse(txtBookingId.Text)
                    };

                    // Kiểm tra nếu có giá trị trong txtWeight
                    if (!string.IsNullOrWhiteSpace(txtWeight.Text))
                    {
                        decimal weight;


                        string normalizedInput = txtWeight.Text.Replace(',', '.');

                        if (decimal.TryParse(normalizedInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out weight))
                        {
                           
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
                                //MessageBox.Show( $"{(decimal?)weight}", "Thông báo");
                                baggage.Id = Int32.Parse(txtGid.Text);
                                baggage.Status = true;
                                baggage.WeightInKg = (decimal?)weight;
                                bagageServices.UpdateBaggage(baggage);
                                MessageBox.Show("Change weight baggage successfully !", "Notification");
                            }
                        }
                        //else
                        //{
                        //    MessageBox.Show("Vui lòng nhập một số hợp lệ (ví dụ: 11.5 hoặc 11) !", "Thông báo");
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Please enter the weight of baggage !", "Notification");
                    }
                }
                else
                {
                    MessageBox.Show("Please selected 1 row to change weight of baggage !", "Notification");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: cannot change the weight of baggage !\nError detail: {ex.Message}\nInner Exception: {ex.InnerException?.Message}", "Error");
            }
            finally
            {
                LoadAllBookingHasBaggage();
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvBookingBaggage.SelectedItem is not null)
                {
                    Baggage baggage = new Baggage();
                    baggage.Id = Int32.Parse(txtGid.Text);
                    baggage.Status = false;
                    baggage.WeightInKg = decimal.Parse(txtWeight.Text);
                    baggage.BookingId = Int32.Parse(txtBookingId.Text);
                    //MessageBox.Show($"{txtGid.Text}", "Thông báo");
                    bagageServices.DeleteBaggage(txtGid.Text);
                    
                } else
                {
                    MessageBox.Show("Please selected 1 row to delete information !", "Notification");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: cannot change weight of bagggage !\nError detail: {ex.Message}\nInner Exception: {ex.InnerException?.Message}", "Error");
            } finally
            {
                LoadAllBookingHasBaggage();
                MessageBox.Show("Restore successfully !", "Notification");
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
            txtGid.Text = "";
            LoadAllBookingHasBaggage();
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
                txtWeight.Text = GetPropertyValue(selectedItem, "Weight") ?? "N/A";
                txtGid.Text = GetPropertyValue(selectedItem, "Gid") ?? "N/A";
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
