using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryVHGP.Core.Models
{
    public class OrderUpdateModel
    {
        //public string? BuildingId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        public double? Total { get; set; }
        public double? ShipCost { get; set; }
        public int? Status { get; set; }
        public int? PaymentType { get; set; }

    }
}
