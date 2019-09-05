using DataLayer.Helper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VacaYAY;
using VacaYAY.Models;

namespace DataLayer
{
    public class AccountRepository : IAccountRepository
    {
        #region Atributes
        private ApplicationDbContext _applicationDbContext;
        private VacaYAYContext _dbContext;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _singInManager;
        private IAuthenticationManager _authenticationManager;
        #endregion
        #region Constructors
        public AccountRepository()
        {

        }
        public AccountRepository(ApplicationDbContext appplicationDbContext, VacaYAYContext dbContext, IAuthenticationManager authenticationManager)
        {
            ApplicationDbContext = appplicationDbContext;
            DbContext = dbContext;
            AuthenticationManager = authenticationManager;
        }
        #endregion
        #region Properties
        public ApplicationDbContext ApplicationDbContext
        {
            get
            {
                return _applicationDbContext;
            }
            private set
            {
                _applicationDbContext = value;
            }
        }
        public VacaYAYContext DbContext
        {
            get
            {
                return _dbContext;
            }
            private set
            {
                _dbContext = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? new ApplicationUserManager(new CustomUserStore(ApplicationDbContext));
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager?? new ApplicationRoleManager(new CustomRoleStore(ApplicationDbContext));
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _singInManager ?? new ApplicationSignInManager(UserManager, AuthenticationManager);
            }
            private set
            {
                _singInManager = value;
            }
        }
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager;
            }
            private set
            {
                _authenticationManager = value;
            }
        }
        #endregion
        #region Methods
        public async Task<int> AccountRegister(string accountEmail, string accountPassowrd, string accountRole)
        {
            var user = new ApplicationUser() { UserName = accountEmail, Email = accountEmail };
            await UserManager.CreateAsync(user, accountPassowrd);
            await UserManager.AddToRoleAsync(user.Id, accountRole);

            var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            await UserManager.EmailService.SendAsync(new IdentitiyMessageModified()
            {
                Destination = accountEmail,
                Token = HttpUtility.UrlEncode(token),
                ID = user.Id,
                Subject = "Confirm email"
            });

            return user.Id;
        }
        public async Task<bool> AccountValidateEmail(string accountEmail)
        {
            var result = await DbContext.AspNetUsers.FirstOrDefaultAsync(x => x.Email == accountEmail);

            if (result == null)
                return true;
            else
                return false;
        }
        public async Task AccountChangeRole(Guid employeeUID, string employeeRole)
        {
            var user = await DbContext.AspNetUsers.Where(x => x.Employee.EmployeeUID == employeeUID).FirstAsync();
            var userRole = user.AspNetRoles.Where(x => x.Name != employeeRole).Select(x => x.Name).First();

            await UserManager.RemoveFromRoleAsync(user.Id, userRole);
            await UserManager.AddToRoleAsync(user.Id, employeeRole);
        }

        public async Task<bool> AccountLogin(string accountEmail, string accountPassword, bool accountPersist)
        {
           var result = await SignInManager.PasswordSignInAsync(accountEmail, accountPassword, accountPersist, true);
            if (result == SignInStatus.Success)
                return true;
            else return false;
        }
        public void AccountLogout(string accountCookie)
        {
            AuthenticationManager.SignOut(accountCookie);
        }

        public async Task<bool> AccountConfrimAccount(int accountID, string accountToken)
        {
            var result = await UserManager.ConfirmEmailAsync(accountID, accountToken);
            if (result.Succeeded)
                return true;
            else
                return false;
        }

        public async Task AccountForgotPassword(string accountEmail)
        {
            var user = await UserManager.FindByNameAsync(accountEmail);
            if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                return;

            var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

            await UserManager.EmailService.SendAsync(new IdentitiyMessageModified()
            {
                Token = HttpUtility.UrlEncode(token),
                Destination = user.Email,
                Subject = "Confirm password reset"
            });
        }

        public async Task AccountResetPassword(string accountEmail, string accountPassword, string token)
        {       
            var user = await UserManager.FindByNameAsync(accountEmail);
            if (user == null)
                return;

            var result = await UserManager.ResetPasswordAsync(user.Id, token, accountPassword);
        }

        public async Task<string> AccountGetAccountRole(Guid employeeUID)
        {
            var employeeID = await DbContext.Employees.Where(x => x.EmployeeUID == employeeUID).Select(x => x.EmployeeID).FirstAsync();
            var role = await UserManager.GetRolesAsync(employeeID);

            return role.First().ToString();
        }
        #endregion
    }
}
