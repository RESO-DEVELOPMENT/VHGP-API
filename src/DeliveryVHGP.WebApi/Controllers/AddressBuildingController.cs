using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryVHGP.WebApi.Controllers
{
    [Route("api/v1/account-building")]
    [ApiController]
    public class AddressBuildingController : Controller
    {

        private readonly IRepositoryWrapper repository;
        public AddressBuildingController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(int pageIndex, int pageSize, [FromQuery] FilterRequestInAccountBuilding request)
        {
            return Ok(await repository.AccountBuilding.GetAll(pageIndex, pageSize, request));
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var brand = await repository.AccountBuilding.GetBuildingByAccountBuilding(id);
            if (brand == null)
                return NotFound();
            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccountBuilding(AccountBuildingModel accountBuilding)
        {
            try
            {
                var result = await repository.AccountBuilding.CreateAccountBuilding(accountBuilding);
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccountBuilding(int id)
        {
            try
            {
                var result = await repository.AccountBuilding.DeleteById(id);
                return Ok(result);
            }
            catch
            {
                return Conflict();
            }

        }
     
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateAccountBuilding(int id, AccountBuildingDto brand)
        {
            try
            {
                //if (id != brand.Id)
                //{
                //    return BadRequest("Brand ID mismatch");
                //}
                var BrandToUpdate = await repository.AccountBuilding.UpdateAccountBuildingById(id, brand);
                return Ok(brand);
            }
            catch
            {
                return Conflict();
            }
        }

        [HttpPatch("address-default")]
        public async Task<ActionResult> SetAddressDefault(int id)
        {
            try
            {
                //if (id != brand.Id)
                //{
                //    return BadRequest("Brand ID mismatch");
                //}
                AccountBuildingDto? accountBuilding = null;
                var BrandToUpdate = await repository.AccountBuilding.UpdateAccountBuildingById(id, accountBuilding);
                return Ok(id);
            }
            catch
            {
                return Conflict();
            }
        }
    }
}

