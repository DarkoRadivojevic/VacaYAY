using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.Interfaces
{
    public interface IAdditonalDaysValidation
    {
        Task<bool> ValidateHasDaysOff(Guid employeeUID, DateTime startDate, DateTime endDate);
    }
}
