using BusinessLayer.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Interfaces
{
    public interface IRequestWorkflow
    {
        Task<List<RequestEntity>> RequestGetRequests(int requestCount, int requestOffset);
        Task<RequestEntity> RequestGetRequest(Guid requestUID);
        Task RequestAddRequest(string employeeEmail, RequestEntity requestEntity);
        Task RequestPermit(Guid requestUID);
        Task RequestDeny(Guid requestUID, string denialComment);
        Task<List<RequestEntity>> RequestsGetRequests(Guid employeeUID);
        Task<int> RequestGetTotalAvailableDays(Guid employeeUID);
        Task RequestEditRequest(RequestEntity requestEntity);
        Task<List<RequestEntity>> RequestSearchRequests(string inputString, DateTime startDate, DateTime endDate);
        Task RequestCollective(int requestID, DateTime startDate, DateTime endDate);
        Task<List<RequestEntity>> RequestGetPendingRequests(Guid employeeUID);
    }
}
