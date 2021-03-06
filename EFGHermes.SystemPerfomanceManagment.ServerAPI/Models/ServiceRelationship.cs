﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGHermes.SystemPerfomanceManagment.ServerAPI.Models
{
    public class ServiceRelationship
    {
        public int FromServiceId { get; set; }
        public virtual Service FromService { get; set; }

        public int ToServiceId { get; set; }
        public virtual Service ToService { get; set; }
    }
}
