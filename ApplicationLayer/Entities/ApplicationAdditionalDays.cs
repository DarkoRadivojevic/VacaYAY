using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities
{
    public class ApplicationAdditionalDays
    {
        public Guid AdditionalDaysUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public int AdditionalDaysNumberOfDays { get; set; }
        public string AdditionalDaysReason { get; set; }
    }
}
