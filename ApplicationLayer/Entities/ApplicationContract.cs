using SolutionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities
{
    public class ApplicationContract
    {
        public Guid ContractUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public int ContractNumber { get; set; }
        public ContractTypes ContractType { get; set; }
        public byte[] ContractFile { get; set; }
        public string ContractFileName { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
    }
}
