using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRequestRepository
    {
        Task<List<Request>> RequestGetRequests(int requestCount, int requestSize);
        Task<Request> RequestsGetRequest(Guid requestUID);
        Task<List<Request>> RequestGetAllEmployeeRequests(Guid employeeUID);
        Task<List<Request>> RequestSearchRequest(string[] searchString, DateTime startDate, DateTime endDate);
        Task RequestInsert(Request request);
        Task RequestDelete(Guid requestUID);
        void ReqeustUpdate(Request request);
        Task RequestSave();
    }
}
