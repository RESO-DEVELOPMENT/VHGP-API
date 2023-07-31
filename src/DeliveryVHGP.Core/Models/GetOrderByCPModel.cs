using System;
namespace DeliveryVHGP.Core.Models
{
	public class GetOrderByCPModel
	{
        public string? edgeNum { get; set; }
        public string? firstEdge { get; set; }
        public string? lastEdge { get; set; }
        public string? orderNum { get; set; }
        public int? shipperId { get; set; }
        public int? status { get; set; }
        public string? totalBill { get; set; }
        public string? totalCod { get; set; }

    }
}

