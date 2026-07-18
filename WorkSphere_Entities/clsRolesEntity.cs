namespace WorkSphere_Entities
{
    public class clsRolesEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; } // allows null
        public bool IsActive { get; set; }
    }
}
