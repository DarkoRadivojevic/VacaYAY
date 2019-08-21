using ApplicationLayer.Entities;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IAccountService
    {
        Task AccountRegister(string accountEmail, string accountPassowrd, ApplicationEmployee applicationEmploye);
        Task<bool> AccountLogin(string accountEmail, string accountPassword, bool accountPersist);
        void AccountLogout(string cookie);
        Task<bool> AccountConfirmAccount(int accountID, string token);
        Task AccountForgotPassword(string accountEmail);
        Task AccountResestPassword(string accountEmail, string accountPawwsord, string token);
    }
}
