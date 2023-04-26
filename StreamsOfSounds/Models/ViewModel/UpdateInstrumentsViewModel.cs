using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StreamsOfSound.Models.ViewModel
{
    public class UpdateInstrumentsViewModel
    {
        [Display(Name = "Current Instrument List")]
        public string? OldInstruments { get; set; }

        [Required]
        [Display(Name = "Updated Instrument List")]
        public string? NewInstruments { get; set; }
        public string StatusMessage { get; set; }   

    }
}
