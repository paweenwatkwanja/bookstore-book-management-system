namespace Domain;

public class Book
{
    public string ObjectId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public DateOnly PublicationDate { get; set; }
}