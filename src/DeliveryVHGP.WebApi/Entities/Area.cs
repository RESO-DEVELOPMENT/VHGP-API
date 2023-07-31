﻿using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class Area
    {
        public Area()
        {
            Clusters = new HashSet<Cluster>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<Cluster> Clusters { get; set; }
    }
}
