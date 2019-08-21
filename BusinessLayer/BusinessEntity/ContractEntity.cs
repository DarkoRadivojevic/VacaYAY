using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessEntity
{
    public class ContractEntity
    {
        public Guid ContractUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public int ContractNumber { get; set; }
        public int ContractType { get; set; }
        public  byte[] ContractFile { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime ContractCreatedOn { get; set; }
        public DateTime? ContractDeletedOn { get; set; }
    }
}
