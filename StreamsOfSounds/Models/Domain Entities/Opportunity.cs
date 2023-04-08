using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StreamsOfSound.Models.Domain_Entities
{
    // TODO: Update the DB table to mirror this
    // TODO: Add to the models as it makes sense (Display column, etc.)
    public partial class Opportunity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Display(Name = "Start Date and Time")]
        [Required(ErrorMessage = "Enter Start Date and Time")]
        public DateTimeOffset StartTime { get; set; }
        [Display(Name = "End Date and Time")]
        public DateTimeOffset EndTime { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter City")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter State")]
        public string State { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter Zip")]
        public string Zip { get; set; } = string.Empty;
        [Display(Name = "Opening Slots")]
        public int SlotsOpenings { get; set; }
        public int SlotsAvailable { get; set; }
        public bool isArchived { get; set; }
        //public string paidAmount { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        
    }
}