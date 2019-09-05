using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{

    public class AdditionalDaysRepository : IAdditionalDaysRepository
    {
        #region Atributes 
        private VacaYAYContext _dbContext;
        #endregion
        #region Constructors
        public AdditionalDaysRepository()
        {
        }
        public AdditionalDaysRepository(VacaYAYContext dbContext)
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
        public async Task<AdditionalDay> AdditionalDaysGetAdditionalDay(Guid additonalDayUID)
        {
            AdditionalDay additionalDayToReturn = await DbContext.AdditionalDays.Where(x => x.AdditionaDaysUID == additonalDayUID).FirstAsync();
            return additionalDayToReturn;
        }

        public async Task<bool> AdditionalDaysHasDaysOff(Guid emploeeUID, int numberOfDays)
        {
            var employeeDays = await DbContext.Employees.Where(x => x.EmployeeUID == emploeeUID).Select(x => x.EmployeeBacklogDays).FirstAsync();

            if ((numberOfDays - (int)employeeDays) <= 0)
                return true;

            var additionalDays = await DbContext.AdditionalDays.Where(x => x.Employee.EmployeeUID == emploeeUID && x.AdditionalDaysDeletedOn == null).Select(x => x.AdditionalDaysNumberOfAdditionalDays).SumAsync();

            if ((numberOfDays - (int)employeeDays - additionalDays ) <= 0)
                return true;
            else
                return false;
        }

        public async Task AdditionalDaysInsert(AdditionalDay additionalDay)
        {
            DbContext.AdditionalDays.Add(additionalDay);
            await this.AdditionalDaysSave();
        }

        public async Task AdditionalDaysSave()
        {
            await DbContext.SaveChangesAsync();
        }

        public void AdditionalDaysUpdate(AdditionalDay additionalDay)
        {
            DbContext.Entry(additionalDay).State = EntityState.Modified;
        }

        public async Task AdditonalDaysDelete(Guid additionalDaysUID)
        {
            AdditionalDay additionalDay = await DbContext.AdditionalDays.Where(x => x.AdditionaDaysUID == additionalDaysUID).FirstAsync();
            additionalDay.AdditionalDaysDeletedOn = DateTime.UtcNow;
            await this.AdditionalDaysSave();
        }

        public async Task<List<AdditionalDay>> AdditonalDaysGetAllAdditionalDays(Guid employeeUID)
        {
            List<AdditionalDay> additionalDayToReturn = await DbContext.AdditionalDays.Where(x => x.Employee.EmployeeUID == employeeUID && x.AdditionalDaysDeletedOn == null).ToListAsync();
            return additionalDayToReturn;
        }

        public async Task<int> AdditionalDayGetTotalAdditionalDays(Guid employeeUID)
        {
            int totalDaysToReturn = await DbContext.AdditionalDays.Where(x => x.Employee.EmployeeUID == employeeUID).Select(x => x.AdditionalDaysNumberOfAdditionalDays).SumAsync();
            return totalDaysToReturn;
        }
        #endregion
    }
}
