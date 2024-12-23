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

namespace Team2_SE1824_FlightManager
{
    /// <summary>
    /// Interaction logic for AccountAuthenication.xaml
    /// </summary>
    public partial class AccountAuthenication : Window
    {
        private readonly IAccountService accountService;
        public AccountAuthenication()
        {
            InitializeComponent();
            accountService = new AccountService();
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            string code = passwordBox.Password;
            if (SaveAccount.listCode.Contains(code))
            {
                int index = SaveAccount.listCode.IndexOf(code);
                Account accountRegister = SaveAccount.listAccount[index];
                accountService.AddAccount(accountRegister);
                MessageBox.Show("Sign up successfully!");

                // remove after signing up successfully
                SaveAccount.listCode.Remove(code);
                SaveAccount.listAccount.Remove(accountRegister);

                // Display the login screen
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Make sure you have entered the correct code!");
            } 
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
