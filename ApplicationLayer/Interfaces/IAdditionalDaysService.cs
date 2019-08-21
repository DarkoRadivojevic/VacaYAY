using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IAdditionalDaysService 
    {
        Task<List<ApplicationAdditionalDays>> AdditionDaysGetAllAdditionalDaysOff(Guid employeeUID);
        Task<ApplicationAdditionalDays> AdditionalDaysGetAdditionalDay(Guid additionalDayUID);
        Task<int> AdditionalDaysGetNumberOfDays(Guid employeeUID);
    }
}
