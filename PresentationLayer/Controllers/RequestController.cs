
using ApplicationLayer;
using ApplicationLayer.Entities;
using Enums;
using SolutionEnums;
using System;
using System.Linq;
using System.Threading.Tasks;
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
        #region Methods
        [Route("Requests/ApproveModal")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult ApproveModal()
        {
            return View("~/Views/Modal/ApproveModal.cshtml");
        }

        [Route("Requests/DenyModal")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult DenyModal(Guid RequestUID)
        {

            var model = new ProcessRequestViewModel()
            {
                RequestUID = RequestUID
            };
            return View("~/Views/Modal/DenyModal.cshtml", model);
        }

        [HttpPost]
        [Route("Requests/GetRequests")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetRequests(int pageCount, int pageOffset)
        {

            var requests = await ApplicationService.RequestService.RequestGetRequests(pageCount, pageOffset);
            var requestsToReturn = requests.Select(x => new ReturnRequestViewModel()
            {
                RequestUID = x.RequestUID,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestType = (RequestTypes)x.RequestType,
                RequestStatus = x.RequestStatus
            }).ToList();

            return View(requestsToReturn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Requests/GetRequest")]
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<ActionResult> GetRequest(Guid requestUID)
        {
            var request = await ApplicationService.RequestService.RequestGetRequest(requestUID);
            var employee = await ApplicationService.EmployeeService.EmployeeGetEmployee(request.EmployeeUID);
            var totalDays = await ApplicationService.RequestService.RequestRequestGetTotalAvailableDays(employee.EmployeeUID);

            var requestToReturn = new ReturnRequestViewModel()
            {
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                RequestType = (RequestTypes)request.RequestType,
                RequestStartDate = (DateTime)request.RequestStartDate,
                RequestEndDate = (DateTime)request.RequestEndDate,
                RequestNumberOfDays = request.RequestNumberOfDays,
                EmployeeAvailableDays = totalDays,
                RequestUID = request.RequestUID,
                RequestComment = request.RequestComment ?? "No employee comment",
                RequestStatus = request.RequestStatus,
                RequestDenialComment = request.RequestDenialComment
            };
            return View(requestToReturn);
        }

        [HttpPost]
        [Route("Request/GetRequestFile")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "USER")]
        public async Task<JsonResult> GetRequestFile(Guid requestUID)
        {
            var request = await ApplicationService.RequestService.RequestGetRequestFile(requestUID);

            return Json(new
            {
                RequestFile = request.RequestFile,
                RequestFileName = request.RequestFileName
            });
        }

        [HttpPost]
        [Route("Requests/GetPendingRequests")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "USER")]
        public async Task<ActionResult> GetPendingRequests()
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var employee = await ApplicationService.EmployeeService.EmployeeFindCurrentEmployee(User.Identity.Name);

            var requests = await ApplicationService.RequestService.RequestGetPendingRequests(employee.EmployeeUID);

            var model = requests.Select(x => new ReturnRequestViewModel()
            {
                RequestUID = x.RequestUID,
                RequestType = (RequestTypes)x.RequestType,
                RequestStatus = x.RequestStatus
            }).ToList();

            return View("GetRequests", model);
        }

        [HttpPost]
        [Route("Requests/GetEmployeeRequests")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEmployeeRequests(Guid employeeUID)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var requests = await ApplicationService.RequestService.RequestGetRequests(employeeUID);
            var requestsToReturn = requests.Select(x => new ReturnRequestViewModel()
            {
                RequestStartDate = (DateTime)x.RequestStartDate,
                RequestEndDate = (DateTime)x.RequestEndDate,
                RequestComment = x.RequestComment,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestType = (RequestTypes)x.RequestType,
                RequestUID = x.RequestUID
            }).ToList();

            return View(requestsToReturn);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Requests/AddRequestView")]
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<ActionResult> AddRequestView(Guid employeeUID)
        {
            var totalDays = await ApplicationService.RequestService.RequestRequestGetTotalAvailableDays(employeeUID);

            var model = new RequestViewModel()
            {
                TotalAvailableDays = totalDays
            };

            return View(model);
        }

        [HttpPost]
        [Route("Request/AddRequest")]
        [Authorize(Roles = "ADMIN, USER")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRequest(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("AddRequestView", model);
            }

            var request = new ApplicationRequest()
            {
                RequestComment = model.RequestComment,
                RequestType = model.RequestType,
                RequestStartDate = model.RequestStartDate,
                RequestEndDate = model.RequestEndDate
            };

            await ApplicationService.RequestService.RequestAddRequest(User.Identity.Name, request);
            return PartialView();
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
                var employee = await ApplicationService.EmployeeService.EmployeeFindCurrentEmployee(User.Identity.Name);

                var approver = employee.EmployeeName + " " + employee.EmployeeSurname;

                await ApplicationService.RequestService.RequestPermit(model.RequestUID, approver);
            }
            else
            {
                var request = new ApplicationRequest()
                {
                    RequestUID = model.RequestUID,
                    RequestDenialComment = model.RequestDenialComment
                };

                await ApplicationService.RequestService.RequestDeny(request);
            }

            return View();
        }

        [HttpPost]
        [Route("Request/EditRequestView")]
        [ValidateAntiForgeryToken]
        public ActionResult EditRequestView(Guid requestUID)
        {
            var model = new EditRequestViewModel()
            {
                RequestUID = requestUID
            };

            return View(model);
        }

        [HttpPost]
        [Route("Request/EditRequest")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRequest(EditRequestViewModel model)
        {
            if (!ModelState.IsValid || model.RequestComment == null && model.RequestEndDate == null && model.RequestStartDate == null && model.RequestType == null)
            {
                return Json(new
                {
                    View = PresentationLayer.Helpers.RenderRazorViewToString(ControllerContext, "EditRequestView", model),
                    Error = true
                });
            }

            var request = new ApplicationRequest()
            {
                RequestUID = model.RequestUID,
                RequestDenialComment = model.RequestComment,
                RequestType = model.RequestType,
                RequestStartDate = model.RequestStartDate,
                RequestEndDate = model.RequestEndDate
            };

            await ApplicationService.RequestService.RequestEditRequest(request);

            return PartialView();
        }

        [HttpPost]
        [Route("Request/Search")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(SearchRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var requests = await ApplicationService.RequestService.RequestSearchRequests(model.SearchParameters, model.RequestStartDate, model.RequestEndDate);

            var requestToReturn = requests.Select(x => new ReturnRequestViewModel()
            {
                RequestUID = x.RequestUID,
                RequestNumberOfDays = x.RequestNumberOfDays,
                RequestType = (RequestTypes)x.RequestType,
            }).ToList();

            return View("GetRequests", requestToReturn);
        }

        [HttpPost]
        [Route("Request/CollectiveView")]
        [ValidateAntiForgeryToken]
        public  ActionResult CollectiveView()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
        }

        [HttpPost]
        [Route("Request/CancelRequest")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelRequest(Guid requestUID)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            await ApplicationService.RequestService.RequestCancel(requestUID);

            return View();
        }


        [HttpPost]
        [Route("Request/Collective")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Collective(CollectiveRequestViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("CollectiveView", model);
            }

            await ApplicationService.RequestService.RequestCollective(model.RequestID, model.StartDate, model.EndDate);

            return View();
        }
        #endregion
    }
}