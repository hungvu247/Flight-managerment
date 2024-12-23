using AssigmentPRN;
using DataAccess.BussinessObjects;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IAccountService _accountService;
        public LoginWindow()
        {
            InitializeComponent();

            _accountService = new AccountService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtUser.Text;
            string password = txtPass.Password;                                       

            if (email.Equals("Admin@gmail.com") && password.Equals("@admin123"))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (email.Equals("Staff@gmail.com") && password.Equals("@staff123"))
            {
                StaffHome staffHome = new StaffHome();
                staffHome.Show();
                this.Close();
            }
            else
            {
                Account? account = _accountService.GetAccountByEmailAndPassword(email, password);

                if (account != null)
                {
                    
                    if(account.MemberId == "Staff")
                    {
                        StaffHome staff = new StaffHome();
                        staff.Show();
                    }
                    else
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.");
                }
            }

        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }
    }
}
