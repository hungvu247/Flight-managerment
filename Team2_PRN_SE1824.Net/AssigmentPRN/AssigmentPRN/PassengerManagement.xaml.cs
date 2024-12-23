using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using DataAccess.BussinessObjects;
using Services;

namespace FlightManagement
{
    public partial class PassengerManagement : Window
    {
        private readonly IPassengerService passengerService;
        private int currentPage = 1;
        private int itemsPerPage = 15;
        private int totalItems;
        private int totalPages;
        

        public PassengerManagement()
        {
            InitializeComponent();
            passengerService = new PassengerService();
            
            DisplayNextPassengerId();
            LoadPassengers();
        }

        public void LoadPassengers(string gender = null, string country = null)
        {
            try
            {
                var passengers = passengerService.GetPassengerActive();

                if (!string.IsNullOrEmpty(gender) && gender != "All")
                {
                    passengers = passengers.Where(p => p.Gender == gender).ToList();
                }

                if (!string.IsNullOrEmpty(country))
                {
                    passengers = passengers.Where(p => p.Country.IndexOf(country, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidNameOrCountry(txtFirstName.Text))
            {
                MessageBox.Show("First Name is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidNameOrCountry(txtLastName.Text))
            {
                MessageBox.Show("Last Name is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidNameOrCountry(txtCountry.Text))
            {
                MessageBox.Show("Country is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email is invalid.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Passenger newPassenger = new Passenger
            {
                Id = passengerService.GetLatestPassengerId() + 1,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null,
                Country = txtCountry.Text,
                Email = txtEmail.Text,
                Gender = ((ComboBoxItem)cbGender.SelectedItem).Content.ToString(),
                Status = true
            };

            passengerService.InsertPassenger(newPassenger);
            LoadPassengers();
            DisplayNextPassengerId();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidNameOrCountry(txtFirstName.Text))
            {
                MessageBox.Show("First Name is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidNameOrCountry(txtLastName.Text))
            {
                MessageBox.Show("Last Name is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidNameOrCountry(txtCountry.Text))
            {
                MessageBox.Show("Country is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email is invalid.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(txtPassengerID.Text))
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
                DisplayNextPassengerId();
            }
        }

        private bool IsValidNameOrCountry(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResults = passengerService.SearchPassengersByNameActive(searchText);
                PassengerDataGrid.ItemsSource = searchResults;
            }
            else
            {
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

        

        private void DisplayNextPassengerId()
        {
            int nextId = passengerService.GetLatestPassengerId() + 1;
            txtPassengerID.Text = nextId.ToString();
        }

        private void Clear_click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            DisplayNextPassengerId();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassengerID.Text))
            {
                // Hiển thị hộp thoại xác nhận
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to undo delete this passenger?",
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
                        Status = false
                    };

                    passengerService.UpdatePassenger(updatedPassenger);
                    LoadPassengers();
                    DisplayNextPassengerId();
                }
                else
                {
                    MessageBox.Show("Operation cancelled.");
                }
                
            }
        }

        private void Bin_Click(object sender, RoutedEventArgs e)
        {
            using (BinPassenger binPassenger = new BinPassenger(this))
            {
                binPassenger.ShowDialog();
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            string selectedGender = filterGender.SelectedItem != null ? (filterGender.SelectedItem as ComboBoxItem).Content.ToString() : null;
            string country = txtFilterCountry.Text;

            LoadPassengers(selectedGender, country);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            filterGender.SelectedIndex = -1;
            txtFilterCountry.Clear();
            LoadPassengers();
        }
    }
}
