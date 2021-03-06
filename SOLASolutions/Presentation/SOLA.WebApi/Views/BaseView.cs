﻿using System.Web.Mvc;
using SOLA.Cache;

namespace SOLA.WebApi.Views
{
    public abstract class BaseView<T> : WebViewPage<T>
    {
        public IRequestScopeCache RequestScopeCache { get; set; }
    }

    public abstract class BaseView : BaseView<object>
    {
    }
}