using Models;
using Repositories;
using Services;
using MongoDB.Driver;
using Newtonsoft.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(mongoDBSettings?.ConnectionString));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDBSettings?.DatabaseName));

builder.Services.AddScoped<IHealthcheckRepository, HealthcheckRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IHealthcheckService, HealthcheckService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Formatting.Indented;
    });;

builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();