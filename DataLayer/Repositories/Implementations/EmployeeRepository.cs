using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Atributes
        private VacaYAYContext _dbContext;
        #endregion
        #region Constructors
        public EmployeeRepository()
        {
        }
        public EmployeeRepository(VacaYAYContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion
        #region Properties
        public VacaYAYContext DbContext
        {
            get
            {
                return _dbContext;
            }
            private set
            {
                _dbContext = value;
            }
        }
        #endregion
        #region Methods
        public async Task EmlpoyeeDelete(Guid employeeUID)
        {
            Employee employee = await DbContext.Employees.Where(x => x.EmployeeUID == employeeUID).FirstAsync();
            employee.EmployeeDeletedOn = DateTime.UtcNow;

            this.EmployeeUpdate(employee);
        }

        public async Task EmlpoyeeInsert(Employee employee)
        {
            DbContext.Employees.Add(employee);
            await this.EmployeeSave();
        }

        public async Task<List<Employee>> EmployeeGetDeletedEmployees()
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeDeletedOn != null)
                                                     .Select(x => new Employee
                                                     {
                                                         EmployeeName = x.EmployeeName,
                                                         EmployeeSurname = x.EmployeeSurname,
                                                         EmployeeDeletedOn = x.EmployeeDeletedOn
                                                     }).ToListAsync();
            return employeesToReturn;
        }

        public async Task<Employee> EmployeeGetEmployee(Guid employeeUID)
        {
            Employee employeeToReturn = await DbContext.Employees.Where(x => x.EmployeeUID == employeeUID).FirstAsync();
            return employeeToReturn;
        }

        public async Task<Employee> EmployeeGetEmployee(string employeeEmail)
        {
            Employee employeeToReturn = await DbContext.Employees.Where(x => x.AspNetUser.Email == employeeEmail)
                                                .Select(x => new Employee
                                                {
                                                    EmployeeUID = x.EmployeeUID,
                                                    EmployeeName = x.EmployeeName,
                                                    EmployeeSurname = x.EmployeeSurname,
                                                    EmployeeBacklogDays = x.EmployeeBacklogDays
                                                }).FirstAsync();
            return employeeToReturn;
        }

        public async Task<List<Employee>> EmployeeGetEmployees(DateTime employeeEmploymentDate)
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeEmploymentDate > employeeEmploymentDate && x.EmployeeDeletedOn == null)
                                                    .Select(x => new Employee
                                                    {
                                                        EmployeeName = x.EmployeeName,
                                                        EmployeeSurname = x.EmployeeSurname,
                                                        EmployeeUID = x.EmployeeUID
                                                    }).ToListAsync();
            return employeesToReturn;
        }

        public async Task<List<Employee>> EmployeeGetEmployees()
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeDeletedOn == null).ToListAsync();

            return employeesToReturn;
        }

        public async Task<List<Employee>> EmployeeGetEmployeesWithBacklogDays()
        {

            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeBacklogDays != 0 && x.EmployeeDeletedOn == null)
                                                     .Select(x => new Employee
                                                     {
                                                         EmployeeUID = x.EmployeeUID,
                                                         EmployeeName = x.EmployeeName,
                                                         EmployeeSurname = x.EmployeeSurname,
                                                         EmployeeBacklogDays = x.EmployeeBacklogDays
                                                     }).ToListAsync();
            return employeesToReturn;

        }

        public async Task<List<Employee>> EmployeeGetEmployees(string employeeName, string employeeSurname)
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeName.Contains(employeeName) || x.EmployeeSurname.Contains(employeeSurname) && x.EmployeeDeletedOn == null)
                                              .Select(x => new Employee
                                              {
                                                  EmployeeName = x.EmployeeName,
                                                  EmployeeSurname = x.EmployeeSurname,
                                                  EmployeeUID = x.EmployeeUID
                                              }).ToListAsync();
            return employeesToReturn;
        }
        public async Task<bool> EmployeeGetCardIDNumber(string employeeCardIDNumber)
        {
            var result = await DbContext.Employees.Where(x => x.EmlpoyeeCardIDNumber == employeeCardIDNumber).FirstAsync();
            if (result == null)
                return true;
            else
                return false;
        }

        public async Task EmployeeSave()
        {
            await DbContext.SaveChangesAsync();
        }

        public void EmployeeUpdate(Employee employee)
        {
            DbContext.Entry(employee).State = EntityState.Modified;
        }
        #endregion
    }
}
