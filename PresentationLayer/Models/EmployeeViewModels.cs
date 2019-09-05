using SolutionEnums;
using System;
using System.ComponentModel.DataAnnotations;

namespace VacaYAY.Models
{
    public class EditEmployeeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public Guid EmployeeUID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(70, ErrorMessage = "No longer than 70 charachters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [MaxLength(70)]
        public string EmployeeName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = " Surname")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [MaxLength(70)]
        public string EmployeeSurname { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "ID Card Number")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use numbers only please")]
        [MaxLength(9, ErrorMessage = "ID Card number must be nine digits")]
        [MinLength(9, ErrorMessage = "ID Card number must be nine digits")]
        public string EmployeeCardIDNumber { get; set; }

        [Display(Name = "Role")]
        [EnumDataType(typeof(AccountTypes))]
        public AccountTypes? EmployeeRole { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Employment date")]
        public DateTime? EmployeeEmploymentDate { get; set; }
    }

    public class FindEmployeeViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Employee Name")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        [MaxLength(140)]
        public string SearchParameters { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Employment Date")]
        public DateTime? EmployeeEmploymentDate { get; set; }
    }

    public class ReturnEmployeeViewModel
    {
        public Guid EmployeeUID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeCardIDNumber { get; set; }
        public DateTime EmployeeEmploymentDate { get; set; }
        public int? EmployeeBacklogDays { get; set; }
        public string EmployeeRole { get; set; }
    }
}


