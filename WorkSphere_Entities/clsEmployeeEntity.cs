using System;

namespace WorkSphere_Entities
{
    public class clsEmployeeEntity
    {
        public int EmployeeID { get; set; }
        public string NationalID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; } // allows null
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }// allows null
        public string Phone { get; set; }// allows null
        public string Address { get; set; }// allows null
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public int StepID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime? EditDate { get; set; }
        public int? EditedByUserID { get; set; }
    }
}
