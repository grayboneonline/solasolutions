using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface IPageManagement : IBaseManagement<Page>
    {
    }

    public class PageManagement : BaseManagement<Page, IPageDA, DataAccess.Models.Page>, IPageManagement
    {
        public PageManagement(IPageDA da) : base(da)
        {
        }
    }
}
