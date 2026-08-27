using TrainingProjectShop.Application;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orders", async (
    Guid customerId,
    OrderService orderService) =>
{
    var orderId = await orderService.CreateOrderAsync(customerId);

    return Results.Ok(orderId);
});

app.Run();
