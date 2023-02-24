using System.ComponentModel.DataAnnotations;

namespace VolunteerWebApplication.Models
{
    public class Opportunities
    {
        public Opportunities()
        {
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Opportunity Name is required.")]
        public string? Name { get; set; }

        public string? Description { get; set; } 

        [Required(ErrorMessage = "Start Date and Time is required.")]
        public DateTimeOffset? StartDateTimeUtc { get; set; }

        [Required(ErrorMessage = "End Date and Time is required.")]
        public DateTimeOffset? EndDateTimeUtc { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Zip is required.")]
        public int? Zip { get; set; }

        [Display(Name = "Slots Available")]
        public int? SlotsAvailable { get; set; }

        [Display(Name = "Slots Opened")]
        public int? SlotsOpenings { get; set; }
    }
}

