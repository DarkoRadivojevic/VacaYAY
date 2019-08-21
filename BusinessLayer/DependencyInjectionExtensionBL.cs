using DataLayer;
using DataLayer.Implementations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Unity;
using Unity.Extension;
using Unity.Injection;
using Microsoft.Owin;
using VacaYAY.Models;
using Unity.Lifetime;

namespace BusinessLayer
{
    public class DependencyInjectionExtensionBL : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IAdditionalDaysRepository, AdditionalDaysRepository>(new PerResolveLifetimeManager());
            Container.RegisterType<IContractRepository, ContractRepository>(new PerResolveLifetimeManager());
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>(new PerResolveLifetimeManager());
            Container.RegisterType<IRequestRepository, RequestRepository>(new PerResolveLifetimeManager());
            Container.RegisterType<IAccountRepository, AccountRepository>(new PerResolveLifetimeManager());
        }
        
    }
}
