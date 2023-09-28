using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;

namespace DeliveryVHGP.Core.Interface.IRepositories
{
    public interface ISupplierRepository : IRepositoryBase<Account>
    {
        Task<OrderDto> CreateNewBillOfLanding(string supplierId, BillOfLandingDto bill);
    }
}