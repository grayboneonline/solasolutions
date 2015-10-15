﻿using System.Collections.Generic;
using System.Linq;
using SOLA.Models.Admin;

namespace SOLA.Cache.CacheObjects
{
    public class ApplicationClientCache : Dictionary<string, ApplicationClient>
    {
        public void AddRange(IEnumerable<ApplicationClient> clients)
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