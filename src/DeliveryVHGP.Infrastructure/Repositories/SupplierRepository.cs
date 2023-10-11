using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Enums;
using DeliveryVHGP.Core.Interface.IRepositories;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using DeliveryVHGP.Infrastructure.Services;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;
using Order = DeliveryVHGP.Core.Entities.Order;

namespace DeliveryVHGP.WebApi.Repositories
{
    public class SupplierRepository : RepositoryBase<Account>, ISupplierRepository
    {
        private readonly IFileService _fileService;

        public SupplierRepository(DeliveryVHGP_DBContext context) : base(context)
        {  
        }

        public async Task<OrderDto> CreateNewBillOfLanding(string supplierId, BillOfLandingDto order)
        {
            string refixOrderCode = "CDCC";
            var orderCount = context.Orders
               .Count() + 1;
            var orderId = refixOrderCode + "-" + orderCount.ToString().PadLeft(6, '0');
            var odCOde = await context.Orders.Where(o => o.Id == orderId).ToListAsync();
            if (odCOde.Any())
            {
                orderId = refixOrderCode + "-" + orderCount.ToString().PadLeft(7, '0');
            }

            var store = context.Stores.FirstOrDefault(s => s.Id == supplierId);
            if (store.Status == false)
            {
                throw new Exception("Đơn hàng không hợp lệ");
            }

            //var shipCost = await context.Menus.Where(x => x.Id == order.MenuId).Select(x => x.ShipCost).FirstOrDefaultAsync();
            var od = new Order
            {
                Id = orderId,
                Total = order.Total,
                StoreId = store.Id,
                BuildingId = order.BuildingId,
                Note = order.Note,
                FullName = order.FullName,
                PhoneNumber = order.PhoneNumber,
                ShipCost = (float)ShipCostEnum.BillOfLanding, // Default: BillOfLanding = 8000
                DeliveryTimeId = order.DeliveryTimeId,
                ServiceId = "1", // Default: giao hang nhanh
                Status = (int)OrderStatusEnum.Assigning // Shop accept and add to segment
            };

            string time = await GetTime();

            var actionReviceHistory = new OrderActionHistory()
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = od.Id,
                FromStatus = (int)OrderStatusEnum.New,
                ToStatus = (int)OrderStatusEnum.Received,
                CreateDate = DateTime.UtcNow.AddHours(7),
                TypeId = "1"
            };

            await context.OrderActionHistories.AddAsync(actionReviceHistory);

            await context.Orders.AddAsync(od);

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }

            return new OrderDto {
                Id = orderId,
                StoreId = supplierId,
                BuildingId = order.BuildingId,
                DeliveryTimeId =order.DeliveryTimeId,
                ServiceId = "1",
                ModeId = "1",
                Total = order.Total,
                PhoneNumber = order?.PhoneNumber,
                FullName = order?.FullName,
            };
        }

        public async Task<string> GetTime()
        {
            DateTime utcDateTime = DateTime.UtcNow;

            string vnTimeZoneKey = "SE Asia Standard Time";
            TimeZoneInfo vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById(vnTimeZoneKey);
            string time = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnTimeZone).ToString("yyyy/MM/dd HH:mm");
            return time;
        }

    }
}