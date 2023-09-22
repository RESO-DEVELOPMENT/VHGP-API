using System;
using System.Linq.Expressions;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Models;

namespace DeliveryVHGP.Core.Interfaces.IRepositories
{
	public interface IFeedbackRepository : IRepositoryBase<Feedback>
    {

        Task<IEnumerable<FeedbackModel>> GetAllFeedbackByStore(string storeId, int pageIndex, int pageSize, bool? isAscending = null);
        Task<FeedbackModel> GetFeedbackById(string feedbackId);
        Task<FeedbackModel> GetFeedbackByOrderId(string orderId);
        Task<FeedbackModel> CreateFeedback(string orderId, FeedbackModel feedback);
    }
}

