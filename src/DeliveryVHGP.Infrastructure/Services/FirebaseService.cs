using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;

namespace DeliveryVHGP.Infrastructure.Services
{
    public class FirebaseService
    {
        public async static Task<UserRecord> GetUserRecordByIdToken(string idToken)
        {
            try
            {
                var auth = FirebaseAuth.DefaultInstance;
                FirebaseToken decodedToken = await auth.VerifyIdTokenAsync(idToken);
                UserRecord userRecord = await auth.GetUserAsync(decodedToken.Uid);
                return userRecord;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
