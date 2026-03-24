using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (environment == null)
{
    environment = "Development";
}

IConfiguration configuration = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
  .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
  .AddEnvironmentVariables()
  .Build();

MongoDBSettings? mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();

MongoClient mongoClient = new MongoClient(mongoDBSettings?.ConnectionString);

builder.Services.AddDbContext<BookManagementSystemDbContext>(options => options.UseMongoDB(mongoClient, mongoDBSettings?.DatabaseName));

builder.Services.AddScoped<IHealthcheckService, HealthcheckService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IHealthcheckRepository, HealthcheckRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();