using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SolutionEnums
{
    public enum RequestStatus
    {
        InReview = 1,
        Accepted,
        Rejected,
        Adjusted
    }
    //prebaci u druge fajlove
    public enum ContractTypes
    {
        [Display(Name = "Indefinite contract")]
        Indefinite = 1,
        [Display(Name = "Temporary contract")]
        Temporary
    }

    public enum AccountTypes
    {
        Admin = 1,
        User
    }

    public enum RequestTypes
    {
        [Display(Name = "Paid leave")]
        Paid = 1,
        [Display(Name =  "Unpaid leave")]
        Unpaid,
        [Display(Name = "Annual leave")]
        Annual
    }
}