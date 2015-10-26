using System.Collections.Generic;
using SOLA.Cache.Contracts;

namespace SOLA.Cache.CacheObjects
{
    public class CustomerDataSourceCache : Dictionary<string, ICustomerDataSource>
    {
        public void AddRange(IEnumerable<ICustomerDataSource> customers)
        {
            foreach (var customer in customers)
                Add(customer.SiteName, customer);
        }
    }
}