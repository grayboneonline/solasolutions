using System.Collections.Generic;
using System.Linq;
using SOLA.Cache.Contracts;

namespace SOLA.Cache.CacheObjects
{
    public class ApplicationClientCache : Dictionary<string, IApplicationClient>
    {
        public void AddRange(IEnumerable<IApplicationClient> clients)
        {
            foreach (var client in clients)
                Add(client.ClientId, client);
        }

        public IEnumerable<string> GetAllClientId()
        {
            return Values.Select(x => x.ClientId);
        }
    }
}