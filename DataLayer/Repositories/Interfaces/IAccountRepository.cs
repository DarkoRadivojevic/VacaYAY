using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{ 
    public interface IAccountRepository
    {
        Task<int> AccountRegister(string accountEmail, string accountPassowrd, string accountRole);
        Task<bool> AccountValidateEmail(string accountEmail);
        Task<string> AccountGetAccountRole(Guid employeeUID);
        Task AccountChangeRole(Guid employeeUID, string employeeRole);
        Task<bool> AccountLogin(string accountEmail, string accountPassword, bool accountPersist);
        void AccountLogout(string accountCookie);
        Task<bool> AccountConfrimAccount(int accountID, string accountToken);
        Task AccountForgotPassword(string accountEmail);
        Task AccountResetPassword(string accountEmail, string accountPassword, string accountToken);
    }
}
