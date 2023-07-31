using System;
namespace DeliveryVHGP.Core.Models
{
    public class OrderTaskModel
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? ShipperId { get; set; }
        public string? Task { get; set; } = "";
        public string? Status { get; set; } = "";
    }
}
