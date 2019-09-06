﻿using BusinessLayer.BusinessEntity;
using BusinessLayer.BusinessWorkflow.Interfaces;
using DataLayer;
using SolutionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessWorkflow.Implementatons
{
    public class RequestWorkflow : IRequestWorkflow
    {
        #region Atributes
        private IRequestRepository _requestRepository;
        private IEmployeeRepository _employeeRepository;
        private IAdditionalDaysRepository _additionalDaysRepository;
        private IAdditionalDaysWorkflow _additionalDaysWorkflow;
        private IEmployeeWorkflow _employeeWorkflow;
        #endregion
        #region Constructors
        public RequestWorkflow()
        {

        }
        public RequestWorkflow(IRequestRepository requestRepository, IEmployeeRepository employeeRepository, IAdditionalDaysRepository additionalDaysRepository, IAdditionalDaysWorkflow additionalDaysWorkflow, IEmployeeWorkflow employeeWorkflow)
        {
            RequestRepository = requestRepository;
            EmployeeRepository = employeeRepository;
            AdditionalDaysRepository = additionalDaysRepository;
            AdditionalDaysWorkflow = additionalDaysWorkflow;
            EmployeeWorkflow = employeeWorkflow;
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
        public IAdditionalDaysRepository AdditionalDaysRepository
        {
            get
            {
                return _additionalDaysRepository;
            }
            private set
            {
                _additionalDaysRepository = value;
            }
        }
        public IAdditionalDaysWorkflow AdditionalDaysWorkflow
        {
            get
            {
                return _additionalDaysWorkflow;
            }
            private set
            {
                _additionalDaysWorkflow = value;
            }

        }
        public IEmployeeWorkflow EmployeeWorkflow
        {
            get
            {
                return _employeeWorkflow;
            }
            private set
            {
                _employeeWorkflow = value;
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

        public async Task<RequestEntity> RequestGetRequest(Guid requestUID)
        {
            var request = await RequestRepository.RequestsGetRequest(requestUID);
            return new RequestEntity()
            {
                EmployeeUID = request.Employee.EmployeeUID,
                RequestUID = request.RequestUID,
                RequestType = (RequestTypes)request.RequestType,
                RequestComment = request.RequestComment,
                RequestStatus = request.RequestStatus,
                RequestDenialComment = request.RequestDenialComment,
                RequestNumberOfDays = request.RequestNumberOfDays,
                RequestStartDate = request.RequestStartDate,
                RequestEndDate = request.RequestEndDate
            };
        }

        public async Task<List<RequestEntity>> RequestGetRequests(int requestCount, int requestOffset)
        {
            var requests = await RequestRepository.RequestGetRequests(requestCount, requestOffset);

            var requestsToReturn = requests.Select(x => new RequestEntity()
            {
                RequestUID = x.RequestUID,
                RequestType = (RequestTypes)x.RequestType,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestStartDate = x.RequestStartDate,
                RequestStatus = x.RequestStatus
            }).ToList();

            return requestsToReturn;
        }
        public async Task RequestPermit(Guid requestUID)
        {
            var request = await RequestRepository.RequestsGetRequest(requestUID);
            request.RequestStatus = (int)RequestStatus.Accepted;

            int remianing = await EmployeeWorkflow.EmployeeRemoveBacklogDays(request.Employee.EmployeeUID, request.RequestNumberOfDays);

            if (remianing > 0)
                await AdditionalDaysWorkflow.AdditionalDaysRemove(request.Employee.EmployeeUID, remianing);

            await RequestRepository.RequestSave();

        }

        public async Task RequestAddRequest(string employeeEmail, RequestEntity requestsEntitiy)
        {

            var employee = await EmployeeRepository.EmployeeGetEmployee(employeeEmail);

            var request = new Request()
            {
                EmployeeID = employee.EmployeeID,
                RequestUID = Guid.NewGuid(),
                RequestType = (int)requestsEntitiy.RequestType,
                RequestNumberOfDays = requestsEntitiy.RequestStartDate.GetBusinessDaysTo(requestsEntitiy.RequestEndDate),
                RequestComment = requestsEntitiy.RequestComment,
                RequestStartDate = requestsEntitiy.RequestStartDate,
                RequestEndDate = requestsEntitiy.RequestEndDate,
                RequestCreatedOn = DateTime.UtcNow,
                RequestStatus = (int)RequestStatus.InReview
            };

            await RequestRepository.RequestInsert(request);
        }

        public async Task<List<RequestEntity>> RequestsGetRequests(Guid employeeUID)
        {
            var requests = await RequestRepository.RequestGetAllEmployeeRequests(employeeUID);
            var requestToReturn = requests.Select(x => new RequestEntity()
            {
                RequestUID = x.RequestUID,
                RequestType = (RequestTypes)x.RequestType,
                RequestComment = x.RequestComment,
                RequestStatus = x.RequestStatus,
                RequestStartDate = x.RequestStartDate,
                RequestEndDate = x.RequestEndDate,
                RequestDenialComment = x.RequestDenialComment,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestFile = x.RequestFile
            }).ToList();

            return requestToReturn;
        }

        public async Task RequestEditRequest(RequestEntity requestEntity)
        {
            var request = await RequestRepository.RequestsGetRequest(requestEntity.RequestUID);

            if (requestEntity.RequestType != 0)
                request.RequestType = (int)requestEntity.RequestType;

            if (requestEntity.RequestDenialComment != null)
                request.RequestDenialComment.Concat(requestEntity.RequestDenialComment);

            if (requestEntity.RequestStartDate != DateTime.MinValue)
                request.RequestStartDate = requestEntity.RequestStartDate;

            if (requestEntity.RequestEndDate != DateTime.MinValue)
                request.RequestEndDate = requestEntity.RequestEndDate;

            request.RequestNumberOfDays = request.RequestStartDate.GetBusinessDaysTo(request.RequestEndDate);

            request.RequestStatus = (int)RequestStatus.Adjusted;

            await RequestRepository.RequestSave();
        }

        public async Task<List<RequestEntity>> RequestSearchRequests(string inputString, DateTime startDate, DateTime endDate)
        {
            var searchString = inputString.Split(' ');

            var requests = await RequestRepository.RequestSearchRequest(searchString, startDate, endDate);

            var requestToReturn = requests.Select(x => new RequestEntity()
            {
                RequestUID = x.RequestUID,
                RequestType = (RequestTypes)x.RequestType,
                RequestNumberOfDays = x.RequestNumberOfDays,
            }).ToList();

            return requestToReturn;
        }

        public async Task<int> RequestGetTotalAvailableDays(Guid employeeUID)
        {
            var additionalDays = await AdditionalDaysRepository.AdditionalDayGetTotalAdditionalDays(employeeUID);

            var employeeBacklogDays = await EmployeeRepository.EmployeeGetEmployeeBacklogDays(employeeUID);

            var totalDaysToReturn = additionalDays + employeeBacklogDays;

            return totalDaysToReturn;
        }

        public async Task RequestCollective(int requestId, DateTime startDate, DateTime endDate)
        {
            var numberOfDays = startDate.GetBusinessDaysTo(endDate);

            Expression<Func<Employee, bool>> emplSpec = x => x.EmployeeDeletedOn == null;

            Expression<Func<Employee, int>> emplProj = x => x.EmployeeID;
 
            var employees = await EmployeeRepository.EmployeeGetListOfIntegers(emplSpec, emplProj);

            string comment = "";
            comment.CollevtiveCommentTo();

            //new guid, created on ide u konstruktor da nije dbfirst, 
            var requests = employees.Select(x => new Request()
            {
                EmployeeID = x,
                RequestUID = Guid.NewGuid(),
                RequestType = (int)RequestTypes.Annual,
                RequestComment = comment,
                RequestCreatedOn = DateTime.UtcNow,
                RequestStatus = (int)RequestStatus.Accepted,
                RequestNumberOfDays = numberOfDays,
                RequestID = requestId,
                RequestStartDate = startDate,
                RequestEndDate = endDate
            }).ToList();

            requests.Select(async x => await RequestRepository.RequestInsert(x));

        }
        #endregion
    }
}
