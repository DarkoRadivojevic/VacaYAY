using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using DataLayer;
using SolutionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Implementatons
{

    public class EmployeeWorkflow : IEmployeeWorkflow
    {
        #region Atributes
        private IEmployeeRepository _employeeRepository;
        private IContractRepository _contractRepository;
        private IRequestRepository _requestRepository;
        private IAdditionalDaysRepository _additionalDaysRepository;
        private IAccountRepository _accountRepository;
        #endregion
        #region Constructors
        public EmployeeWorkflow()
        {
        }
        public EmployeeWorkflow(IEmployeeRepository employeeRepository, IContractRepository contractRepository, IRequestRepository requestRepository, IAdditionalDaysRepository additionalDaysOffRepository, IAccountRepository accountRepository)
        {
            EmployeeRepository = employeeRepository;
            ContractRepository = contractRepository;
            RequestRepository = requestRepository;
            AdditionalDaysRepository = additionalDaysOffRepository;
            AccountRepository = accountRepository;
        }
        #endregion
        #region Properties
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
        public IContractRepository ContractRepository
        {
            get
            {
                return _contractRepository;
            }
            private set
            {
                _contractRepository = value;
            }
        }
        public IRequestRepository RequestRepository
        {
            get
            {
                return _requestRepository;
            }
            private set
            {
                _requestRepository = value;
            }
        }
        public IAdditionalDaysRepository AdditionalDaysRepository
        {
            get
            {
                return _additionalDaysRepository;
            }
            private set
            {
                _additionalDaysRepository = value;
            }
        }
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
        #endregion
        #region Methods
        public async Task EmployeeDeleteEmployee(Guid employeeUID)
        {
            await EmployeeRepository.EmlpoyeeDelete(employeeUID);

            var requests = await RequestRepository.RequestGetAllEmployeeRequests(employeeUID);
            requests.Select(async x => await RequestRepository.RequestDelete(x.RequestUID));

            var contracts = await ContractRepository.ContractsGetContractByEmployee(employeeUID);
            contracts.Select(async x => await ContractRepository.ContractDelete(x.ContractUID));

            var additionalDays = await AdditionalDaysRepository.AdditonalDaysGetAllAdditionalDays(employeeUID);
            additionalDays.Select(async x => await AdditionalDaysRepository.AdditonalDaysDelete(x.AdditionaDaysUID));
        }

        public async Task EmployeeEditEmployee(EmployeeEntity employeeEntitiy, string employeeRole)
        {
            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeEntitiy.EmployeeUID);

            if (employeeEntitiy.EmployeeName != null)
                employee.EmployeeName = employeeEntitiy.EmployeeName;

            if (employeeEntitiy.EmployeeSurname != null)
                employee.EmployeeSurname = employeeEntitiy.EmployeeSurname;

            if (employeeEntitiy.EmployeeCardIDNumber != null)
                employee.EmlpoyeeCardIDNumber = employeeEntitiy.EmployeeCardIDNumber;

            if (employeeEntitiy.EmployeeEmploymentDate != DateTime.MinValue)
                employee.EmployeeEmploymentDate = (DateTime)employeeEntitiy.EmployeeEmploymentDate;


            if (employeeEntitiy.EmployeeRole != null)
                await AccountRepository.AccountChangeRole(employee.EmployeeUID, employeeRole);

            await EmployeeRepository.EmployeeSave();
        }

        public async Task<EmployeeEntity> EmployeeFindCurrentEmployee(string employeeEmail)
        {

            Expression<Func<Employee, bool>> spec = x => x.AspNetUser.Email == employeeEmail;

            Expression<Func<Employee, MojTestDTO>> proj = x => new MojTestDTO()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeBacklogDays = x.EmployeeBacklogDays
            };


            var employee = await EmployeeRepository.EmployeeGetEmployee<MojTestDTO>(spec, proj);

            EmployeeEntity employeeToReturn = new EmployeeEntity()
            {
                EmployeeUID = (Guid)employee.EmployeeUID,
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                EmployeeBacklogDays = employee.EmployeeBacklogDays
            };


            return employeeToReturn;
        }

        public async Task<List<EmployeeEntity>> EmployeeFindEmployeesByEmploymentDate(DateTime employeeEmploymentDate)
        {
            var employees = await EmployeeRepository.EmployeeGetEmployees(employeeEmploymentDate);

            var employeesToReturn = employees.Select(x => new EmployeeEntity()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeEmploymentDate = x.EmployeeEmploymentDate
            }).ToList();

            return employeesToReturn;
        }

        public async Task<List<EmployeeEntity>> EmployeeFindEmployeesByName(string searchParameters, DateTime employeeEmploymentDate)
        {
            List<string> searchString = new List<string>();
            Expression<Func<Employee, bool>> spec;

            if (searchParameters != null)
            {
                searchString = searchParameters.Split(' ').ToList();

                spec = x => (searchString.Any(p => x.EmployeeName.Contains(p)) ||
                            searchString.Any(p => x.EmployeeSurname.Contains(p)));
            }
            else
            {
                spec = x => true;
            }


            var employees = await EmployeeRepository.EmployeeSearchEmployees(spec, employeeEmploymentDate);

            var employeesToReturn = employees.Select(x => new EmployeeEntity()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();

            return employeesToReturn;
        }

        public async Task<List<EmployeeEntity>> EmployeeGetDeletedEmployees()
        {
            var employees = await EmployeeRepository.EmployeeGetDeletedEmployees();

            var employeesToReturn = employees.Select(x => new EmployeeEntity()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeDeletedOn = x.EmployeeDeletedOn
            }).ToList();

            return employeesToReturn;
        }

        public async Task<EmployeeEntity> EmployeeGetEmployee(Guid employeeUID)
        {
            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeUID);
            var employeRole = await AccountRepository.AccountGetAccountRole(employeeUID);

            return new EmployeeEntity()
            {
                EmployeeUID = employee.EmployeeUID,
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                EmployeeCardIDNumber = employee.EmlpoyeeCardIDNumber,
                EmployeeBacklogDays = employee.EmployeeBacklogDays,
                EmployeeEmploymentDate = employee.EmployeeEmploymentDate,
                EmployeeRole = employeRole
            };
        }

        public async Task<List<EmployeeEntity>> EmployeeGetEmployees(int employeeCount, int employeeOffset)
        {
            var employees = await EmployeeRepository.EmployeeGetEmployees(employeeCount, employeeOffset);

            var employeesToReturn = employees.Select(x => new EmployeeEntity()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();

            return employeesToReturn;
        }

        public async Task<List<EmployeeEntity>> EmployeeGetEmployeesWithBacklogDays()
        {
            var employees = await EmployeeRepository.EmployeeGetEmployeesWithBacklogDays();

            var employeesToReturn = employees.Select(x => new EmployeeEntity()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeBacklogDays = x.EmployeeBacklogDays
            }).ToList();

            return employeesToReturn;
        }

        public async Task EmployeeAddEmployee(int employeeID, EmployeeEntity employeeEntitiy)
        {
            Employee employee = new Employee()
            {
                EmployeeID = employeeID,
                EmployeeUID = Guid.NewGuid(),
                EmployeeCreatedOn = DateTime.UtcNow,
                EmlpoyeeCardIDNumber = employeeEntitiy.EmployeeCardIDNumber,
                EmployeeEmploymentDate = employeeEntitiy.EmployeeEmploymentDate,
                EmployeeName = employeeEntitiy.EmployeeName,
                EmployeeSurname = employeeEntitiy.EmployeeSurname,
                EmployeeBacklogDays = 0
            };
            await EmployeeRepository.EmlpoyeeInsert(employee);
        }
        public async Task<int> EmployeeGetBacklogDays(Guid employeeUID)
        {
            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeUID);
            return (int)employee.EmployeeBacklogDays;
        }
        public async Task<bool> EmployeeValidateCardIDNumber(string employeeCardIDNumber)
        {
            return await EmployeeRepository.EmployeeGetCardIDNumber(employeeCardIDNumber);
        }

        public async Task<int> EmployeeRemoveBacklogDays(Guid employeeUID, int numberOfDays)
        {
            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeUID);

            var result = employee.EmployeeBacklogDays - numberOfDays;

            if (result > 0)
            {
                await EmployeeRepository.EmployeeSave();
                return 0;
            }
            else
            {
                int remainingDaysToReturn = numberOfDays - (int)employee.EmployeeBacklogDays;

                employee.EmployeeBacklogDays = 0;

                await EmployeeRepository.EmployeeSave();

                return remainingDaysToReturn;
            }
        }

        public int EmployeeCalculateDaysOff(ContractEntity contractEntity)
        {
            if (contractEntity.ContractType == (int)ContractTypes.Temporary)
            {
                var amountOfDays = contractEntity.ContractStartDate
                    .GetNumberOfMonths((DateTime)contractEntity.ContractEndDate) *
                    DateTimeExtensions.GetContractExpression();

                return (int)amountOfDays;
            }

            return 20;
        }
        #endregion
    }


    public class MojTestDTO
    {
        public Guid EmployeeUID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeSurname { get; set; }

        public int? EmployeeBacklogDays { get; set; }

    }
}
