using System;
using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interfaces.IRepositories;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DeliveryVHGP.Infrastructure.Repositories
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(DeliveryVHGP_DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FeedbackModel>> GetAllFeedbackByStore(string storeId, int pageIndex, int pageSize, bool? isAscending = null)
        {
            // Check is store existed
            var store = context.Stores.Where(s => s.Id == storeId).FirstOrDefault();

            if (store == null)
            {
                throw new Exception("Store does not exist");
            }


            var feedbacks = context.Orders.Join(context.Feedbacks, order => order.Id, feedback => feedback.OrderId,
                (order, feedback) => new
                {
                    order.StoreId,
                    feedback.Description,
                    feedback.Rating
                }).Where(f => f.StoreId == storeId).Select(f => new FeedbackModel
                {
                    Description = f.Description,
                    Rating = f.Rating,
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            /*feedbacks = isAscending ? feedbacks.OrderBy(feedback => feedback.Rating) : feedbacks.OrderByDescending(feedback => feedback.Rating);*/

            // Sort by rating 
            if (isAscending != null)
            {
                if (isAscending == true)
                {
                    feedbacks = feedbacks.OrderBy(feedback => feedback.Rating);
                } 
                else
                {
                    feedbacks = feedbacks.OrderByDescending(feedback => feedback.Rating);
                }
            }
            


            return await feedbacks.ToListAsync();
        }

        public async Task<FeedbackModel> GetFeedbackById(string feedbackId)
        {
            var feedback = await context.Feedbacks.Where(f => f.Id == feedbackId).Select(
                f => new FeedbackModel
                {
                    Description = f.Description,
                    Rating = f.Rating,
                }).FirstOrDefaultAsync();

            if (feedback == null)
            {
                throw new Exception("Feedback does not exist.");
            }

            return feedback;
        }

        public async Task<FeedbackModel> CreateFeedback(string orderId, FeedbackModel feedbackModel)
        {

            var order = await context.Orders.Where(o => o.Id == orderId).Include(o => o.Store).FirstOrDefaultAsync();

            if (order == null)
            {
                throw new Exception("Order does not exist");
            }

            if (order.Status != 5)
            {
                throw new Exception("Order does not finish yet");
            }

            Feedback feedback = new Feedback
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = orderId,
                Description = feedbackModel.Description,
                Rating = feedbackModel.Rating,
            };

            // Calculate average rate for store
            var sumRatings = context.Orders.Where(f => f.StoreId == order.StoreId)
                .Join(context.Feedbacks, o => o.Id, f => f.OrderId, (o, f) => f.Rating)
                .Sum();


            var allRatings = context.Orders.Where(f => f.StoreId == order.StoreId)
                .Join(context.Feedbacks, o => o.Id, f => f.OrderId, (o, f) => f.Rating)
                .Count();
            
            order.Store.Rate = ((double)(sumRatings + feedbackModel.Rating) / (allRatings + 1)).ToString();


            await context.Feedbacks.AddAsync(feedback);
            await context.SaveChangesAsync();

            return feedbackModel;
        }

    }
}

