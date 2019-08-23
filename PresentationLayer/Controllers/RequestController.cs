using ApplicationLayer;
using ApplicationLayer.Entities;
using Microsoft.AspNet.Identity.Owin;
using SolutionEnums;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VacaYAY.Models;

namespace VacaYAY.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        #region Atributes
        private ApplicationService _applicationService;
        #endregion
        #region Constructors
        public RequestController()
        {

        }
        public RequestController(ApplicationService applicationService)
        {
            ApplicationService = applicationService;
        }
        #endregion
        #region Properties
        public ApplicationService ApplicationService
        {
            get
            {
                return _applicationService;
            }
            private set
            {
                _applicationService = value;
            }
        }
        #endregion

        [Route("Requests/ApproveModal")]
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public ActionResult ApproveModal()
        {
            return View("~/Views//Modal/ApproveModal.cshtml");
        }

        [Route("Requests/DenyModal")]
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public ActionResult DenyModal()
        {
            return View("~/Views/Modal/DenyModal.cshtml");
        }
        [HttpGet]
        [Route("Requests/GetRequests/{pageOffset}/{pageCount}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetRequests()
        {

            int requestOffset = Int32.Parse(Request["pageOffset"]);
            int requestCount = Int32.Parse(Request["pageCount"]);

            var requests = await ApplicationService.RequestService.RquestGetRequests((int)requestCount, (int)requestOffset);
            var requestsToReturn = requests.Select(x => new ReturnRequestViewModel()
            {
                RequestUID = x.RequestUID,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestType = x.RequestType,
            }).ToList();

            return View(requestsToReturn);
        }

        [HttpGet]
        [Route("Requests/GetRequest/{UID}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetRequest(Guid? UID)
        {

            var requestUID = Guid.Parse(Request["requestUID"]);
            var request = await ApplicationService.RequestService.RequestGetRequest(requestUID);
            var employee = await ApplicationService.EmployeeService.EmployeeGetEmployee(request.EmployeeUID);
            var requestToReturn = new ReturnRequestViewModel()
            {
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                RequestType = request.RequestType,
                RequestStartDate = request.RequestStartDate,
                RequestEndDate = request.RequestEndDate,
                RequestUID = request.RequestUID,
                RequestComment = request.RequestComment ?? "No employee comment"
            };
            return View(requestToReturn);
        }

        [HttpGet]
        [Route("Requests/AddRequestView")]
        [Authorize(Roles = "ADMIN, USER")]
        public ActionResult AddRequestView()
        {
            return View();
        }

        [HttpPost]
        [Route("Request/AddRequest")]
        [Authorize(Roles = "ADMIN, USER")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRequest(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddRequestView");
            }

            var request = new ApplicationRequest()
            {
                RequestComment = model.RequestComment,
                RequestType = model.RequestType,
                RequestNumberOfDays = model.RequestNumberOfDays,
                RequestStartDate = model.RequestStartDate,
                RequestEndDate = model.RequestEndDate
            };

            await ApplicationService.RequestService.RequestAddRequest(User.Identity.Name, request);

            //var availableDays = await DbContext.AdditionalDays.Where(x => x.EmployeeID == UserManager.FindByNameAsync(User.Identity.Name).Id)
            //                                                  .Select(x => x.AdditionalDaysNumberOfAdditionalDays)
            //                                                  .SumAsync();
            //availableDays += (int) await DbContext.Employees.Where(x => x.EmployeeID == UserManager.FindByNameAsync(User.Identity.Name).Id).Select(x => x.EmployeeBacklogDays).FirstAsync();

            //if(availableDays < model.RequestNumberOfDays)
            //{
            //    return View("Not enough available days");
            //}

            //double calcBusinessDays = 1 + ((model.RequestEndDate - model.RequestStartDate).TotalDays * 5 - (model.RequestStartDate.DayOfWeek - model.RequestEndDate.DayOfWeek) * 2) / 7;
            //if (model.RequestEndDate.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            //if (model.RequestStartDate.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;


            //var request = new Request()
            //{
            //    EmployeeID = (await UserManager.FindByNameAsync(User.Identity.Name)).Id,
            //    ReqeustUID = Guid.NewGuid(),
            //    RequestType = model.RequestType,
            //    RequestComment = model.RequestComment,
            //    RequestStatus = (int)RequestStatus.InReview,
            //    RequestNumberOfDays = (int)calcBusinessDays,
            //    RequestStartDate = model.RequestStartDate,
            //    RequestEndDate = model.RequestEndDate,
            //    RequestCreatedOn = DateTime.UtcNow
            //};

            //DbContext.Requests.Add(request);
            //await DbContext.SaveChangesAsync();

            return View();
        }

        [HttpPost]
        [Route("Request/PermitOrDenyRequest")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PermitOrDenyRequest(ProcessRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (model.RequestStatus == RequestStatus.Accepted)
            {
                await ApplicationService.RequestService.RequestPermit(model.RequestUID);
            }
            else
            {

            }
            //var request = await DbContext.Requests.Where(x => x.ReqeustUID == model.RequestUID).FirstAsync();
            //request.RequestDenialComment = model.RequestDenialComment;
            //request.RequestStatus = model.RequestStatus;

            //if(request.RequestStatus == (int)RequestStatus.Accepted)
            //{
            //    var totalDays = request.RequestNumberOfDays;
            //    totalDays -= (int)request.Employee.EmployeeBacklogDays;
            //    if (totalDays >= 0)
            //    {
            //        request.Employee.EmployeeBacklogDays = 0;

            //        var result = await DbContext.AdditionalDays.Where(x => x.EmployeeID == request.EmployeeID && x.AdditionalDaysDeletedOn == null).ToListAsync();
            //        foreach(var res in result)
            //        {
            //            if (totalDays == 0)
            //                continue;
            //            totalDays -= res.AdditionalDaysNumberOfAdditionalDays;
            //            if (totalDays != 0)
            //                res.AdditionalDaysDeletedOn = DateTime.UtcNow;
            //            else
            //                res.AdditionalDaysNumberOfAdditionalDays -= totalDays;
            //        }
            //    }
            //    else
            //        request.Employee.EmployeeBacklogDays -= totalDays;
            //}
            //await DbContext.SaveChangesAsync();
            return View();

           // return new Json(new { HasError }); //todo ende
        }


        [HttpPost]
        [Route("Request/Alter")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AlterRequest(AlterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //var request = await DbContext.Requests.Where(x => x.ReqeustUID == model.RequestUID).FirstAsync();
            //request.RequestStartDate = model.RequestStartDate;
            //request.RequestEndDate = model.RequestEndDate;
            //request.RequestType = model.RequestType;

            //if(User.IsInRole("ADMIN"))
            //{
            //    request.RequestDenialComment = model.RequestComment;
            //}
            //else
            //{
            //    request.RequestComment = model.RequestComment;
            //}

            //await DbContext.SaveChangesAsync();

            return View();
        }


    }
}