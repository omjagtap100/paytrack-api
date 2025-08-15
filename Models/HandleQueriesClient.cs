using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace paytrack_api.Models
{

    [Table("handleQueriesClient")]
    public class HandleQueriesClient
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;

        [Required]
        [Column("clientId")]
        public int ClientId { get; set; }

        [Column("assignedTo")]
        public int? AssignedTo { get; set; }

        [Required]
        [Column("organizationId")]
        public int OrganizationId { get; set; }

        [Required]
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = "pending";

        [Required]
        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("resolvedAt")]
        public DateTime? ResolvedAt { get; set; }
    }

}