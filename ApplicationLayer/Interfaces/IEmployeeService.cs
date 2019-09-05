using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApplicationEmployee> EmployeeGetEmployee(Guid employeeUID);
        Task<ApplicationEmployee> EmployeeFindCurrentEmployee(string employeeEmail);
        Task<List<ApplicationEmployee>> EmployeeFindEmployeesByName(string searchParameters, DateTime employeeEmploymentDate);
        Task<List<ApplicationEmployee>> EmployeeFindEmployeesByEmploymentDate(DateTime employeeEmploymentDay);
        Task<List<ApplicationEmployee>> EmployeeGetEmployeesWithBacklogDays();
        Task<List<ApplicationEmployee>> EmployeeGetEmployees(int employeeCount, int employeeOffset);
        Task<List<ApplicationEmployee>> EmployeeGetDeletedEmployees();
        Task EmployeeEditEmployee(ApplicationEmployee applicationEmployee, string employeeRole);
        Task EmployeeDeleteEmployee(Guid employeeUID);
    }
}
