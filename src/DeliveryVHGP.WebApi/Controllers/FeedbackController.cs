using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryVHGP.WebApi.Controllers
{

    [Route("api/v1/feedback")]
    [ApiController]
    public class FeedbackController : Controller
    {

        private readonly IRepositoryWrapper repository;

        public FeedbackController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get list feedback by store with pagination
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetListFeedbackByStore([FromQuery] string storeId, [FromQuery] bool? isAscending, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                return Ok(await repository.Feedback.GetAllFeedbackByStore(storeId, pageIndex, pageSize, isAscending));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a feedback by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFeedbackById([FromRoute] string id)
        {
            try
            {
                return Ok(await repository.Feedback.GetFeedbackById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a feedback
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateFeedback([FromQuery] string orderId, [FromBody] FeedbackModel feedbackModel)
        {
            try
            {
                return Ok(await repository.Feedback.CreateFeedback(orderId, feedbackModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
