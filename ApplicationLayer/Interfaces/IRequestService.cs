using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IRequestService
    {
        Task<List<ApplicationRequest>> RquestGetRequests(int requestCount, int requestOffset);
        Task<ApplicationRequest> RequestGetRequest(Guid requestGuid);
        Task RequestAddRequest(string employeeEmail, ApplicationRequest applicationRequest);
        Task RequestPermit(Guid requestUID);
        Task RequestDeny(ApplicationRequest applicationRequest);
    }
}
