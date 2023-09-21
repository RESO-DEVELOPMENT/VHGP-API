using System;
using System.Collections.Generic;

namespace DeliveryVHGP.Core.Entities
{
    public partial class MenuInArea
    {
        public string Id { get; set; } = null!;
        public string? MenuId { get; set; }
        public string? AreaId { get; set; }

        public virtual Area? Area { get; set; }
        public virtual Menu? Menu { get; set; }
    }
}
