using DataLayer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SolutionEnums;
using System;
using System.Collections.Generic;

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
            var number = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;
            return number;
        }

        public static double GetContractExpression()
        {
            return 20 / 12;
        }
    }
    public static class CommentExtension
    {
        public static string CollectiveCommentTo(this string comment)
        {
            return  "Collective leave";
        }
    }

    public static class SearchStringExtension
    {
        public static List<string> DefaultRequestTypesTo(this List<string> parameters)
        {
            parameters.Add(RequestTypes.Annual.ToString());
            parameters.Add(RequestTypes.Paid.ToString());
            parameters.Add(RequestTypes.Unpaid.ToString());

            return parameters;
        }

        public static List<string> DefaultContractTypesTo(this List<string> parameters)
        {
            parameters.Add(ContractTypes.Indefinite.ToString());
            parameters.Add(ContractTypes.Temporary.ToString());

            return parameters;
        }
    }
}
