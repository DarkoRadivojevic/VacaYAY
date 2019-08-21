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
                ContractFile = contractEntity.ContractFile
            };

            await ContractRepository.ContractInsert(contract);
        }

        public async Task ContractDeleteContract(Guid contractUID)
        {
            await ContractRepository.ContractDelete(contractUID);
        }

        public async Task<List<ContractEntity>> ContractGetAllContracts()
        {
            var contracts = await ContractRepository.ContractGetAllContracts();

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
                ContractUID = contract.ContractUID,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType,
                ContractFile = contract.ContractFile,
                ContractStartDate = contract.ContractStartDate,
                ContractEndDate = contract.ContractEndDate,
                ContractCreatedOn = contract.ContractCreatedOn,
                ContractDeletedOn = contract.ContractDeletedOn
            };
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
                ContractType = x.ContractType
            }).ToList();       

            return contractsToReturn;
        }
        #endregion
    }
}
