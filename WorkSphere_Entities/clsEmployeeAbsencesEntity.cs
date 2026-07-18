using System;

namespace WorkSphere_Entities
{
    public class clsEmployeeAbsencesEntity
    {
        public int AbsenceID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime AbsenceDate { get; set; }
        public bool IsExcused { get; set; }
        public string Reason { get; set; }//allows null
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime? EditedDate { get; set; }
        public int? EditedByUserID { get; set; }
    }
}
