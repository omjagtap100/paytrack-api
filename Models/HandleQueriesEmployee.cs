using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace paytrack_api.Models
{
    [Table("HandleQueriesEmployees")]
    public class HandleQueriesEmployee
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("description")]
        [MaxLength(255)]
        public string Description { get; set; } = null!;

        [Required]
        [Column("employeeId")]
        public int EmployeeId { get; set; }

        [Column("assignedTo")]
        public int? AssignedTo { get; set; }

        [Required]
        [Column("status")]
        [MaxLength(255)]
        public string Status { get; set; } = "pending";

        [Required]
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("resolvedAt")]
        public DateTime? ResolvedAt { get; set; }
    }
}
