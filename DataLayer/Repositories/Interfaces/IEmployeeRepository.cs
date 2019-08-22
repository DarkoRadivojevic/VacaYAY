using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IEmployeeRepository
    {
        Task<Employee> EmployeeGetEmployee(Guid employeeUID);
        Task<Employee> EmployeeGetEmployee(int employeeID);
        Task<Employee> EmployeeGetEmployee(string emlpoyeeEmail);
        Task<List<Employee>> EmployeeGetEmployees(string employeeName, string employeeSurname);
        Task<List<Employee>> EmployeeGetEmployees(DateTime employeeEmploymentDate);
        Task<List<Employee>> EmployeeGetEmployees();
        Task<List<Employee>> EmployeeGetEmployeesWithBacklogDays();
        Task<List<Employee>> EmployeeGetDeletedEmployees();
        Task<bool> EmployeeGetCardIDNumber(string employeeCardIDNumber);
        Task EmlpoyeeInsert(Employee employee);
        Task EmlpoyeeDelete(Guid employeeUID);
        void EmployeeUpdate(Employee employee);
        Task EmployeeSave();
    }
}
