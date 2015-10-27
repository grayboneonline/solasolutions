using SOLA.DataAccess.Base;
using SOLA.DataAccess.Models;

namespace SOLA.DataAccess
{
    public interface IUserDA : IBaseDA<User>
    {
        User GetByUserNameAndPassword(string username, string password);
    }

    public class UserDA : BaseDA<User>, IUserDA
    {
        public UserDA(CustomerDatabase database) : base(database) { }

        public User GetByUserNameAndPassword(string username, string password)
        {
            return Get<User>("SELECT * FROM [Users] WHERE Email=@0 AND Password=@1", username, password);
        }
    }
}
