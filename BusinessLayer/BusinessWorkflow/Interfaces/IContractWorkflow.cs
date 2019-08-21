using BusinessLayer.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Interfaces
{
    public interface IContractWorkflow
    {
        Task<List<ContractEntity>> ContractGetAllContracts();
        Task<List<ContractEntity>> ContractGetContracts(string employeeName, string employeeSurname);
        Task<List<ContractEntity>> ContractGetContracts(Guid employeeUID);
        Task<ContractEntity> ContractGetContract(Guid contractUID);
        Task ContractAddContract(ContractEntity contractEntity);
        Task ContractDeleteContract(Guid contractUID);
    }
}
