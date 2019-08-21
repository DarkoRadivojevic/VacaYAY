using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.Interfaces
{
    public interface IAccountValidation

    {
        Task<bool> ValidateEmail(string accountEmail);
    }
}
