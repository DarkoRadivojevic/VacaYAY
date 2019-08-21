using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces;
using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using SolutionEnums;

namespace ApplicationLayer.Implementations
{
    public class ContractService : IContractService
    {
        #region Atributes
        private IContractWorkflow _contractWorkflow;
        #endregion
        #region Constructors
        public ContractService()
        {

        }
        public ContractService(IContractWorkflow contractWorkflow)
        {
            ContractWorkflow = contractWorkflow;
        }
        #endregion
        #region Properties
        public IContractWorkflow ContractWorkflow
        {
            get
            {
                return _contractWorkflow;
            }
            private set
            {
                _contractWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task ContractAddContract(ApplicationContract applicationContract)
        {
            var contractEntitty = new ContractEntity()
            {
                ContractUID = Guid.NewGuid(),
                EmployeeUID = applicationContract.EmployeeUID,
                ContractType = (int)applicationContract.ContractType,
                ContractFile = applicationContract.ContractFile,
                ContractCreatedOn = DateTime.UtcNow,
                ContractStartDate = applicationContract.ContractStartDate,
                ContractEndDate = applicationContract.ContractEndDate
            };
            await ContractWorkflow.ContractAddContract(contractEntitty);
        }

        public async Task ContractDeleteContract(Guid contractUID)
        {
            await ContractWorkflow.ContractDeleteContract(contractUID);
        }

        public async Task<List<ApplicationContract>> ContractGetAllContracts()
        {
            var contracts = await ContractWorkflow.ContractGetAllContracts();

            var toReturn = contracts.Select(x => new ApplicationContract()
            {
                ContractUID = x.ContractUID,
                ContractNumber = x.ContractNumber,
                ContractStartDate = x.ContractStartDate
            }).ToList();
            return toReturn;
        }

        public async Task<ApplicationContract> ContractGetContract(Guid contractUID)
        {
            var contracts = await ContractWorkflow.ContractGetContract(contractUID);

            var toReutn = new ApplicationContract()
            {
                EmployeeUID = contracts.EmployeeUID,
                ContractNumber = contracts.ContractNumber,
                ContractType = (ContractTypes)contracts.ContractType,
                ContractStartDate = contracts.ContractStartDate,
                ContractEndDate = (DateTime)contracts.ContractEndDate
            };
            return toReutn;
        }

        public async Task<List<ApplicationContract>> ContractGetContracts(string employeeName, string employeeSurname)
        {
            var contracts = await ContractWorkflow.ContractGetContracts(employeeName, employeeSurname);

            var toReturn = contracts.Select(x => new ApplicationContract()
            {
                ContractUID = x.ContractUID,
                ContractType = (ContractTypes)x.ContractType,
                ContractStartDate = x.ContractStartDate,
                ContractEndDate = (DateTime)x.ContractEndDate
            }).ToList();
            return toReturn;
        }

        public async Task<List<ApplicationContract>> contractGetContracts(Guid employeeUID)
        {
            var contracts = await ContractWorkflow.ContractGetContracts(employeeUID);

            var toRetrun = contracts.Select(x => new ApplicationContract()
            {
                ContractUID = x.ContractUID,
                ContractType = (ContractTypes)x.ContractType,
                ContractStartDate = x.ContractStartDate,
                ContractEndDate = (DateTime)x.ContractEndDate
            }).ToList();
         
            return toRetrun;
        }
        #endregion
    }
}
