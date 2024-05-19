namespace BusinessObject.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
        public int RoleID { get; set; }
    }
}
