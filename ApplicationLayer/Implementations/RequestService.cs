using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces;
using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using SolutionEnums;

namespace ApplicationLayer.Implementations
{
    public class RequestService : IRequestService
    {
        #region Atributes 
        private IRequestWorkflow _requestWorkflow;
        #endregion
        #region Constructors 
        public RequestService()
        {

        }
        public RequestService(IRequestWorkflow requestWorkflow)
        {
            RequestWorkflow = requestWorkflow;
        }
        #endregion
        #region Properties
        public IRequestWorkflow RequestWorkflow
        {
            get
            {
                return _requestWorkflow;
            }
            private set
            {
                _requestWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task RequestAddRequest(string employeeEmail, ApplicationRequest applicationRequest)
        {


            var requestEntity = new RequestEntity()
            {
                RequestUID = Guid.NewGuid(),
                RequestComment = applicationRequest.RequestComment,
                RequestCreatedOn = DateTime.UtcNow,
                RequestNumberOfDays = applicationRequest.RequestNumberOfDays,
                RequestStartDate = (DateTime)applicationRequest.RequestStartDate,
                RequestEndDate = (DateTime)applicationRequest.RequestEndDate,
                RequestStatus = RequestStatus.InReview,
                RequestType = (RequestTypes)applicationRequest.RequestType
            };
            await RequestWorkflow.RequestAddRequest(employeeEmail, requestEntity);
        }

        public async Task RequestDeny(ApplicationRequest applicationRequest)
        {
            await RequestWorkflow.RequestDeny(applicationRequest.RequestUID, applicationRequest.RequestDenialComment);
        }

        public async Task<ApplicationRequest> RequestGetRequest(Guid requestGuid)
        {
            var request = await RequestWorkflow.RequestGetRequest(requestGuid);

            var requestToReturn = new ApplicationRequest()
            {
                RequestUID = request.RequestUID,
                RequestComment = request.RequestComment,
                EmployeeUID = request.EmployeeUID,
                RequestDenialComment = request.RequestDenialComment,
                RequestType = request.RequestType,
                RequestStartDate = request.RequestStartDate,
                RequestEndDate = request.RequestEndDate,
                RequestNumberOfDays = request.RequestNumberOfDays,
                RequestStatus = request.RequestStatus,
                RequestFile = request.RequestFile
            };

            return requestToReturn;
        }

        public async Task RequestPermit(Guid requestUID)
        {
            await RequestWorkflow.RequestPermit(requestUID);
        }

        public async Task<List<ApplicationRequest>> RequestGetRequests(int requestCount, int requestOffset)
        {
            var requests = await RequestWorkflow.RequestGetRequests(requestCount, requestOffset);

            var requestsToReturn = requests.Select(x => new ApplicationRequest()
            {
                RequestUID = x.RequestUID,
                RequestType = x.RequestType,
                RequestNumberOfDays = x.RequestNumberOfDays
            }).ToList();

            return requestsToReturn;
        }

        public async Task<List<ApplicationRequest>> RequestGetRequests(Guid employeeUID)
        {
            var requests = await RequestWorkflow.RequestsGetRequests(employeeUID);

            var requestsToReturn = requests.Select(x => new ApplicationRequest()
            {
                RequestUID = x.RequestUID,
                RequestType = x.RequestType,
                RequestComment = x.RequestComment,
                RequestStatus = (RequestStatus)x.RequestStatus,
                RequestStartDate = x.RequestStartDate,
                RequestEndDate = x.RequestEndDate,
                RequestDenialComment = x.RequestDenialComment,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestFile = x.RequestFile
            }).ToList();

            return requestsToReturn;
        }

        public async Task RequestEditRequest(ApplicationRequest applicationRequest)
        {
            var request = new RequestEntity()
            {
                RequestUID = applicationRequest.RequestUID,
                RequestDenialComment = applicationRequest.RequestDenialComment,
            };

            if (applicationRequest.RequestType != null)
                request.RequestType = (RequestTypes)applicationRequest.RequestType;

            if (applicationRequest.RequestStartDate != null)
                request.RequestStartDate = (DateTime)applicationRequest.RequestStartDate;

            if (applicationRequest.RequestEndDate != null)
                request.RequestEndDate = (DateTime)applicationRequest.RequestEndDate;

            await RequestWorkflow.RequestEditRequest(request);
        }

        public async Task<List<ApplicationRequest>> RequestSearchRequests(string searchInput, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null)
                startDate = DateTime.MinValue;
            if (endDate == null)
                endDate = DateTime.MaxValue;

            var requests = await RequestWorkflow.RequestSearchRequests(searchInput, (DateTime)startDate, (DateTime)endDate);

            var requestToReturn = requests.Select(x => new ApplicationRequest()
            {
                RequestUID = x.RequestUID,
                RequestType = x.RequestType,
                RequestNumberOfDays = x.RequestNumberOfDays

            }).ToList();

            return requestToReturn;
        }

        public async Task<int> RequestRequestGetTotalAvailableDays(Guid employeeUID)
        {
            var totalDaysToReturn = await RequestWorkflow.RequestGetTotalAvailableDays(employeeUID);

            return totalDaysToReturn;
        }

        public async Task RequestCollective(int requestID, DateTime startDate, DateTime endDate)
        {
            await RequestWorkflow.RequestCollective(requestID, startDate, endDate);
        }

        public async Task<List<ApplicationRequest>> RequestGetPendingRequests(Guid employeeUID)
        {
            var requests = await RequestWorkflow.RequestGetPendingRequests(employeeUID);

            var requestToReturn = requests.Select(x => new ApplicationRequest()
            {
                RequestUID = x.RequestUID,
                RequestType = x.RequestType,
                RequestStatus = x.RequestStatus
            }).ToList();

            return requestToReturn;
        }
        #endregion
    }
}
