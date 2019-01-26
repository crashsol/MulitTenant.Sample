using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MulitTenant.Sample.Models
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Post:BaseEntity
    {

        public string Name { get; set; }

        public string Content { get; set; }
    }

}
