using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MulitTenant.Sample.Services
{
    public class AppSessionProvider:IAppSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppSessionProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int TenantId => int.Parse(_httpContextAccessor.HttpContext.User?.Claims.SingleOrDefault(b => b.Type == "TenantId")?.Value ?? "0");

        public int UserId => int.Parse(_httpContextAccessor.HttpContext.User?.Claims.SingleOrDefault(b => b.Type == "UserId")?.Value ?? "0");

        public string UserName => _httpContextAccessor.HttpContext.User?.Claims.SingleOrDefault(b => b.Type == "Name")?.Value ?? "";
    }
}
