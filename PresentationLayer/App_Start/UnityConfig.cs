using ApplicationLayer;
using ApplicationLayer.Implementations;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Validation.Implementations;
using ApplicationLayer.Validation.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace PresentationLayer
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }

        public static void RegisterComplonents()
        {
            var container = new UnityContainer();

            container.RegisterType<IEmployeeService, EmployeeService>(new PerResolveLifetimeManager());
            container.RegisterType<IRequestService, RequestService>(new PerResolveLifetimeManager());
            container.RegisterType<IContractService, ContractService>(new PerResolveLifetimeManager());
            container.RegisterType<IAdditionalDaysService, AdditionalDaysService>(new PerResolveLifetimeManager());
            container.RegisterType<IAccountService, AccountService>(new PerResolveLifetimeManager());
            container.RegisterType<IAccountValidation, AccountValidation>(new PerResolveLifetimeManager());
            container.RegisterFactory<IAuthenticationManager>(x => HttpContext.Current.GetOwinContext().Authentication);



            container.AddNewExtension<DependencyInjectionExtensionAL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}