﻿using System;
using System.Collections.Generic;

namespace DeliveryVHGP.Core.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            CategoryInMenus = new HashSet<CategoryInMenu>();
            DeliveryTimeFrames = new HashSet<DeliveryTimeFrame>();
            MenuInAreas = new HashSet<MenuInArea>();
            Orders = new HashSet<Order>();
            ProductInMenus = new HashSet<ProductInMenu>();
            StoreInMenus = new HashSet<StoreInMenu>();
        }

        public string Id { get; set; } = null!;
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public DateTime? DayFilter { get; set; }
        public string? HourFilter { get; set; }
        public double? StartHour { get; set; }
        public double? EndHour { get; set; }
        public string? SaleMode { get; set; }
        public string? Status { get; set; }
        public double? ShipCost { get; set; }
        public int? Priority { get; set; }

        public virtual ICollection<CategoryInMenu> CategoryInMenus { get; set; }
        public virtual ICollection<DeliveryTimeFrame> DeliveryTimeFrames { get; set; }
        public virtual ICollection<MenuInArea> MenuInAreas { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductInMenu> ProductInMenus { get; set; }
        public virtual ICollection<StoreInMenu> StoreInMenus { get; set; }
    }
}
