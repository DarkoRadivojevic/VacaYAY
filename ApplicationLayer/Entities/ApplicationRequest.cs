using SolutionEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities
{
    public class ApplicationRequest
    {
        public Guid RequestUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public int RequestNumber { get; set; }
        public string RequestType { get; set; }
        public string RequestComment { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string RequestDenialComment { get; set; }
        public int RequestNumberOfDays { get; set; }
        public byte[] RequestFile { get; set; }
        public DateTime RequestStartDate { get; set; }
        public DateTime RequestEndDate { get; set; }
    }
}
