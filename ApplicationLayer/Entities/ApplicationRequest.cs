using SolutionEnums;
using System;

namespace ApplicationLayer.Entities
{
    public class ApplicationRequest
    {
        public Guid RequestUID { get; set; }
        public Guid EmployeeUID { get; set; }
        public RequestTypes? RequestType { get; set; }
        public string RequestComment { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string RequestDenialComment { get; set; }
        public int RequestNumberOfDays { get; set; }
        public byte[] RequestFile { get; set; }
        public DateTime? RequestStartDate { get; set; }
        public DateTime? RequestEndDate { get; set; }
    }
}
