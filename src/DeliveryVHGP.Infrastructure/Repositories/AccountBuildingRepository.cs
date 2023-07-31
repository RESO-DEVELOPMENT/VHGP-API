using System;
using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interface.IRepositories;
using DeliveryVHGP.Core.Interfaces.IRepositories;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DeliveryVHGP.Infrastructure.Repositories
{
	public class AccountBuildingRepository : RepositoryBase<AccountBuilding>, IAccountBuildingRepository
    {
        public AccountBuildingRepository(DeliveryVHGP_DBContext context) : base(context)
        {
        }

        public async Task<AccountBuildingModel> CreateAccountBuilding(AccountBuildingModel accountBuilding)
        {
            context.AccountBuildings.Add(new AccountBuilding {
                //AccountBuildId = accountBuilding.AccountBuildId,
               
            SoDienThoai = accountBuilding.SoDienThoai,
            Name = accountBuilding.Name,
            AccountId = accountBuilding.AccountId,
            BuildingId = accountBuilding.BuildingId,IsDefault = accountBuilding.IsDefault});
            await context.SaveChangesAsync();
            return accountBuilding;
        }

        public async Task<object> DeleteById(int accountBuildingId)
        {
            var accountBuilding = await context.AccountBuildings.FindAsync(accountBuildingId);
            context.AccountBuildings.Remove(accountBuilding);
            await context.SaveChangesAsync();

            return accountBuilding;
        }

        public async Task<IEnumerable<AccountBuildingDto>> GetAll(int pageIndex, int pageSize, FilterRequestInAccountBuilding request)
        {
            var listAccountBuilding = await context.AccountBuildings
                .Include(a => a.Building).ThenInclude(c => c.Cluster).ThenInclude(d => d.Area)
                .Where(b => b.AccountId == request.AccountId)
                .Select(x => new AccountBuildingDto
                {
                    AccountBuildId = x.AccountBuildId,
                    AccountId = x.AccountId,
                    BuildingId = x.BuildingId,
                    
                    IsDefault = x.IsDefault,
                    SoDienThoai = x.SoDienThoai,
                    Name = x.Name,
                    ClusterId = x.Building.Cluster.Id,
                    AreaId = x.Building.Cluster.Area.Id,
                    BuildingName = x.Building.Name,
                    ClusterName = x.Building.Cluster.Name,
                    AreaName = x.Building.Cluster.Area.Name

                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
           
            return listAccountBuilding;
        }

        public async Task<AccountBuildingModel> GetBuildingByAccountBuilding(int accountBuildingId)
        {
            var accountBuilding = await context.AccountBuildings.Where(x => x.AccountBuildId == accountBuildingId).Select(x => new AccountBuildingModel
            {
                AccountBuildId = x.AccountBuildId,
                AccountId = x.AccountId,
                BuildingId = x.BuildingId,
                IsDefault = x.IsDefault,
                SoDienThoai = x.SoDienThoai,
                Name = x.Name
            }).FirstOrDefaultAsync();
            return accountBuilding;
        }

        public async Task<object> UpdateAccountBuildingById(int accountBuildingId, AccountBuildingDto accountBuilding)
        {
            if (accountBuildingId < 0 )
            {
                return null;
            }
            var result_check = await context.AccountBuildings.FindAsync(accountBuildingId);
            if (accountBuilding == null)
            {
                var result_update = await context.AccountBuildings.Where(x => x.AccountId == result_check.AccountId).ToListAsync();
                foreach (var item in result_update)
                {
                    item.IsDefault = 0;
                    context.Entry(result_check).State = EntityState.Modified;
                    try
                    {
                        await context.SaveChangesAsync();
                    }

                    catch
                    {
                        throw;
                    }
                }

            }
            var result = await context.AccountBuildings.FindAsync(accountBuildingId);
            if(accountBuilding != null)
            {
                result.AccountBuildId = accountBuilding.AccountBuildId;
                result.AccountId = accountBuilding.AccountId;
                result.BuildingId = accountBuilding.BuildingId;
                result.IsDefault = accountBuilding.IsDefault;
                result.SoDienThoai = accountBuilding.SoDienThoai;
                result.Name = accountBuilding.Name;
            }
            else
            {
                result.IsDefault = 1;
                

            }
           
            
            context.Entry(result).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }

            catch
            {
                throw;
            }
            
            return accountBuilding;
        }
    }
}

