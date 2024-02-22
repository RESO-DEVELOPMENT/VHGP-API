using Microsoft.AspNetCore.Mvc;
using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Services;

namespace DeliveryVHGP.WebApi.Controllers
{
    [Route("api/v1/account-management/accounts")]
    [ApiController]
    public class AccountManagementController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;
 
        public AccountManagementController(IRepositoryWrapper repository)
        {
            this.repository = repository;

        }

        /// <summary>
        /// Get list all Account with pagination
        /// </summary>
        //GET: api/v1/Account?pageIndex=1&pageSize=3
        [HttpGet]
        public async Task<ActionResult> GetAll(int pageIndex, int pageSize)
        {
            return Ok(await repository.Account.GetAll(pageIndex, pageSize));
        }
        /// <summary>
        /// Check Account with pagination
        /// </summary>
        //GET: api/v1/Account?pageIndex=1&pageSize=3
        [HttpGet("check-Account")]
        public async Task<ActionResult> CheckAccount(string id)
        {
            try
            {
                return Ok(await repository.Account.CheckAccount(id));
            }
            catch  
            {
                return NotFound();
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login(string id, string pass)
        {
            try
            {
                return Ok(await repository.Account.Login(id,pass));
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAccount(string username, string pass, string name,  string imageUrl)
        {
            try
            {
                var result = await repository.Account.CreateAcc(username, pass,name, imageUrl);
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpPatch]
        public async Task<ActionResult>UpdateAccount(AccountViewModel model)
        {
            try
            {
                var result = await repository.Account.UpdateAccount(model);
                return Ok(result);
            }
            catch(Exception e)
            {
                return Ok(new
                {
                    e.Message
                });
            }
        }

        [HttpPatch("change-pass")]
        public async Task<ActionResult> ChangePass(string id, string pass, string newPass)
        {
            try
            {
                var result = await repository.Account.ChangePass( id,  pass,  newPass);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    e.Message
                });
            }
        }
    }
}

