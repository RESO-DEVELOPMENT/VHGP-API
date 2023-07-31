using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            Sessions = new HashSet<Session>();
        }

        public Guid Id { get; set; }
        public TimeSpan? Starttime { get; set; }
        public TimeSpan? Endtime { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
