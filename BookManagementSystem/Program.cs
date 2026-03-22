using Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "mongodb://admin:password@mongodb:27017/bookstore?authSource=admin";

var client = new MongoClient(connectionString);

builder.Services.AddDbContext<BookManagementSystemDbContext>(options => options.UseMongoDB(connectionString, "bookstore"));

builder.Services.AddScoped<HealthcheckService>();

builder.Services.AddScoped<HealthcheckRepository>();

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