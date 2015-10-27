using SOLA.Business.Base;
using SOLA.Business.Models;
using SOLA.DataAccess;

namespace SOLA.Business
{
    public interface IUserManagement : IBaseManagement<User>
    {
        User GetByUserNameAndPassword(string username, string password);
    }

    public class UserManagement : BaseManagement<User, IUserDA, DataAccess.Models.User>, IUserManagement
    {
        public UserManagement(IUserDA da) : base(da)
        {
        }

        public User GetByUserNameAndPassword(string username, string password)
        {
            var user = DA.GetByUserNameAndPassword(username, password);
            return user != null && user.IsEnabled ? user.MapTo<User>() : null;
        }
    }
}
