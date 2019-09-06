using SolutionEnums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VacaYAY.Models
{
    public class ContractAddViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use numbers only please")]
        [Display(Name = "Contract number")]
        public int? ContractNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public Guid EmployeeUID { get; set; }

        [Required]
        [EnumDataType(typeof(ContractTypes))]
        [Display(Name = "Type")]
        public ContractTypes? ContractType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime? ContractStartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End date")]
        public DateTime? ContractEndDate { get; set; }

        [Required]
        [Display(Name = "File")]
        public HttpPostedFileBase ContractFile { get; set; }
    }

    public class SearchContractViewModel
    {
        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        [MaxLength(70)]
        public string SearchParameters { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ContractStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ContactEndDate { get; set; }


    }
    public class ReturnContractViewModel
    {
        public Guid EmployeeUID { get; set; }
        public Guid ContractUID { get; set; }
        public int ContractNumber { get; set; }
        public ContractTypes ContractType { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public byte[] ContractFile { get; set; }
        public string ContractFileName { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
    }
}