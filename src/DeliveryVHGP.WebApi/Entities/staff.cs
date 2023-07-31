using System;
using System.Collections.Generic;

namespace DeliveryVHGP.WebApi.Entities
{
    public partial class staff
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int Role { get; set; }
        public int Status { get; set; }
    }
}
