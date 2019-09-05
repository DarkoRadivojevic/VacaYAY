using ApplicationLayer;
using ApplicationLayer.Entities;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using VacaYAY.Models;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class AdditionalDaysController : Controller
    {
        #region Atributes
        private ApplicationService _applicationService;
        #endregion
        #region Constructors
        public AdditionalDaysController()
        {

        }

        public AdditionalDaysController(ApplicationService applicationService)
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
            set
            {
                _applicationService = value;
            }
        }
        #endregion
        #region Methods
        [HttpPost]
        [Route("AdditionalDays/AddAdditionalDaysView")]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdditionalDaysView(Guid EmployeeUID)
        {
            var model = new AddAdditionalDaysModel() { EmployeeUID = EmployeeUID };
            return View(model);
        }

        [HttpPost]
        [Route("AdditionalDays/AddAdditionalDay")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAdditionalDays(AddAdditionalDaysModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("AddAdditionalDaysView", model);
            }

            var additionalDays = new ApplicationAdditionalDays()
            {
                EmployeeUID = model.EmployeeUID,
                AdditionalDaysNumberOfDays = (int)model.AdditionalDaysNumberOfDays,
                AdditionalDaysReason = model.AdditionalDaysReason
            };

            await ApplicationService.AdditionalDaysService.AdditionalDaysAddAdditionalDays(additionalDays);

            return View("Success", model);
        }
        #endregion
    }
}