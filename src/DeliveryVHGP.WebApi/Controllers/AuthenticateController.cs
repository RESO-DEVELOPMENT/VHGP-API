using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryVHGP.WebApi.Controllers
{

    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly IRepositoryWrapper repository;
        public AuthenticateController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginWithPhoneNumber([FromBody] LoginByFirebaseTokenRequest request)
        {
            var response = repository.Account.LoginWithPhoneNumber(request.IdToken, request.FcmToken);

            return Ok(response);
        }
    }
}
