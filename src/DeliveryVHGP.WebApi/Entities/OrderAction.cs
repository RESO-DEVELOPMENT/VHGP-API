﻿using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class OrderAction
    {
        public string Id { get; set; } = null!;
        public string? RouteEdgeId { get; set; }
        public string? OrderId { get; set; }
        public int? OrderActionType { get; set; }
        public int? Status { get; set; }

        public virtual Order? Order { get; set; }
        public virtual RouteEdge? RouteEdge { get; set; }
    }
}
