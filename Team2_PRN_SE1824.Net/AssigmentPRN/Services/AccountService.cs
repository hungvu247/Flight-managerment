using DataAccess.BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepository _accountService;   
        public AccountService()
        {
            _accountService = new AccountRepository();
        }
        public Account? GetAccountById(string id)
        {
            return _accountService.GetAccountById(id);  
        }
        public Account? GetAccountByEmailAndPassword(string email, string password)
        {
            return _accountService.GetAccountByEmailAndPassword(email, password);
        }
        public Account? GetAccountByEmail(string email)
        {
            return _accountService.GetAccountByEmail(email);
        }

        public void AddAccount(Account account)
        {
            _accountService.AddAccount(account);
        }
    }
}
