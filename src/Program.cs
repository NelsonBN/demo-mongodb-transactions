using Demo.MongoDB.Transactions.DataBase;
using Demo.MongoDB.Transactions.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IMongoDBContext, MongoDBContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.MapControllers();

app.Run();
