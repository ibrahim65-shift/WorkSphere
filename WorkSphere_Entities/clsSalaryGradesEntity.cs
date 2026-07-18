using System;


namespace WorkSphere_Entities
{
    public class clsSalaryGradesEntity
    {
        public int GradeID { get; set; }
        public short JobGrade { get; set; }
        public string Description { get; set; }
        public int DepartmentID { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedByUserID { get; set; }
        public DateTime? EditedDate { get; set; }
        public int? EditedByUserID { get; set; }
    }
}
