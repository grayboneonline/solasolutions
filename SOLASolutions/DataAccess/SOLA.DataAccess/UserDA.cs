using System.Collections.Generic;
using SOLA.Models;

namespace SOLA.DataAccess
{
    public interface IUserDA
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }

    public class UserDA : BaseDA, IUserDA
    {
        public UserDA(ICustomerConfig config) : base(config)
        {
        }

        public IEnumerable<User> GetAll()
        {
            return Select<User>();
        }

        public User GetById(int id)
        {
            return Get<User>("SELECT * FROM [Users] WHERE UserId = @0", id);
        }

        public void Add(User user)
        {
            Insert(user);
        }

        public void Update(User user)
        {
            Save(user);
        }

        public void Delete(int id)
        {
            Delete<User>(id);
        }
    }
}
