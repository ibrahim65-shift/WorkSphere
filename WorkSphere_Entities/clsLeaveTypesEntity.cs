using System;

namespace WorkSphere_Entities
{
    public class clsLeaveTypesEntity
    {
        public int LeaveTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // allows null
        public int? MaxDaysPerYear { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }
    }
}
