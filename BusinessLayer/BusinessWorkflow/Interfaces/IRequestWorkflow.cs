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
        Task<List<RequestsEntity>> RequestGetRequests(int requestCount, int requestOffset);
        Task<RequestsEntity> RequestGetRequest(Guid requestUID);
        Task RequestAddRequest(string employeeEmail, RequestsEntity requestsEntitiy);
        Task RequestPermit(Guid requestUID);
        Task RequestDeny(Guid requestUID, string denialComment);
    }
}
