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
        Task<List<ApplicationRequest>> RequestGetRequests(int requestCount, int requestOffset);
        Task<ApplicationRequest> RequestGetRequest(Guid requestGuid);
        Task RequestAddRequest(string employeeEmail, ApplicationRequest applicationRequest);
        Task<int> RequestRequestGetTotalAvailableDays(Guid employeeUID);
        Task RequestPermit(Guid requestUID);
        Task RequestDeny(ApplicationRequest applicationRequest);
        Task<List<ApplicationRequest>> RequestGetRequests(Guid employeeUID);
        Task RequestEditRequest(ApplicationRequest applicationRequest);
        Task<List<ApplicationRequest>> RequestSearchRequests(string searchInput, DateTime? startDate, DateTime? endDate);
    }
}
