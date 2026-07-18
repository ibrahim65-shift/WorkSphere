namespace WrokSphere_Shared
{
    public class clsRolePermissionItem
    {
        public int PermissionID { get; set; }
        public bool IsAllowed { get; set; }

        public clsRolePermissionItem(int permID, bool isAllowed)
        {
            PermissionID = permID;
            IsAllowed = isAllowed;
        }
    }
}
