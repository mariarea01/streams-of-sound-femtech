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
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date and Time is required.")]
        public DateTimeOffset StartDateTimeUtc { get; set; }
        public int Duration { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        [Display(Name = "Opening Slots")]
        public int SlotsOpenings { get; set; }
        public int SlotsAvailable { get; set; }
    }
}

