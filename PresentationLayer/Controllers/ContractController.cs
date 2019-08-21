using ApplicationLayer;
using ApplicationLayer.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VacaYAY.Models;

namespace VacaYAY.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContractController : Controller
    {
        private ApplicationService _applicationService;

        public ApplicationService ApplicationService
        {
            get
            {
                return _applicationService ?? HttpContext.GetOwinContext().Get<ApplicationService>();
            }
            private set
            {
                _applicationService = value;
            }
        }
        #region Constructors
        public ContractController(ApplicationService applicationService)
        {
            ApplicationService = applicationService;
        }
        #endregion
        [HttpGet]
        [Route("Contracts/All")]
        public async Task<ActionResult> GetAllContracts()
        {
            if(!ModelState.IsValid)
            {
                return View();
            }      
            return View();
        }
        [HttpGet]
        [Route("Contracts/{Name}/{Surname}")]
        public async Task<ActionResult> GetContractsByEmployee(SearchViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }


            return View();
        }
        [HttpGet]
        [Route("Contracts/{UID}")]
        public async Task<ActionResult> GetEmployeeContracts(Guid UID)
        {
            if(!ModelState.IsValid)
            {
                return View(UID);
            }
            return View(UID);
        }   

        [HttpGet]
        [Route("Contracts/Vacation/{UID}")]
        public async Task<ActionResult> GetVacations(Guid UID)
        {
            if(!ModelState.IsValid)
            {
                return View(UID);
            }

    
            return View(UID);
        }

        [HttpPost]
        [Route("Contracts/Add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContract(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            return View(model);
        }
    }
}