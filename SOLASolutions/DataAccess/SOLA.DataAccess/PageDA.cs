using SOLA.DataAccess.Base;
using SOLA.DataAccess.Models;

namespace SOLA.DataAccess
{
    public interface IPageDA : IBaseDA<Page>
    {
    }

    public class PageDA : BaseDA<Page>, IPageDA
    {
        public PageDA(CustomerDatabase database) : base(database)
        {
        }
    }
}
