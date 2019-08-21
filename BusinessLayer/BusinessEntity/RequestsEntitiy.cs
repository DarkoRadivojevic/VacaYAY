using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessEntity
{
    public class RequestsEntity
    {
        public Guid EmployeeUID { get; set; }
        public Guid RequestUID { get; set; }
        public int RequestNumber { get; set; }
        public string RequestType { get; set; }
        public string RequestComment { get; set; }
        public int RequestStatus { get; set; }
        public string RequestDenialComment { get; set; }
        public int RequestNumberOfDays { get; set; }
        public byte[] RequestFile { get; set; }
        public DateTime RequestStardDate { get; set; }
        public DateTime RequestEndDate { get; set; }
        public DateTime RequestCreatedOn { get; set; }
    }
}
