using System;
using PetaPoco;

namespace SOLA.Models
{
    [TableName("Users")]
    [PrimaryKey("UserId")]
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoleId { get; set; }

        public string ResetPasswordToken { get; set; }

        public DateTime ResetPasswordTokenExpiresDatetime { get; set; }

        public bool IsEnabled { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDatetime { get; set; }

        public int ModifiedUser { get; set; }

        public DateTime ModifiedDatetime { get; set; }
    }
}
