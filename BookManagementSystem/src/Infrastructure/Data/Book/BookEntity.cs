using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.EntityFrameworkCore;

namespace Data;

[Collection("healthchecks")]
public class BookEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ObjectId { get; set; }

    [BsonElement("title")]
    public string? Title { get; set; }

    [BsonElement("author")]
    public string? Author { get; set; }

    [BsonElement("isbn")]
    public string? ISBN { get; set; }

    [BsonElement("publisher")]
    public string? Publisher { get; set; }

    [BsonElement("publicationDate")]
    public DateOnly PublicationDate { get; set; }
}