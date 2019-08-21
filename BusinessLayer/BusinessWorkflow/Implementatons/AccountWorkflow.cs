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
        #endregion
        #region Constructors
        public AccountWorkflow()
        {

        }
        public AccountWorkflow(IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
        {
            AccountRepository = accountRepository;
            EmployeeRepository = employeeRepository;
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
        #endregion
        #region Methods
        public async Task AccountRegister(string accountEmail, string accountPassowrd, EmployeeEntity employeeEntity)
        {
            var result = await AccountRepository.AccountRegister(accountEmail, accountPassowrd, employeeEntity.EmployeeRole);

            var employee = new Employee()
            {
                EmlpoyeeCardIDNumber = employeeEntity.EmployeeCardIDNumber,
                EmployeeID = result,
                EmployeeUID = Guid.NewGuid(),
                EmployeeCreatedOn = DateTime.UtcNow,
                EmployeeBacklogDays = 0,
                EmployeeEmploymentDate = employeeEntity.EmployeeEmploymentDate,
                EmployeeName = employeeEntity.EmployeeName,
                EmployeeSurname = employeeEntity.EmployeeSurname
            };
            await EmployeeRepository.EmlpoyeeInsert(employee);
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
