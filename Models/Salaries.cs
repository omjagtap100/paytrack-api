using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("salaries")]
public class Salaries
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("employeeId")]
    [Required]
    public int EmployeeId { get; set; }

    [Column("organizationId")]
    [Required]
    public int OrganizationId { get; set; }

    [Column("salaryAmount")]
    [Required]
    public long SalaryAmount { get; set; }

    [Column("basicSalary")]
    [Required]
    public long BasicSalary { get; set; }

    [Column("HRA")]
    [Required]
    public long HRA { get; set; }

    [Column("DA")]
    [Required]
    public long DA { get; set; }

    [Column("PF")]
    [Required]
    public long PF { get; set; }

    [Column("deductions")]
    [Required]
    public long Deductions { get; set; }

    [Column("netSalary")]
    [Required]
    public long NetSalary { get; set; }

    [Column("isPrevious")]
    [Required]
    public bool IsPrevious { get; set; }

    [Column("effectiveFrom")]
    [Required]
    public DateTime EffectiveFrom { get; set; }
}
