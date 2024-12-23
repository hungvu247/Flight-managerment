using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using DataAccess.BussinessObjects;
using Services;

namespace FlightManagement
{
    public partial class AirlineManagement : Window
    {
        private readonly IAirlineService airlineService;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalItems;
        private int totalPages;

        public AirlineManagement()
        {
            InitializeComponent();
            airlineService = new AirlineService();
            DisplayNextAirlineId();
            LoadAirlines();
        }

        public void LoadAirlines()
        {
            try
            {
                var airlines = airlineService.GetActiveAirlines();
                totalItems = airlines.Count;
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                var pagedAirlines = airlines
                    .Skip((currentPage - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList();

                AirlineDataGrid.ItemsSource = pagedAirlines;
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
                LoadAirlines();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidNameOrCountry(txtAirlineName.Text))
            {
                MessageBox.Show("Airline Name is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidNameOrCountry(txtAirlineCountry.Text))
            {
                MessageBox.Show("Country is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsCodeUnique(txtAirlineCode.Text))
            {
                MessageBox.Show("Airline Code must be unique.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Airline newAirline = new Airline
            {
                Id = airlineService.GetLatestAirlineId() + 1,
                Code = txtAirlineCode.Text,
                Name = txtAirlineName.Text,
                Country = txtAirlineCountry.Text,
                Status = true
            };

            airlineService.InsertAirline(newAirline);
            LoadAirlines();
            DisplayNextAirlineId();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidNameOrCountry(txtAirlineName.Text))
            {
                MessageBox.Show("Airline Name is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsValidNameOrCountry(txtAirlineCountry.Text))
            {
                MessageBox.Show("Country is invalid. It should not contain numbers.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(txtAirlineID.Text))
            {
                Airline updatedAirline = new Airline
                {
                    Id = int.Parse(txtAirlineID.Text),
                    Code = txtAirlineCode.Text,
                    Name = txtAirlineName.Text,
                    Country = txtAirlineCountry.Text,
                    Status = true
                };

                airlineService.UpdateAirline(updatedAirline);
                LoadAirlines();
                DisplayNextAirlineId();
            }
        }

        private bool IsValidNameOrCountry(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
        }

        private bool IsCodeUnique(string code)
        {
            code= code.ToUpper();
            return !airlineService.GetAirlines().Any(a => a.Code == code);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResults = airlineService.SearchAirlinesByNameActive(searchText);
                AirlineDataGrid.ItemsSource = searchResults;
            }
            else
            {
                LoadAirlines();
            }
        }

        private void AirlineDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AirlineDataGrid.SelectedItem is Airline selectedAirline)
            {
                txtAirlineID.Text = selectedAirline.Id.ToString();
                txtAirlineCode.Text = selectedAirline.Code;
                txtAirlineName.Text = selectedAirline.Name;
                txtAirlineCountry.Text = selectedAirline.Country;
            }
            else
            {
                ClearFields();
            }
        }

        private void ClearFields()
        {
            txtAirlineID.Clear();
            txtAirlineCode.Clear();
            txtAirlineName.Clear();
            txtAirlineCountry.Clear();
        }

        

        private void DisplayNextAirlineId()
        {
            int nextId = airlineService.GetLatestAirlineId() + 1;
            txtAirlineID.Text = nextId.ToString();
        }

        private void Clear_click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            DisplayNextAirlineId();
        }

        private void Bin_click(object sender, RoutedEventArgs e)
        {
            using (BinAirline trashBinWindow = new BinAirline(this))
            {
                trashBinWindow.ShowDialog();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAirlineID.Text))
            {
                // Hiển thị hộp thoại xác nhận
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to undo delete this airline?",
                    "Confirm Undo Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                // Kiểm tra kết quả của hộp thoại xác nhận
                if (result == MessageBoxResult.Yes)
                {
                    Airline updatedAirline = new Airline
                    {
                        Id = int.Parse(txtAirlineID.Text),
                        Code = txtAirlineCode.Text,
                        Name = txtAirlineName.Text,
                        Country = txtAirlineCountry.Text,
                        Status = false
                    };

                    airlineService.UpdateAirline(updatedAirline);
                    LoadAirlines();
                    DisplayNextAirlineId();
                }
                else
                {
                    MessageBox.Show("Operation cancelled.");
                }
                
            }
        }
    }
}
