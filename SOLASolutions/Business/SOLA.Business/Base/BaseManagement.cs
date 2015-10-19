using System.Collections.Generic;
using SOLA.DataAccess.Base;

namespace SOLA.Business.Base
{
    public interface IBaseManagement<TBusinessModel>
    {
        IEnumerable<TR> GetAll<TR>();
        TR GetById<TR>(int id);
        void Add(TBusinessModel user);
        int Update(TBusinessModel user);
        int Delete(int id);
    }

    public abstract class BaseManagement<TBusinessModel, TDA, TDAModel> : IBaseManagement<TBusinessModel>
        where TDA : IBaseDA<TDAModel>
    {
        protected readonly TDA DA;

        protected BaseManagement(TDA da)
        {
            DA = da;
        }

        #region Implement interface

        public IEnumerable<TR> GetAll<TR>()
        {
            return DA.GetAll<TR>();
        }

        public TR GetById<TR>(int id)
        {
            return DA.GetById<TR>(id);
        }

        public void Add(TBusinessModel user)
        {
            DA.Add(user.MapTo<TDAModel>());
        }

        public int Update(TBusinessModel user)
        {
            return DA.Update(user.MapTo<TDAModel>());
        }

        public int Delete(int id)
        {
            return DA.Delete(id);
        } 

        #endregion
    }
}
