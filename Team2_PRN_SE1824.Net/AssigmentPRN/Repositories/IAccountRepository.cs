using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{

    public interface IAccountRepository
    {
    
        Account? GetAccountById(string id);
        Account? GetAccountByEmailAndPassword(string email, string password);

        Account? GetAccountByEmail(string email);
        void AddAccount(Account account);
    }
}
