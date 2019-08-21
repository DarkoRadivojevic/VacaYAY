using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities
{
    public class ApplicationEmployee
    {
        public Guid EmployeeUID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeCardIDNumber { get; set; }
        public DateTime EmployeeEmploymentDate { get; set; }
        public int? EmployeeBacklogDays { get; set; }
        public DateTime? EmployeeDeletedOn { get; set; }
        public string EmployeeRole { get; set; }
    }
}
