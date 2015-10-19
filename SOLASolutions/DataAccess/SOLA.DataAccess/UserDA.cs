using SOLA.DataAccess.Base;
using SOLA.DataAccess.Models;

namespace SOLA.DataAccess
{
    public interface IUserDA : IBaseDA<User>
    {
    }

    public class UserDA : BaseDA<User>, IUserDA
    {
        public UserDA(CustomerDatabase database) : base(database) { }
    }
}
