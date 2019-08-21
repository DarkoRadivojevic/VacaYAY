using DataLayer;
using DataLayer.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using VacaYAY.Models;

namespace VacaYAY
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentitiyMessageModified message)
        {
            // Plug in your email service here to send an email. SendGrid prob
            return Task.FromResult(0);
            
        }

        public Task SendAsync(IdentityMessage message)
        {
            //throw new Exception("Unintended use");
            return Task.CompletedTask;
        }
    }
    public class ApplicationUserManager : UserManager<ApplicationUser, int>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
            : base(store)
        {
            UserValidator = new UserValidator<ApplicationUser, int>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true,
                RequireUppercase = true
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, int>(new DpapiDataProtectionProvider("VacaaYAY").Create("ASP.NET Identity"));

            EmailService = new EmailService();
        }
    }
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}
