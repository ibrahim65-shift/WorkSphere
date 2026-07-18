using System;

namespace WorkSphere_Entities
{
    public class clsEmployeeAttendanceEntity
    {
        public int AttendanceID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public bool IsLate { get; set; }
        public string Notes { get; set; }// allows null
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime? EditedDate { get; set; }
        public int? EditedByUserID { get; set; }
    }
}
