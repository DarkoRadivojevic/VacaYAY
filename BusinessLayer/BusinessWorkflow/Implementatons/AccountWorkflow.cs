using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using DataLayer;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Implementatons
{
    public class AccountWorkflow : IAccountWorkflow
    {
        #region Atributes
        private IAccountRepository _accountRepository;
        private IEmployeeRepository _employeeRepository;
        private IEmployeeWorkflow _employeeWorkflow;
        #endregion
        #region Constructors
        public AccountWorkflow()
        {

        }
        public AccountWorkflow(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEmployeeWorkflow employeeWorkflow)
        {
            AccountRepository = accountRepository;
            EmployeeRepository = employeeRepository;
            EmployeeWorkflow = employeeWorkflow;
        }
        #endregion
        #region Properties
        public IAccountRepository AccountRepository
        {
            get
            {
                return _accountRepository;
            }
            private set
            {
                _accountRepository = value;
            }
        }
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository;
            }
            private set
            {
                _employeeRepository = value;
            }
        }
        public IEmployeeWorkflow EmployeeWorkflow
        {
            get
            {
                return _employeeWorkflow;
            }
            private set
            {
                _employeeWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task AccountRegister(string accountEmail, string accountPassowrd, EmployeeEntity employeeEntity)
        {
            var result = await AccountRepository.AccountRegister(accountEmail, accountPassowrd, employeeEntity.EmployeeRole);

            await EmployeeWorkflow.EmployeeAddEmployee(result, employeeEntity);

        }
        public async Task<bool> AccountValidateEmail(string accountEmail)
        {
            return await AccountRepository.AccountValidateEmail(accountEmail);
        }
        public async Task AccountChangeRole(Guid employeeUID, string employeeRole)
        {
            await AccountRepository.AccountChangeRole(employeeUID, employeeRole);
        }
        public async Task<bool> AccountLogin(string accountEmail, string accountPassword, bool accountPersist)
        {
            return await AccountRepository.AccountLogin(accountEmail, accountPassword, accountPersist);
        }
        public void AccountLogout(string accountCookie)
        {
            AccountRepository.AccountLogout(accountCookie);
        }

        public async Task<bool> AccountConfirmAccount(int accountID, string token)
        {
            return await AccountRepository.AccountConfrimAccount(accountID, token);
        }
        public async Task AccountForgotPassword(string accountEmail)
        {
            await AccountRepository.AccountForgotPassword(accountEmail);
        }

        public async Task AccountResetPassword(string accountEmail, string accountPassword, string accountToken)
        {
            await AccountRepository.AccountResetPassword(accountEmail, accountPassword, accountToken);
        }
        #endregion
    }
}
