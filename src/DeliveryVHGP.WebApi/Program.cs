using CorePush.Apple;
using CorePush.Google;
using DeliveryVHGP.Core.Data;
using DeliveryVHGP.Core.Interfaces;
using DeliveryVHGP.Core.Models.Noti;
using DeliveryVHGP.DeliveryAlgorithm;
using DeliveryVHGP.Infrastructure.Repositories.Common;
using DeliveryVHGP.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITimeStageService, TimeStageService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFirestoreService, FirestoreService>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddHostedService<DeliveryService>();
builder.Services.Configure<HostOptions>(hostOptions =>
{
    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});
//builder.Services.AddControllers();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();

// Configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DeliveryVHGP_DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "VHGP Delivery Web API",
        Description = "Document for Web API",
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "VHGP",
            Email = string.Empty
        }
    }
    );
});
builder.Services.AddCors(c =>
{
    c.AddPolicy("alloworigin", options => options.AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VHGP Delivery Web API v1");
    c.RoutePrefix = string.Empty;
});
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); ;
app.UseAuthorization();

app.MapControllers();

app.Run();
