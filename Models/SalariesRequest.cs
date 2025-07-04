public class SalariesRequest
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public long MonthYear { get; set; }
    public long CreatedBy { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string Status { get; set; } = "draft";
    public string OrgAccountNumber { get; set; } = null!;
    public string OrgIfscCode { get; set; } = null!;
    public string Remarks { get; set; } = null!;
}
