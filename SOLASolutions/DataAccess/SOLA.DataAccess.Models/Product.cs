using PetaPoco;

namespace SOLA.DataAccess.Models
{
    [TableName("Products")]
    [PrimaryKey("ProductId")]
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
}
