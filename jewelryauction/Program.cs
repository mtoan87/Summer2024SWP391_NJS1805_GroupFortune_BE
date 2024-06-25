using Microsoft.AspNetCore.Authentication.JwtBearer;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL.Authenticate;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<JewelryAuctionContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<JewelryGoldRepository>();
builder.Services.AddScoped<JewelryGoldService>();
builder.Services.AddScoped<JewelryGoldDiaRepository>();
builder.Services.AddScoped<JewelryGoldDiaService>();
builder.Services.AddScoped<JewelryService>();
builder.Services.AddScoped<JewelrySilverRepository>();
builder.Services.AddScoped<JewelrySilverService>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AuctionRepository>();
builder.Services.AddScoped<AccountWalletService>();
builder.Services.AddScoped<AccountWalletRepository>();
builder.Services.AddScoped<AuctionService>();
builder.Services.AddScoped<AuctionResultRepository>();
builder.Services.AddScoped<AuctionResultService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RequestAuctionRepository>();
builder.Services.AddScoped<JoinAuctionRepository>();
builder.Services.AddScoped<JoinAuctionService>();
builder.Services.AddScoped<BidRepository>();
builder.Services.AddScoped<BidService>();
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
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
