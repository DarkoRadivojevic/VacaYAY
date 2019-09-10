using SolutionEnums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Implementations
{
    public class ContractRepository : IContractRepository
    {
        #region Atributes
        private VacaYAYContext _dbContext;
        #endregion
        #region Constructors
        public ContractRepository()
        {

        }
        public ContractRepository(VacaYAYContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion
        #region Properties
        public VacaYAYContext DbContext
        {
            get
            {
                return _dbContext;
            }
            private set
            {
                _dbContext = value;
            }
        }
        #endregion
        #region Methods
        public async Task ContractDelete(Guid contractUID)
        {
            Contract contract = await DbContext.Contracts.Where(x => x.ContractUID == contractUID).FirstAsync();
            contract.ContractDeletedOn = DateTime.UtcNow;
            await this.ContractSave();
        }

        public async Task<List<Contract>> ContractGetAllContracts(int contractOffset, int contractCount)
        {
            List<Contract> contractsToReturn = await DbContext.Contracts.Where(x => x.ContractDeletedOn == null)
                                                                        .OrderBy(x => x.ContractCreatedOn)
                                                                        .Skip(contractOffset * (contractCount - 1))
                                                                        .Take(contractOffset)
                                                                        .ToListAsync();
            return contractsToReturn;
        }

        public async Task ContractInsert(Contract contract)
        {
            DbContext.Contracts.Add(contract);
            await this.ContractSave();
        }

        public async Task ContractSave()
        {
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Contract>> ContractsGetContractByEmployee(Guid employeeUID)
        {
            List<Contract> contractsToReturn = await DbContext.Contracts.Where(x => x.Employee.EmployeeUID == employeeUID && x.ContractDeletedOn == null).ToListAsync();
            return contractsToReturn;
        }

        public void ContractUpdate(Contract contract)
        {
            DbContext.Entry(contract).State = EntityState.Modified;
        }

        public async Task<Contract> ContactGetContract(Guid contractUID)
        {
            Contract contractToReturn = await DbContext.Contracts.Where(x => x.ContractUID == contractUID).FirstAsync();
            return contractToReturn;
        }

        public async Task<List<Contract>> ContractGetAllContracts(string employeeNmae, string employeeSurname)
        {
            List<Contract> contractsToReturn = await DbContext.Contracts.Where(x => x.Employee.EmployeeName.Contains(employeeNmae) || x.Employee.EmployeeSurname.Contains(employeeSurname) &&
                                                                               x.ContractDeletedOn == null)
                                                                        .ToListAsync();
            return contractsToReturn;
        }

        public async Task<Contract> ContractGetContractFile(Guid contractUID)
        {
            Contract contractToReturn = await DbContext.Contracts.Where(x => x.ContractUID == contractUID).FirstAsync();

            return contractToReturn;
        }

        public async Task<List<Contract>> ContractSearchContracts(string[] searchParameters, DateTime startDate, DateTime endDate)
        {
            var contractsToReturn = await DbContext.Contracts
                .Where(x => x.ContractDeletedOn == null && x.ContractStartDate >= startDate &&
                (x.ContractEndDate ?? DateTime.MaxValue) <= endDate &&
                (searchParameters.Any(p => x.ContractFileName.Contains(p)) ||
                searchParameters.Any(p => x.ContractNumber.ToString().Contains(p)) ||
                searchParameters.Any(p => ((ContractTypes)x.ContractType).ToString().Contains(p)) ||
                searchParameters.Any(p => x.Employee.EmployeeName.Contains(p)) ||
                searchParameters.Any(p => x.Employee.EmployeeSurname.Contains(p)))).ToListAsync();

            return contractsToReturn;

        }
        #endregion
    }
}
