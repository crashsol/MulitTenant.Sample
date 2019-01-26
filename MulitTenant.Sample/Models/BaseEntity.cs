using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MulitTenant.Sample.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public int TenantId { get; set; }

    }
}
