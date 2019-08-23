using ApplicationLayer;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        //[ValidateAntiForgeryToken]
        public ActionResult Find()
        {
            return View();
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<ActionResult> FindCurrentEmployee()
        {
            var email = User.Identity.Name;

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
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [Route("Employees/{EmployeeName}/{EmployeeSurname}")]
        public async Task<ActionResult> FindEmployeesByName(FindByNameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            return PartialView(model);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        [Route("Employees/{EmployeeEmploymentDate}")]
        public async Task<ActionResult> FindEmployeesByEmploymentDate(FindByEmploymentDateViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

   
            return View();
        }

        [HttpGet]
        [Route("Employee/BacklogDays")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEmployeesWithBacklogDays()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
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
                EmployeeEmploymentDate = employee.EmployeeEmploymentDate,
                EmployeeRole = employee.EmployeeRole
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

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetDeletedEmployees()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditEmployee(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View();
        }

        [HttpDelete]
        [Route("Employee/{UID}")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteEmployee(Guid UID)
        {
            if (!ModelState.IsValid)
            {
                return View(UID);
            }

           
            return View(UID);
        }
    }
}