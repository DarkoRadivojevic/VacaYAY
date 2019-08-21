using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRequestRepository
    {
        Task<List<Request>> RequestGetRequests(int requestCount, int requestSize);
        Task<Request> RequestsGetRequest(Guid requestUID);
        Task<List<Request>> RequestGetAllEmployeeRequests(Guid employeeUID);
        Task RequestInsert(Request request);
        Task RequestDelete(Guid requestUID);
        void ReqeustUpdate(Request request);
        Task RequestSave();
    }
}
