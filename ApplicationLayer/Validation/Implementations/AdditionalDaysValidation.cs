using ApplicationLayer.Validation.Interfaces;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.Implementations
{
    public class AdditionalDaysValidation : IAdditonalDaysValidation
    {
        #region Atributes
        private IAdditionalDaysWorkflow _additionalDaysWorkflow;
        #endregion
        #region Constructors
        public AdditionalDaysValidation()
        {

        }
        public AdditionalDaysValidation(IAdditionalDaysWorkflow additionalDaysWorkflow)
        {
            AdditionalDaysWorkflow = additionalDaysWorkflow;
        }
        #endregion
        #region Properties
        public IAdditionalDaysWorkflow AdditionalDaysWorkflow
        {
            get
            {
                return _additionalDaysWorkflow;
            }
            private set
            {
                _additionalDaysWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task<bool> ValidateHasDaysOff(Guid employeeUID, DateTime startDate, DateTime endDate)
        {
            return await AdditionalDaysWorkflow.AdditionalDaysValidateHasDaysOff(employeeUID, startDate, endDate);
        }
        #endregion
    }
}
