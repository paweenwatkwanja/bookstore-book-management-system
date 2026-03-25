using Newtonsoft.Json;

namespace Models;

public class BookRequest
{
    [JsonProperty("object_id")]
    public string ObjectId { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("author")]
    public string Author { get; set; }

    [JsonProperty("isbn")]
    public string ISBN { get; set; }

    [JsonProperty("publisher")]
    public string Publisher { get; set; }

    [JsonProperty("publication_date")]
    public DateOnly PublicationDate { get; set; }
}