﻿namespace DeliveryVHGP.Core.Models
{
    public class ViewListClusterInArea
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }   
        public List<ViewListBuilding> ListBuilding { get; set; }
    } 
    public class ViewListCluster
    {
        //public string Id { get; set; } = null!;
        public string? Name { get; set; }
        //public string? AreaId { get; set; }
    }
}
