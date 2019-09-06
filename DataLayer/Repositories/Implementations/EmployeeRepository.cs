using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

            await this.EmployeeSave();
        }

        public async Task EmlpoyeeInsert(Employee employee)
        {
            DbContext.Employees.Add(employee);
            await this.EmployeeSave();
        }

        public async Task<List<Employee>> EmployeeGetDeletedEmployees()
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeDeletedOn != null).ToListAsync();
            return employeesToReturn;
        }

        public async Task<Employee> EmployeeGetEmployee(Guid employeeUID)
        {
            Employee employeeToReturn = await DbContext.Employees.Where(x => x.EmployeeUID == employeeUID && x.EmployeeDeletedOn == null).FirstAsync();
            return employeeToReturn;
        }

        public async Task<Employee> EmployeeGetEmployee(string employeeEmail)
        {
            Employee employeeToReturn = await DbContext.Employees.Where(x => x.AspNetUser.Email == employeeEmail).FirstAsync();
            return employeeToReturn;
        }

        public async Task<List<Employee>> EmployeeGetEmployees(DateTime employeeEmploymentDate)
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeEmploymentDate > employeeEmploymentDate && x.EmployeeDeletedOn == null).ToListAsync();
            return employeesToReturn;
        }

        public async Task<List<Employee>> EmployeeGetEmployees(int employeeCount, int employeeOffset)
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeDeletedOn == null)
                                .OrderBy(x => x.EmployeeCreatedOn)
                                .Skip(employeeOffset * (employeeCount - 1))
                                .Take(employeeOffset)
                                .ToListAsync();

            return employeesToReturn;
        }

        public async Task<List<Employee>> EmployeeGetEmployeesWithBacklogDays()
        {

            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => x.EmployeeBacklogDays != 0 && x.EmployeeDeletedOn == null).ToListAsync();
            return employeesToReturn;

        }

        public async Task<List<Employee>> EmployeeSearchEmployees(string[] searchString, DateTime employeeEmploymentDate)
        {
            List<Employee> employeesToReturn = await DbContext.Employees.Where(x => (searchString.Any(p => x.EmployeeName.Contains(p)) || searchString.Any(p => x.EmployeeSurname.Contains(p))) && 
                                                                               x.EmployeeDeletedOn == null && x.EmployeeEmploymentDate >= employeeEmploymentDate)
                                                                        .ToListAsync();
            return employeesToReturn;
        }

        public async Task<bool> EmployeeGetCardIDNumber(string employeeCardIDNumber)
        {
            Employee result = await DbContext.Employees.Where(x => x.EmlpoyeeCardIDNumber == employeeCardIDNumber).FirstAsync();
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

        public async Task<Employee> EmployeeGetEmployee(int employeeID)
        {
            Employee employee = await DbContext.Employees.Where(x => employeeID == x.EmployeeID).FirstAsync();
            return employee;
        }

        public async Task<int> EmployeeGetEmployeeBacklogDays(Guid employeeUID)
        {
            int daysToReturn = await DbContext.Employees.Where(x => x.EmployeeUID == employeeUID).Select(x => x.EmployeeBacklogDays).FirstAsync() ?? 0;
            return daysToReturn;
        }




        public async Task<T> EmployeeGetEmployee<T>(Expression<Func<Employee, bool>> expressionSpecification,
            Expression<Func<Employee, T>> expressionProjection)
        {
            T employeeToReturn = await DbContext.Employees
                .Where(expressionSpecification)
                .Select(expressionProjection)
                .FirstOrDefaultAsync();

            return employeeToReturn;
        }

        public async Task<List<T>> EmployeeGetList<T>(Expression<Func<Employee, bool>> expresionSpecification,
            Expression<Func<Employee, T>> expressionProjection)
        {
            List<T> employeesToReturn = await DbContext.Employees
                .Where(expresionSpecification)
                .Select(expressionProjection)
                .ToListAsync();

            return employeesToReturn;
        }
        #endregion
    }
}
