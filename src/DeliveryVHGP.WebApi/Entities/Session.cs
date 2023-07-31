using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class Session
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid Timeslotid { get; set; }
        public int Status { get; set; }

        public virtual TimeSlot Timeslot { get; set; } = null!;
    }
}
