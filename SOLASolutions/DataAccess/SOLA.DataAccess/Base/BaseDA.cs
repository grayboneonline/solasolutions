using System;
using System.Collections.Generic;
using System.Reflection;
using PetaPoco;

namespace SOLA.DataAccess.Base
{
    public interface IBaseDA<T>
    {
        IEnumerable<TR> GetAll<TR>();
        TR GetById<TR>(int id);
        void Add(T user);
        int Update(T user);
        int Delete(int id);
    }

    public abstract class BaseDA<T> : IBaseDA<T>
    {
        protected Database Database { get; private set; }

        protected BaseDA(Database database)
        {
            Database = database;
        }

        protected IEnumerable<TR> Select<TR>(string queryStr = "", params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(queryStr))
            {
                var attribute = typeof(T).GetCustomAttribute<TableNameAttribute>();
                if (attribute == null || string.IsNullOrWhiteSpace(attribute.Value))
                    throw new ArgumentNullException("queryStr");

                queryStr = string.Format("SELECT * FROM [{0}]", attribute.Value);

            }
            return Database.Query<TR>(queryStr, parameters);
        }

        protected TR Get<TR>(string queryStr, params object[] parameters)
        {
            return Database.FirstOrDefault<TR>(queryStr, parameters);
        }
        
        protected int Delete(string whereClause, params object[] parameters)
        {
            return Database.Delete<T>(whereClause, parameters);
        }

        #region Implement IBaseDA

        public IEnumerable<TR> GetAll<TR>()
        {
            return Select<TR>();
        }

        public TR GetById<TR>(int id)
        {
            return Database.SingleOrDefault<TR>(id);
        }

        public void Add(T user)
        {
            Database.Insert(user);
        }

        public int Update(T user)
        {
            return Database.Update(user);
        }

        public int Delete(int id)
        {
            return Database.Delete<T>(id);
        } 

        #endregion
    }
}
