using System.Collections.Generic;
using SOLA.Models.Admin;

namespace SOLA.Cache.CacheObjects
{
    public class CustomerDataSourceCache : Dictionary<string, CustomerDataSource>
    {
        public void AddRange(IEnumerable<CustomerDataSource> customers)
        {
            foreach (var customer in customers)
                Add(customer.SiteName, customer);
        }
    }
}