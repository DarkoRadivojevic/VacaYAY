using BusinessLayer.BusinessEntity;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Interfaces
{
    public interface IAccountWorkflow
    {
        Task AccountRegister(string accountEmail, string accountPassowrd, EmployeeEntity employeeEntity);
        Task<bool> AccountValidateEmail(string accountEmail);
        Task AccountChangeRole(Guid employeeUID, string employeeRole);
        Task<bool> AccountLogin(string accountEmail, string accountPassowrd, bool accountPersist);
        void AccountLogout(string accountCookie);
        Task<bool> AccountConfirmAccount(int accountID, string accountToken);
        Task AccountForgotPassword(string accountEmail);
        Task AccountResetPassword(string accountEmail, string accountPassword, string accountToken);
    }
}
