using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GameStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "GameStoreDB"));
builder.Services.AddScoped<IGameStoreDbContext>(provider => provider.GetService<GameStoreDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

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
