using SOLA.DataAccess.Base;
using SOLA.Models;

namespace SOLA.DataAccess
{
    public interface IRoleDA : IBaseDA<Role>
    {
    }

    public class RoleDA : BaseDA<Role>, IRoleDA
    {
        public RoleDA(CustomerDatabase database) : base(database)
        {
        }
    }
}
