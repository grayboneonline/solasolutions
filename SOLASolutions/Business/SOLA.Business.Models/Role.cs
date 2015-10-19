using System;

namespace SOLA.Business.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public string IsSystemRole { get; set; }

        public bool IsEnabled { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDatetime { get; set; }

        public int ModifiedUser { get; set; }

        public DateTime ModifiedDatetime { get; set; }
    }
}
