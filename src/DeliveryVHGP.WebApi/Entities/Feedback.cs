using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class Feedback
    {
        public string Id { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public string? Description { get; set; }
        public int? Rating { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
