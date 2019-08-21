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


            var requestEntity = new RequestsEntity()
            {
                RequestUID = Guid.NewGuid(),
                RequestComment = applicationRequest.RequestComment,
                RequestCreatedOn = DateTime.UtcNow,
                RequestNumberOfDays = applicationRequest.RequestNumberOfDays,
                RequestStardDate = applicationRequest.RequestStartDate,
                RequestEndDate = applicationRequest.RequestEndDate,
                RequestStatus = (int)RequestStatus.InReview,
                RequestType = applicationRequest.RequestType
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
                RequestComment = request.RequestComment,
                EmployeeUID = request.EmployeeUID,
                RequestDenialComment = request.RequestDenialComment,
                RequestType = request.RequestType,
                RequestStartDate = request.RequestStardDate,
                RequestEndDate = request.RequestEndDate,
                RequestNumber = request.RequestNumber,
                RequestNumberOfDays = request.RequestNumberOfDays,
                RequestStatus = (RequestStatus)request.RequestStatus,
                RequestFile = request.RequestFile
            };

            return requestToReturn;
        }

        public async Task RequestPermit(Guid requestUID)
        {
            await RequestWorkflow.RequestPermit(requestUID);
        }

        public async Task<List<ApplicationRequest>> RquestGetRequests(int requestCount, int requestOffset)
        {
            var requests = await RequestWorkflow.RequestGetRequests(requestCount, requestOffset);

            var requestsToReturn = requests.Select(x => new ApplicationRequest()
            {
                RequestUID = x.RequestUID,
                RequestType = x.RequestType,
                RequestNumber = x.RequestNumber,
            }).ToList();

            return requestsToReturn;
        }
        #endregion
    }
}
