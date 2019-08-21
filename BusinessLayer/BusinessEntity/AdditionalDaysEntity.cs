using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessEntity
{
    public class AdditionalDaysEntity
    {
        public Guid AdditionalDaysUID { get; set; }
        public int AdditionalDaysNumberOfAdditionalDays { get; set; }
        public string AdditionalDaysReason { get; set; }
        public DateTime AdditionalDaysCreatedOn { get; set; }
    }
}
