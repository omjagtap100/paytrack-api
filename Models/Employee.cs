

public class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int OrganizationId { get; set; }

    public bool IsAccount { get; set; }

    public bool IsHR { get; set; }

    public string? AccountNo { get; set; }

    public string? IfscCode { get; set; }

    public int HRId { get; set; }

    public bool IsManager { get; set; }
}
