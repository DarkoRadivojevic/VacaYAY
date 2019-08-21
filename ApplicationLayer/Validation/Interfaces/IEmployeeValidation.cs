using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.Interfaces
{
    public interface IEmployeeValidation
    {
        Task<bool> ValidateCardID(string employeeCardIDNumber);
    }
}
