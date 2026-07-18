using System;

namespace WorkSphere_Entities
{
    public class clsEmployeeLeavesEntity
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; } // allows null
        public byte Status { get; set; }
        public int? ApprovedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime? EditedDate { get; set; }
        public int? EditedByUserID { get; set; }
    }
}
