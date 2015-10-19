using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface IRoleManagement : IBaseManagement<Role>
    {
    }

    public class RoleManagement : BaseManagement<Role, IRoleDA, DataAccess.Models.Role>, IRoleManagement
    {
        public RoleManagement(IRoleDA da) : base(da)
        {
        }
    }
}
