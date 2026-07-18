using System;

namespace WorkSphere_Entities
{
    public class clsDepartmentEntity
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }// allows null
        public DateTime CreatedDate { get; set; }
    }
}
