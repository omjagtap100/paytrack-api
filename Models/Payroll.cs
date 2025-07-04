public class Payroll
{
    public int Id { get; set; }
    public long PayrollMonth { get; set; }
    public long BasicSalary { get; set; }
    public long HRA { get; set; }
    public long DA { get; set; }
    public long PF { get; set; }
    public long Deductions { get; set; }
    public long NetSalary { get; set; }
    public string Status { get; set; } = "pending";
    public int EmployeeId { get; set; }
    public int OrganizationId { get; set; }
}
