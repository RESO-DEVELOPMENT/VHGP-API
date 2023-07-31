using System;
namespace DeliveryVHGP.Core.Models
{
	public class AccountBuildingDto
	{
        public int AccountBuildId { get; set; }
        public string? AccountId { get; set; }
        public string? BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public int? IsDefault { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Name { get; set; }
        public string? AreaId { get; set; }
        public string? ClusterId { get; set; }
        public string? AreaName { get; set; }
        public string? ClusterName { get; set; }
    }
}

