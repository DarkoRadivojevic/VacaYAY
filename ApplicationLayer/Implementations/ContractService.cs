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
                EmployeeUID = applicationContract.EmployeeUID,
                ContractType = (int)applicationContract.ContractType,
                ContractFile = applicationContract.ContractFile,
                ContractNumber = applicationContract.ContractNumber,
                ContractFileName = applicationContract.ContractFileName,
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

        public async Task<List<ApplicationContract>> ContractGetAllContracts(int contractOffset, int contractCount)
        {
            var contracts = await ContractWorkflow.ContractGetAllContracts(contractOffset, contractCount);

            var toReturn = contracts.Select(x => new ApplicationContract()
            {
                ContractUID = x.ContractUID,
                ContractNumber = x.ContractNumber,
                ContractStartDate = x.ContractStartDate
            }).ToList();
            return toReturn;
        }

        public async Task<ApplicationContract> ContractGetContactFile(Guid contractUID)
        {
            var contract = await ContractWorkflow.ContractGetContractFile(contractUID);

            var contractToReturn = new ApplicationContract()
            {
                ContractFile = contract.ContractFile,
                ContractFileName = contract.ContractFileName
            };

            return contractToReturn;
        }

        public async Task<ApplicationContract> ContractGetContract(Guid contractUID)
        {
            var contract = await ContractWorkflow.ContractGetContract(contractUID);

            var toReturn = new ApplicationContract()
            {
                ContractUID = contract.ContractUID,
                EmployeeUID = contract.EmployeeUID,
                ContractNumber = contract.ContractNumber,
                ContractType = (ContractTypes)contract.ContractType,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate
            };
            return toReturn;
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

        public async Task<List<ApplicationContract>> ContractGetContracts(Guid employeeUID)
        {
            var contracts = await ContractWorkflow.ContractGetContracts(employeeUID);

            var toRetrun = contracts.Select(x => new ApplicationContract()
            {
                ContractUID = x.ContractUID,
                ContractNumber = x.ContractNumber,
                ContractType = (ContractTypes)x.ContractType,
                ContractStartDate = x.ContractStartDate,
                ContractEndDate = x.ContractEndDate
            }).ToList();
         
            return toRetrun;
        }

        public async Task<List<ApplicationContract>> ContractSearchContracts(string inputParameters, DateTime startDate, DateTime endDate)
        {
            var contracts = await ContractWorkflow.ContractSearchContract(inputParameters, startDate, endDate);

            var contractsToReturn = contracts.Select(x => new ApplicationContract()
            {
                ContractNumber = x.ContractNumber,
                ContractUID = x.ContractUID,
                EmployeeUID = x.EmployeeUID
            }).ToList();

            return contractsToReturn;
        }
        #endregion
    }
}
