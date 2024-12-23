using Azure;
using DataAccess.BussinessObjects;
using DataAccessLayer;
using FlightManagement;
using Microsoft.Win32;
using OfficeOpenXml;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Team2_SE1824_FlightManager
{
    /// <summary>
    /// Interaction logic for BookingPlatformWindow.xaml
    /// </summary>
    public partial class BookingPlatformWindow : Window
    {
        private readonly IBookingPlatformServices bookingPlatformServices;
        int currentPage = 1;
        int itemsPerPage = 17;
        public BookingPlatformWindow()
        {
            bookingPlatformServices = new BookingPlatformServices();
            InitializeComponent();
            LoadBookingPlatforms();


        }
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Tổng chiều rộng của ListView trừ đi padding và border nếu có
            //double totalWidth = lvBookingPlatform.ActualWidth - 10; // Trừ đi khoảng cách padding và border

            //// Phân chia đều chiều rộng giữa các cột
            //var gridView = lvBookingPlatform.View as GridView;
            //if (gridView != null)
            //{
            //    double columnWidth = totalWidth / gridView.Columns.Count;
            //    foreach (var column in gridView.Columns)
            //    {
            //        column.Width = columnWidth;
            //    }
            //}
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

        private void LoadBookingPlatforms()
        {
            try
            {
                lvBookingPlatform.ItemsSource = null;

                var listPlatform = bookingPlatformServices.GetAllBookingPlatform();
                
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
                System.Windows.MessageBox.Show("Errors: Upload booking platform is fail !");
            }
        }


        

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            txtBookingPlatformID.Text = null;
            txtBookingPlatformName.Text = null;
            txtBookingPlatformURL.Text = null;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy tên nền tảng từ TextBox hoặc từ một control khác trong giao diện
                string platformNameName = txtBookingPlatformName.Text.Trim();

                // Validate tên nền tảng
                if (ValidateBookingPlatformName(platformNameName))
                {
                    if(!bookingPlatformServices.IsDuplicateBookingPlatformName(platformNameName) && !bookingPlatformServices.IsDuplicateBookingPlatformUrl(txtBookingPlatformURL.Text))
                    {
                        // Thêm nền tảng đặt vé thành công
                        BookingPlatform bookingPlatform = new BookingPlatform();
                        bookingPlatform.Id = bookingPlatformServices.GetCurrentID() + 1;
                        bookingPlatform.Name = platformNameName;
                        bookingPlatform.Url = txtBookingPlatformURL.Text;
                        bookingPlatform.Status = true;

                        //FlightManagementDbContext.BookingPlatforms.Add(bookingPlatform);
                        bookingPlatformServices.InsertBookingPlatform(bookingPlatform);
                        System.Windows.MessageBox.Show("Add new platofrm successfully !");
                    } else
                    {
                        if(bookingPlatformServices.IsDuplicateBookingPlatformName(platformNameName) && !bookingPlatformServices.IsDuplicateBookingPlatformUrl(txtBookingPlatformURL.Text))
                        {
                            System.Windows.MessageBox.Show("Booking platform name is already exist !");
                        }
                        if(!bookingPlatformServices.IsDuplicateBookingPlatformName(platformNameName) && bookingPlatformServices.IsDuplicateBookingPlatformUrl(txtBookingPlatformURL.Text))
                        {
                            System.Windows.MessageBox.Show("Url is already exist !");
                        }
                        if(bookingPlatformServices.IsDuplicateBookingPlatformName(platformNameName) && bookingPlatformServices.IsDuplicateBookingPlatformUrl(txtBookingPlatformURL.Text))
                        {
                            System.Windows.MessageBox.Show("Booking platform name and url is already exist !");
                        }
                    }
                    // Thực hiện các hành động khác sau khi thêm thành công
                }
                else
                {
                    // Tên nền tảng không hợp lệ, hiển thị thông báo lỗi
                    throw new Exception("Booking platform name is invalid!");
                    
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error: Add new booking platform name fail !");
            }
            finally
            {
                //System.Windows.MessageBox.Show("Thêm nền tảng đặt vé thành công !");
                LoadBookingPlatforms();
            }

        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBookingPlatformID.Text is not null)
                {
                    if(ValidateBookingPlatformName(txtBookingPlatformName.Text))
                    {
                        if (!bookingPlatformServices.CheckExistName(txtBookingPlatformID.Text, txtBookingPlatformName.Text) && !bookingPlatformServices.CheckExistUrl(txtBookingPlatformID.Text, txtBookingPlatformURL.Text))
                        {
                            var bookingPltform = bookingPlatformServices.GetBookingPlatformById(txtBookingPlatformID.Text);
                            bookingPltform.Id = Int32.Parse(txtBookingPlatformID.Text);
                            bookingPltform.Name = txtBookingPlatformName.Text;
                            bookingPltform.Url = txtBookingPlatformURL.Text;
                            bookingPltform.Status = true;
                            bookingPlatformServices.UpdateBookingPlatform(bookingPltform);
                        }else 
                        {
                            if(bookingPlatformServices.CheckExistName(txtBookingPlatformID.Text, txtBookingPlatformName.Text) && !bookingPlatformServices.CheckExistUrl(txtBookingPlatformID.Text, txtBookingPlatformURL.Text))
                            {
                                System.Windows.MessageBox.Show("Booking platform name is duplicated !");
                            }
                            if(!bookingPlatformServices.CheckExistName(txtBookingPlatformID.Text, txtBookingPlatformName.Text) && bookingPlatformServices.CheckExistUrl(txtBookingPlatformID.Text, txtBookingPlatformURL.Text))
                            {
                                System.Windows.MessageBox.Show("Url is already exist !");
                            }
                            if (bookingPlatformServices.CheckExistName(txtBookingPlatformID.Text, txtBookingPlatformName.Text) && bookingPlatformServices.CheckExistUrl(txtBookingPlatformID.Text, txtBookingPlatformURL.Text))
                            {
                                System.Windows.MessageBox.Show("Booking platform name and Url is duplicated !");
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid format.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Exception occurred: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            finally
            {
                LoadBookingPlatforms();
                System.Windows.MessageBox.Show("Change booking platform name successfully!");

            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtBookingPlatformID.Text != null)
                {
                    bookingPlatformServices.DeleteBookingPlatform(txtBookingPlatformID.Text);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Exception occurred: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
            }
            finally
            {
                LoadBookingPlatforms();
                System.Windows.MessageBox.Show("Delete Successfully !");
            }
        }

        private void lvBookingPlatform_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvBookingPlatform.SelectedItem is not null)
            {
                int seletedId = ((BookingPlatform)lvBookingPlatform.SelectedItem).Id;
                var BookingPlatform = (BookingPlatform)lvBookingPlatform.SelectedItem;
                txtBookingPlatformID.Text = seletedId.ToString();
                txtBookingPlatformName.Text = BookingPlatform.Name;
                txtBookingPlatformURL.Text = BookingPlatform.Url;
            }
        }

        private bool ValidateBookingPlatformName(string name)
        {
            // Kiểm tra null hoặc chuỗi rỗng
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            // Kiểm tra độ dài (từ 3 đến 50 ký tự)
            if (name.Length < 3 || name.Length > 50)
            {
                return false;
            }

            // Kiểm tra ký tự hợp lệ (chỉ chữ cái, số, dấu gạch dưới và khoảng trắng)
            if (!Regex.IsMatch(name, @"^[a-zA-Z0-9 _-]*$"))
            {
                return false;
            }

            // Kiểm tra không có khoảng trắng ở đầu và cuối chuỗi
            if (name.Trim() != name)
            {
                return false;
            }

            // Kiểm tra không có quá nhiều ký tự đặc biệt liên tiếp
            if (Regex.IsMatch(name, @"\s{2,}") || Regex.IsMatch(name, @"[_-]{2,}"))
            {
                return false;
            }

            // Kiểm tra chống XSS (Cross-Site Scripting)
            if (name.Contains("<") || name.Contains(">") || name.Contains("&") ||
                name.Contains("'") || name.Contains("\""))
            {
                return false;
            }

            return true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearch.Text != null)
                {
                    lvBookingPlatform.ItemsSource = bookingPlatformServices.GetAllBookingPlatform()
                        .Where(name => name.Name.Contains(txtSearch.Text.Trim()) || name.Url.Contains(txtSearch.Text.Trim()))
                        .Where(p => p.Status  == true)
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
        private void Active_Booking_Platform_Button_Click(object sender, RoutedEventArgs e)
        {
            var currentWindow = this;

            
            BookingPlatformHistory bookingPlatformWindow = new BookingPlatformHistory
            {
                WindowState = WindowState.Maximized
            };
            bookingPlatformWindow.Show();

            
            bookingPlatformWindow.Closed += (s, args) =>
            {
                
                LoadBookingPlatforms();
                currentWindow.Show();
            };

            currentWindow.Hide();

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
                saveFileDialog.FileName = "BookingPlatform.xlsx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Tạo file Excel mới
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        // Tạo một worksheet
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Flights");
                        var headers = new[] { "Mã", "Tên nền tảng", "Đường đãn url" };
                        // Lấy tiêu đề từ DataGrid
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = headers[i].ToString();
                        }

                        // Lấy dữ liệu từ cơ sở dữ liệu
                        var flights = bookingPlatformServices.GetAllBookingPlatform();

                        // Ghi dữ liệu vào file Excel, bắt đầu từ hàng thứ 2
                        int row = 2;
                        foreach (var flight in flights)
                        {
                            worksheet.Cells[row, 1].Value = flight.Id;
                            worksheet.Cells[row, 2].Value = flight.Name ?? "";
                            worksheet.Cells[row, 3].Value = flight.Url ?? "";
                            
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
