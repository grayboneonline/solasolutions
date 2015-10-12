using System.Collections.Generic;
using SOLA.Models;

namespace SOLA.DataAccess.Contracts
{
    public interface IUserDA
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
