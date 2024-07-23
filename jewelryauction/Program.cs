using DAL.Authenticate;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Implement;
using Repository.Interface;
using Service.Hubs;
using Service.Implement;
using Service.Interface;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<JewelryAuctionContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DBDefault");
    options.UseSqlServer(connectionString);
});

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddScoped<IJewelryGoldRepository, JewelryGoldRepository>();
builder.Services.AddScoped<IJewelryGoldService, JewelryGoldService>();
builder.Services.AddScoped<IJewelryGoldDiamondRepository, JewelryGoldDiaRepository>();
builder.Services.AddScoped<IJewelryGoldDiamondService, JewelryGoldDiaService>();
builder.Services.AddScoped<IJewelryService, JewelryService>();
builder.Services.AddScoped<IJewelrySilverRepository, JewelrySilverRepository>();
builder.Services.AddScoped<IJewelrySilverService, JewelrySilverService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAccountWalletService, AccountWalletService>();
builder.Services.AddScoped<IAccountWalletRepository, AccountWalletRepository>();
builder.Services.AddScoped<IAuctionResultRepository, AuctionResultRepository>();
builder.Services.AddScoped<IAuctionResultService, AuctionResultService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<BiddingHub>();
builder.Services.AddScoped<RequestAuctionRepository>();
builder.Services.AddScoped<IJoinAuctionRepository, JoinAuctionRepository>();
builder.Services.AddScoped<IJoinAuctionService, JoinAuctionService>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IBidRecordRepository, BidRecordRepository>();
builder.Services.AddScoped<IBidRecordService, BidRecordService>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<BiddingHub>("/bidding-hub");

});


app.Run();
