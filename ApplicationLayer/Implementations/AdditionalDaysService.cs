using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Implementations
{
    public class AdditionalDaysService : IAdditionalDaysService
    {
        #region Atributes 
        private IAdditionalDaysWorkflow _additionalDaysWorkflow;
        #endregion
        #region Constructors
        public AdditionalDaysService()
        {

        }
        public AdditionalDaysService(IAdditionalDaysWorkflow additionalDaysWorkflow)
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
        public async Task<ApplicationAdditionalDays> AdditionalDaysGetAdditionalDay(Guid additionalDayUID)
        {
            var additionalDays = await AdditionalDaysWorkflow.AdditionalDaysGetAdditionalDay(additionalDayUID);

            ApplicationAdditionalDays toReturn = new ApplicationAdditionalDays()
            {
                AdditionalDaysNumberOfDays = additionalDays.AdditionalDaysNumberOfAdditionalDays,
                AdditionalDaysReason = additionalDays.AdditionalDaysReason
            };

            return toReturn;
        }

        public async Task<int> AdditionalDaysGetNumberOfDays(Guid employeeUID)
        {
            return await AdditionalDaysWorkflow.AdditionalDaysGetNumberOfDays(employeeUID);
        }

        public async Task<List<ApplicationAdditionalDays>> AdditionDaysGetAllAdditionalDaysOff(Guid employeeUID)
        {
            var additionalDays = await AdditionalDaysWorkflow.AdditionalDaysGetAllAdditionalDays(employeeUID);

            var toReturn = additionalDays.Select(x => new ApplicationAdditionalDays()
            {
                AdditionalDaysUID = x.AdditionalDaysUID,
                AdditionalDaysNumberOfDays = x.AdditionalDaysNumberOfAdditionalDays,
                AdditionalDaysReason = x.AdditionalDaysReason
            }).ToList();

            return toReturn;
        }
        #endregion
    }
}
