﻿using System;
using System.Collections.Generic;

namespace DeliveryVHGP.Core.Entities
{
    public partial class ProductInMenu
    {
        public string Id { get; set; } = null!;
        public double? Price { get; set; }
        public string? MenuId { get; set; }
        public string? ProductId { get; set; }
        public bool? Status { get; set; }
        public int? Priority { get; set; }

        public virtual Menu? Menu { get; set; }
        public virtual Product? Product { get; set; }
    }
}
