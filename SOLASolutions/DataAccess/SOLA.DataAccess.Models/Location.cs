using PetaPoco;

namespace SOLA.DataAccess.Models
{
    [TableName("Locations")]
    [PrimaryKey("LocationId")]
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
