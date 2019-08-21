using ApplicationLayer.Validation.Interfaces;
using BusinessLayer.BusinessWorkflow.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationLayer.Validation.Implementations
{
    public class AccountValidation : IAccountValidation
    {
        #region Atributes
        private IAccountWorkflow _accountWorkflow;
        #endregion
        #region Constructors
        public AccountValidation()
        {

        }
        public AccountValidation(IAccountWorkflow accountWorkflow)
        {
            AccountWorkflow = accountWorkflow;
        }
        #endregion
        #region Properties
        public IAccountWorkflow AccountWorkflow
        {
            get
            {
                return _accountWorkflow;
            }

            private set
            {
                _accountWorkflow = value;
            }
        }
        #endregion
        #region Methods
        public async Task<bool> ValidateEmail(string accountEmail)
        {
            string expression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Match match = Regex.Match(accountEmail, expression, RegexOptions.IgnoreCase);

            if (match.Success)
                return await AccountWorkflow.AccountValidateEmail(accountEmail);
            else
                return false;
        }
        #endregion
    }
}
