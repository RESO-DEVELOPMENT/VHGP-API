using System;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;

namespace DeliveryVHGP.Core.Interface.IRepositories
{
	public interface IOrderTaskRepository : IRepositoryBase<OrderTask>
    {
        Task<bool> CreateOrderTask(OrderTaskModel orderTask);
        Task<List<OrderShipperModel>> GetOrderTaskByShipper(string shipperId);
    }
}

