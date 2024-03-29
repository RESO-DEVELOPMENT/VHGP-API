﻿using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryVHGP.WebApi.Controllers.AdminController
{
    [Route("api/v1/menu-management/menus")]
    [ApiController]
    public class MenuManagementController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;
        public MenuManagementController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }
        [HttpGet("menuId")]
        public async Task<ActionResult> GetMenuDetail(string menuId)
        {
            try
            {
                //List<string> listA = new List<string>() {"1", "3", "4"};
                //List<string> listB = new List<string>() { "3", "4", "8", "9" };
                //var listFinal = listA.Intersect(listB);
                //var list = listB.Except(listFinal);
                var detail = await repository.Menu.GetMenuDetail(menuId);
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MenuDto>> CreatNewMenu(MenuDto menu)
        {
            try
            {
                await repository.Menu.CreatNewMenu(menu);
            }
            catch
            {
                return Conflict();
            }
            return Ok(menu);
        }

        [HttpPatch("{menuId}")]
        public async Task<ActionResult<MenuDto>> UpdateMenu(string menuId, MenuDto menu)
        {
            if (menuId == null)
            {
                return BadRequest();
            }
            try
            {
                await repository.Menu.UpdateMenu(menuId, menu);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok(menu);
        }

        [HttpDelete("{menuId}")]
        public async Task<ActionResult<MenuDto>> DeleteMenu(string menuId)
        {
            if (menuId == null)
            {
                return BadRequest();
            }
            try
            {
                await repository.Menu.DeleteMenu(menuId);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Get area of a menu
        /// </summary>
        [HttpGet("get-area")]
        public async Task<ActionResult> GetAreaOfMenu(string menuId, int pageIndex, int pageSize)
        {
            try
            {
                var detail = await repository.Menu.GetAreaOfMenu(menuId, pageIndex, pageSize);
                return Ok(detail);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Remove products from a menu
        /// </summary>
        [HttpPost("{menuId}/remove-product")]
        public async Task<ActionResult> RemoveProductFromMenu(string menuId, List<string> listProductId)
        {
            try
            {
                await repository.Menu.RemoveProductFromMenu(menuId, listProductId);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok();
        }

    }
}
