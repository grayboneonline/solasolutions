﻿using SOLA.DataAccess.Base;
using SOLA.DataAccess.Models;

namespace SOLA.DataAccess
{
    public interface IPermissionDA : IBaseDA<Permission>
    {
    }

    public class PermissionDA : BaseDA<Permission>, IPermissionDA
    {
        public PermissionDA(CustomerDatabase database) : base(database)
        {
        }
    }
}
