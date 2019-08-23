using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VacaYAY.Models
{
    public class EditViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public Guid EmployeeUID { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(70, ErrorMessage = "No longer than 70 charachters")]
        [MaxLength(70)]
        public string EmployeeName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = " Surname")]
        [StringLength(70, ErrorMessage = "No longer than 70 charachters")]
        [MaxLength(70)]
        public string EmployeeSurname { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "ID Card Number")]
        [StringLength(9, ErrorMessage = "ID Card number must be nine digits")]
        [MaxLength(9)]
        [MinLength(9)]
        public string EmployeeCardIDNumber { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Role")]
        [StringLength(10, ErrorMessage = "No longer than 10 charachters")]
        public string EmployeeRole { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Employment date")]
        public DateTime EmployeeEmploymentDate { get; set; }
    }

    public class FindByNameViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(70, ErrorMessage = "No longer than 70 charachters")]
        [MaxLength(70)]
        public string EmployeeName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [StringLength(70, ErrorMessage = "No longer than 70 charachters")]
        [MaxLength(70)]
        public string EmployeeSurname { get; set; }
    }

    public class FindByEmploymentDateViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Employment Date")]
        public DateTime EmployeeEmploymentDate { get; set; }
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


