using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IEmployeeRepository
    {
        Task<Employee> EmployeeGetEmployee(Guid employeeUID);
        Task<Employee> EmployeeGetEmployee(int employeeID);
        Task<int> EmployeeGetEmployeeBacklogDays(Guid employeeUID);
        Task<Employee> EmployeeGetEmployee(string emlpoyeeEmail);
        Task<List<Employee>> EmployeeSearchEmployees(string[] searchString, DateTime employeeEmploymentDate);
        Task<List<Employee>> EmployeeGetEmployees(DateTime employeeEmploymentDate);
        Task<List<Employee>> EmployeeGetEmployees(int employeeCount, int employeeOffset);
        Task<List<Employee>> EmployeeGetEmployeesWithBacklogDays();
        Task<List<Employee>> EmployeeGetDeletedEmployees();
        Task<bool> EmployeeGetCardIDNumber(string employeeCardIDNumber);
        Task EmlpoyeeInsert(Employee employee);
        Task EmlpoyeeDelete(Guid employeeUID);
        void EmployeeUpdate(Employee employee);
        Task EmployeeSave();
    }
}
