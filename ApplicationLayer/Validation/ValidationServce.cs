using ApplicationLayer.Validation.Interfaces;

namespace ApplicationLayer.Validation
{
    public class ValidationService
    {
        #region Atgributes
        private IAccountValidation _accountValidation;
        #endregion
        #region Constructors
        public ValidationService()
        {

        }
        public ValidationService(IAccountValidation accountValidation)
        {
            AccountValidation = accountValidation;
        }
        #endregion
        #region Properties
        public IAccountValidation AccountValidation
        {
            get
            {
                return _accountValidation;
            }
            private set
            {
                _accountValidation = value;
            }
        }
        #endregion
    }
}
