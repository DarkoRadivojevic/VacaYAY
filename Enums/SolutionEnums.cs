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
        [Display(Name = "Admin")]
        Admin = 1,

        [Display(Name = "User")]
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

        public static string AccountTypesToString(this AccountTypes accountTypes)
        {
            return accountTypes.GetType()?
                .GetMember(accountTypes.ToString())?
                .First()?
                .GetCustomAttribute<DisplayAttribute>()?
                .Name;
        }

        public static AccountTypes AccountTypesToEnum(this string accountTypes)
        {
            switch (accountTypes)
            {
                case "ADMIN":
                    return AccountTypes.Admin;
                default:
                    return AccountTypes.User;
            }
        }
    }
}