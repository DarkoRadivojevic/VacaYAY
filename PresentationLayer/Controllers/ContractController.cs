using ApplicationLayer;
using ApplicationLayer.Entities;
using SolutionEnums;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using VacaYAY.Models;

namespace VacaYAY.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        #region Atributes
        private ApplicationService _applicationService;
        #endregion
        #region Constructors
        public ContractController()
        {

        }

        public ContractController(ApplicationService applicationService)
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
        [Authorize(Roles = "ADMIN")]
        [Route("Contract/AddContractView")]
        public ActionResult AddContractView(Guid EmployeeUID)
        {
            var model = new ContractAddViewModel()
            {
                EmployeeUID = EmployeeUID
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Contract/GetContracts")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetContracts()
        {
            int contractOffset = Int32.Parse(Request["pageOffset"]);
            int contractCount = Int32.Parse(Request["pageCount"]);

            var contracts = await ApplicationService.ContractService.ContractGetAllContracts(contractOffset, contractCount);

            var contractsToReturn = contracts.Select(x => new ReturnContractViewModel()
            {
                ContractUID = x.ContractUID,
                EmployeeUID = x.EmployeeUID,
                ContractNumber = x.ContractNumber
            }).ToList();

            return View(contractsToReturn);
        }

        [HttpPost]
        [Route("Contract/GetContract")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<ActionResult> GetContract(Guid contractUID)
        {
            if(!ModelState.IsValid)
            {
                return View(contractUID);
            }


            var contract = await ApplicationService.ContractService.ContractGetContract(contractUID);
            var employee = await ApplicationService.EmployeeService.EmployeeGetEmployee(contract.EmployeeUID);
            var contractToReturn = new ReturnContractViewModel()
            {
                EmployeeUID = contract.EmployeeUID,
                ContractUID = contract.ContractUID,
                ContractNumber = contract.ContractNumber,
                ContractType = contract.ContractType,
                ContractStartDate= contract.ContractStartDate,
                EmployeeName = employee.EmployeeName,
                EmployeeSurname = employee.EmployeeSurname
            };

            if (contract.ContractType == ContractTypes.Temporary)
                contractToReturn.ContractEndDate = (DateTime)contract.ContractEndDate;

            return View(contractToReturn);
        }

        [HttpPost]
        [Route("Contract/GetContractFile")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<JsonResult> GetContractFile(Guid contractUID)
        {
            var contract = await ApplicationService.ContractService.ContractGetContactFile(contractUID);

            return Json(new 
            {
                ContractFile = contract.ContractFile,
                ContractFileName = contract.ContractFileName
            });      
        }

        [HttpPost]
        [Route("Contract/GetEmployeeContracts")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> GetEmployeeContracts(Guid employeeUID)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var contract = await ApplicationService.ContractService.ContractGetContracts(employeeUID);

            var contractToReturn = contract.Select(x => new ReturnContractViewModel()
            {
                ContractUID = x.ContractUID,
                ContractNumber = x.ContractNumber,
                ContractStartDate = x.ContractStartDate,
                ContractEndDate = x.ContractEndDate,
                ContractType = x.ContractType
            }).ToList();

            return View(contractToReturn);
        }

        [HttpPost]
        [Route("Contract/GetUserContracts")]
        [Authorize(Roles = "USER")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetUserContracts()
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var employee = await ApplicationService.EmployeeService.EmployeeFindCurrentEmployee(User.Identity.Name);

            var contracts = await ApplicationService.ContractService.ContractGetContracts(employee.EmployeeUID);

            var contractsToReturn = contracts.Select(x => new ReturnContractViewModel()
            {
                ContractUID = x.ContractUID,
                ContractNumber = x.ContractNumber
            }).ToList();

            return View("GetContracts", contractsToReturn);
        }
        
        [HttpPost]
        [Route("Contract/Search")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> Search(SearchContractViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (model.ContractStartDate == null)
                model.ContractStartDate = DateTime.MinValue;

            if (model.ContactEndDate == null)
                model.ContactEndDate = DateTime.MaxValue;

            var contracts = await ApplicationService.ContractService.ContractSearchContracts(model.SearchParameters, (DateTime)model.ContractStartDate, (DateTime)model.ContactEndDate);

            var contractsToReturn = contracts.Select(x => new ReturnContractViewModel(){
                ContractNumber = x.ContractNumber,
                ContractUID = x.ContractUID,
                EmployeeUID = x.EmployeeUID
            }).ToList();


            return View("GetContracts", contractsToReturn);
        }

        [HttpPost]
        [Route("Contract/AddContract")]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContract(ContractAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddContractView", model);
            }

            var contract = new ApplicationContract()
            {
                EmployeeUID = model.EmployeeUID,
                ContractStartDate = (DateTime)model.ContractStartDate,
                ContractEndDate = model.ContractEndDate,
                ContractType = (ContractTypes)model.ContractType,
                ContractNumber = (int)model.ContractNumber
            };

            MemoryStream memoryStream = new MemoryStream();
            await model.ContractFile.InputStream.CopyToAsync(memoryStream);
            contract.ContractFile = memoryStream.ToArray();
            contract.ContractFileName = model.ContractFile.FileName;

            await ApplicationService.ContractService.ContractAddContract(contract);


            return PartialView();
        }

        #endregion
    }
}