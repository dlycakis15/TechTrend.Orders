using System.Reflection;
using MongoDB.Driver;
using TechTrend.Orders.API.Middleware;
using TechTrend.Orders.Domain.Seed;
using TechTrend.Orders.Infrastructure.Repositories.Customers;
using TechTrend.Orders.Infrastructure.Repositories.Orders;
using TechTrend.Orders.Infrastructure.Repositories.Products;
using TechTrend.Orders.Infrastructure.Services.Orders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(TechTrend.Orders.Application.Handlers.Orders.CreateOrderHandler)
        .Assembly));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    const string connectionString = "mongodb://localhost:27017";
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("ECommerce");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<IMongoDatabase>();
    var dbInitializer = new MongoDbInitialiser(db);
    dbInitializer.Initialize();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();