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
        [DataType(DataType.Text)]
        public int RequestStatus { get; set; }

    }

    public class AlterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public Guid RequestUID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Alter comment")]
        public string RequestComment { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime RequestStartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime RequestEndDate { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string RequestType { get; set; }
    }

    public class RequestViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Comment")]
        public string RequestComment { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string RequestType { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Number of days")]
        public int RequestNumberOfDays { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime RequestStartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime RequestEndDate { get; set; }
    }

    public class ReturnRequestViewModel
    {
        public Guid RequestUID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public int RequestNumber { get; set; }
        public string RequestType { get; set; }
        public string RequestComment { get; set; }
        public int RequestNumberOfDays { get; set; }
        public DateTime RequestStartDate { get; set; }
        public DateTime RequestEndDate { get; set; }
    }
}