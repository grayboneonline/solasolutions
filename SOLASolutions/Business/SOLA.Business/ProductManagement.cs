using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface IProductManagement : IBaseManagement<Product>
    {
    }

    public class ProductManagement : BaseManagement<Product, IProductDA, DataAccess.Models.Product>, IProductManagement
    {
        public ProductManagement(IProductDA da) : base(da)
        {
        }
    }
}
