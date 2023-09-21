using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Entities;
using DeliveryVHGP.Core.Interface.IRepositories;
using DeliveryVHGP.Core.Models;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using DeliveryVHGP.Infrastructure.Services;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

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
                    Name = x.Name,
                    RoleId = x.RoleId,
                    Status = x.Status,
                    ImageUrl = x.ImageUrl
                })
                .FirstOrDefaultAsync();

            return check;
        }

        public async Task<dynamic> LoginWithPhoneNumber(string idToken, string fcmToken)
        {
            UserRecord userRecord;

            try
            {
                /*         if (fcmToken != null && fcmToken.Trim().Length > 0)
                                 {
                                     if (!await _customerFcmtokenService.ValidToken(fcmToken))
                                         return new
                                         {
                                             status = new
                                             {
                                                 success = false,
                                                 message = Constants.MES_INVALID_FCM_TOKEN,
                                                 status = Constants.STATUS_BAD_REQ,
                                             },
                                             data = new
                                             { }
                                         };
                                 }*/

                userRecord = await FirebaseService.GetUserRecordByIdToken(idToken);
            }
            catch (Exception x)
            {
                var responseFailValidToken = new
                {
                    status = new
                    {
                        success = false,
                        message = x.Message,
                        // status = Constants.STATUS_BAD_REQ,
                    },
                    data = new
                    { }
                };
                return responseFailValidToken;
            }

            if (string.IsNullOrEmpty(userRecord.PhoneNumber))
            {
                return null;
            }

            var account = context.Accounts.Where(a => a.Id == userRecord.PhoneNumber).FirstOrDefault();

            try
            {
                //new customer => add fcm map with Id
                if (account == null)
                {
                    //Create customer
                    Account newAccount = new Account
                    {
                        Id = userRecord.PhoneNumber,
                        Name = "User",
                        RoleId = "1",
                        Status = "true",
                    };

                    //Add fcm token
                    if (fcmToken != null && fcmToken.Trim().Length > 0)
                        await context.FcmTokens.AddAsync(new FcmToken
                        {
                            Id = fcmToken,
                            AccountId = newAccount.Id,
                        });

                    var responseSuccess = new
                    {
                        status = new
                        {
                            success = true,
                            /*           message = Constants.MES_LOGIN_SUCCESS,
                                       status = Constants.STATUS_SUCCESS*/
                        },
                        data = new
                        {
                            id = newAccount.Id,
                            isFristLogin = true,
                            name = newAccount.Name,

                        },
                    };

                    return responseSuccess;
                }
            }
            catch (Exception ex)
            {
                var responseFail = new
                {
                    status = new
                    {
                        success = false,
                        /*                message = Constants.MES_LOGIN_FAIL,
                                        status = Constants.STATUS_SUCCESS*/
                    },
                    data = new { }
                };

                return responseFail;
            }

            var response = new
            {
                status = new
                {
                    success = true,
                    /*message = Constants.MES_LOGIN_FAIL,
                    status = Constants.STATUS_SUCCESS*/
                },
                data = new
                {
                    acountId = account.Id,
                    phone = userRecord.PhoneNumber,
                    isFirstLogin = false,
                }
            };

            return response;
        }

        public async Task<string> CreateAcc(string id, string pass, string name, string imageUrl)
        {
            string fileImg = "ImagesAccounts";
            string image = await _fileService.UploadFile(fileImg, imageUrl);

            Account acc = new Account() { Id = id, Name = name, RoleId = "2", Password = pass, Status = "true", ImageUrl = image };
            await context.Accounts.AddAsync(acc);
            context.SaveChangesAsync();
            return id;
        }

        public async Task<string> ChangePass(string id, string pass, string newPass)
        {
            var check = await context.Accounts.
            Where(x => x.Id == id && x.Password == pass).FirstOrDefaultAsync();
            if (check == null)
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
