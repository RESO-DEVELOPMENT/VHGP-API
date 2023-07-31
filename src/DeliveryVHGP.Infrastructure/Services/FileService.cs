using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Firebase.Auth;

namespace DeliveryVHGP.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private IConfiguration _configuration;

        private string _bucket;
        //
        private static string ApiKey;
        private static string Bucket;
        private static string AuthEmail;
        private static string AuthPassword;
        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            //_bucket = _configuration.GetSection("Firebase:Bucket").Value;
            ApiKey = _configuration["Firebase:ApiKey"];
            Bucket = _configuration["Firebase:Bucket"];
            AuthEmail = _configuration["Firebase:AuthEmail"];
            AuthPassword = _configuration["Firebase:AuthPassword"];
        }
        public bool ValidationFile(IFormFile file)
        {
            const int MAX_SIZE = 5 * 1024 * 1024; // 5MB
            string[] listExtensions = { ".png", ".jpeg", ".jpg", ".jfif", ".gif", ".webp" };

            bool isValid = false;

            if (file.Length == 0) throw new NullReferenceException("Null File");

            if (file.Length > 0 && file.Length < MAX_SIZE) isValid = true;

            if (isValid)
            {
                string extensionFile = Path.GetExtension(file.FileName);

                foreach (var extension in listExtensions)
                {
                    if (extensionFile.Equals(extension))
                    {
                        isValid = true;
                        break;
                    }
                }
            }
            return isValid;
        }

        //public async Task<string> UploadFile(IFormFile file)
        //{
        //    bool isValid = ValidationFile(file);
        //    if (!isValid) throw new ArgumentException("Just image and the size is less than 5MB");

        //    Stream stream = file.OpenReadStream();
        //    var filename = Guid.NewGuid();

        //    return await Upload(filename, stream);
        //}
        public async Task<string> UploadFile(string fileimg ,string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return "";
            }
            Stream stream = ConvertBase64ToStream(base64String);
            var filename = Guid.NewGuid();
            return await Upload(fileimg,filename, stream);
        }

        private async Task<string> Upload(string fileImg ,Guid filename, Stream stream)
        {
            //var cancellationToken = new CancellationTokenSource().Token;
            //return await new FirebaseStorage(_bucket).Child("assets").Child($"{fileImg}/{filename}").PutAsync(stream, cancellationToken);

            //
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellationToken = new CancellationTokenSource().Token;

            var task = new FirebaseStorage(Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }).Child("assets").Child($"{fileImg}/{filename}").PutAsync(stream, cancellationToken);

            return (await task).ToString();
            
        }

        private Stream ConvertBase64ToStream(string base64)
        {
            //if (string.IsNullOrWhiteSpace(base64))
            //{
            //    return null;
            //}
            base64 = base64.Trim();
            if ((base64.Length % 4 != 0) || !Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None)) throw new ArgumentException("Invalid image");
            byte[] bytes = Convert.FromBase64String(base64);
            return new MemoryStream(bytes);
        }


    }
}
