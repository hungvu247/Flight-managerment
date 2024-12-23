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
using System.Windows.Shapes;

namespace FlightManagement
{
    /// <summary>
    /// Interaction logic for BookingPlatformHistory.xaml
    /// </summary>
    public partial class BookingPlatformHistory : Window
    {
        private readonly IBookingPlatformServices bookingPlatformServices;
        int currentPage = 1;
        int itemsPerPage = 17;

        public BookingPlatformHistory()
        {
            bookingPlatformServices = new BookingPlatformServices();
            InitializeComponent();
            LoadBookingPlatforms();
        }

        public void LoadBookingPlatforms()
        {
            try
            {
                lvBookingPlatform.ItemsSource = null;
                // Gán dữ liệu vào ListView
                var listPlatform = bookingPlatformServices.getBookingPlatformsDeactive();

                var pagedList = listPlatform
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

                lvBookingPlatform.ItemsSource = pagedList;

                // Cập nhật số trang và tổng số trang
                // Bạn có thể sử dụng pagedResult.TotalCount để cung cấp thông tin phân trang
                // ví dụ: TotalItemCount = pagedResult.TotalCount;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: Upload booking platform name is successfully !");
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadBookingPlatforms();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            LoadBookingPlatforms();
        }

        private void lvBookingPlatform_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvBookingPlatform.SelectedItem is BookingPlatform selectedPlatform)
            {
                txtBookingPlatformID.Text = selectedPlatform.Id.ToString();
                txtBookingPlatformName.Text = selectedPlatform.Name;
                txtBookingPlatformURL.Text = selectedPlatform.Url;
            }
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            txtBookingPlatformID.Text = null;
            txtBookingPlatformName.Text = null;
            txtBookingPlatformURL.Text = null;
        }


        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvBookingPlatform.SelectedItem is not null)
                {
                    bookingPlatformServices.Active(txtBookingPlatformID.Text);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Exception occurred: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            finally
            {
                LoadBookingPlatforms();
                System.Windows.MessageBox.Show("Restore successfully !");
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != null)
                {
                    lvBookingPlatform.ItemsSource = bookingPlatformServices.GetAllBookingPlatform()
                        .Where(name => name.Name.Contains(txtSearch.Text.Trim()) || name.Url.Contains(txtSearch.Text.Trim()))
                        .Where(p => p.Status == false)
                        .ToList();

                }
                else
                {
                    LoadBookingPlatforms();
                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show($"Exception occurred: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
        }
    }
}
