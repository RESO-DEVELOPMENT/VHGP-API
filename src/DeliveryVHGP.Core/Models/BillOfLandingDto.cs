namespace DeliveryVHGP.Core.Models
{
    public class BillOfLandingDto
    {
        public string? PhoneNumber { get; set; }
        public double? Total { get; set; }
        public string? BuildingId { get; set; }
        public string? Note { get; set; }
        public string? FullName { get; set; }
        public double? ShipCost { get; set; }
        public string? DeliveryTimeId { get; set; }
        public int? PaymentType { get; set; }

        //public List<BillProductDetailDto> ProductDetail { get; set; }
        //public List<PaymentDto> Payments { get; set; }


    }
}
