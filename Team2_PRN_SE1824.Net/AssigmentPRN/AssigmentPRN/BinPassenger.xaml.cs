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
using DataAccess.BussinessObjects;
using Services;

namespace FlightManagement
{
    /// <summary>
    /// Interaction logic for BinPassenger.xaml
    /// </summary>
    public partial class BinPassenger : Window, IDisposable
    {
        PassengerManagement passengerManagement;

        private readonly IPassengerService passengerService;
        private int currentPage = 1;
        private int itemsPerPage = 15;
        private int totalItems;
        private int totalPages;
        public BinPassenger(PassengerManagement passengerManagement1)
        {
            InitializeComponent();
            passengerService = new PassengerService();
            LoadPassengers();
            passengerManagement = passengerManagement1;
        }
        public void Dispose()
        {
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResults = passengerService.SearchPassengersByNameInactive(searchText);
                PassengerDataGrid.ItemsSource = searchResults;
            }
            else
            {
                LoadPassengers();
            }
        }

        private void Restore_click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassengerID.Text))
            {
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to restore this passenger?",
                    "Confirm Undo Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                // Kiểm tra kết quả của hộp thoại xác nhận
                if (result == MessageBoxResult.Yes)
                {
                    Passenger updatedPassenger = new Passenger
                    {
                        Id = int.Parse(txtPassengerID.Text),
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null,
                        Country = txtCountry.Text,
                        Email = txtEmail.Text,
                        Gender = ((ComboBoxItem)cbGender.SelectedItem).Content.ToString(),
                        Status = true
                    };

                    passengerService.UpdatePassenger(updatedPassenger);
                    LoadPassengers();
                    passengerManagement.LoadPassengers();
                }
                else
                {
                    MessageBox.Show("Operation cancelled.");
                }
                
            }

        }
        public void LoadPassengers()
        {
            try
            {
                var passengers = passengerService.GetPassengerInactive();
                totalItems = passengers.Count;
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                var pagedPassengers = passengers
                    .Skip((currentPage - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();

                PassengerDataGrid.ItemsSource = pagedPassengers;
                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdatePaginationControls()
        {
            PaginationPanel.Children.Clear();

            if (totalPages <= 10)
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    var button = new Button
                    {
                        Content = i.ToString(),
                        Margin = new Thickness(2),
                        Padding = new Thickness(5),
                        Tag = i
                    };
                    button.Click += PageButton_Click;
                    if (i == currentPage)
                    {
                        button.IsEnabled = false;
                    }
                    PaginationPanel.Children.Add(button);
                }
            }
            else
            {
                if (currentPage > 1)
                {
                    var prevButton = new Button
                    {
                        Content = "Previous",
                        Margin = new Thickness(2),
                        Padding = new Thickness(5),
                        Tag = currentPage - 1
                    };
                    prevButton.Click += PageButton_Click;
                    PaginationPanel.Children.Add(prevButton);
                }

                var firstPageButton = new Button
                {
                    Content = "1",
                    Margin = new Thickness(2),
                    Padding = new Thickness(5),
                    Tag = 1
                };
                firstPageButton.Click += PageButton_Click;
                PaginationPanel.Children.Add(firstPageButton);

                if (currentPage > 4)
                {
                    var ellipsis1 = new TextBlock
                    {
                        Text = "...",
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(2)
                    };
                    PaginationPanel.Children.Add(ellipsis1);
                }

                for (int i = Math.Max(2, currentPage - 2); i <= Math.Min(totalPages - 1, currentPage + 2); i++)
                {
                    var button = new Button
                    {
                        Content = i.ToString(),
                        Margin = new Thickness(2),
                        Padding = new Thickness(5),
                        Tag = i
                    };
                    button.Click += PageButton_Click;
                    if (i == currentPage)
                    {
                        button.IsEnabled = false;
                    }
                    PaginationPanel.Children.Add(button);
                }

                if (currentPage < totalPages - 3)
                {
                    var ellipsis2 = new TextBlock
                    {
                        Text = "...",
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(2)
                    };
                    PaginationPanel.Children.Add(ellipsis2);
                }

                var lastPageButton = new Button
                {
                    Content = totalPages.ToString(),
                    Margin = new Thickness(2),
                    Padding = new Thickness(5),
                    Tag = totalPages
                };
                lastPageButton.Click += PageButton_Click;
                PaginationPanel.Children.Add(lastPageButton);

                if (currentPage < totalPages)
                {
                    var nextButton = new Button
                    {
                        Content = "Next",
                        Margin = new Thickness(2),
                        Padding = new Thickness(5),
                        Tag = currentPage + 1
                    };
                    nextButton.Click += PageButton_Click;
                    PaginationPanel.Children.Add(nextButton);
                }
            }
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag.ToString(), out int page))
            {
                currentPage = page;
                LoadPassengers();
            }
        }

        private void PassengerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PassengerDataGrid.SelectedItem is Passenger selectedPassenger)
            {
                txtPassengerID.Text = selectedPassenger.Id.ToString();
                txtFirstName.Text = selectedPassenger.FirstName;
                txtLastName.Text = selectedPassenger.LastName;
                dpDateOfBirth.SelectedDate = selectedPassenger.DateOfBirth.HasValue ? selectedPassenger.DateOfBirth.Value.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null;
                txtCountry.Text = selectedPassenger.Country;
                txtEmail.Text = selectedPassenger.Email;
                SetSelectedGender(selectedPassenger.Gender);
            }
            else
            {
                ClearFields();
            }
        }
        private void SetSelectedGender(string gender)
        {
            foreach (ComboBoxItem item in cbGender.Items)
            {
                if (item.Content.ToString() == gender)
                {
                    cbGender.SelectedItem = item;
                    break;
                }
            }
        }

        private void ClearFields()
        {
            txtPassengerID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            dpDateOfBirth.SelectedDate = null;
            txtCountry.Clear();
            txtEmail.Clear();
            cbGender.SelectedIndex = -1;
        }
    }
}
