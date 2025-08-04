
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace paytrack_api.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("FirstName")]

        public string? FirstName { get; set; }
        [Column("LastName")]
        public string? LastName { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
        [Column("OrganizationId")]
        public int OrganizationId { get; set; }
        [Column("IsAccount")]
        public bool? IsAccount { get; set; }
        [Column("IsHR")]
        public bool? IsHR { get; set; }
        [Column("AccountNo")]
        public string? AccountNo { get; set; }
        [Column("IfscCode")]
        public string? IfscCode { get; set; }
        [Column("HRId")]
        public int? HRId { get; set; }
        [Column("IsManager")]
        public bool? IsManager { get; set; }
        [Column("keycloak_user_id")]
        public string? keycloak_user_id { get; set; }
    }
}