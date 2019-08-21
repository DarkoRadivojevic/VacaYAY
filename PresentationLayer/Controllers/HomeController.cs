using ApplicationLayer;
using ApplicationLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VacaYAY.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Atributes
        private ApplicationService _applicationService;
        #endregion
        #region Constructors
        public HomeController()
        {

        }
        public HomeController(ApplicationService applicationService)
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
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }
        #endregion
    }
}