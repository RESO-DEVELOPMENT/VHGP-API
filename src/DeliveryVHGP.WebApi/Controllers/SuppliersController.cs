using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeliveryVHGP.WebApi.Controllers
{
    [Route("api/v1/suppliers")]
    [ApiController]
    public class SuppliersController : Controller
    {

        private readonly IRepositoryWrapper repository;
        public SuppliersController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Make an order (for supplier)
        /// </summary>
        //POST: api/v1/order
        [HttpPost("{supplierId}/order")]
        public async Task<ActionResult> MakeNewOrder(string supplierId, OrderDto order)
        {
            try
            {

                var accountCheck = await repository.Account.CheckAccount(supplierId);
                if (accountCheck == null)
                {
                    return Ok(new
                    {
                        StatusCode = "Fail",
                        message = "Account không tồn tại"
                    });
                }
                else if (accountCheck.RoleId != "2")
                {
                    return Ok(new
                    {
                        StatusCode = "Fail",
                        message = "Role của account không phải là supplier"
                    });
                }
                var result = await repository.Order.CreatNewOrder(order);
                if (result != null)
                {
                    await repository.Segment.CreatSegment(result);
                }

                return Ok(new { StatusCode = "Successful", data = result });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = "Fail",
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Supplier creates a bill of landing for customer
        /// </summary>
        //POST: api/v1/billOfLanding
        [HttpPost("{supplierId}/billOfLanding")]
        public async Task<ActionResult> CreateNewBillOfLanding(string supplierId, BillOfLandingDto order)
        {
            try
            {

                var accountCheck = await repository.Account.CheckAccount(supplierId);
                if (accountCheck == null)
                {
                    return Ok(new
                    {
                        StatusCode = "Fail",
                        message = "Account không tồn tại"
                    });
                }
                else if (accountCheck.RoleId != "2")
                {
                    return Ok(new
                    {
                        StatusCode = "Fail",
                        message = "Role của account không phải là supplier"
                    });
                }
                var result = await repository.Supplier.CreateNewBillOfLanding(supplierId, order);
                if (result != null)
                {
                    await repository.Segment.CreatSegment(result);
                }

                return Ok(new { StatusCode = "Successful", data = result });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = "Fail",
                    message = ex.Message
                });
            }
        }

    }
}