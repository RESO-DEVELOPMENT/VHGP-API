using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryVHGP.Core.Models
{
    public class LoginByFirebaseTokenRequest
    {
        public string IdToken { get; set; }
        public string FcmToken { get; set; }
    }
}
