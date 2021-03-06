﻿using SOLA.DataAccess.Base;
using SOLA.DataAccess.Models.Admin;

namespace SOLA.DataAccess.Admin
{
    public interface IAzureSqlServerDA : IBaseDA<AzureSqlServer>
    {
    }

    public class AzureSqlServerDA : BaseDA<AzureSqlServer>, IAzureSqlServerDA
    {
        public AzureSqlServerDA(AdminDatabase database) : base(database)
        {
        }
    }
}