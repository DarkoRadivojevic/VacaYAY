using ApplicationLayer.Implementations;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Validation;
using ApplicationLayer.Validation.Implementations;
using ApplicationLayer.Validation.Interfaces;
using BusinessLayer;
using BusinessLayer.BusinessWorkflow.Implementatons;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System.ComponentModel;
using Unity;
using Unity.Extension;
using Unity.Lifetime;

namespace ApplicationLayer
{
    public class DependencyInjectionExtensionAL : UnityContainerExtension
    {
        protected override void Initialize()
        {
 
            Container.RegisterType<IContractWorkflow, ContractWorkflow>(new PerResolveLifetimeManager());
            Container.RegisterType<IAdditionalDaysWorkflow, AdditionalDaysWorkflow>(new PerResolveLifetimeManager());
            Container.RegisterType<IEmployeeWorkflow, EmployeeWorkflow>(new PerResolveLifetimeManager());
            Container.RegisterType<IRequestWorkflow, RequestWorkflow>(new PerResolveLifetimeManager());
            Container.RegisterType<IAccountWorkflow, AccountWorkflow>(new PerResolveLifetimeManager());
            

            Container.AddNewExtension<DependencyInjectionExtensionBL>();
        }
    }
}
