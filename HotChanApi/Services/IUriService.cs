using System;

using HotChanApi.Filters;

namespace HotChanApi.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);

    }
}
