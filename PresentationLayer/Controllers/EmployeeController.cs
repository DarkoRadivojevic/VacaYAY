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
        #region Constructors
        public EmployeeController()
        {

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
            

            return PartialView();
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

        [HttpGet]
        [Route("Employee/{UID}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEmployee(Guid UID)
        {
            if (!ModelState.IsValid)
            {
                return View(UID);
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEmployees()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
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