using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models;

namespace DeliveryVHGP.Core.Interface.IRepositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<List<AccountModel>> GetAll(int pageIndex, int pageSize);
        Task<AccountCheck> CheckAccount(string id);
        Task<AccountModel> Login(string id, string pass);
        Task<dynamic> LoginWithPhoneNumber(string idToken, string fcmToken);
        Task<string> CreateAcc(string id, string pass, string name, string imageUrl);
        Task<string> UpdateAccount(AccountViewModel acc);
        Task<string> ChangePass(string id, string pass, string newPass);

    }
}
