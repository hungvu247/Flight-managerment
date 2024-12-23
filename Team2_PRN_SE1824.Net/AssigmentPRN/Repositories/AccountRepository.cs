using DataAccess;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account? GetAccountById(string id) => AccountDAO.GetAccountById(id);
        public Account? GetAccountByEmailAndPassword(string email, string password) => AccountDAO.GetAccountByEmailAndPassword(email, password);

        public Account? GetAccountByEmail(string email) => AccountDAO.GetAccountByEmail(email);

        public void AddAccount(Account account)
        {
            AccountDAO.AddAccount(account);
        }
    }
}
