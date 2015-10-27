using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface ILocationManagement : IBaseManagement<Location>
    {
    }

    public class LocationManagement : BaseManagement<Location, ILocationDA, DataAccess.Models.Location>, ILocationManagement
    {
        public LocationManagement(ILocationDA da) : base(da)
        {
        }
    }
}
