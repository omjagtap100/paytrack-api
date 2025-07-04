public class OrganizationTransaction
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public string Description { get; set; } = null!;
    public string Status { get; set; } = "pending";
    public int OrganizationId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string TypeOfPayment { get; set; } = "receivable";
}
