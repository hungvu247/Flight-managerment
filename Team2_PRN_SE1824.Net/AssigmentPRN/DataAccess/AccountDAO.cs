using BCrypt.Net;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO
    {
        private static List<Account> getAccounts()
        {
            List<Account> accounts = new List<Account>();
            FlightManagementDbContext storeContext = new FlightManagementDbContext();

            accounts = storeContext.Accounts.ToList();
            return accounts;
        }
        public static Account? GetAccountById(string id)
        {
            Account? accountMember = new Account();

            FlightManagementDbContext storeContext = new FlightManagementDbContext();
            accountMember = storeContext.Accounts.Find(id);
            return accountMember;
        }
        public static Account? GetAccountByEmailAndPassword(string email, string password)
        {
            Account? accountMember = new Account();

            // Initialize your DbContext
            using (FlightManagementDbContext storeContext = new FlightManagementDbContext())
            {
                // Use LINQ to query the database
                accountMember = storeContext.Accounts
                                .FirstOrDefault(acc => acc.Email.Equals(email));

                if (accountMember != null && BCrypt.Net.BCrypt.EnhancedVerify(password, accountMember.PassWord) == true)
                {
                    return accountMember;
                }
            }

            return null;
        }

        public static Account? GetAccountByEmail(string email)
        {
            FlightManagementDbContext storeContext = new FlightManagementDbContext();
            Account? account = storeContext.Accounts
                .Where(acc => acc.Email.Equals(email))
                .FirstOrDefault();
            return account;
        }
        public static void AddAccount(Account account)
        {
            FlightManagementDbContext storeContext = new FlightManagementDbContext();

            // Encrypt password by Bcrypt
            account.PassWord = BCrypt.Net.BCrypt.EnhancedHashPassword(account.PassWord);
            storeContext.Accounts.Add(account);
            storeContext.SaveChanges();
        }
    }
}
