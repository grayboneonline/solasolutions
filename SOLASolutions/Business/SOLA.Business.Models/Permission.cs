namespace SOLA.Business.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public int PageId { get; set; }

        public string Name { get; set; }

        public PermissionType PermissionType { get; set; }
    }

    public enum PermissionType
    {
        View = 0,
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}
