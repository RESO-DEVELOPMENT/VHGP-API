using DeliveryVHGP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryVHGP.Core.Utils
{
    public static class Utils
    {
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        //public static string GetIpAddress()
        //{
        //    string ipAddress;
        //    try
        //    {

        //        ipAddress = Req.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //        if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown"))
        //            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    catch (Exception ex)
        //    {
        //        ipAddress = "Invalid IP:" + ex.Message;
        //    }

        //    return ipAddress;
        //}

        public static int GetPaymentType(int paymentType)
        {
            var type = 0;

            if (paymentType == 0)
            {
                type = (int)PaymentEnum.Cash;
            }
            else if (paymentType == 1)
            {
                type = (int)PaymentEnum.VNPay;
            }
            else if (paymentType == 2)
            {
                type = (int)PaymentEnum.Paid;
            }

            return type;
        }

        public static int GetPaymentStatus(int paymentType)
        {
            var status = 0;

            if (paymentType == 0)
            {
                status = (int)PaymentStatusEnum.unpaid;
            }
            else if (paymentType == 1)
            {
                status = (int)PaymentStatusEnum.successful;
            }
            else if (paymentType == 2)
            {
                status = (int)PaymentStatusEnum.successful;
            }

            return status;
        }
    }
}
