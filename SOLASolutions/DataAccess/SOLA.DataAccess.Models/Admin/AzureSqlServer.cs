using System;
using PetaPoco;

namespace SOLA.DataAccess.Models.Admin
{
    [TableName("AzureSQLServers")]
    [PrimaryKey("AzureSQLServerId")]
    public class AzureSqlServer
    {
        public int AzureSqlServerId { get; set; }

        public string ServerName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int ModifiedUser { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }
}
