using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using DataLayer;
using SolutionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Implementatons
{
    public class RequestWorkflow : IRequestWorkflow
    {
        #region Atributes
        private IRequestRepository _requestRepository;
        private IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructors
        public RequestWorkflow()
        {

        }
        public RequestWorkflow(IRequestRepository requestRepository, IEmployeeRepository employeeRepository)
        {
            RequestRepository = requestRepository;
            EmployeeRepository = employeeRepository;
        }
        #endregion
        #region Properties
        public IRequestRepository RequestRepository
        {
            get
            {
                return _requestRepository;
            }
            private set
            {
                _requestRepository = value;
            }
        }
        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return _employeeRepository;
            }
            private set
            {
                _employeeRepository = value;
            }
        }
        #endregion
        #region Methods
        public async Task RequestDeny(Guid requestUID, string denialComment)
        {
            var request = await RequestRepository.RequestsGetRequest(requestUID);
            request.RequestStatus = (int)RequestStatus.Rejected;
            request.RequestDenialComment = denialComment;
            request.RequestDeletedOn = DateTime.UtcNow;
            await RequestRepository.RequestSave();
        }

        public async Task<RequestsEntity> RequestGetRequest(Guid requestUID)
        {
            var request = await RequestRepository.RequestsGetRequest(requestUID);
            return new RequestsEntity()
            {
                RequestUID = request.RequestUID,
                RequestNumber = request.RequestNumber,
                RequestType = request.RequestType,
                RequestComment = request.RequestComment,
                RequestStatus = request.RequestStatus,
                RequestDenialComment = request.RequestDenialComment,
                RequestNumberOfDays = request.RequestNumberOfDays,
                RequestStardDate = request.RequestStartDate,
                RequestEndDate = request.RequestEndDate
            };
        }

        public async Task<List<RequestsEntity>> RequestGetRequests(int requestCount, int requestOffset)
        {
            var requests = await RequestRepository.RequestGetRequests(requestCount, requestOffset);

            var requestsToReturn = requests.Select(x => new RequestsEntity()
            {
                RequestUID = x.RequestUID,
                RequestType = x.RequestType,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestStardDate = x.RequestStartDate,
                RequestStatus = x.RequestStatus
            }).ToList();

            return requestsToReturn;
        }

        public async Task RequestPermit(Guid requestUID)
        {
            var request = await RequestRepository.RequestsGetRequest(requestUID);
            request.RequestStatus = (int)RequestStatus.Accepted;
            
        }

        public async Task RequestAddRequest(string employeeEmail, RequestsEntity requestsEntitiy)
        {

            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeEmail);

            var request = new Request()
            {
                EmployeeID = employee.EmployeeID,
                RequestUID = Guid.NewGuid(),
                RequestType = requestsEntitiy.RequestType,
                RequestNumberOfDays = requestsEntitiy.RequestNumberOfDays,
                RequestComment = requestsEntitiy.RequestComment,
                RequestStartDate = requestsEntitiy.RequestStardDate,
                RequestEndDate = requestsEntitiy.RequestEndDate,
                RequestCreatedOn = DateTime.UtcNow,
                RequestStatus = (int)RequestStatus.InReview
            };

            await RequestRepository.RequestInsert(request);
        }
        #endregion
    }
}
