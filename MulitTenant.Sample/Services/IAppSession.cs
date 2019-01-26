using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MulitTenant.Sample.Services
{
    public interface IAppSession
    {
        int TenantId { get;}

        int UserId { get; }

        string UserName { get; }


    }
}
