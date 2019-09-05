using BusinessLayer.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Interfaces
{
    public interface IAdditionalDaysWorkflow
    {
        Task<List<AdditionalDaysEntity>> AdditionalDaysGetAllAdditionalDays(Guid employeeUID);
        Task<AdditionalDaysEntity> AdditionalDaysGetAdditionalDay(Guid additionalDayUID);
        Task AdditionalDaysInsert(Guid employeeUID, AdditionalDaysEntity additionalDays);
        Task<int> AdditionalDaysGetNumberOfDays(Guid employeeUID);
        Task AdditionalDaysRemove(Guid employeeUID, int numberOfDays);
        Task<bool> AdditionalDaysValidateHasDaysOff(Guid employeeUID, DateTime startDate, DateTime endDate);

    }
}
