using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class FcmToken
    {
        public string Id { get; set; } = null!;
        public string? AccountId { get; set; }
        public string? Token { get; set; }

        public virtual Account? Account { get; set; }
    }
}
