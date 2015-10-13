using System;
using System.Collections.Generic;
using System.Reflection;
using PetaPoco;

namespace SOLA.DataAccess.Helpers
{
    public static class DataAccessHelper
    {
        public static IEnumerable<T> Select<T>(IConfig config, string queryStr = "")
        {
            if (string.IsNullOrWhiteSpace(queryStr))
            {
                var attribute = typeof (T).GetCustomAttribute<TableNameAttribute>();
                if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                    throw new ArgumentNullException("Empty query string");

                queryStr = string.Format("SELECT * FROM [{0}]", attribute.Value);

            }
            var db = new Database(config.ConnectionString, "System.Data.SqlClient");
            return db.Query<T>(queryStr);
        }
    }
}
