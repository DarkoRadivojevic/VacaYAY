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
    public class EmployeeController : Controller
    {
        #region Atributes
        private ApplicationService _applicationService;
        #endregion
        #region Constructors
        public EmployeeController()
        {

        }
        public EmployeeController(ApplicationService applicationService)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FindCurrentEmployee()
        {
            var email = User.Identity.Name;
            if (email == null)
                return PartialView("NullView");


            var employee = await ApplicationService.EmployeeService.EmployeeFindCurrentEmployee(email);
            var employeeToReturn = new ReturnEmployeeViewModel()
            {
                EmployeeUID = employee.EmployeeUID,
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                EmployeeBacklogDays = employee.EmployeeBacklogDays
            };

            return PartialView(employeeToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("Employees/FindByName}")]
        public async Task<ActionResult> Search(FindEmployeeViewModel model)
       {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.EmployeeEmploymentDate == null)
                model.EmployeeEmploymentDate = DateTime.MinValue;


            var employees = await ApplicationService.EmployeeService.EmployeeFindEmployeesByName(model.SearchParameters, (DateTime)model.EmployeeEmploymentDate);

            var employeesToReturn = employees.Select(x => new ReturnEmployeeViewModel()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();

            return PartialView("GetEmployees", employeesToReturn);
        }

        [HttpPost]
        [Route("Employee/BacklogDays")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEmployeesWithBacklogDays()
        {
            var employees = await ApplicationService.EmployeeService.EmployeeGetEmployeesWithBacklogDays();

            var employeesToReturn = employees.Select(x => new ReturnEmployeeViewModel()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();

            return View(employeesToReturn);
        }

        [HttpPost]
        [Route("Employee/GetEmployee")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetEmployee(Guid employeeUID)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeUID);
            }

            var employee = await ApplicationService.EmployeeService.EmployeeGetEmployee(employeeUID);

            var employeeToReturn = new ReturnEmployeeViewModel()
            {
                EmployeeUID = employee.EmployeeUID,
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname,
                EmployeeBacklogDays = employee.EmployeeBacklogDays,
                EmployeeCardIDNumber = employee.EmployeeCardIDNumber,
                EmployeeEmploymentDate = (DateTime)employee.EmployeeEmploymentDate,
                EmployeeRole = employee.EmployeeRole.AccountTypesToEnum()
            };
            return View(employeeToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetEmployees()
        {
            int employeeOffset = Int32.Parse(Request["pageOffset"]);
            int employeeCount = Int32.Parse(Request["pageCount"]);

            var employees = await ApplicationService.EmployeeService.EmployeeGetEmployees(employeeCount, employeeOffset);
            var employeesToReturn = employees.Select(x => new ReturnEmployeeViewModel()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();


            return View(employeesToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("Employee/GetDeleted")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetDeletedEmployees()
        {
            var employees = await ApplicationService.EmployeeService.EmployeeGetDeletedEmployees();

            var employeesToReturn = employees.Select(x => new ReturnEmployeeViewModel()
            {
                EmployeeUID = x.EmployeeUID,
                EmployeeName = x.EmployeeName,
                EmployeeSurname = x.EmployeeSurname
            }).ToList();

            return View(employeesToReturn);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployeeView(EditEmployeeViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(EditEmployeeViewModel model)
        {
            if (!ModelState.IsValid || model.EmployeeName == null && model.EmployeeSurname == null && model.EmployeeRole == null && model.EmployeeCardIDNumber == null && model.EmployeeEmploymentDate == null)
            {
                return Json(new
                {
                    View = PresentationLayer.Helpers.RenderRazorViewToString(ControllerContext, "EditEmployeeView", model),
                    Error = true
                });
            }

            var employee = new ApplicationEmployee();

            if (model.EmployeeName != null)
                employee.EmployeeName = model.EmployeeName;

            if (model.EmployeeSurname != null)
                employee.EmployeeSurname = model.EmployeeSurname;

            if (model.EmployeeCardIDNumber != null)
                employee.EmployeeCardIDNumber = model.EmployeeCardIDNumber;

            if (model.EmployeeRole != null)
                employee.EmployeeRole = Helpers.AccountTypesConverter((AccountTypes)model.EmployeeRole);

            if (model.EmployeeEmploymentDate != null)
                employee.EmployeeEmploymentDate = (DateTime)model.EmployeeEmploymentDate;
            else
                employee.EmployeeEmploymentDate = DateTime.MinValue;


            employee.EmployeeUID = model.EmployeeUID;

            await ApplicationService.EmployeeService.EmployeeEditEmployee(employee, employee.EmployeeRole);
            var employeeToReturn = await ApplicationService.EmployeeService.EmployeeGetEmployee(employee.EmployeeUID);

            model.EmployeeName = employeeToReturn.EmployeeName;
            model.EmployeeSurname = employeeToReturn.EmployeeSurname;

            return View(model);
        }

        [HttpPost]
        [Route("Employee/DeleteEmployeeModal")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployeeModal()
        {
            return View("~/Views/Modal/DeleteEmployeeModal.cshtml");
        }

        [HttpPost]
        [Route("Employee/DeleteEmployee")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEmployee(Guid employeeUID)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeUID);
            }

            await ApplicationService.EmployeeService.EmployeeDeleteEmployee(employeeUID);

            return View();
        }
        #endregion
    }
}