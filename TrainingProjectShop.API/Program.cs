using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.API.Customers;
using TrainingProjectShop.API.Orders;
using TrainingProjectShop.API.Products;
using TrainingProjectShop.Application;
using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Domain.Products;
using TrainingProjectShop.Infrastructure;
using TrainingProjectShop.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapPost("/customers", async (
    CreateCustomerDto dto,
    CustomerService customerService) =>
{
    var customerId = await customerService.CreateCustomerAsync(dto.Name, dto.Email);

    return Results.Ok(customerId);
});

app.MapPost("/products", async (
    CreateProductDto dto,
    ProductService productService) => 
{
    var price = new Price(
        dto.Amount,
        dto.Currency);

    var productId = await productService.CreateProductAsync(
        dto.Name,
        price,
        dto.Description);

    return Results.Ok(productId);
});

app.MapPost("/products/{id:guid}/publish", async (
    Guid id,
    ProductService productService) => 
{
    await productService.PublishProductAsync(id);

    return Results.Ok();
});

app.MapPost("/products/{id:guid}/archive", async (
    Guid id,
    ProductService productService) => 
{
    await productService.ArchiveProductAsync(id);

    return Results.Ok();
});

app.MapPost("/orders", async (
    CreateOrderDto dto,
    OrderService orderService) =>
{
    var orderId = await orderService.CreateOrderAsync(dto.CustomerId);

    return Results.Ok(orderId);
});

app.MapPost("/orders/{orderId:guid}/items", async (
    Guid orderId,
    AddOrderItemDto dto,
    OrderService orderService) => 
{
    await orderService.AddItemAsync(orderId, dto.ProductId, dto.Quantity);

    return Results.Ok();
});

app.MapGet("/orders/{orderId:guid}", async (
    Guid orderId,
    OrderService orderService) => 
{
    var order = await orderService.GetByIdAsync(orderId);

    if (order is null)
    {
        return Results.NotFound();
    }
    var dto = new OrderDto(
        order.Id,
        order.CustomerId,
        order.Status,
        order.Items
            .Select(item => new OrderItemDto(
                item.ProductId,
                item.Price.Amount,
                item.Price.Currency,
                item.Quantity,
                item.GetTotal()))
            .ToList());

    return Results.Ok(dto);
});

app.MapPost("/orders/{orderId:guid}/confirm", async (
    Guid orderId,
    OrderService orderService) => 
{
    await orderService.ConfirmAsync(orderId);

    return Results.Ok();
});

app.MapPost("/orders/{orderId:guid}/pay", async (
    Guid orderId,
    OrderService orderService) =>
{
    await orderService.PayAsync(orderId);

    return Results.Ok();
});

app.MapPost("/orders/{orderId:guid}/cancel", async (
    Guid orderId,
    OrderService orderService) =>
{
    await orderService.CancelAsync(orderId);

    return Results.Ok();
});

app.Run();
