using Orders.Api.Services.Interfaces;
using Orders.Api.Services;
using Orders.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IOrderService, OrderService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapOrderEndpoints();

app.Run();