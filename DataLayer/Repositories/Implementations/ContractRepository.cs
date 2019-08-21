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
        {   get
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

        public async Task<List<Contract>> ContractGetAllContracts()
        {
            List<Contract> contractsToReturn = await DbContext.Contracts.Where(x => x.ContractDeletedOn == null)
                                                    .Select(x => new Contract
                                                    {
                                                        EmployeeID = x.EmployeeID,
                                                        ContractNumber = x.ContractNumber,
                                                        ContractType = x.ContractType,
                                                        ContractStartDate = x.ContractStartDate,
                                                        ContractEndDate = x.ContractEndDate
                                                    }).ToListAsync();
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
            List<Contract> contractsToReturn = await DbContext.Contracts.Where(x => x.Employee.EmployeeUID == employeeUID && x.ContractDeletedOn == null)
                                                     .Select(x => new Contract
                                                     {
                                                         ContractNumber = x.ContractNumber,
                                                         ContractType = x.ContractType,
                                                         ContractStartDate = x.ContractStartDate,
                                                         ContractEndDate = x.ContractEndDate
                                                     }).ToListAsync();
            return contractsToReturn;
        }

        public void ContractUpdate(Contract contract)
        {
            DbContext.Entry(contract).State = EntityState.Modified;
        }

        public async Task<Contract> ContactGetContract(Guid contractUID)
        {
            var contractToReturn = await DbContext.Contracts.Where(x => x.ContractUID == contractUID).FirstAsync();
            return contractToReturn;
        }

        public async Task<List<Contract>> ContractGetAllContracts(string employeeNmae, string employeeSurname)
        {
            var contractsToReturn = await DbContext.Contracts.Where(x => x.Employee.EmployeeName.Contains(employeeNmae) || x.Employee.EmployeeSurname.Contains(employeeSurname) && x.ContractDeletedOn == null)
                                                     .ToListAsync();
            return contractsToReturn;
        }
        #endregion
    }
}
