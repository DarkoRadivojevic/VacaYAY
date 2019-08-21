using ApplicationLayer.Validation.Interfaces;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.Implementations
{
    public class EmployeeValidation : IEmployeeValidation
    {
        #region Atributes
        private IEmployeeWorkflow _employeeWorkflow;
        #endregion
        #region Constructors
        public EmployeeValidation()
        {

        }
        public EmployeeValidation(IEmployeeWorkflow employeeWorkflow)
        {
            EmployeeWorkflow = employeeWorkflow;
        }
        #endregion
        #region Properties
        public IEmployeeWorkflow EmployeeWorkflow
        {
            get
            {
                return _employeeWorkflow;
            }
            private set
            {
                _employeeWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task<bool> ValidateCardID(string employeeCardIDNumber)
        { 
            short val = 0;
            var result = Int16.TryParse(employeeCardIDNumber, out val);
            if (result)
                return await EmployeeWorkflow.EmployeeValidateCardIDNumber(employeeCardIDNumber);
            else
                return false;
        }
        #endregion
    }
}
