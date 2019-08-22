using ApplicationLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace VacaYAY
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationService>(ApplicationService.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                //Provider = new CookieAuthenticationProvider
                //{
                //    OnValidateIdentity = SecurityStampValidator
                //  .OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                //      validateInterval: TimeSpan.FromMinutes(30),
                //      regenerateIdentityCallback: (manager, user) =>
                //          user.GenerateUserIdentityAsync(manager),
                //      getUserIdCallback: (id) => (id.GetUserId<int>()))
                //}
            });
        }
    }
}