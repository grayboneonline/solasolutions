using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface IUserManagement : IBaseManagement<User>
    {
    }

    public class UserManagement : BaseManagement<User, IUserDA, DataAccess.Models.User>, IUserManagement
    {
        public UserManagement(IUserDA da) : base(da)
        {
        }
    }
}
