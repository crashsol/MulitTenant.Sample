﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MulitTenant.Sample.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Host { get; set; }
    }
}
