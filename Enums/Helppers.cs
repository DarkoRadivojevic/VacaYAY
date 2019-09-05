using SolutionEnums;

namespace Enums
{
    public static class Helpers
    {
        public static string AccountTypesConverter(AccountTypes accountTypes)
        {
            if (accountTypes == AccountTypes.Admin)
                return "ADMIN";
            else
                return "USER";
        }

        public static string LeaveTypesConverter(RequestTypes leaveTypes)
        {
            if (leaveTypes == RequestTypes.Paid)
                return "Paid leave";
            else if (leaveTypes == RequestTypes.Unpaid)
                return "Unpaid leave";
            else
                return "Annual leave";
        }
    }
}
