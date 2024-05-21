
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JewelryAuctionContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    options.UseSqlServer(connectionString);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
