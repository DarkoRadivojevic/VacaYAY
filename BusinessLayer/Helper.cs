using System;

namespace BusinessLayer
{
    public static class DateTimeExtensions
    {
        public static int GetBusinessDaysTo(this DateTime startDate, DateTime endDate)
        {
            double calcBusinessDays =
               1 + ((endDate - startDate).TotalDays * 5 -
               (startDate.DayOfWeek - endDate.DayOfWeek) * 2) / 7;

            if (endDate.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startDate.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return (int)calcBusinessDays;
        }

        public static int GetNumberOfMonths(this DateTime startDate, DateTime endDate)
        {
            var number = ((startDate.Year - endDate.Year) * 12) + startDate.Month - endDate.Month;
            return number;
        }

        public static double GetContractExpression()
        {
            return 20 / 12;
        }
    }
    public static class CommentExtension
    {
        public static string CollevtiveCommentTo(this string comment)
        {
            return "Collective leave";
        }
    }
}
