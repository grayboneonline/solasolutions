using SOLA.DataAccess.Base;
using SOLA.DataAccess.Models;

namespace SOLA.DataAccess
{
    public interface ILocationDA : IBaseDA<Location>
    {
    }

    public class LocationDA : BaseDA<Location>, ILocationDA
    {
        public LocationDA(CustomerDatabase database) : base(database)
        {
        }
    }
}
