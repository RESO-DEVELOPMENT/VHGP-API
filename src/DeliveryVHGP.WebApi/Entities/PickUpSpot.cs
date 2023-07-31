using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class PickUpSpot
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = null!;
        public Guid Locationid { get; set; }
        public int Status { get; set; }

        public virtual Location Location { get; set; } = null!;
    }
}
