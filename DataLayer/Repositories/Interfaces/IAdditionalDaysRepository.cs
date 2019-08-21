using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IAdditionalDaysRepository
    {
        Task<List<AdditionalDay>> AdditonalDaysGetAllAdditionalDays(Guid employeeUID);
        Task<AdditionalDay> AdditionalDaysGetAdditionalDay(Guid additonalDayUID);
        Task AdditionalDaysInsert(AdditionalDay additionalDay);
        Task AdditonalDaysDelete(Guid additionalDayUID);
        Task AdditionalDaysSave();
        void AdditionalDaysUpdate(AdditionalDay additionalDay);
        Task<bool> AdditionalDaysHasDaysOff(Guid emploeeUID, int numberOfDays);
    }
}
