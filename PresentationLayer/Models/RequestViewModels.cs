using SolutionEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VacaYAY.Models
{

    public class ProcessRequestViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Reject comment")]
        public string RequestDenialComment { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public Guid RequestUID { get; set; }

        [Required]
        [EnumDataType(typeof(RequestStatus))]
        public RequestStatus RequestStatus { get; set; }

    }

    public class SearchRequestViewModel
    {
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string SearchParameters { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RequestStartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? RequestEndDate { get; set; }
    }

    public class EditRequestViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public Guid RequestUID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Alter comment")]
        public string RequestComment { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime? RequestStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime? RequestEndDate { get; set; }
  
        [EnumDataType(typeof(RequestTypes))]
        [Display(Name = "Type")]
        public RequestTypes? RequestType { get; set; }
    }

    public class RequestViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Comment")]
        public string RequestComment { get; set; }

        [Required]
        [EnumDataType(typeof(RequestTypes))]
        [Display(Name = "Type")]
        public RequestTypes RequestType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime? RequestStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime? RequestEndDate { get; set; }

        [DataType(DataType.Text)]
        public int TotalAvailableDays { get; set; }
    }

    public class ReturnRequestViewModel
    {
        public Guid RequestUID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public RequestTypes RequestType { get; set; }
        public string RequestComment { get; set; }
        public int RequestNumberOfDays { get; set; }
        public DateTime RequestStartDate { get; set; }
        public DateTime RequestEndDate { get; set; }
    }

    public class CollectiveRequestViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}