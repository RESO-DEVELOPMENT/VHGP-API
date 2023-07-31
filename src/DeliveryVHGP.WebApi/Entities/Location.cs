using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class Location
    {
        public Location()
        {
            PickUpSpots = new HashSet<PickUpSpot>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }

        public virtual ICollection<PickUpSpot> PickUpSpots { get; set; }
    }
}
