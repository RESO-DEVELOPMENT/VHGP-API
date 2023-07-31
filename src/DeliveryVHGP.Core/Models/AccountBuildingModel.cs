using System;
namespace DeliveryVHGP.Core.Models
{
	public class AccountBuildingModel
	{
        public int AccountBuildId { get; set; }
        public string? AccountId { get; set; }
        public string? BuildingId { get; set; }
        public int? IsDefault { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Name { get; set; }
    }
    public class FilterRequestInAccountBuilding
    {
        public string? AccountId { get; set; } = "";
    }
}

