using System.ComponentModel.DataAnnotations;

namespace MyApi.Domain.Entities
{
    public class ActivityEntity
    {   
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required DateTime StartDate { get; set; }

        public required DateTime EndDate { get; set; }

        public string? Description { get; set; }

        public string? AdditionalInformation { get; set; }

        [Required]
        public string? Location { get; set; } 

        [Required]
        public bool Active { get; set; }

        // Navigation property
        // [Required]
        public int OrganisatorId { get; set; }
    }
}