using ApplicationLayer.Interfaces;
using System;
using ApplicationLayer.Validation;
namespace ApplicationLayer
{
    public class ApplicationService : IDisposable
    {
        #region Atributes
        private IEmployeeService _employeeService;
        private IContractService _contractService;
        private IAdditionalDaysService _additionalDaysService;
        private IRequestService _requestService;
        private IAccountService _accountService;
        private ValidationService _validation;
        #endregion
        #region Constructors
        public ApplicationService()
        {
                
        }
        public ApplicationService(IEmployeeService employeeService, IContractService contractService, IAdditionalDaysService additionalDaysService, IRequestService requestService, IAccountService accountService, ValidationService validationService)
        {
            EmployeeService = employeeService;
            ContractService = contractService;
            AdditionalDaysService = additionalDaysService;
            RequestService = requestService;
            AccountService = accountService;
            Validation = validationService;
        }
        #endregion
        #region Properties
        public IEmployeeService EmployeeService
        {
            get
            {
                return _employeeService;
            }
            private set
            {
                _employeeService = value;
            }
        }

        public IContractService ContractService
        {
            get
            {
                return _contractService;
            }
            private set
            {
                _contractService = value;
            }
        }

        public IRequestService RequestService
        {
            get
            {
                return _requestService;
            }
            private set
            {
                _requestService = value;
            }
        }

        public IAdditionalDaysService AdditionalDaysService
        {
            get
            {
                return _additionalDaysService;
            }
            private set
            {
                _additionalDaysService = value;
            }
        }
        public IAccountService AccountService
        {
            get
            {
                return _accountService;
            }
            private set
            {
                _accountService = value;
            }
        }
        public ValidationService Validation
        {
            get
            {
                return _validation;
            }
            private set
            {
                _validation = value;
            }
        }
        #endregion
        #region Methods   
        public static ApplicationService Create()
        {
            return new ApplicationService();
        }
        public void Dispose()
        {
            return;
        }
        #endregion
    }
}
