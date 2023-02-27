using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StreamsOfSound.Models.Domain_Entities
{
    // TODO: Update the DB table to mirror this
    // TODO: Add to the models as it makes sense (Display column, etc.)
    public partial class Opportunity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Display(Name="Start Date and Time")]
        public DateTimeOffset StartDateTimeUtc { get; set; }
        public DateTimeOffset EndDateTimeUtc { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        [Display(Name = "Opening Slots")]
        public int SlotsOpenings { get; set; }
        public int SlotsAvailable { get; set; }
        public Guid UserId { get; set; }

    }
}