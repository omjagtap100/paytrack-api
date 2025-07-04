public class HandleQueriesClient
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public int ClientId { get; set; }
    public string Status { get; set; } = "pending";
}
