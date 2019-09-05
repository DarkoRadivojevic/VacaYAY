using BusinessLayer.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Interfaces
{
    public interface IContractWorkflow
    {
        Task<List<ContractEntity>> ContractGetAllContracts(int contractOffset, int contractCount);
        Task<List<ContractEntity>> ContractGetContracts(string employeeName, string employeeSurname);
        Task<List<ContractEntity>> ContractSearchContract(string inputParameters, DateTime startDate, DateTime endDate);
        Task<List<ContractEntity>> ContractGetContracts(Guid employeeUID);
        Task<ContractEntity> ContractGetContract(Guid contractUID);
        Task ContractAddContract(ContractEntity contractEntity);
        Task ContractDeleteContract(Guid contractUID);
        Task<ContractEntity> ContractGetContractFile(Guid contactUID);
    }
}
