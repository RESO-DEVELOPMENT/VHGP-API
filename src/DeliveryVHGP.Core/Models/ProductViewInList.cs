﻿namespace DeliveryVHGP.Core.Models
{
    public class ProductViewInList
    {
        public String Id { get; set; }
        public String Image { get; set; }
        public String Name { get; set; }
        public String StoreId { get; set; }
        public String StoreName { get; set; }
        public double? PricePerPack { get; set; }
        public double? PackNetWeight { get; set; }
        public String PackDes { get; set; }
        public String Unit { get; set; }
        public double? MinimumDeIn { get; set; }
        public double? MaximumQuantity { get; set; }
        public String productMenuId { get; set; }
        public bool? Status { get; set; }

    }
}
