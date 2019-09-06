using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Implementatons
{
    public class AdditionalDaysWorkflow : IAdditionalDaysWorkflow
    {
        #region Atributes
        private IAdditionalDaysRepository _additionalDaysRepository;
        private IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructors
        public AdditionalDaysWorkflow()
        {
        }
        public AdditionalDaysWorkflow(IAdditionalDaysRepository additionalDaysRepository, IEmployeeRepository employeeRepository)
        {
            AdditionalDaysRepository = additionalDaysRepository;
            EmployeeRepository = employeeRepository;
        }
        #endregion
        #region Properties
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
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository;
            }
            set
            {
                _employeeRepository = value;
            }
        }
        #endregion
        #region Methods
        public async Task<AdditionalDaysEntity> AdditionalDaysGetAdditionalDay(Guid additionalDayUID)
        {
            var daysoff = await AdditionalDaysRepository.AdditionalDaysGetAdditionalDay(additionalDayUID);
            return new AdditionalDaysEntity()
            {
                AdditionalDaysUID = daysoff.AdditionaDaysUID,
                AdditionalDaysNumberOfAdditionalDays = daysoff.AdditionalDaysNumberOfAdditionalDays,
                AdditionalDaysReason = daysoff.AdditionalDaysReason
            };
        }

        public async Task<List<AdditionalDaysEntity>> AdditionalDaysGetAllAdditionalDays(Guid employeeUID)
        {
            var daysoff = await AdditionalDaysRepository.AdditonalDaysGetAllAdditionalDays(employeeUID);

            var daysoffToReturn = daysoff.Where(x => x.AdditionalDaysDeletedOn == null)
                                         .Select(x => new AdditionalDaysEntity()
                                         {
                                             AdditionalDaysUID = x.AdditionaDaysUID,
                                             AdditionalDaysNumberOfAdditionalDays = x.AdditionalDaysNumberOfAdditionalDays,
                                             AdditionalDaysCreatedOn = x.AdditionalDaysCreatedOn
                                         }).ToList();
            return daysoffToReturn;
        }

        public async Task<int> AdditionalDaysGetNumberOfDays(Guid employeeUID)
        {
            var daysoff = await AdditionalDaysRepository.AdditonalDaysGetAllAdditionalDays(employeeUID);
            int numberOfDaysToReturn = daysoff.Select(x => x.AdditionalDaysNumberOfAdditionalDays).Count();

            return numberOfDaysToReturn;
        }

        public async Task AdditionalDaysInsert(Guid employeeUID, AdditionalDaysEntity additionalDays)
        {
            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeUID);

            var days = new AdditionalDay()
            {
                AdditionaDaysUID = Guid.NewGuid(),
                AdditionalDaysNumberOfAdditionalDays = additionalDays.AdditionalDaysNumberOfAdditionalDays,
                AdditionalDaysReason = additionalDays.AdditionalDaysReason,
                AdditionalDaysCreatedOn = DateTime.UtcNow,
                EmployeeID = employee.EmployeeID
            };

            await AdditionalDaysRepository.AdditionalDaysInsert(days);
        }
        public async Task AdditionalDaysRemove(Guid employeeUID, int numberOfDays)
        {
            var daysOff = await AdditionalDaysRepository.AdditonalDaysGetAllAdditionalDays(employeeUID);

            foreach (var day in daysOff)
            {
                numberOfDays -= day.AdditionalDaysNumberOfAdditionalDays;
                if (numberOfDays < 0)
                {
                    day.AdditionalDaysNumberOfAdditionalDays += numberOfDays;
                    break;
                }
                if (numberOfDays >= 0)
                    day.AdditionalDaysDeletedOn = DateTime.UtcNow;
            }
            await AdditionalDaysRepository.AdditionalDaysSave();
        }
        public async Task<bool> AdditionalDaysValidateHasDaysOff(Guid employeeUID, DateTime startDate, DateTime endDate)
        {
            var numberOfBusinessDays = 1 + ((endDate - startDate).TotalDays * 5 - (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;
            if (endDate.DayOfWeek == DayOfWeek.Saturday) numberOfBusinessDays--;
            if (startDate.DayOfWeek == DayOfWeek.Sunday) numberOfBusinessDays--;

            return await AdditionalDaysRepository.AdditionalDaysHasDaysOff(employeeUID, (int)numberOfBusinessDays);
        }
        #endregion
    }
}
