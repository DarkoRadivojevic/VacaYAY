using BusinessLayer.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Interfaces
{
    public interface IEmployeeWorkflow
    {
        Task<EmployeeEntity> EmployeeGetEmployee(Guid employeeUID);
        Task<EmployeeEntity> EmployeeFindCurrentEmployee(string employeeEmail);
        Task<List<EmployeeEntity>> EmployeeFindEmployeesByName(string searchParameters, DateTime employeeEmploymentDate);
        Task<List<EmployeeEntity>> EmployeeFindEmployeesByEmploymentDate(DateTime employeeEmploymentDate);
        Task<List<EmployeeEntity>> EmployeeGetEmployeesWithBacklogDays();
        Task<List<EmployeeEntity>> EmployeeGetEmployees(int employeeCount, int employeeOffset);
        Task<List<EmployeeEntity>> EmployeeGetDeletedEmployees();
        Task EmployeeEditEmployee(EmployeeEntity employeeEntity, string employeeRole);
        Task EmployeeDeleteEmployee(Guid employeeUID);
        Task EmployeeAddEmployee(int employeeID, EmployeeEntity employeeEntity);
        int EmployeeCalculateDaysOff(ContractEntity contractEntity);
        Task<bool> EmployeeValidateCardIDNumber(string employeeCardIDNumber);
        Task<int> EmployeeRemoveBacklogDays(Guid employeeUID, int numberOfDays);
    }
}
