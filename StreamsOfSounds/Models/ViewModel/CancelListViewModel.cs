using System.ComponentModel.DataAnnotations;

namespace StreamsOfSound.Models.ViewModel
{
    public class CancelListViewModel
    {
        [Display(Name = "Reason for Cancelling")]
        public string? ThisIsMyLastResort { get; set; }

        [Display(Name = "Volunteer's First Name")]
        public string firstName { get; set; }

        [Display(Name = "Volunteer's Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Volunteer's Email")]
        public string Email { get; set; }

        [Display(Name = "Opportunity Name")]
        public string OppName { get; set; }

        [Display(Name = "Opportunity Date")]
        public DateTimeOffset OppStartTime { get; set; }
        
        public string Instrument { get; set; }

        [Display(Name = "Slot Start Time")]
        public DateTime SlotStartTime { get; set; }

        [Display(Name = "Slot End Time")]
        public DateTime SlotEndTime { get; set; }

    }
}
