using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            Request request = await DbContext.Requests.Where(x => x.RequestUID == requestUID && x.RequestDeletedOn == null).FirstAsync();
            request.RequestDeletedOn = DateTime.UtcNow;
          
            await this.RequestSave();
        }

        public async Task<List<Request>> RequestGetRequests(int requestSize, int requestOffset)
        {
            List<Request> requestToReturn = await DbContext.Requests.Where(x => x.RequestDeletedOn == null && x.RequestStatus == (int)RequestStatus.InReview)
                                                                    .OrderBy(x => x.RequestCreatedOn)
                                                                    .Skip(requestOffset * ( requestSize - 1))
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
        #endregion
    }
}
