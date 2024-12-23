using DataAccess.BussinessObjects;

using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using System.Windows.Shapes;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Team2_SE1824_FlightManager
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly IAccountService accountService;

        public Register()
        {
            InitializeComponent();
            accountService = new AccountService(); 
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void sendEmail(string receiver, Account accountRegister)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Flight Management", "lhmsswp391@gmail.com"));
            email.To.Add(new MailboxAddress("User", accountRegister.Email));

            email.Subject = "Account Authenication";

            // Create the corresponding code and account 
            string authenicationCode = generateOTP();   
            // If code is existing then generate a new code
            while (CodeIsExisting(authenicationCode))
            {
                authenicationCode = generateOTP();
            }
            // If the account already exists, delete the corresponding code and account 
            checkAccountIsExisting(accountRegister);
            // save both into list
            SaveAccount.listCode.Add(authenicationCode);
            SaveAccount.listAccount.Add(accountRegister);

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<b>Your authentication code is: {authenicationCode}</b>"
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("lhmsswp391@gmail.com", "swpkteujuqnauatb");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }

        private void checkAccountIsExisting(Account accountRegister) 
        {
            if (SaveAccount.listAccount.Contains(accountRegister))
            {
                int index = SaveAccount.listAccount.IndexOf(accountRegister);
                string code = SaveAccount.listCode[index];
                
                SaveAccount.listAccount.Remove(accountRegister);
                SaveAccount.listCode.Remove(code);
            }
        }

        private bool CodeIsExisting(string code)
        {
            return SaveAccount.listCode.Contains(code);
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = passwordBox.Password;
            string rePassword = rePasswordBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(rePassword))
            {
                MessageBox.Show("Please fill in all information when registering");
                return;
            }


            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter the correct email syntax");
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Enter a password with length >= 8 and <= 32 characters, " +
                    "with at least 1 lowercase letter, 1 uppercase letter, 1 number and 1 special character");
                return;
            }

            if (!rePassword.Equals(password))
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            if (IsAccountExist(email))
            {
                MessageBox.Show("The email has been registered by another account");
                return;
            }

            Account accountRegister = new Account()
            {
                Email = email,
                PassWord = password,
                MemberId = "Staff",
            };

            sendEmail(email, accountRegister);

            AccountAuthenication accountAuthenication = new AccountAuthenication();
            accountAuthenication.Show();
            this.Close();
        }

        private string generateOTP()
        {
            string rs = "";
            Random random = new Random();

            for (int i = 1; i <= 6; i++)
            {
                rs += random.Next(0, 10) + ""; // Tạo số ngẫu nhiên từ 0 đến 9
            }

            return rs;
        }

        private bool IsAccountExist(string email)
        {
            return accountService.GetAccountByEmail(email) != null;
        }

        private bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,32}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
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

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private void reTextPassword_MouseDown(object sender, MouseButtonEventArgs e) 
        {
            rePasswordBox.Focus();
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
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

        private void rePasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
            {
                textRePassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textRePassword.Visibility = Visibility.Visible;
            }
        }
    }
}
