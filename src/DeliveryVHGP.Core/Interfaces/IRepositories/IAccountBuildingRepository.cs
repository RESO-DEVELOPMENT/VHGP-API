using System;
using System.Linq.Expressions;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Models;

namespace DeliveryVHGP.Core.Interfaces.IRepositories
{
	public interface IAccountBuildingRepository : IRepositoryBase<AccountBuilding>
    {
        Task<IEnumerable<AccountBuildingDto>> GetAll(int pageIndex, int pageSize, FilterRequestInAccountBuilding request);
        Task<AccountBuildingModel> GetBuildingByAccountBuilding(int accountBuildingId);
        Task<AccountBuildingModel> CreateAccountBuilding(AccountBuildingModel accountBuilding);
        Task<Object> UpdateAccountBuildingById(int accountBuildingId, AccountBuildingDto accountBuilding);
        Task<Object> DeleteById(int accountBuildingId);
    }
}

