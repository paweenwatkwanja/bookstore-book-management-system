using Newtonsoft.Json;

namespace Models;

public class HealthcheckResponse
{
    [JsonProperty("object_id")]
    public string ObjectId { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}