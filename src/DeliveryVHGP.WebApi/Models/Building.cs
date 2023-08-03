﻿using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Models
{
    public partial class Building
    {
        public Building()
        {
            AccountBuildings = new HashSet<AccountBuilding>();
            Orders = new HashSet<Order>();
            Segments = new HashSet<Segment>();
            Stores = new HashSet<Store>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? ClusterId { get; set; }
        public string? HubId { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        public virtual Cluster? Cluster { get; set; }
        public virtual Hub? Hub { get; set; }
        public virtual ICollection<AccountBuilding> AccountBuildings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Segment> Segments { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
