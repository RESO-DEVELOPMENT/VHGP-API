using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryVHGP.Core.Models
{
    public class OrderShipperModel
    {
        public int? edgeNum { get; set; }
        public  string? firstEdge  { get; set; }
        public  string? lastEdge  { get; set; }
        public int? orderNum { get; set; }
        public string? shiperId { get; set; }
        public int? status { get; set; }
        public double? totalCod { get; set; }
        public double? totalBill { get; set; }
        public string? routeId { get; set; }
    }
}
