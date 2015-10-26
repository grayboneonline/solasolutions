using SOLA.DataAccess.Base;

namespace SOLA.DataAccess
{
    public interface IProductDA : IBaseDA<Models.Product>
    {
    }

    public class ProductDA : BaseDA<Models.Product>, IProductDA
    {
        public ProductDA(CustomerDatabase database) : base(database)
        {
        }
    }
}
