using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IContractRepository
    {
        Task<List<Contract>> ContractGetAllContracts(int contractOffset, int contractCount);
        Task<List<Contract>> ContractGetAllContracts(string employeeNmae, string employeeSurname);
        Task<List<Contract>> ContractSearchContracts(string[] searchParameters, DateTime startDate, DateTime endDate);
        Task<List<Contract>> ContractsGetContractByEmployee(Guid employeUID);
        Task<Contract> ContractGetContractFile(Guid contractUID);
        Task<Contract> ContactGetContract(Guid contractUID);
        Task ContractInsert (Contract contract);
        Task ContractDelete(Guid contractUID);
        Task ContractSave();
        void ContractUpdate(Contract contract);
        
    }
}
