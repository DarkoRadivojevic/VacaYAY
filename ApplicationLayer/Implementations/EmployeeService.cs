using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces;
using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Implementatons;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationLayer.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        #region Atributes
        private IAdditionalDaysWorkflow _additionalDaysWorkflow;
        private IContractWorkflow _contractWorkflow;
        private IEmployeeWorkflow _employeeWorkflow;
        private IRequestWorkflow _requestWorkflow;
        #endregion
        #region Constructors
        public EmployeeService()
        {
        }
        public EmployeeService(IAdditionalDaysWorkflow additionalDaysWorkflow, IContractWorkflow contractWorkflow, IEmployeeWorkflow employeeWorkflow, IRequestWorkflow requestWorkflow)
        {
            AdditionalDaysWorkflow = additionalDaysWorkflow;
            ContractWorkflow = contractWorkflow;
            EmployeeWorkflow = employeeWorkflow;
            RequestWorkflow = requestWorkflow;
        }
        #endregion
        #region Properties
        public IAdditionalDaysWorkflow AdditionalDaysWorkflow
        {
            get
            {
                return _additionalDaysWorkflow;
            }
            private set
            {
                _additionalDaysWorkflow = value;
            }
        }

        public IContractWorkflow ContractWorkflow
        {
            get
            {
                return _contractWorkflow;
            }
            private set
            {
                _contractWorkflow = value;
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

        public IRequestWorkflow RequestWorkflow
        {
            get
            {
                return _requestWorkflow;
            }
            private set
            {
                _requestWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task EmployeeDeleteEmployee(Guid employeeUID)
        {
            await EmployeeWorkflow.EmployeeDeleteEmployee(employeeUID);
        }

        public async Task EmployeeEditEmployee(ApplicationEmployee applicationEmployee, string employeeRole )
        {
            var employeeEntitiy = new EmployeeEntity()
            {
                EmployeeUID = applicationEmployee.EmployeeUID,
                EmployeeName = applicationEmployee.EmployeeName,
                EmployeeSurname = applicationEmployee.EmployeeSurname,
                EmployeeCardIDNumber = applicationEmployee.EmployeeCardIDNumber,
                EmployeeEmploymentDate = applicationEmployee.EmployeeEmploymentDate
            };
            await EmployeeWorkflow.EmployeeEditEmployee(employeeEntitiy, employeeRole);
        }

        public async Task<ApplicationEmployee> EmployeeFindCurrentEmployee(string employeeEmail)
        {
            var employee = await EmployeeWorkflow.EmployeeFindCurrentEmployee(employeeEmail);

            var employeeToReturn = new ApplicationEmployee()
            {
                EmployeeUID = employee.EmployeeUID,
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                EmployeeBacklogDays = employee.EmployeeBacklogDays
            };

            return employeeToReturn;
        }

        public async Task<List<ApplicationEmployee>> EmployeeFindEmployeesByName(string employeeName, string employeeSurname)
        {
            var employees = await EmployeeWorkflow.EmployeeFindEmployeesByName(employeeName, employeeSurname);

            var employeesToReturn = employees.Select(x => new ApplicationEmployee()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeEmploymentDate = x.EmployeeEmploymentDate
            }).ToList();

            return employeesToReturn;
        }

        public async Task<List<ApplicationEmployee>> EmployeeFindEmployeesByEmploymentDate(DateTime employeeEmploymentDate)
        {
            var employees = await EmployeeWorkflow.EmployeeFindEmployeesByEmploymentDate(employeeEmploymentDate);

            var employeesToReturn = employees.Select(x => new ApplicationEmployee()
            { 
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeEmploymentDate = x.EmployeeEmploymentDate
            }).ToList();

            return employeesToReturn;
        }

        public async Task<List<ApplicationEmployee>> EmployeeGetDeletedEmployees()
        {
            var employees = await EmployeeWorkflow.EmployeeGetDeletedEmployees();

            var employeesToReturn = employees.Select(x => new ApplicationEmployee()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeDeletedOn = x.EmployeeDeletedOn
            }).ToList();

            return employeesToReturn;
        }

        public async Task<ApplicationEmployee> EmployeeGetEmployee(Guid employeeUID)
        {
            var employee = await EmployeeWorkflow.EmployeeGetEmployee(employeeUID);

            var employeeToReturn = new ApplicationEmployee()
            {
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                EmployeeBacklogDays = employee.EmployeeBacklogDays,
                EmployeeCardIDNumber = employee.EmployeeCardIDNumber,
                EmployeeEmploymentDate = employee.EmployeeEmploymentDate,
                EmployeeDeletedOn = employee.EmployeeDeletedOn
            };

            return employeeToReturn;
        }

        public async Task<List<ApplicationEmployee>> EmployeeGetEmployees()
        {
            var employees = await EmployeeWorkflow.EmployeeGetEmployees();

            var employeesToReturn = employees.Select(x => new ApplicationEmployee()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();

            return employeesToReturn;
        }

        public async Task<List<ApplicationEmployee>> EmployeeGetEmployeesWithBacklogDays()
        {
            var employees = await EmployeeWorkflow.EmployeeGetEmployeesWithBacklogDays();

            var employeesToReturn = employees.Select(x => new ApplicationEmployee()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname,
                EmployeeBacklogDays = x.EmployeeBacklogDays
            }).ToList();

            return employeesToReturn;
        }
        #endregion
    }
}
