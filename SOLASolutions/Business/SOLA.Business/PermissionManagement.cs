using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface IPermissionManagement : IBaseManagement<Permission>
    {
    }

    public class PermissionManagement : BaseManagement<Permission, IPermissionDA, DataAccess.Models.Permission>, IPermissionManagement
    {
        public PermissionManagement(IPermissionDA da) : base(da)
        {
        }
    }
}
