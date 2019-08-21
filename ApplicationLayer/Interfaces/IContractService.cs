using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IContractService
    {
        Task<List<ApplicationContract>> ContractGetAllContracts();
        Task<List<ApplicationContract>> ContractGetContracts(string employeeName, string employeeSurname);
        Task<List<ApplicationContract>> contractGetContracts(Guid employeeUID);
        Task<ApplicationContract> ContractGetContract(Guid contractUID);
        Task ContractAddContract(ApplicationContract applicationContract);
        Task ContractDeleteContract(Guid contractUID);
    }
}
