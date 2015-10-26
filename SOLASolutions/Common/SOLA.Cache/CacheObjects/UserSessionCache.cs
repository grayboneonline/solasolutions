using System;
using System.Collections.Generic;
using System.Linq;
using SOLA.Cache.Contracts;

namespace SOLA.Cache.CacheObjects
{
    public class UserSessionCache : Dictionary<Guid, IUserSession>
    {
        public void Add(IUserSession userSession)
        {
            Add(userSession.Id, userSession);
        }

        public IUserSession FindByHashedToken(string hashedToken)
        {
            return Values.FirstOrDefault(x => x.RefreshToken.Token == hashedToken);
        }

        public void RemoveByHashedToken(string hashedToken)
        {
            var session = Values.FirstOrDefault(x => x.RefreshToken.Token == hashedToken);
            if (session != null)
                Remove(session.Id);
        }
    }
}