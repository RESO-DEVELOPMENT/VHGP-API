using System;
using System.Security.Cryptography;
using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Enums;
using DeliveryVHGP.Core.Interface.IRepositories;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using DeliveryVHGP.Infrastructure.Services;
using Google.Protobuf.WellKnownTypes;
using Google.Type;
using Microsoft.EntityFrameworkCore;
using static DeliveryVHGP.Core.Models.OrderAdminDto;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace DeliveryVHGP.Infrastructure.Repositories
{
    public class OrderTaskRepository : RepositoryBase<OrderTask>, IOrderTaskRepository
    {

        public OrderTaskRepository(DeliveryVHGP_DBContext context) : base(context)
        {
            
        }
        public async Task<bool> CreateOrderTask(OrderTaskModel orderTask)
        {
            var odTask = new OrderTask
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = orderTask.OrderId,
                ShipperId = orderTask.ShipperId,
                Task = orderTask.Task,
                Status = orderTask.Status
            };
            await context.OrderTasks.AddAsync(odTask);
            var segDeliRoute = new SegmentDeliveryRoute
            {
                //Id = Guid.NewGuid().ToString(),
                //ShipperId = orderTask.ShipperId,
                //FromShipperId = null,
                //ToShipperId = null,
                //Distance = 1000,
                //Duration = null,
                //Description = null,
                //Type = 3,
                //Status = 3
                Id = Guid.NewGuid().ToString(),
                ShipperId = null,
                FromShipperId = null,
                ToShipperId = null,
                Distance = 1000,
                Duration = null,
                Description = null,
                Type = 3,
                Status = 1
            };
            await context.SegmentDeliveryRoutes.AddAsync(segDeliRoute);
            var orderGet = await context.Orders.Where(x => x.Id == orderTask.OrderId).FirstOrDefaultAsync();

            var store = await context.Stores.Where(x => x.Id == orderGet.StoreId).FirstOrDefaultAsync();

            var routeEdge = new RouteEdge
            {
                Id = Guid.NewGuid().ToString(),
                FromBuildingId = store.BuildingId,
                ToBuildingId = orderGet.BuildingId,
                Priority = 1,
                Distance = 1000,
                RouteId = segDeliRoute.Id,
                //Status = 3
                Status = 1
            };
            await context.RouteEdges.AddAsync(routeEdge);
            var odAction = new OrderAction
            {
                Id = Guid.NewGuid().ToString(),
                RouteEdgeId = routeEdge.Id,
                OrderId = orderTask.OrderId,
                //DeliveryCus
                OrderActionType = 4,
                // todo 1
                Status = 1
            };
            await context.OrderActions.AddAsync(odAction);

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("hihi ngu");
            }

            return true;
        }
        public async Task<List<OrderShipperModel>> GetOrderTaskByShipper(string shipperId)
        {
            var lstOrder = await (from order in context.Orders
                                  join s in context.Stores on order.StoreId equals s.Id
                                  join b in context.Buildings on order.BuildingId equals b.Id
                                  join p in context.OrderTasks on order.Id equals p.OrderId
                                  join oa in context.OrderActions on order.Id equals oa.OrderId
                                  where p.ShipperId == shipperId
                                  select new OrderShipperModel()
                                  {
                                      edgeNum=2,
                                      firstEdge = s.Building.Name,
                                      lastEdge = b.Name,
                                      orderNum=1,
                                      shiperId=shipperId,
                                      status=order.Status,
                                      totalBill=order.Total,
                                      totalCod=order.ShipCost,
                                      routeId=oa.RouteEdge.RouteId

                                  }
                               ).OrderByDescending(t => t.orderNum).ToListAsync();

           return lstOrder;
        }

    }
}

