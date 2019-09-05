using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IContractService
    {
        Task<List<ApplicationContract>> ContractGetAllContracts(int contractOffset, int contractCount);
        Task<List<ApplicationContract>> ContractGetContracts(string employeeName, string employeeSurname);
        Task<List<ApplicationContract>> ContractSearchContracts(string inputParameters, DateTime startDate, DateTime endDate);
        Task<List<ApplicationContract>> ContractGetContracts(Guid employeeUID);
        Task<ApplicationContract> ContractGetContract(Guid contractUID);
        Task ContractAddContract(ApplicationContract applicationContract);
        Task ContractDeleteContract(Guid contractUID);
        Task<ApplicationContract> ContractGetContactFile(Guid contractUID);
    }
}
