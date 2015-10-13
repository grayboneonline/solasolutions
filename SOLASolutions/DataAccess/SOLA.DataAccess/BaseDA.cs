using System;
using System.Collections.Generic;
using System.Reflection;
using PetaPoco;

namespace SOLA.DataAccess
{
    public abstract class BaseDA
    {
        private const string Provider = "System.Data.SqlClient";
        private readonly IConfig config;

        private Database database;
        private Database Database
        {
            get { return database ?? (database = new Database(config.ConnectionString, Provider)); }
        }

        protected BaseDA()
        {
            
        }

        protected BaseDA(IConfig config)
        {
            this.config = config;
        }

        protected IEnumerable<T> Select<T>(string queryStr = "")
        {
            if (string.IsNullOrWhiteSpace(queryStr))
            {
                var attribute = typeof(T).GetCustomAttribute<TableNameAttribute>();
                if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                    throw new ArgumentNullException("queryStr");

                queryStr = string.Format("SELECT * FROM [{0}]", attribute.Value);

            }
            return Database.Query<T>(queryStr);
        }

        protected T Get<T>(string queryStr, params object[] parameters)
        {
            return Database.FirstOrDefault<T>(queryStr, parameters);
        }

        protected void Insert(object user)
        {
            Database.Insert(user);
        }

        protected void Save(object user)
        {
            Database.Update(user);
        }

        protected void Delete<T>(object user)
        {
            Database.Delete<T>(user);
        }

        protected void Delete<T>(int id)
        {
            Database.Delete<T>(id);
        }

        protected void Delete<T>(string whereClause, params object[] parameters)
        {
            Database.Delete<T>(whereClause, parameters);
        }
    }
}
