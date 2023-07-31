using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class Account
    {
        public Account()
        {
            AccountBuildings = new HashSet<AccountBuilding>();
            FcmTokens = new HashSet<FcmToken>();
            Wallets = new HashSet<Wallet>();
        }

        public string Id { get; set; } = null!;
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? RoleId { get; set; }
        public string? Status { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<AccountBuilding> AccountBuildings { get; set; }
        public virtual ICollection<FcmToken> FcmTokens { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
