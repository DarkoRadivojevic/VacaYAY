using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SolutionEnums
{
    public enum RequestStatus
    {
        [Display(Name = "In review")]
        InReview = 1,

        [Display(Name = "Accepted")]
        Accepted,

        [Display(Name = "Rejected")]
        Rejected,

        [Display(Name = "Adjusted")]
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

        [Display(Name = "Unpaid leave")]
        Unpaid,

        [Display(Name = "Annual leave")]
        Annual
    }

    public static class EnumExtenssion
    {
        public static string RequestStatusToString(this RequestStatus status)
        {

            return status.GetType()?
                     .GetMember(status.ToString())?
                     .First()?
                     .GetCustomAttribute<DisplayAttribute>()?
                     .Name;

        }
    }
}