using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SolutionEnums;

namespace DataLayer.Implementations
{
    public class RequestRepository : IRequestRepository
    {
        #region Atributes 
        private VacaYAYContext _dbContext;
        #endregion
        #region Constructors
        public RequestRepository()
        {

        }
        public RequestRepository(VacaYAYContext dbContext)
        {
            DbContext = dbContext;
        }
        #endregion
        #region Properties
        public VacaYAYContext DbContext
        {
            get
            {
                return _dbContext;
            }
            private set
            {
                _dbContext = value;
            }
        }
        #endregion
        #region Methods
        public async Task RequestSave()
        {
            await DbContext.SaveChangesAsync();
        }

        public void ReqeustUpdate(Request request)
        {
            DbContext.Entry(request).State = EntityState.Modified;

        }

        public async Task RequestDelete(Guid requestUID)
        {
            Request request = await DbContext.Requests.Where(x => x.RequestUID == requestUID && 
                    x.RequestDeletedOn == null)
                .FirstAsync();
            request.RequestDeletedOn = DateTime.UtcNow;

            await this.RequestSave();
        }

        public async Task<List<Request>> RequestGetRequests(int requestCount, int requestOffset)
        {
            List<Request> requestToReturn = await DbContext.Requests
                .Where(x => x.RequestDeletedOn == null &&
                       x.RequestStatus == (int)RequestStatus.InReview)
                .OrderBy(x => x.RequestCreatedOn)
                .Skip(requestOffset * (requestCount - 1))
                .Take(requestOffset).ToListAsync();

            return requestToReturn;
        }

        public async Task RequestInsert(Request request)
        {
            DbContext.Requests.Add(request);
            await this.RequestSave();
        }

        public async Task<Request> RequestsGetRequest(Guid requestUID)
        {
            Request requestToReturn = await DbContext.Requests.Where(x => x.RequestUID == requestUID).FirstAsync();

            return requestToReturn;
        }

        public async Task<List<Request>> RequestGetAllEmployeeRequests(Guid employeeUID)
        {
            List<Request> requestsToReturn = await DbContext.Requests.Where(x => x.Employee.EmployeeUID == employeeUID).ToListAsync();
            return requestsToReturn;
        }

        public async Task<List<Request>> RequestSearchRequest(string[] searchString, DateTime startDate, DateTime endDate)
        {
            IQueryable<Request> queryableRequests = DbContext.Requests.AsQueryable();

            queryableRequests = queryableRequests.Where(x => x.RequestDeletedOn == null &&
                                                        x.RequestStatus == (int)RequestStatus.InReview &&
                                                        x.RequestStartDate >= startDate && 
                                                        x.RequestEndDate <= endDate &&
                                                        (searchString.Any(s => x.RequestComment.Contains(s)) ||
                                                        searchString.Any(s => x.RequestDenialComment.Contains(s)) ||
                                                        searchString.Contains(x.RequestFileName) ||
                                                        searchString.Contains(x.RequestNumberOfDays.ToString()) ||
                                                        searchString.Contains(((RequestTypes)x.RequestType).ToString()) || 
                                                        searchString.Contains(x.Employee.EmployeeName) ||
                                                        searchString.Contains(x.Employee.EmployeeSurname)));

            List<Request> requestsToReturn = await queryableRequests.ToListAsync();

            return requestsToReturn;
        }

        public async Task<List<Request>> RequestGetRequests(Expression<Func<Request, bool>> expressionSpecification,
            Expression<Func<Request, Request>> expressionProjection)
        {
            List<Request> requestToReturn = await DbContext.Requests
                .Where(expressionSpecification)
                .Select(expressionProjection)
                .ToListAsync();

            return requestToReturn;
        }
        #endregion
    }
}
