using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Models
{
    public partial class AccountBuilding
    {
        public int AccountBuildId { get; set; }
        public string? AccountId { get; set; }
        public string? BuildingId { get; set; }
        public int? IsDefault { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Name { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Building? Building { get; set; }
    }
}
