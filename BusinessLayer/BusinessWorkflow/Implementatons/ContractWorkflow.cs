using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Implementatons
{
    public class ContractWorkflow : IContractWorkflow
    {
        #region Atributes
        private IContractRepository _contractRepository;
        private IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructors
        public ContractWorkflow()
        {

        }
        public ContractWorkflow(IContractRepository contractRepository, IEmployeeRepository employeeRepository)
        {
            ContractRepository = contractRepository;
            EmployeeRepository = employeeRepository;
        }
        #endregion
        #region Properties
        public IContractRepository ContractRepository
        {
            get
            {
                return _contractRepository;
            }
            private set
            {
                _contractRepository = value;
            }
        }
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository;
            }
            private set
            {
                _employeeRepository = value;
            }
        }
        #endregion
        #region Methods
        public async Task ContractAddContract(ContractEntity contractEntity)
        {
            var employee = await EmployeeRepository.EmployeeGetEmployee(contractEntity.EmployeeUID);

            Contract contract = new Contract()
            {
                EmployeeID = employee.EmployeeID,
                ContractUID = Guid.NewGuid(),
                ContractType = contractEntity.ContractType,
                ContractStartDate = contractEntity.ContractStartDate,
                ContractEndDate = contractEntity.ContractEndDate,
                ContractCreatedOn = DateTime.UtcNow,
                ContractFile = contractEntity.ContractFile,
                ContractNumber = contractEntity.ContractNumber,
                ContractFileName = contractEntity.ContractFileName
            };

            await ContractRepository.ContractInsert(contract);
        }

        public async Task ContractDeleteContract(Guid contractUID)
        {
            await ContractRepository.ContractDelete(contractUID);
        }

        public async Task<List<ContractEntity>> ContractGetAllContracts(int contractOffset, int contractCount)
        {
            var contracts = await ContractRepository.ContractGetAllContracts(contractOffset, contractCount);

            List<ContractEntity> contractsToReturn = new List<ContractEntity>();

            contractsToReturn = contracts.Select(x => new ContractEntity()
            {
                ContractNumber = x.ContractNumber,
                ContractUID = x.ContractUID,
                EmployeeUID = x.Employee.EmployeeUID
            }).ToList();

            return contractsToReturn;
        }

        public async Task<ContractEntity> ContractGetContract(Guid contractUID)
        {
            var contract = await ContractRepository.ContactGetContract(contractUID);
            return new ContractEntity()
            {
                EmployeeUID = contract.Employee.EmployeeUID,
                ContractUID = contract.ContractUID,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate,
            };
        }

        public async Task<ContractEntity> ContractGetContractFile(Guid contactUID)
        {
            var contract = await ContractRepository.ContractGetContractFile(contactUID);

            var contractToReturn = new ContractEntity()
            {
                ContractFile = contract.ContractFile,
                ContractFileName = contract.ContractFileName
            };

            return contractToReturn;
        }

        public async Task<List<ContractEntity>> ContractGetContracts(string employeeName, string employeeSurname)
        {
            var contracts = await ContractRepository.ContractGetAllContracts(employeeName, employeeSurname);

            List<ContractEntity> contractsToReturn = new List<ContractEntity>();
            contractsToReturn = contracts.Select(x => new ContractEntity()
            {
                ContractUID = x.ContractUID,
                EmployeeUID = x.Employee.EmployeeUID,
                ContractNumber = x.ContractNumber,
                ContractType = x.ContractType
            }).ToList();
     
            return contractsToReturn;
        }

        public async Task<List<ContractEntity>> ContractGetContracts(Guid employeeUID)
        {
            var contracts = await ContractRepository.ContractsGetContractByEmployee(employeeUID);

            List<ContractEntity> contractsToReturn = new List<ContractEntity>();
            contractsToReturn = contracts.Select(x => new ContractEntity()
            {
                ContractUID = x.ContractUID,
                ContractNumber = x.ContractNumber,
                ContractType = x.ContractType,
                ContractStartDate = x.ContractStartDate,
                ContractEndDate = x.ContractEndDate
            }).ToList();       

            return contractsToReturn;
        }

        public async Task<List<ContractEntity>> ContractSearchContract(string inputParameters, DateTime startDate, DateTime endDate)
        {
            var searchParameters = inputParameters.Split(' ');

            var contracts = await ContractRepository.ContractSearchContracts(searchParameters, startDate, endDate);

            var contractsToReturn = contracts.Select(x => new ContractEntity()
            {
                ContractNumber = x.ContractNumber,
                ContractUID = x.ContractUID,
                EmployeeUID = x.Employee.EmployeeUID
            }).ToList();

            return contractsToReturn;
        }
        #endregion
    }
}
