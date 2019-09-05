using System;
using System.ComponentModel.DataAnnotations;

namespace VacaYAY.Models
{
    public class AddAdditionalDaysModel
    {
        [Required]
        [DataType(DataType.Text)]
        public Guid EmployeeUID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use numbers only please")]
        [Display(Name = "Number of additional days")]
        public int? AdditionalDaysNumberOfDays { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Illegal characters")]
        [Display(Name = "Reason for additional days")]
        [MaxLength(300, ErrorMessage = "Too many characters" )]
        public string AdditionalDaysReason { get; set; }
    }
}