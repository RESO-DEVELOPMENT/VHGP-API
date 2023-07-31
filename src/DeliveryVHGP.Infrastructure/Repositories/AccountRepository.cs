using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interface.IRepositories;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using DeliveryVHGP.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace DeliveryVHGP.WebApi.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private readonly IFileService _fileService;
        public AccountRepository(DeliveryVHGP_DBContext context, IFileService fileService) : base(context)
        {
            _fileService = fileService;
        }

        public async Task<List<AccountModel>> GetAll(int pageIndex, int pageSize)
        {
            var listAccount = await context.Accounts.
                Select(x => new AccountModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Password = x.Password,
                    RoleId = x.RoleId,
                    Status = x.Status,
                    ImageUrl = x.ImageUrl
                }).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return listAccount;
        }
        public async Task<AccountCheck> CheckAccount(string id)
        {
            var check = await context.Accounts.
                Where(x => x.Id == id).
                Select(x => new AccountCheck
                {
                    RoleId = x.RoleId,
                })
                .FirstOrDefaultAsync();

            return check;
        }

        public async Task<AccountModel> Login(string id, string pass)
        {
            var check = await context.Accounts.
                Where(x => x.Id == id && x.Password == pass).
                Select(x => new AccountModel
                {
                    Id = x.Id,
                    Name =x.Name,
                    RoleId = x.RoleId,
                    Status = x.Status,
                    ImageUrl=x.ImageUrl
         })
                .FirstOrDefaultAsync();

            return check;
        }
        public async Task<string> CreateAcc(string id, string pass, string name, string imageUrl)
        {
            string fileImg = "ImagesAccounts";
            string image = await _fileService.UploadFile(fileImg, imageUrl);

            Account acc = new Account() { Id = id, Name = name, RoleId = "2", Password = pass, Status = "true", ImageUrl=image };
            await context.Accounts.AddAsync(acc);
            context.SaveChangesAsync();
            return id;
        }   

        public async Task<string> ChangePass(string id, string pass, string newPass)
        {
            var check = await context.Accounts.
            Where(x => x.Id == id && x.Password == pass).FirstOrDefaultAsync();
            if (check==null)
            {
                throw new Exception("Id or pass not correct");
            }

            check.Password = newPass;

            try
            {
                context.Entry(check).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return check.Id;
        }

        public async Task<string> UpdateAccount(AccountViewModel a)
        {

            var check = await context.Accounts.
               Where(x => x.Id == a.Id).FirstOrDefaultAsync();
                if (check == null)
                {
                    throw new Exception("acc not exits");
                }

            string fileImg = "ImagesAccounts";
            string image = await _fileService.UploadFile(fileImg, a.ImageUrl);

            if (!String.IsNullOrEmpty(image))
            {
                check.ImageUrl = image;
            }
            if (!String.IsNullOrEmpty(a.Name))
            {
                check.Name = a.Name;
            }
            try
            {
                context.Entry(check).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return check.Id;
        }

    }
}
