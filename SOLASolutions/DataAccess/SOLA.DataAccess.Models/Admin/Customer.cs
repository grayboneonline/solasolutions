using System;
using PetaPoco;

namespace SOLA.DataAccess.Models.Admin
{
    [TableName("Customers")]
    [PrimaryKey("CustomerId")]
    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string SiteName { get; set; }

        public string AzureSqlServerId { get; set; }

        public bool IsEnable { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int ModifiedUser { get; set; }

        public DateTime ModifiedDateTime { get; set; }
    }

    public class CustomerDataSource
    {
        public string SiteName { get; set; }

        public string ServerName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
