using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces;
using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System;
using System.Threading.Tasks;

namespace ApplicationLayer.Implementations
{
    public class AccountService : IAccountService
    {
        #region Atributes 
        private IAccountWorkflow _accountWorkflow;
        #endregion
        #region Constructors
        public AccountService()
        {

        }
        public AccountService(IAccountWorkflow accountWorkflow)
        {
            AccountWorkflow = accountWorkflow;
        }
        #endregion
        #region Properties
        public IAccountWorkflow AccountWorkflow
        {
            get
            {
                return _accountWorkflow;
            }
            private set
            {
                _accountWorkflow = value;   
            }
        }
        #endregion
        #region Methods
        public async Task AccountRegister(string accountEmail, string accountPassowrd, ApplicationEmployee applicationEmployee)
        {
            var employee = new EmployeeEntity()
            {
                EmployeeUID = Guid.NewGuid(),
                EmployeeName = applicationEmployee.EmployeeName,
                EmployeeSurname = applicationEmployee.EmployeeSurname,
                EmployeeEmploymentDate = (DateTime)applicationEmployee.EmployeeEmploymentDate,
                EmployeCreatedOn = DateTime.UtcNow,
                EmployeeBacklogDays = 0,
                EmployeeCardIDNumber = applicationEmployee.EmployeeCardIDNumber,
                EmployeeRole = applicationEmployee.EmployeeRole
            };

            await AccountWorkflow.AccountRegister(accountEmail, accountPassowrd, employee);
        }
        public async Task<bool> AccountLogin(string accountEmail, string accountPassword, bool accountPersisit)   
        {
            return await AccountWorkflow.AccountLogin(accountEmail, accountPassword, accountPersisit);
        }
        public void  AccountLogout(string cookie)
        {
            AccountWorkflow.AccountLogout(cookie);
        }

        public async Task<bool> AccountConfirmAccount(int accountID, string token)
        {
            return await AccountWorkflow.AccountConfirmAccount(accountID, token);
        }
        public async Task AccountForgotPassword(string accountEmail)
        {
            await AccountWorkflow.AccountForgotPassword(accountEmail);
        }

        public async Task AccountResestPassword(string accountEmail, string accountPawwsord, string token)
        {
            await AccountWorkflow.AccountResetPassword(accountEmail, accountPawwsord, token);
        }
        #endregion
    }
}
