namespace WorkSphere_Entities
{
    public class clsRolePermissionsEntity
    {
        public int RolePermissionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public bool IsAllowed { get; set; }
    }
}
