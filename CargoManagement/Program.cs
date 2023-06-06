using CargoManagement.API.Jobs;
using CargoManagement.BLL.Infrastructure;
using CargoManagement.BLL.Services;
using CargoManagement.DAL.DataContext;
using CargoManagement.DAL.Models;
using CargoManagement.DAL.Repositories;
using CargoManagement.DAL.Repositories.Contracts;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CargoManagementDbContext>(options => {

    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
    });

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICarrierConfigurationService, CarrierConfigurationService>();
builder.Services.AddScoped<ICarrierService, CarrierService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICarrierReportsService, CarrierReportsService>();

var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireSql");
builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(hangfireConnectionString);
    RecurringJob.AddOrUpdate<Job>(j => j.TotalFees(), "0 * * * *"); //It is triggered per hour
});

builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHangfireDashboard();

app.MapControllers();

app.Run();
