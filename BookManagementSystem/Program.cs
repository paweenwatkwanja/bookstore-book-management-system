using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "mongodb://admin:password@mongodb:27017/bookstore?authSource=admin";

var client = new MongoClient(connectionString);

var collection = client.GetDatabase("bookstore").GetCollection<Healthcheck>("healthchecks");
Healthcheck result = collection.Find(Builders<Healthcheck>.Filter.Empty).FirstOrDefault();


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/healthchecks", () =>
{
    return result;
});

app.MapGet("/helloworld", () =>
{
    return "Hello, World!";
});

app.Run();

public class Healthcheck {

    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("status")]
    public string status {get; set; }
}