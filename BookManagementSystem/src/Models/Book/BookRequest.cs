using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class BookRequest
{
    [Required(ErrorMessage = "Title is required")]
    [JsonProperty("title")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Author is required")]
    [JsonProperty("author")]
    public string? Author { get; set; }

    [Required(ErrorMessage = "ISBN is required")]
    [JsonProperty("isbn")]
    public string? ISBN { get; set; }

    [Required(ErrorMessage = "Publisher is required")]
    [JsonProperty("publisher")]
    public string? Publisher { get; set; }

    [JsonProperty("publication_date")]
    public DateOnly PublicationDate { get; set; }

    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }
}