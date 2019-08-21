using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SolutionEnums;

namespace VacaYAY.Models
{
    public class AddViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public int EmployeeID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        [MaxLength(10)]
        public int ContractType { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime ContractStartDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "End date")]
        public DateTime ContractEndDate { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "File")]
        public byte[] ContractFile { get; set; }
    }

    public class SearchViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(70, ErrorMessage = "No more than 70 charachters")]
        [MaxLength(70)]
        public string EmployeeName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [StringLength(70, ErrorMessage = "No more than 70 charachters")]
        [MaxLength(70)]
        public string EmployeeSurname { get; set; }
    }
    public class ReturnContractViewModel
    {
        public Guid EmployeeUID { get; set; }
        public int ContractNumber { get; set; }
        public ContractTypes ContractType { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
    }
}