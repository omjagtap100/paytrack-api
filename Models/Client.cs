using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("clients")]  
public class Client
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("clientName")]
    public string ClientName { get; set; } = null!;

    [Column("organizationId")]
    public int OrganizationId { get; set; }

    [Column("employeeId")]
    public int EmployeeId { get; set; }
}
